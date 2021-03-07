using kanban.Models;
using kanban.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Services
{
    public interface ICardService
    {
        Task<Card> GetCard(int cardID);
        Task<Card> MoveCard(int moveCardID, CardMove cardMove);
        Task<Card> UpdateCard(int id, Card card);
        Task<Card> AddCard(Card card);
        Task DeleteCard(int cardID);
    }
}
