﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using HomeWithYou.API.Converters;
using HomeWithYou.API.Infrastructure;
using HomeWithYou.Models.ShoppingLists;
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

        public ShoppingListController(IShoppingListRepository shoppingListRepository)
        {
            this.shoppingListRepository = shoppingListRepository;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody][Required] View.ShoppingListCreationRequest request)
        {
            var shoppingList = await this.shoppingListRepository.CreateAsync(ShoppingListConverter.Convert(request));

            return this.Created("", ShoppingListConverter.Convert(shoppingList));
        }

        [HttpGet]
        [Route("{shoppingListId:guid}")]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid shoppingListId)
        {
            var shoppingList = await this.shoppingListRepository.GetAsync(shoppingListId);

            if (shoppingList == null)
            {
                return this.NotFoundResult("shoppingLists", shoppingListId.ToString());
            }
            
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
