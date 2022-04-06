using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWithYou.Models.ShoppingLists
{
    public interface IShoppingListRepository
    {
        Task CreateAsync(ShoppingListCreationRequest creationRequest);

        Task<ShoppingList> GetAsync(Guid shoppingListId);

        Task<ShoppingList> PutProductsAsync(Guid shoppingListId, IReadOnlyCollection<Guid> productIds);

        Task DeleteAsync(Guid shoppingListId);

        Task UpdateAsync(Guid shoppingListId, string name);
    }
}
