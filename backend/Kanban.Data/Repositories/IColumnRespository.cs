using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanban.Data.Repositories
{
    public interface IColumnRespository
    {
        Task<List<Column>> GetColumns();
        Task<Column> GetColumn(int columnID);
    }
}
