using kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Repositories
{
    public interface IColumnRespository
    {
        Task<ICollection<Column>> GetColumns();
        Task<Column> GetColumn(int columnID);
    }
}
