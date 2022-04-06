using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using HomeWithYou.Models.ShoppingLists;
using Microsoft.AspNetCore.Mvc;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shoppingLists")]
    public class ShoppingListItemController : ControllerBase
    {
        private readonly IShoppingListRepository shoppingListRepository;

        public ShoppingListItemController(IShoppingListRepository shoppingListRepository)
        {
            this.shoppingListRepository = shoppingListRepository;
        }

        [HttpPut]
        [Route("{shoppingListId:guid}/items")]
        public async Task<IActionResult> PutProductsAsync([FromRoute] Guid shoppingListId, [FromBody][Required] View.ItemList itemList)
        {
            var result = await this.shoppingListRepository.PutProductsAsync(shoppingListId, itemList.Ids);

            return this.Ok(result);
        }
    }
}
