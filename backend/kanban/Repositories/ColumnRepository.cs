using kanban.Data;
using kanban.Models;
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

        public Task<Column> GetColumn(int columnID)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Column>> GetColumns()
        {
            throw new NotImplementedException();
        }
    }
}
