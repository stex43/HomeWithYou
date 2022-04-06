using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWithYou.Models.ShoppingLists
{
    public sealed class ShoppingListInMemoryRepository : IShoppingListRepository
    {
        private readonly IDictionary<Guid, ShoppingList> database = new Dictionary<Guid, ShoppingList>();

        public Task CreateAsync(ShoppingListCreationRequest creationRequest)
        {
            var shoppingList = new ShoppingList(creationRequest.Id, creationRequest.Name);
            this.database[creationRequest.Id] = shoppingList;

            return Task.CompletedTask;
        }

        public Task<ShoppingList> GetAsync(Guid shoppingListId)
        {
            if (!this.database.TryGetValue(shoppingListId, out var shoppingList))
            {
                return Task.FromResult<ShoppingList>(null);
            }

            return Task.FromResult(shoppingList);
        }

        public Task<ShoppingList> PutProductsAsync(Guid shoppingListId, IReadOnlyCollection<Guid> productIds)
        {
            if (!this.database.TryGetValue(shoppingListId, out var shoppingList))
            {
                return Task.FromResult<ShoppingList>(null);
            }

            shoppingList.AddProducts(productIds);

            return Task.FromResult(shoppingList);
        }

        public Task DeleteAsync(Guid shoppingListId)
        {
            this.database.Remove(shoppingListId);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Guid shoppingListId, string name)
        {
            if (!this.database.TryGetValue(shoppingListId, out var shoppingList))
            {
                return Task.CompletedTask;
            }

            var updatedShoppingList = new ShoppingList(shoppingListId, name);
            updatedShoppingList.AddProducts(shoppingList.Products);

            this.database[shoppingListId] = updatedShoppingList;

            return Task.CompletedTask;
        }
    }
}
