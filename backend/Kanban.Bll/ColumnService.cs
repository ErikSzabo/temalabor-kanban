using Kanban.Bll.Exceptions;
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

        public async Task<Column> GetColumn(int columnID)
        {
            await CheckColumnExistance(columnID);
            var column = await columnRepo.GetColumn(columnID);
            return column;
        }

        public async Task<List<Card>> GetColumnCards(int columnID)
        {
            await CheckColumnExistance(columnID);
            var cards = await cardRepo.GetCardsByColumn(columnID);
            if (cards == null) return new List<Card>();
            return cards;
        }

        public async Task<List<Column>> GetColumnsInOrder()
        {
            var columns = await columnRepo.GetColumns();
            if (columns == null) return new List<Column>();
            return columns;
        }

        public async Task<Card> AddCardToColumn(int columnID, Card card)
        {
            await CheckColumnExistance(columnID);
            card.ColumnID = columnID;
            var lastCard = await cardRepo.GetLastCardInColumn(columnID);
            var sort = lastCard == null ? 0 : lastCard.Sort + 1;
            card.Sort = sort;
            var savedCard = await cardRepo.AddCard(card);
            return savedCard;
        }

        private async Task CheckColumnExistance(int columnID)
        {
            var column = await columnRepo.GetColumn(columnID);
            if (column == null) throw new NotFoundException("Column not found");
        }
    }
}
