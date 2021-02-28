using kanban.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Data
{
    public static class DbInitializer
    {
        public static void Initialize(KanbanContext context)
        {
            context.Database.EnsureCreated();

            if (context.Columns.Any()) return;

            var columns = new Column[]
            {
                new Column{ Name="Todo", Sort=0},
                new Column{ Name="In Progress", Sort=1},
                new Column{ Name="Done", Sort=2},
                new Column{ Name="Postponed", Sort=3},
            };

            foreach (var column in columns)
                context.Columns.Add(column);

            context.SaveChanges();
        }
    }
}
