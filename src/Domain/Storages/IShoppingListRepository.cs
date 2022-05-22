using System;
using System.Threading.Tasks;
using HomeWithYou.Domain.ShoppingLists;

namespace HomeWithYou.Domain.Storages
{
    public interface IShoppingListRepository
    {
        Task<ShoppingList> SaveAsync(ShoppingListCreateRequest request);
        
        Task<ShoppingList?> GetAsync(Guid id);
        
        Task RemoveAsync(Guid id);
    }
}