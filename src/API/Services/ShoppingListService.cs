using System;
using System.Threading.Tasks;
using HomeWithYou.API.Infrastructure;
using HomeWithYou.Domain.Items;
using HomeWithYou.Domain.ShoppingLists;
using HomeWithYou.Domain.Storages;
using JetBrains.Annotations;

namespace HomeWithYou.API.Services
{
    [UsedImplicitly]
    internal sealed class ShoppingListService : IShoppingListService
    {
        private const string ShoppingListsResource = "shoppingLists";
        private const string ItemsResource = "items";
        
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

        public Task<ShoppingList> GetAsync(Guid id)
        {
            return this.GetShoppingListAsync(id);
        }
        
        public async Task<ShoppingList> AddItemAsync(Guid shoppingListId, ShoppingListItemAddRequest request)
        {
            var itemId = request.ItemId;
            var shoppingList = await this.GetShoppingListAsync(shoppingListId);

            await this.CheckItemExistence(itemId);
            
            var shoppingListItem = new ShoppingListItem
            {
                ShoppingListId = shoppingListId,
                ItemId = itemId,
                Amount = request.Amount,
                Unit = request.Unit
            };

            var isAdded = await this.shoppingListItemRepository.SaveAsync(shoppingListItem);

            if (!isAdded)
            {
                throw ConflictException.ShoppingListItemAlreadyAdded(shoppingListId, itemId);
            }

            return shoppingList;
        }

        public async Task<ShoppingList> CrossOutItemAsync(Guid shoppingListId, ShoppingListItemCrossOutRequest request)
        {
            var shoppingList = await this.GetShoppingListAsync(shoppingListId);

            await this.shoppingListItemRepository.RemoveAsync(shoppingListId, request.ItemId);

            return shoppingList;
        }

        private async Task<ShoppingList> GetShoppingListAsync(Guid id)
        {
            var shoppingList = await this.shoppingListRepository.GetAsync(id);

            if (shoppingList == null)
            {
                throw new NotFoundException(ShoppingListsResource, id);
            }

            return shoppingList;
        }

        private async Task CheckItemExistence(Guid id)
        {
            var item = await this.itemRepository.GetAsync(id);

            if (item == null)
            {
                throw new NotFoundException(ItemsResource, id);
            }
        }
    }
}