using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using HomeWithYou.API.Converters;
using HomeWithYou.API.Services;
using HomeWithYou.Models.Storages;
using Microsoft.AspNetCore.Mvc;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shopping-lists")]
    public sealed class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListRepository shoppingListRepository;
        private readonly IShoppingListService shoppingListService;

        public ShoppingListController(IShoppingListRepository shoppingListRepository, IShoppingListService shoppingListService)
        {
            this.shoppingListRepository = shoppingListRepository;
            this.shoppingListService = shoppingListService;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody][Required] View.ShoppingListCreateRequest request)
        {
            var creationRequest = ShoppingListConverter.Convert(request);
            
            var shoppingList = await this.shoppingListService.CreateAsync(creationRequest);

            return this.Created("", ShoppingListConverter.Convert(shoppingList));
        }

        [HttpGet]
        [Route("{shoppingListId:guid}")]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid shoppingListId)
        {
            var shoppingList = await this.shoppingListService.GetAsync(shoppingListId);
            
            return this.Ok(ShoppingListConverter.Convert(shoppingList));
        }

        [HttpDelete]
        [Route("{shoppingListId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid shoppingListId)
        {
            await this.shoppingListRepository.RemoveAsync(shoppingListId);

            return this.NoContent();
        }
    }
}
