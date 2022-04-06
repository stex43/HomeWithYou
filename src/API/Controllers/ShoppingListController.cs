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
    [Route("api/shoppingLists")]
    public sealed class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListRepository shoppingListRepository;

        public ShoppingListController(IShoppingListRepository shoppingListRepository)
        {
            this.shoppingListRepository = shoppingListRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody][Required] View.ShoppingListCreationRequest creationRequest)
        {
            var creationInfo = new ShoppingListCreationRequest(creationRequest.Name);
            await this.shoppingListRepository.CreateAsync(creationInfo);

            return this.Created("", creationInfo);
        }

        [HttpGet]
        [Route("{shoppingListId:guid}")]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid shoppingListId)
        {
            var productList = await this.shoppingListRepository.GetAsync(shoppingListId);

            if (productList == null)
            {
                return this.NotFound(shoppingListId);
            }

            return this.Ok(new View.ShoppingList
            {
                Id = productList.Id,
                Name = productList.Name,
                Products = productList.Products
            });
        }

        [HttpDelete]
        [Route("{shoppingListId:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid shoppingListId)
        {
            await this.shoppingListRepository.DeleteAsync(shoppingListId);

            return this.NoContent();
        }

        [HttpPatch]
        [Route("{shoppingListId:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid shoppingListId, [FromBody] View.ShoppingListUpdate update)
        {
            await this.shoppingListRepository.DeleteAsync(shoppingListId);

            return this.NoContent();
        }
    }
}
