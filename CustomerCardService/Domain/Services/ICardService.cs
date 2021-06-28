using CustomerCardService.Domain.Models;
using System.Threading.Tasks;

namespace CustomerCardService.Domain.Services
{
    public interface ICardService
    {
        Task<Card> SaveCard(Card card);
        Task<bool> ValidateToken(Card card);
    }
}
