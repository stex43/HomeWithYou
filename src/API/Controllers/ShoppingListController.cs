using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HomeWithYou.Models.EF;
using HomeWithYou.Models.Items;
using HomeWithYou.Models.ShoppingLists;
using Microsoft.AspNetCore.Mvc;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Controllers
{
    [ApiController]
    [Route("api/shoppingLists")]
    public sealed class ShoppingListController : ControllerBase
    {
        private readonly ApplicationContext applicationContext;

        public ShoppingListController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody][Required] View.ShoppingListCreationRequest creationRequest)
        {
            var shoppingList = new ShoppingList
            {
                Id = new Guid(),
                Name = creationRequest.Name
            };
            
            await this.applicationContext.AddAsync(shoppingList);

            await this.applicationContext.SaveChangesAsync();

            return this.Created("", new View.ShoppingList
            {
                Id = shoppingList.Id,
                Name = shoppingList.Name
            });
        }

        [HttpGet]
        [Route("{shoppingListId:guid}")]
        [ProducesResponseType(typeof(View.ShoppingList), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid shoppingListId)
        {
            var shoppingList = await this.applicationContext.ShoppingLists
                .FindAsync(shoppingListId);

            if (shoppingList == null)
            {
                return this.NotFound();
            }

            await this.applicationContext.Entry(shoppingList)
                .Collection(x => x.ShoppingListItems)
                .LoadAsync();

            foreach (var shoppingListItem in shoppingList.ShoppingListItems)
            {
                await this.applicationContext.Entry(shoppingListItem)
                    .Reference(x => x.Item)
                    .LoadAsync();
            }
            
            return this.Ok(new View.ShoppingList
            {
                Id = shoppingList.Id,
                Name = shoppingList.Name,
                Items = new View.ItemList
                {
                    Items = shoppingList.ShoppingListItems?.Select(
                        x => new View.Item
                        {
                            Name = x.Item.Name,
                            Amount = x.Amount,
                            Unit = x.Unit
                        }).ToArray()
                }
            });
        }

        [HttpDelete]
        [Route("{shoppingListId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid shoppingListId)
        {
            var shoppingList = await this.applicationContext.ShoppingLists.FindAsync(shoppingListId);
            this.applicationContext.ShoppingLists.Remove(shoppingList);
            await this.applicationContext.SaveChangesAsync();

            return this.NoContent();
        }
    }
}
