using System.Linq;
using HomeWithYou.Models.Items;
using HomeWithYou.Models.ShoppingLists;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Converters
{
    public static class ShoppingListConverter
    {
        public static View.ShoppingList Convert(ShoppingList value)
        {
            return new View.ShoppingList
            {
                Id = value.Id,
                Name = value.Name,
                Items = new View.ItemList
                {
                    Items = value.ShoppingListItems.Select(Convert).ToArray()
                }
            };
        }

        public static ShoppingListCreationRequest Convert(View.ShoppingListCreationRequest value)
        {
            return new ShoppingListCreationRequest
            {
                Name = value.Name
            };
        }

        private static View.Item Convert(ShoppingListItem value)
        {
            return new View.Item
            {
                Name = value.Item.Name,
                Amount = value.Amount,
                Unit = value.Unit
            };
        }
    }
}