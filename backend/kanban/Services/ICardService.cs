using Kanban.Data;
using Kanban.Models;
using Kanban.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Services
{
    public interface ICardService
    {
        Task<Card> GetCard(int cardID);
        Task<Card> MoveCard(int moveCardID, CardMove cardMove);
        Task<Card> UpdateCard(int cardID, Card card);
        Task<Card> AddCard(Card card);
        Task DeleteCard(int cardID);
    }
}
