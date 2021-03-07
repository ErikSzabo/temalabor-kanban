using kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Services
{
    public interface IColumnService
    {
        Task<IEnumerable<Column>> GetColumnsInOrder();
        Task<Column> GetColumn(int columnID);
        Task<IEnumerable<Card>> GetColumnCards(int columnID);
        Task<Card> AddCardToColumn(int columnID, Card card);
    }
}
