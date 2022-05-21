using System;
using System.Threading.Tasks;
using HomeWithYou.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace HomeWithYou.Models.Storages
{
    public sealed class ShoppingListItemRepository : IShoppingListItemRepository
    {
        private readonly SqlContext sqlContext;

        public ShoppingListItemRepository(SqlContext sqlContext)
        {
            this.sqlContext = sqlContext;
        }

        public async Task<bool> SaveAsync(ShoppingListItem shoppingListItem)
        {
            try
            {
                await this.sqlContext.AddAsync(shoppingListItem);
                await this.sqlContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task RemoveAsync(Guid shoppingListId, Guid itemId)
        {
            var shoppingListItem = await this.sqlContext.FindAsync<ShoppingListItem>(shoppingListId, itemId);
            
            if (shoppingListItem == null)
            {
                return;
            }
            
            this.sqlContext.Remove(shoppingListItem);
            await this.sqlContext.SaveChangesAsync();
        }
    }
}