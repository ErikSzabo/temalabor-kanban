using kanban.Exceptions;
using kanban.Models;
using kanban.Models.Requests;
using kanban.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kanban.Services
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

        public Task<Card> AddCard(Card card)
        {
            return repository.AddCard(card);
        }

        public async Task DeleteCard(int cardID)
        {
            await CheckCardExistance(cardID);
            await repository.DeleteCard(cardID);
        }

        public async Task<Card> GetCard(int cardID)
        {
            await CheckCardExistance(cardID);
            return await repository.GetCard(cardID);
        }

        public async Task<Card> UpdateCard(int cardID, Card card)
        {
            await CheckCardExistance(cardID);
            card.ID = cardID;
            return await repository.UpdateCard(card);
        }

        public async Task<Card> MoveCard(int moveCardID, CardMove cardMove)
        {
            if (cardMove.ColumnId == null) throw new BadRequestException("columnId field is required");
            var targetColumn = (int)cardMove.ColumnId;
            if (await columnRespository.GetColumn(targetColumn) == null) throw new NotFoundException("Target column not found");
            var cardToMove = await GetCard(moveCardID);

            // If there isn't a card to move after, then move the card to the top.
            if(cardMove.PreviousCardId == null)
            {
                return await repository.MoveCard(cardToMove, null, targetColumn);
            } 
            else
            {
                var previousCard = await GetCard((int)cardMove.PreviousCardId);
                if (previousCard.ColumnID != targetColumn) throw new BadRequestException("Provided columnId and the previos card columnId does not match");
                return await repository.MoveCard(cardToMove, previousCard, targetColumn);
            }
        }

        private async Task CheckCardExistance(int cardID)
        {
            var card = await repository.GetCard(cardID);
            if (card == null) throw new NotFoundException($"Card with id: {cardID} not found");
        }
    }
}
