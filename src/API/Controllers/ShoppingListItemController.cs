using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using HomeWithYou.Models.ShoppingLists;
using Microsoft.AspNetCore.Mvc;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shoppingLists/{shoppingListId:guid}/items")]
    public class ShoppingListItemController : ControllerBase
    {
        private readonly IShoppingListRepository shoppingListRepository;

        public ShoppingListItemController(IShoppingListRepository shoppingListRepository)
        {
            this.shoppingListRepository = shoppingListRepository;
        }

        [HttpPatch]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PutProductsAsync([FromRoute] Guid shoppingListId, [FromBody][Required] View.ItemList itemList)
        {
            var result = await this.shoppingListRepository.PutProductsAsync(shoppingListId, itemList.Ids);

            return this.Ok(result);
        }

        [HttpPost]
        [Route("{itemId:guid}/cross-out")]
        public async Task<IActionResult> CrossOutProductsAsync([FromRoute] Guid shoppingListId, [FromRoute] Guid itemId)
        {
            //var result = await this.shoppingListRepository.PutProductsAsync(shoppingListId, itemList.Ids);

            //return this.Ok(result);

            return this.NoContent();
        }
    }
}
