using Kanban.Models;
using Kanban.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
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

        public async Task<Card> MoveCard(Card cardToMove, Card previousCard, int targetColumn)
        {
            if (previousCard == null)
            {
                InvokedWithNullPreviosCard++;
                // If we doesn't have any card in the column, we just add the card with the position of 0
                var firstCard = await GetFirstCardInColumn(targetColumn);
                if (firstCard == null)
                {
                    return MoveCardTopToEmptyColumn(cardToMove, targetColumn);
                }

                // If we have the first card, then this card will get its place and every other card
                // position will be incremented
                return MoveCardTop(cardToMove, firstCard, targetColumn);
            }

            // If we have the previous card, increment position for everything after the previos card
            // then insert the card at the previous card position + 1
            return MoveCardAfterAnother(cardToMove, previousCard, targetColumn);
        }

        public Task<Card> UpdateCard(Card card)
        {
            var result = store.Find(c => c.ID == card.ID);
            if (result == null) return null;

            result.Title = card.Title;
            result.Deadline = card.Deadline;
            result.Description = card.Description;

            return Task.FromResult(result);
        }

        private Card MoveCardTop(Card cardToMove, Card currentFirstCard, int targetColumn)
        {
            int firstSort = currentFirstCard.Sort;
            foreach(var card in store)
                if(card.ColumnID == targetColumn) card.Sort++;
            cardToMove.Sort = firstSort;
            cardToMove.ColumnID = targetColumn;
            return cardToMove;
        }

        private Card MoveCardTopToEmptyColumn(Card cardToMove, int targetColumn)
        {
            cardToMove.Sort = 0;
            cardToMove.ColumnID = targetColumn;
            return cardToMove;
        }

        private Card MoveCardAfterAnother(Card cardToMove, Card previousCard, int targetColumn)
        {
            int sort = previousCard.Sort;
            var cards = store.FindAll(c => c.ColumnID == targetColumn && c.Sort > sort);
            foreach (var card in cards) card.Sort++;

            cardToMove.ColumnID = targetColumn;
            cardToMove.Sort = sort + 1;

            return cardToMove;
        }
    }
}
