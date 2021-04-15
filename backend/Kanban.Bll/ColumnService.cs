using Kanban.Bll.Exceptions;
using Kanban.Bll.Models;
using Kanban.Data;
using Kanban.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kanban.Bll
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRespository columnRepo;
        private readonly ICardRepository cardRepo;

        public ColumnService(IColumnRespository columnRepo, ICardRepository cardRepo)
        {
            this.columnRepo = columnRepo;
            this.cardRepo = cardRepo;
        }

        public async Task<ColumnDto> GetColumn(int columnID)
        {
            await CheckColumnExistance(columnID);
            var column = await columnRepo.GetColumn(columnID);
            return new ColumnDto(column.ID, column.Name);
        }

        public async Task<List<CardDto>> GetColumnCards(int columnID)
        {
            await CheckColumnExistance(columnID);
            var cards = await cardRepo.GetCardsByColumn(columnID);
            if (cards == null) return new List<CardDto>();
            return cards.Select(card => new CardDto(card)).ToList();
        }

        public async Task<List<ColumnDto>> GetColumnsInOrder()
        {
            var columns = await columnRepo.GetColumns();
            if (columns == null) return new List<ColumnDto>();
            return columns.Select(c => new ColumnDto(c.ID, c.Name)).ToList();
        }

        public async Task<CardDto> AddCardToColumn(int columnID, CardDto card)
        {
            await CheckColumnExistance(columnID);
            var newCard = new Card() { Title = card.Title, Description = card.Description, Deadline = (DateTime)card.Deadline, ColumnID = columnID };
            var lastCard = await cardRepo.GetLastCardInColumn(columnID);
            var sort = lastCard == null ? 0 : lastCard.Sort + 1;
            newCard.Sort = sort;
            var savedCard = await cardRepo.AddCard(newCard);
            return new CardDto(savedCard);
        }

        private async Task CheckColumnExistance(int columnID)
        {
            var column = await columnRepo.GetColumn(columnID);
            if (column == null) throw new NotFoundException("Column not found");
        }
    }
}
