using Kanban.Data;
using Kanban.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KanbanTests
{
    class CardRepositoryMock : ICardRepository
    {
        private List<Card> store = new List<Card>();

        public List<Card> Cards { get => new List<Card>(store); }

        public int InvokedWithNullPreviosCard { get; private set; } = 0;

        public Task<Card> AddCard(Card card)
        {
            store.Add(card);
            return Task.FromResult(card);
        }

        public Task DeleteCard(int cardID)
        {
            foreach(var card in store) 
                if(card.ID == cardID)
                {
                    store.Remove(card);
                    break;
                }
            return Task.FromResult(0);
        }

        public Task<Card> GetCard(int cardID)
        {
            return Task.FromResult(store.Find(c => c.ID == cardID));
        }

        public Task<List<Card>> GetCardsByColumn(int columnID)
        {
            return Task.FromResult(store.FindAll(c => c.ColumnID == columnID));
        }

        public Task<Card> GetFirstCardInColumn(int columnID)
        {
            var cards = store.FindAll(c => c.ColumnID == columnID);
            cards.Sort((a, b) => a.Sort - b.Sort);
            return Task.FromResult(cards[0]);
        }

        public Task<Card> GetLastCardInColumn(int columnID)
        {
            var cards = store.FindAll(c => c.ColumnID == columnID);
            cards.Sort((a, b) => b.Sort - a.Sort);
            return Task.FromResult(cards[0]);
        }

        public Task<Card> MoveCardAfterAnother(Card cardToMove, Card previousCard, int targetColumn)
        {
            throw new System.NotImplementedException();
        }

        public Task<Card> MoveCardTop(Card cardToMove, Card currentFirstCard, int targetColumn)
        {
            throw new System.NotImplementedException();
        }

        public Task<Card> MoveCardTopInEmptyColumn(Card cardToMove, int targetColumn)
        {
            throw new System.NotImplementedException();
        }

        public Task<Card> UpdateCard(int cardID, Card card)
        {
            var result = store.Find(c => c.ID == cardID);
            if (result == null) return null;

            result.Title = card.Title;
            result.Deadline = card.Deadline;
            result.Description = card.Description;

            return Task.FromResult(result);
        }
    }
}
