using System;
using System.Threading.Tasks;
using HomeWithYou.Models.ShoppingLists;

namespace HomeWithYou.API.Services
{
    public interface IShoppingListService
    {
        Task<ShoppingList> CreateAsync(ShoppingListCreationRequest request);

        Task<ShoppingList> GetAsync(Guid id);
    }
}