using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWithYou.Models.Items
{
    public sealed class ItemInMemoryRepository : IItemRepository
    {
        private readonly IDictionary<Guid, Item> database = new Dictionary<Guid, Item>();

        public Task CreateAsync(ItemCreationRequest creationRequest)
        {
            var shoppingList = new Item(creationRequest.Id, creationRequest.Name);
            this.database[creationRequest.Id] = shoppingList;

            return Task.CompletedTask;
        }

        public Task<Item> GetAsync(Guid productId)
        {
            if (!this.database.TryGetValue(productId, out var product))
            {
                return Task.FromResult<Item>(null);
            }

            return Task.FromResult(product);
        }
    }
}
