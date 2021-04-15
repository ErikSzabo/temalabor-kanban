using Kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Services
{
    public interface IColumnService
    {
        Task<List<Column>> GetColumnsInOrder();
        Task<Column> GetColumn(int columnID);
        Task<List<Card>> GetColumnCards(int columnID);
        Task<Card> AddCardToColumn(int columnID, Card card);
    }
}
