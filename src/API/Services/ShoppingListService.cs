using System;
using System.Threading.Tasks;
using HomeWithYou.API.Infrastructure;
using HomeWithYou.Domain.Items;
using HomeWithYou.Domain.ShoppingLists;
using HomeWithYou.Domain.Storages;

namespace HomeWithYou.API.Services
{
    internal sealed class ShoppingListService : IShoppingListService
    {
        private readonly IShoppingListRepository shoppingListRepository;
        private readonly IItemRepository itemRepository;
        private readonly IShoppingListItemRepository shoppingListItemRepository;

        public ShoppingListService(
            IShoppingListRepository shoppingListRepository, 
            IItemRepository itemRepository,
            IShoppingListItemRepository shoppingListItemRepository)
        {
            this.shoppingListRepository = shoppingListRepository;
            this.itemRepository = itemRepository;
            this.shoppingListItemRepository = shoppingListItemRepository;
        }

        public Task<ShoppingList> CreateAsync(ShoppingListCreateRequest request)
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
        
        public async Task<ShoppingList> AddItemAsync(Guid shoppingListId, ShoppingListItemAddRequest request)
        {
            var shoppingList = await this.shoppingListRepository.GetAsync(shoppingListId);

            if (shoppingList == null)
            {
                throw new NotFoundException("shoppingLists", shoppingListId);
            }

            var item = await this.itemRepository.GetAsync(request.ItemId);

            if (item == null)
            {
                throw new NotFoundException("items", request.ItemId);
            }
            
            var shoppingListItem = new ShoppingListItem
            {
                ShoppingListId = shoppingListId,
                ItemId = item.Id,
                Amount = request.Amount,
                Unit = request.Unit
            };

            await this.shoppingListItemRepository.SaveAsync(shoppingListItem);

            return shoppingList;
        }

        public async Task<ShoppingList> CrossOutItemAsync(Guid shoppingListId, ShoppingListItemCrossOutRequest request)
        {
            var shoppingList = await this.shoppingListRepository.GetAsync(shoppingListId);

            if (shoppingList == null)
            {
                throw new NotFoundException("shoppingLists", shoppingListId);
            }

            await this.shoppingListItemRepository.RemoveAsync(shoppingListId, request.ItemId);

            return shoppingList;
        }
    }
}