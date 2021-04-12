using kanban.Models;
using kanban.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KanbanTests
{
    class ColumnRepositoryMock : IColumnRespository
    {
        private List<Column> columns = new List<Column>() 
        {
            new Column{ ID=1, Name="Todo", Sort=0 },
            new Column{ ID=2, Name="In Progress", Sort=1 },
            new Column{ ID=3, Name="Done", Sort=2 },
            new Column{ ID=4, Name="Postponed", Sort=3 },
        };

        public Task<Column> GetColumn(int columnID)
        {
            return Task.FromResult(columns.Find(c => c.ID == columnID));
        }

        public Task<List<Column>> GetColumns()
        {
            return Task.FromResult(columns);
        }
    }
}
