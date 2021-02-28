using kanban.Data;
using kanban.Models;
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

        public Task<Card> AddCard(Card card)
        {
            throw new NotImplementedException();
        }

        public void DeleteCard(int cardID)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Card>> GetCard()
        {
            throw new NotImplementedException();
        }

        public Task<Card> GetCard(int cardID)
        {
            throw new NotImplementedException();
        }

        public Task<Card> UpdateCard(Card card)
        {
            throw new NotImplementedException();
        }
    }
}
