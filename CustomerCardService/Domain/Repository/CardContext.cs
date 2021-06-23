using CustomerCardService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerCardService.Domain.Repository
{
    public class CardContext : DbContext
    {
        public CardContext(DbContextOptions<CardContext> options)
            : base(options)
        {

        }

        public DbSet<Card> Cards { get; set; }
    }
}
