using System;
using System.Threading.Tasks;
using HomeWithYou.Domain.Items;
using HomeWithYou.Domain.ShoppingLists;

namespace HomeWithYou.API.Services
{
    public interface IShoppingListService
    {
        Task<ShoppingList> CreateAsync(ShoppingListCreateRequest request);

        Task<ShoppingList> GetAsync(Guid id);

        Task<ShoppingList> AddItemAsync(Guid shoppingListId, ShoppingListItemAddRequest request);
        
        Task<ShoppingList> CrossOutItemAsync(Guid shoppingListId, ShoppingListItemCrossOutRequest request);
    }
}