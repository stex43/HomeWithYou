using System.Linq;
using HomeWithYou.Client.Models;
using ShoppingList = HomeWithYou.Domain.ShoppingLists.ShoppingList;
using ShoppingListCreateRequest = HomeWithYou.Domain.ShoppingLists.ShoppingListCreateRequest;
using ShoppingListItem = HomeWithYou.Domain.Items.ShoppingListItem;

namespace HomeWithYou.API.Converters
{
    internal static class ShoppingListConverter
    {
        public static Client.Models.ShoppingList Convert(ShoppingList value)
        {
            return new Client.Models.ShoppingList
            {
                Id = value.Id,
                Name = value.Name,
                Items = new ShoppingListItemList
                {
                    Items = value.ShoppingListItems.Select(Convert).ToArray()
                }
            };
        }

        public static ShoppingListCreateRequest Convert(Client.Models.ShoppingListCreateRequest value)
        {
            return new ShoppingListCreateRequest
            {
                Name = value.Name
            };
        }

        private static Client.Models.ShoppingListItem Convert(ShoppingListItem value)
        {
            return new Client.Models.ShoppingListItem
            {
                Name = value.Item.Name,
                Amount = value.Amount,
                Unit = value.Unit
            };
        }
    }
}