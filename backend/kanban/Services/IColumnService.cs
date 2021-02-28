using kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Services
{
    public interface IColumnService
    {
        Task<ICollection<Column>> GetColumnsInOrder();
        Task<Column> GetColumn(int columnID);
    }
}
