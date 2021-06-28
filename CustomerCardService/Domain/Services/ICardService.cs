
using CustomerCardService.Api.Models.Output;
using CustomerCardService.Domain.Models;


namespace CustomerCardService.Domain.Services
{
    public interface ICardService
    {
        Card SaveCard(Card card);
        bool ValidateToken(Card card);
    }
}
