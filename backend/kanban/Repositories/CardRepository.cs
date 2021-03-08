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
            var result = await kanbanContext.Cards.FindAsync(cardID);
            if (result == null) return;
            kanbanContext.Cards.Remove(result);
            await kanbanContext.SaveChangesAsync();
        }

        public async Task<List<Card>> GetCards()
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

        public async Task<List<Card>> GetCardsByColumn(int columnID)
        {
           return await kanbanContext.Cards
                    .Where(c => c.ColumnID == columnID)
                    .OrderBy(c => c.Sort)
                    .ToListAsync();
        }

        public async Task<Card> GetFirstCardInColumn(int columnID)
        {
            return await kanbanContext.Cards
                    .Where(c => c.ColumnID == columnID)
                    .OrderBy(c => c.Sort)
                    .FirstOrDefaultAsync();
        }

        public async Task<Card> GetLastCardInColumn(int columnID)
        {
            return await kanbanContext.Cards
                    .Where(c => c.ColumnID == columnID)
                    .OrderByDescending(c => c.Sort)
                    .FirstOrDefaultAsync();
        }

        public async Task<Card> MoveCard(Card cardToMove, Card previousCard, int targetColumn)
        {
            if(previousCard == null)
            {
                var firstCard = await GetFirstCardInColumn(targetColumn);
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
                    await kanbanContext.Cards
                        .Where(c => c.ColumnID == targetColumn)
                        .ForEachAsync(c => c.Sort++);

                    cardToMove.Sort = firstSort;
                    cardToMove.ColumnID = targetColumn;
                    await kanbanContext.SaveChangesAsync();
                    return cardToMove;
                }
            }

            int sort = previousCard.Sort;
            // Increment sort for everything after the previos card 
            await kanbanContext.Cards
                .Where(c =>  c.Sort > previousCard.Sort && c.ColumnID == previousCard.ColumnID)
                .ForEachAsync(c => c.Sort++);

            // Update the card and save everything
            cardToMove.ColumnID = previousCard.ColumnID;
            cardToMove.Sort = sort + 1;

            await kanbanContext.SaveChangesAsync();
            return cardToMove;
        }
    }
}
