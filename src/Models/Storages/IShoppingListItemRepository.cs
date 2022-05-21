using System;
using System.Threading.Tasks;
using HomeWithYou.Models.Items;

namespace HomeWithYou.Models.Storages
{
    public interface IShoppingListItemRepository
    {
        Task<bool> SaveAsync(ShoppingListItem shoppingListItem);

        Task RemoveAsync(Guid shoppingListId, Guid itemId);
    }
}