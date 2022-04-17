using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using HomeWithYou.Models.EF;
using HomeWithYou.Models.ShoppingLists;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shoppingLists")]
    public sealed class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListRepository shoppingListRepository;
        private readonly ApplicationContext applicationContext;

        public ShoppingListController(IShoppingListRepository shoppingListRepository, ApplicationContext applicationContext)
        {
            this.shoppingListRepository = shoppingListRepository;
            this.applicationContext = applicationContext;
        }

        [HttpPost]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAsync([FromBody][Required] View.ShoppingListCreationRequest creationRequest)
        {
            this.applicationContext.Add(new ShoppingList(Guid.NewGuid(), "te123123st"));
            await this.applicationContext.SaveChangesAsync();
            var q = await this.applicationContext.FindAsync<ShoppingList>();
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
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid shoppingListId)
        {
            await this.shoppingListRepository.DeleteAsync(shoppingListId);

            return this.NoContent();
        }

        [HttpPatch]
        [Route("{shoppingListId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid shoppingListId, [FromBody] View.ShoppingListUpdate update)
        {
            await this.shoppingListRepository.UpdateAsync(shoppingListId, update.Name);

            return this.NoContent();
        }
    }
}
