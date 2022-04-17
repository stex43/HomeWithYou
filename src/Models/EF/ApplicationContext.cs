using HomeWithYou.Models.ShoppingLists;
using Microsoft.EntityFrameworkCore;

namespace HomeWithYou.Models.EF
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<ShoppingList> ShoppingLists { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
