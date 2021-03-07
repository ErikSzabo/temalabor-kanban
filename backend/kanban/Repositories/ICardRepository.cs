using kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Repositories
{
    public interface ICardRepository
    {
        Task<Card> GetCard(int cardID);
        Task<Card> AddCard(Card card);
        Task<Card> UpdateCard(Card card);
        Task DeleteCard(int cardID);
        IEnumerable<Card> GetCardsByColumn(int columnID);
        Task<Card> MoveCard(Card cardToMove, Card previousCard, int targetColumn);
    }
}
