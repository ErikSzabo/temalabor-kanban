using kanban.Data;
using kanban.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly KanbanContext kanbanContext;

        public CardRepository(KanbanContext kanbanContext)
        {
            this.kanbanContext = kanbanContext;
        }

        public async Task<Card> AddCard(Card card)
        {
            var result = await kanbanContext.AddAsync(card);
            await kanbanContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCard(int cardID)
        {
            var result = await kanbanContext.Cards.FirstOrDefaultAsync(c => c.ID == cardID);
            if (result == null) return;
            kanbanContext.Cards.Remove(result);
            await kanbanContext.SaveChangesAsync();
        }

        public async Task<ICollection<Card>> GetCards()
        {
            return await kanbanContext.Cards.ToListAsync();
        }

        public async Task<Card> GetCard(int cardID)
        {
            return await kanbanContext.Cards.FindAsync(cardID);
        }

        public async Task<Card> UpdateCard(Card card)
        {
            var result = await kanbanContext.Cards.FindAsync(card.ID);
            if (result == null) return null;

            result.Title = card.Title;
            result.Deadline = card.Deadline;
            result.Description = card.Description;

            await kanbanContext.SaveChangesAsync();
            return result;
        }

        public IEnumerable<Card> GetCardsByColumn(int columnID)
        {
           return kanbanContext.Cards.Where(c => c.ColumnID == columnID);
        }

        public async Task<Card> MoveCard(Card cardToMove, Card previousCard, int targetColumn)
        {
            if(previousCard == null)
            {
                var firstCard = GetCardsByColumn(targetColumn).OrderBy(c => c.Sort).FirstOrDefault();
                if (firstCard == null)
                {
                    cardToMove.Sort = 0;
                    cardToMove.ColumnID = targetColumn;
                    await kanbanContext.SaveChangesAsync();
                    return cardToMove;
                }
                else
                {
                    int firstSort = firstCard.Sort;
                    kanbanContext.Cards.AsParallel()
                        .Where(c => c.ColumnID == targetColumn)
                        .ForAll(c => c.Sort++);

                    cardToMove.Sort = firstSort;
                    cardToMove.ColumnID = targetColumn;
                    await kanbanContext.SaveChangesAsync();
                    return cardToMove;
                }
            }

            int sort = previousCard.Sort;
            // Increment sort for everything after the previos card 
            kanbanContext.Cards.AsParallel()
                .Where(c =>  c.Sort > previousCard.Sort && c.ColumnID == previousCard.ColumnID)
                .ForAll(c => c.Sort++);

            // Update the card and save everything
            cardToMove.ColumnID = previousCard.ColumnID;
            cardToMove.Sort = sort + 1;

            await kanbanContext.SaveChangesAsync();
            return cardToMove;
        }
    }
}
