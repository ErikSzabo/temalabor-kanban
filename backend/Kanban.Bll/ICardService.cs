using Kanban.Bll.Models;
using Kanban.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Bll
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
