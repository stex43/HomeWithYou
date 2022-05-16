using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HomeWithYou.Models.EntityFramework;
using HomeWithYou.Models.Items;
using HomeWithYou.Views;
using Microsoft.AspNetCore.Mvc;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shopping-lists/{shoppingListId:guid}/items")]
    public sealed class ShoppingListItemController : ControllerBase
    {
        private readonly SqlContext sqlContext;

        public ShoppingListItemController(SqlContext sqlContext)
        {
            this.sqlContext = sqlContext;
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

            await this.sqlContext.AddAsync(item);
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

            return this.Ok();
        }
    }
}