using HomeWithYou.Models.Items;
using View = HomeWithYou.Views;

namespace HomeWithYou.API.Converters
{
    internal static class ShoppingListItemConverter
    {
        public static ShoppingListItemAddRequest Convert(View.ShoppingListItemAddRequest value)
        {
            return new ShoppingListItemAddRequest
            {
                ItemId = value.ItemId!.Value,
                Amount = value.Amount,
                Unit = value.Unit
            };
        }
        
        public static ShoppingListItemCrossOutRequest Convert(View.ShoppingListItemCrossOutRequest value)
        {
            return new ShoppingListItemCrossOutRequest
            {
                ItemId = value.ItemId!.Value
            };
        }
    }
}