using kanban.Models;
using System;
using System.Linq;

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

            context.Cards.Add(new Card { ColumnID = 1, Title = "Teszt 11", Description = "Teszt leírás", Deadline = DateTime.Now, Sort = 1});
            context.Cards.Add(new Card { ColumnID = 1, Title = "Teszt 12", Description = "Teszt leírás", Deadline = DateTime.Now, Sort = 2 });
            context.Cards.Add(new Card { ColumnID = 1, Title = "Teszt 13", Description = "Teszt leírás", Deadline = DateTime.Now, Sort = 3 });

            context.Cards.Add(new Card { ColumnID = 2, Title = "Teszt 21", Description = "Teszt leírás", Deadline = DateTime.Now, Sort = 1 });
            context.Cards.Add(new Card { ColumnID = 2, Title = "Teszt 22", Description = "Teszt leírás", Deadline = DateTime.Now, Sort = 2 });
            context.Cards.Add(new Card { ColumnID = 2, Title = "Teszt 23", Description = "Teszt leírás", Deadline = DateTime.Now, Sort = 3 });

            context.Cards.Add(new Card { ColumnID = 3, Title = "Teszt 31", Description = "Teszt leírás", Deadline = DateTime.Now, Sort = 1 });
            context.Cards.Add(new Card { ColumnID = 3, Title = "Teszt 32", Description = "Teszt leírás", Deadline = DateTime.Now, Sort = 2 });
            context.SaveChanges();
        }
    }
}
