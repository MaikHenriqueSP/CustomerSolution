using CustomerCardService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerCardService.Repository
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
