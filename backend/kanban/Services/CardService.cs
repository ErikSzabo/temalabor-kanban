using kanban.Models;
using kanban.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository repository;

        public CardService(ICardRepository repository)
        {
            this.repository = repository;
        }

        public void DeleteCard(int cardID)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Card>> GetAllCards()
        {
            throw new NotImplementedException();
        }

        public Task<Card> GetCard(int cardID)
        {
            throw new NotImplementedException();
        }

        public List<Card> GetCardsInOrderByColumn(int columnID)
        {
            var cards = repository.GetCardsByColumn(columnID);
            if (cards == null || cards.Count == 0) return null;

            var sortedCards = new List<Card>();
            sortedCards.Add(cards.Find(c => c.ParentID == null));

            for (var i = 0; i < cards.Count; i++)
            {
                foreach (var card in cards)
                {
                    if (card.ParentID == cards[i].ID)
                    {
                        sortedCards.Add(card);
                        break;
                    }
                }
            }

            return sortedCards;
        }

        public Task<Card> MoveCard(int moveCardID, int moveAfterCardID)
        {
            throw new NotImplementedException();
        }

        public Task<Card> MoveCardBottom(int moveCardID, int columnID)
        {
            throw new NotImplementedException();
        }

        public Task<Card> MoveCardTop(int moveCardID, int columnID)
        {
            throw new NotImplementedException();
        }

        public Task<Card> UpdateCard(string title, string description, DateTime deadLine)
        {
            throw new NotImplementedException();
        }

        public Task<Card> UpdateCardDeadline(int cardID, DateTime deadLine)
        {
            throw new NotImplementedException();
        }

        public Task<Card> UpdateCardDescription(int cardID, string description)
        {
            throw new NotImplementedException();
        }

        public Task<Card> UpdateCardTitle(int cardID, string title)
        {
            throw new NotImplementedException();
        }
    }
}
