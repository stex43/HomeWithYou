using System;
using System.Threading.Tasks;
using HomeWithYou.Domain.Items;
using JetBrains.Annotations;

namespace HomeWithYou.Domain.Storages
{
    [UsedImplicitly]
    internal sealed class ShoppingListItemRepository : IShoppingListItemRepository
    {
        private readonly SqlContext sqlContext;

        public ShoppingListItemRepository(SqlContext sqlContext)
        {
            this.sqlContext = sqlContext;
        }

        public async Task<bool> SaveAsync(ShoppingListItem shoppingListItem)
        {
            var shoppingListId = shoppingListItem.ShoppingListId;
            var itemId = shoppingListItem.ItemId;

            var savedItem = await this.sqlContext.FindAsync<ShoppingListItem>(shoppingListId, itemId);

            if (savedItem != null)
            {
                return false;
            }

            await this.sqlContext.AddAsync(shoppingListItem);
            await this.sqlContext.SaveChangesAsync();
            return true;
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