using System;
using System.Threading.Tasks;
using HomeWithYou.Domain.Items;

namespace HomeWithYou.Domain.Storages
{
    public interface IShoppingListItemRepository
    {
        Task<bool> SaveAsync(ShoppingListItem shoppingListItem);

        Task RemoveAsync(Guid shoppingListId, Guid itemId);
    }
}