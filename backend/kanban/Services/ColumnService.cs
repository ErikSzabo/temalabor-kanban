using kanban.Models;
using kanban.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRespository repository;

        public ColumnService(IColumnRespository repository)
        {
            this.repository = repository;
        }

        public Task<Column> GetColumn(int columnID)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Column>> GetColumnsInOrder()
        {
            throw new NotImplementedException();
        }
    }
}
