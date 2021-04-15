using Kanban.Data;
using Kanban.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Repositories
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
            // If the doesn't have a previos card, the card should be moved to the top
            if (previousCard == null)
            {
                // If we doesn't have any card in the column, we just add the card with the position of 0
                var firstCard = await GetFirstCardInColumn(targetColumn);
                if (firstCard == null)
                {
                    return await MoveCardTopToEmptyColumn(cardToMove, targetColumn);
                } 

                // If we have the first card, then this card will get its place and every other card
                // position will be incremented
                return await MoveCardTop(cardToMove, firstCard, targetColumn);
            }

            // If we have the previous card, increment position for everything after the previos card
            // then insert the card at the previous card position + 1
            return await MoveCardAfterAnother(cardToMove, previousCard, targetColumn);
        }


        private async Task<Card> MoveCardTop(Card cardToMove, Card currentFirstCard, int targetColumn) 
        {
            int firstSort = currentFirstCard.Sort;
            await kanbanContext.Database.ExecuteSqlInterpolatedAsync($"UPDATE [Card] SET [Sort] = [Sort] + 1 WHERE [ColumnID] = {targetColumn};");
            cardToMove.Sort = firstSort;
            cardToMove.ColumnID = targetColumn;
            await kanbanContext.SaveChangesAsync();
            return cardToMove;
        }

        private async Task<Card> MoveCardTopToEmptyColumn(Card cardToMove, int targetColumn)
        {
            cardToMove.Sort = 0;
            cardToMove.ColumnID = targetColumn;
            await kanbanContext.SaveChangesAsync();
            return cardToMove;
        }

        private async Task<Card> MoveCardAfterAnother(Card cardToMove, Card previousCard, int targetColumn)
        {
            int sort = previousCard.Sort;
            await kanbanContext.Database.ExecuteSqlInterpolatedAsync(
                    $"UPDATE [Card] SET [Sort] = [Sort] + 1 WHERE [ColumnID] = {targetColumn} AND [Sort] > {sort};"
                );

            cardToMove.ColumnID = targetColumn;
            cardToMove.Sort = sort + 1;

            await kanbanContext.SaveChangesAsync();
            return cardToMove;
        }
    }
}
