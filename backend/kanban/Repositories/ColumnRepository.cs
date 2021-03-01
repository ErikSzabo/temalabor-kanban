using kanban.Data;
using kanban.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Repositories
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
            return await kanbanContext.Columns.FindAsync(columnID);
        }

        public async Task<ICollection<Column>> GetColumns()
        {
            return await kanbanContext.Columns.ToListAsync();
        }
    }
}
