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
            return repository.GetColumn(columnID);
        }

        public async Task<ICollection<Column>> GetColumnsInOrder()
        {
            var columns = await repository.GetColumns();
            return columns.OrderBy(c1 => c1.Sort).ToList();
        }
    }
}
