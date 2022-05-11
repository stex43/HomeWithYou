using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HomeWithYou.Models.EF;
using HomeWithYou.Models.Items;
using HomeWithYou.Views;
using Microsoft.AspNetCore.Mvc;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shoppingLists/{shoppingListId:guid}/items")]
    public sealed class ShoppingListItemController : ControllerBase
    {
        private readonly ApplicationContext applicationContext;

        public ShoppingListItemController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddItemAsync([FromRoute] Guid shoppingListId, [FromBody] [Required] ItemAddingRequest addingRequest)
        {
            var item = new ShoppingListItem
            {
                ShoppingListId = shoppingListId,
                ItemId = addingRequest.Id,
                Amount = addingRequest.Amount,
                Unit = addingRequest.Unit
            };

            await this.applicationContext.AddAsync(item);
            await this.applicationContext.SaveChangesAsync();

            return this.Ok();
        }
        
        [HttpPost]
        [Route("cross-out")]
        public async Task<IActionResult> CrossOutItemAsync([FromRoute] Guid shoppingListId, [FromQuery] [Required] Guid itemId)
        {
            var item = await this.applicationContext.ShoppingListItems.FindAsync(shoppingListId, itemId);
            
            this.applicationContext.Remove(item);
            await this.applicationContext.SaveChangesAsync();

            return this.Ok();
        }
    }
}