using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Data.Repositories
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

        public async Task<Card> UpdateCard(int cardID, Card card)
        {
            var result = await kanbanContext.Cards.FindAsync(cardID);
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

        public async Task<Card> MoveCardTop(Card cardToMove, Card currentFirstCard, int targetColumn) 
        {
            using var transaction = await kanbanContext.Database.BeginTransactionAsync();
            try
            {
                int firstSort = currentFirstCard.Sort;
                await kanbanContext.Database.ExecuteSqlInterpolatedAsync($"UPDATE [Card] SET [Sort] = [Sort] + 1 WHERE [ColumnID] = {targetColumn};");
                cardToMove.Sort = firstSort;
                cardToMove.ColumnID = targetColumn;
                await kanbanContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return cardToMove;
            } 
            catch(Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Card> MoveCardTopInEmptyColumn(Card cardToMove, int targetColumn)
        {
            cardToMove.Sort = 0;
            cardToMove.ColumnID = targetColumn;
            await kanbanContext.SaveChangesAsync();
            return cardToMove;
        }

        public async Task<Card> MoveCardAfterAnother(Card cardToMove, Card previousCard, int targetColumn)
        {
            using var transaction = await kanbanContext.Database.BeginTransactionAsync();
            try
            {
                int sort = previousCard.Sort;
                await kanbanContext.Database.ExecuteSqlInterpolatedAsync(
                        $"UPDATE [Card] SET [Sort] = [Sort] + 1 WHERE [ColumnID] = {targetColumn} AND [Sort] > {sort};"
                    );

                cardToMove.ColumnID = targetColumn;
                cardToMove.Sort = sort + 1;

                await kanbanContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return cardToMove;
            }
            catch(Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
            
        }
    }
}
