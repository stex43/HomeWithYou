using System;
using HomeWithYou.Domain.Items;
using HomeWithYou.Domain.ShoppingLists;
using Microsoft.EntityFrameworkCore;

namespace HomeWithYou.Domain.Storages
{
    public sealed class SqlContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<ShoppingList> ShoppingLists { get; set; }
        public DbSet<ShoppingListItem> ShoppingListItems { get; set; }

        public SqlContext(DbContextOptions<SqlContext> options)
            : base(options)
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasAlternateKey(x => x.Name);
            
            modelBuilder.Entity<ShoppingListItem>()
                .HasKey(nameof(ShoppingListItem.ShoppingListId), nameof(ShoppingListItem.ItemId));
            
            var shid1 = new Guid("09ffd0ea-e760-4878-9bcc-f8f06731cbc9");
            var shid2 = Guid.NewGuid();
            
            var iid1 = Guid.NewGuid();
            var iid2 = Guid.NewGuid();
            var iid3 = new Guid("49fcfac9-34c2-46d6-9b4b-3be965c6f7d2");
            
            modelBuilder.Entity<ShoppingList>().HasData(
                new ShoppingList { Id = shid1, Name = "shoppingList1" },
                new ShoppingList { Id = shid2, Name = "shoppingList2" });
            
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = iid1, Name = "item1", Type = ItemType.Pasta },
                new Item { Id = iid2, Name = "item2", Type = ItemType.Meat },
                new Item { Id = iid3, Name = "item3", Type = ItemType.Alcohol });
            
            modelBuilder.Entity<ShoppingListItem>().HasData(
                new ShoppingListItem { ShoppingListId = shid1, ItemId = iid2, Amount = 200, Unit = "грамм" },
                new ShoppingListItem { ShoppingListId = shid1, ItemId = iid3, Amount = 1, Unit = "пачка" });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}