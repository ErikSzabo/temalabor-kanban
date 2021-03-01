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

        public async void DeleteCard(int cardID)
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
            return await kanbanContext.Cards.FirstOrDefaultAsync(c => c.ID == cardID);
        }

        public async Task<Card> UpdateCard(Card card)
        {
            var result = await kanbanContext.Cards.FirstOrDefaultAsync(c => c.ID == card.ID);
            if (result == null) return null;

            result.Title = card.Title;
            result.Deadline = card.Deadline;
            result.Description = card.Description;
            result.Sort = card.Sort;

            await kanbanContext.SaveChangesAsync();
            return result;
        }

        public List<Card> GetCardsByColumn(int columnID)
        {
           return kanbanContext.Cards.Where(c => c.ColumnID == columnID).ToList();
        }
    }
}
