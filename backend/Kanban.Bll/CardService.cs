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
    public class CardService : ICardService
    {
        private readonly ICardRepository repository;
        private readonly IColumnRespository columnRespository;

        public CardService(ICardRepository repository, IColumnRespository columnRespository)
        {
            this.repository = repository;
            this.columnRespository = columnRespository;
        }

        public async Task DeleteCard(int cardID)
        {
            await CheckCardExistance(cardID);
            await repository.DeleteCard(cardID);
        }

        public async Task<CardDto> GetCard(int cardID)
        {
            await CheckCardExistance(cardID);
            var card = await repository.GetCard(cardID);
            return new CardDto(card);
        }

        public async Task<CardDto> UpdateCard(int cardID, CardDto card)
        {
            await CheckCardExistance(cardID);
            var newCard = new Card() { Title = card.Title, Description = card.Description, Deadline = (DateTime)card.Deadline };
            var savedCard = await repository.UpdateCard(cardID, newCard);
            return new CardDto(savedCard);
        }

        public async Task<CardDto> MoveCard(int moveCardID, CardMoveDto cardMove)
        {
            if (cardMove.ColumnId == null) throw new BadRequestException("columnId field is required");
            var targetColumn = (int)cardMove.ColumnId;
            if (await columnRespository.GetColumn(targetColumn) == null) throw new NotFoundException("Target column not found");
            await CheckCardExistance(moveCardID);
            var cardToMove = await repository.GetCard(moveCardID);

            // If there isn't a card to move after, then move the card to the top.
            if(cardMove.PreviousCardId == null)
            {
                var firstCardInColumn = await repository.GetFirstCardInColumn(targetColumn);
                if(firstCardInColumn == null)
                {
                    var movedTopCardInEmptyColumn = await repository.MoveCardTopInEmptyColumn(cardToMove, targetColumn);
                    return new CardDto(movedTopCardInEmptyColumn);
                }
                var movedTopCard = await repository.MoveCardTop(cardToMove, firstCardInColumn, targetColumn);
                return new CardDto(movedTopCard);
            }   
            var previousCard = await repository.GetCard((int)cardMove.PreviousCardId);
            if (previousCard.ColumnID != targetColumn) throw new BadRequestException("Provided columnId and the previous card columnId does not match");
            var movedCard = await repository.MoveCardAfterAnother(cardToMove, previousCard, targetColumn);
            return new CardDto(movedCard);
        }

        private async Task CheckCardExistance(int cardID)
        {
            var card = await repository.GetCard(cardID);
            if (card == null) throw new NotFoundException($"Card with id: {cardID} not found");
        }
    }
}
