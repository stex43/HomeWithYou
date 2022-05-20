using System;
using System.Threading.Tasks;
using HomeWithYou.API.Infrastructure;
using HomeWithYou.Models.ShoppingLists;
using HomeWithYou.Models.Storages;

namespace HomeWithYou.API.Services
{
    internal sealed class ShoppingListService : IShoppingListService
    {
        private readonly IShoppingListRepository shoppingListRepository;
        
        
        
        public Task<ShoppingList> CreateAsync(ShoppingListCreationRequest request)
        {
            return this.shoppingListRepository.SaveAsync(request);
        }

        public async Task<ShoppingList> GetAsync(Guid id)
        {
            var shoppingList = await this.shoppingListRepository.GetAsync(id);

            if (shoppingList == null)
            {
                throw new NotFoundException("shoppingLists", id);
            }

            return shoppingList;
        }
    }
}