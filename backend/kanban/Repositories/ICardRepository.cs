using kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Repositories
{
    interface ICardRepository
    {
        Task<ICollection<Card>> GetCard();
        Task<Card> GetCard(int cardID);
        Task<Card> AddCard(Card card);
        Task<Card> UpdateCard(Card card);
        void DeleteCard(int cardID);
    }
}
