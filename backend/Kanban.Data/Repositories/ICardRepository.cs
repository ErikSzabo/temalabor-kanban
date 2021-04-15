using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kanban.Data.Repositories
{
    public interface ICardRepository
    {
        Task<Card> GetCard(int cardID);
        Task<Card> AddCard(Card card);
        Task<Card> UpdateCard(int cardID, Card card);
        Task DeleteCard(int cardID);
        Task<List<Card>> GetCardsByColumn(int columnID);
        Task<Card> GetFirstCardInColumn(int columnID);
        Task<Card> GetLastCardInColumn(int columnID);
        Task<Card> MoveCardTop(Card cardToMove, Card currentFirstCard, int targetColumn);
        Task<Card> MoveCardTopInEmptyColumn(Card cardToMove, int targetColumn);
        Task<Card> MoveCardAfterAnother(Card cardToMove, Card previousCard, int targetColumn);

    }
}
