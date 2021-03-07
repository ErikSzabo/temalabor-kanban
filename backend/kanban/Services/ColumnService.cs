using kanban.Exceptions;
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

        public async Task<IEnumerable<Card>> GetColumnCards(int columnID)
        {
            await CheckColumnExistance(columnID);
            var cards = cardRepo.GetCardsByColumn(columnID);
            if (cards == null || cards.Count() == 0) return new List<Card>();
            return cards.OrderBy(c => c.Sort);
        }

        public async Task<IEnumerable<Column>> GetColumnsInOrder()
        {
            var columns = await columnRepo.GetColumns();
            return columns.OrderBy(c1 => c1.Sort);
        }

        public async Task<Card> AddCardToColumn(int columnID, Card card)
        {
            await CheckColumnExistance(columnID);
            card.ColumnID = columnID;
            var lastCard = cardRepo.GetCardsByColumn(columnID).OrderByDescending(c => c.Sort).FirstOrDefault();
            var sort = lastCard == null ? 0 : lastCard.Sort + 1;
            card.Sort = sort;
            var savedCard = await cardRepo.AddCard(card);
            return savedCard;
        }

        private async Task CheckColumnExistance(int columnID)
        {
            var column = await columnRepo.GetColumn(columnID);
            if (column == null) throw new NotFound("Column not found");
        }
    }
}
