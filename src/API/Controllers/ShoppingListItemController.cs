using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HomeWithYou.Models.EntityFramework;
using HomeWithYou.Models.Items;
using HomeWithYou.Models.Storages;
using HomeWithYou.Views;
using Microsoft.AspNetCore.Mvc;
using Item = HomeWithYou.Models.Items.Item;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shopping-lists/{shoppingListId:guid}/items")]
    public sealed class ShoppingListItemController : ControllerBase
    {
        private readonly SqlContext sqlContext;
        private readonly IShoppingListRepository shoppingListRepository;

        public ShoppingListItemController(SqlContext sqlContext, IShoppingListRepository shoppingListRepository)
        {
            this.sqlContext = sqlContext;
            this.shoppingListRepository = shoppingListRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddItemAsync([FromRoute] Guid shoppingListId, [FromBody] [Required] ItemAddingRequest addingRequest)
        {
            var shoppingList = await this.shoppingListRepository.GetAsync(shoppingListId);

            if (shoppingList == null)
            {
                return this.NotFound();
            }

            var item = await this.sqlContext.FindAsync<Item>(addingRequest.Id);

            if (item == null)
            {
                return this.NotFound();
            }
            
            var shoppingListItem = new ShoppingListItem
            {
                ShoppingListId = shoppingListId,
                ItemId = addingRequest.Id,
                Amount = addingRequest.Amount,
                Unit = addingRequest.Unit
            };

            await this.sqlContext.AddAsync(shoppingListItem);
            await this.sqlContext.SaveChangesAsync();

            return this.Ok();
        }
        
        [HttpPost]
        [Route("cross-out")]
        public async Task<IActionResult> CrossOutItemAsync([FromRoute] Guid shoppingListId, [FromQuery] [Required] Guid itemId)
        {
            var item = await this.sqlContext.ShoppingListItems.FindAsync(shoppingListId, itemId);
            
            if (item == null)
            {
                return this.NotFound();
            }
            
            this.sqlContext.Remove(item);
            await this.sqlContext.SaveChangesAsync();

            return this.NoContent();
        }
    }
}