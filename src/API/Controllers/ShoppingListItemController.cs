using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HomeWithYou.API.Converters;
using HomeWithYou.API.Services;
using Microsoft.AspNetCore.Mvc;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shopping-lists/{shoppingListId:guid}/items")]
    public sealed class ShoppingListItemController : ControllerBase
    {
        private readonly IShoppingListService shoppingListService;

        public ShoppingListItemController(IShoppingListService shoppingListService)
        {
            this.shoppingListService = shoppingListService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddItemAsync([FromRoute] Guid shoppingListId, [FromBody] [Required] View.ShoppingListItemAddRequest request)
        {
            var addingRequest = ShoppingListItemConverter.Convert(request);
            
            var shoppingList = await this.shoppingListService.AddItemAsync(shoppingListId, addingRequest);

            return this.Ok(ShoppingListConverter.Convert(shoppingList));
        }
        
        [HttpPost]
        [Route("cross-out")]
        public async Task<IActionResult> CrossOutItemAsync([FromRoute] Guid shoppingListId, [FromBody] [Required] View.ShoppingListItemCrossOutRequest request)
        {
            var crossingOutRequest = ShoppingListItemConverter.Convert(request);

            var shoppingList = await this.shoppingListService.CrossOutItemAsync(shoppingListId, crossingOutRequest);

            return this.Ok(ShoppingListConverter.Convert(shoppingList));
        }
    }
}