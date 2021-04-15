using Kanban.Bll;
using Kanban.Bll.Exceptions;
using Kanban.Bll.Models;
using Kanban.Data;
using System;
using Xunit;

namespace KanbanTests
{
    public class CardServiceTest
    {
        private CardRepositoryMock cardRepo;
        private ColumnRepositoryMock columnRepo;
        private ICardService cardService;

        public CardServiceTest()
        {
            cardRepo = new CardRepositoryMock();
            columnRepo = new ColumnRepositoryMock();
            cardService = new CardService(cardRepo, columnRepo);
        }

        [Fact]
        public async void TestUpdate()
        {
            var card = new Card() { ID = 2, Title = "Test", ColumnID = 1, Sort = 2 };
            await cardRepo.AddCard(card);
            var currentDate = DateTime.Now;
            var newCard = new CardDto(1, 1, "Test2", "Test", currentDate);
            var updatedCard = await cardService.UpdateCard(2, newCard);
            Assert.Equal("Test2", updatedCard.Title);
            Assert.Equal("Test", updatedCard.Description);
            Assert.Equal(currentDate, updatedCard.Deadline);
        }

        [Fact]
        public async void TestUpdateNonExisting()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => cardService.UpdateCard(98765, new CardDto(1, 1, "", "", DateTime.Now)));
        }

        [Fact]
        public async void TestGet()
        {
            var card = new Card() { ID = 3, Title = "Test", ColumnID = 1, Sort = 3 };
            await cardRepo.AddCard(card);
            var getCard = await cardService.GetCard(3);
            Assert.NotNull(getCard);
        }

        [Fact]
        public async void TestGetNonExisting()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => cardService.GetCard(98765));
        }

        [Fact]
        public async void TestDelete()
        {
            var card = new Card() { ID = 4, Title = "Test", ColumnID = 1, Sort = 3 };
            await cardRepo.AddCard(card);
            await cardService.DeleteCard(4);
            var deletedCard = cardRepo.Cards.Find(c => c.ID == 4);
            Assert.Null(deletedCard);
        }

        [Fact]
        public async void TestDeleteNonExisting()
        {
            await Assert.ThrowsAsync<NotFoundException>(() => cardService.DeleteCard(98765));
        }

        [Fact]
        public async void TestMoveCardRepositoryExceptions()
        {
            var card1 = new Card() { ID = 7, Title = "Test", ColumnID = 4, Sort = 1 };
            var card2 = new Card() { ID = 8, Title = "Test", ColumnID = 4, Sort = 2 };
            await cardRepo.AddCard(card1);
            await cardRepo.AddCard(card2);

            await Assert.ThrowsAsync<BadRequestException>(() => cardService.MoveCard(7, new CardMove() { ColumnId = 3, PreviousCardId = 8 }));
            await Assert.ThrowsAsync<NotFoundException>(() => cardService.MoveCard(7, new CardMove() { ColumnId = 98765, PreviousCardId = 8 }));
            await Assert.ThrowsAsync<BadRequestException>(() => cardService.MoveCard(7, new CardMove() { ColumnId = null, PreviousCardId = 8 }));
        }
    }
}
