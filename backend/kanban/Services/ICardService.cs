using kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Services
{
    public interface ICardService
    {
        List<Card> GetCardsInOrderByColumn(int columnID);
        Task<ICollection<Card>> GetAllCards();
        Task<Card> GetCard(int cardID);
        Task<Card> MoveCard(int moveCardID, int moveAfterCardID);
        Task<Card> MoveCardTop(int moveCardID, int columnID);
        Task<Card> MoveCardBottom(int moveCardID, int columnID);
        Task<Card> UpdateCard(string title, string description, DateTime deadLine);
        Task<Card> UpdateCardTitle(int cardID, string title);
        Task<Card> UpdateCardDescription(int cardID, string description);
        Task<Card> UpdateCardDeadline(int cardID, DateTime deadLine);
        void DeleteCard(int cardID);
    }
}
