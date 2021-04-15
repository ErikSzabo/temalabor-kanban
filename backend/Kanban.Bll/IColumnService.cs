using Kanban.Bll.Models;
using Kanban.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Bll
{
    public interface IColumnService
    {
        Task<List<ColumnDto>> GetColumnsInOrder();
        Task<ColumnDto> GetColumn(int columnID);
        Task<List<CardDto>> GetColumnCards(int columnID);
        Task<CardDto> AddCardToColumn(int columnID, CardDto card);
    }
}
