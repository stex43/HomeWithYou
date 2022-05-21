using System.Linq;
using HomeWithYou.Models.Items;
using HomeWithYou.Models.ShoppingLists;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Converters
{
    internal static class ShoppingListConverter
    {
        public static View.ShoppingList Convert(ShoppingList value)
        {
            return new View.ShoppingList
            {
                Id = value.Id,
                Name = value.Name,
                Items = new View.ShoppingListItemList
                {
                    Items = value.ShoppingListItems.Select(Convert).ToArray()
                }
            };
        }

        public static ShoppingListCreateRequest Convert(View.ShoppingListCreateRequest value)
        {
            return new ShoppingListCreateRequest
            {
                Name = value.Name
            };
        }

        private static View.ShoppingListItem Convert(ShoppingListItem value)
        {
            return new View.ShoppingListItem
            {
                Name = value.Item.Name,
                Amount = value.Amount,
                Unit = value.Unit
            };
        }
    }
}