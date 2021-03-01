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
            repository.DeleteCard(cardID);
        }

        public Task<ICollection<Card>> GetAllCards()
        {
            return repository.GetCards();
        }

        public Task<Card> GetCard(int cardID)
        {
            return repository.GetCard(cardID);
        }

        public List<Card> GetCardsInOrderByColumn(int columnID)
        {
            var cards = repository.GetCardsByColumn(columnID);
            if (cards == null || cards.Count == 0) return null;
            return cards.OrderBy(c => c.Sort).ToList();
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
