using CustomerCardService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerCardService.Domain.Repository
{
    public class CardContext : DbContext
    {
        public CardContext()
        {
        }

        public CardContext(DbContextOptions<CardContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Card> Cards { get; set; }
    }
}
