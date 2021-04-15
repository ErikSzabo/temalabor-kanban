using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Data.Repositories
{
    public class ColumnRepository : IColumnRespository
    {
        private readonly KanbanContext kanbanContext;

        public ColumnRepository(KanbanContext kanbanContext)
        {
            this.kanbanContext = kanbanContext;
        }

        public async Task<Column> GetColumn(int columnID)
        {
            var column = await kanbanContext.Columns.FindAsync(columnID);
            return column;
        }

        public async Task<List<Column>> GetColumns()
        {
            return await kanbanContext.Columns.OrderBy(c => c.Sort).ToListAsync();
        }
    }
}
