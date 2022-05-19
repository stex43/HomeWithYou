using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HomeWithYou.API.Converters;
using HomeWithYou.API.Infrastructure;
using HomeWithYou.Models.Items;
using HomeWithYou.Models.Storages;
using Microsoft.AspNetCore.Mvc;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shopping-lists/{shoppingListId:guid}/items")]
    public sealed class ShoppingListItemController : ControllerBase
    {
        private readonly SqlContext sqlContext;
        private readonly IShoppingListRepository shoppingListRepository;
        private readonly IItemRepository itemRepository;

        public ShoppingListItemController(
            SqlContext sqlContext, 
            IShoppingListRepository shoppingListRepository,
            IItemRepository itemRepository)
        {
            this.sqlContext = sqlContext;
            this.shoppingListRepository = shoppingListRepository;
            this.itemRepository = itemRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddItemAsync([FromRoute] Guid shoppingListId, [FromBody] [Required] View.ItemAddingRequest request)
        {
            var shoppingList = await this.shoppingListRepository.GetAsync(shoppingListId);

            if (shoppingList == null)
            {
                return this.NotFoundResult("shoppingLists", shoppingListId.ToString());
            }

            var item = await this.itemRepository.GetAsync(request.Id);

            if (item == null)
            {
                return this.NotFoundResult("items", request.Id.ToString());
            }
            
            var shoppingListItem = new ShoppingListItem
            {
                ShoppingListId = shoppingListId,
                ItemId = request.Id,
                Amount = request.Amount,
                Unit = request.Unit
            };

            await this.sqlContext.AddAsync(shoppingListItem);
            await this.sqlContext.SaveChangesAsync();

            return this.Ok(ShoppingListConverter.Convert(shoppingList));
        }
        
        [HttpPost]
        [Route("cross-out")]
        public async Task<IActionResult> CrossOutItemAsync([FromRoute] Guid shoppingListId, [FromBody] [Required] View.ItemCrossingOutRequest request)
        {
            var shoppingList = await this.shoppingListRepository.GetAsync(shoppingListId);

            if (shoppingList == null)
            {
                return this.NotFoundResult("shoppingLists", shoppingListId.ToString());
            }
            
            var item = await this.sqlContext.ShoppingListItems.FindAsync(shoppingListId, request.ItemId);
            
            if (item == null)
            {
                return this.Ok(ShoppingListConverter.Convert(shoppingList));
            }
            
            this.sqlContext.Remove(item);
            await this.sqlContext.SaveChangesAsync();

            return this.Ok(ShoppingListConverter.Convert(shoppingList));
        }
    }
}