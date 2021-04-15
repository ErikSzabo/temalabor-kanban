using Kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Repositories
{
    public interface IColumnRespository
    {
        Task<List<Column>> GetColumns();
        Task<Column> GetColumn(int columnID);
    }
}
