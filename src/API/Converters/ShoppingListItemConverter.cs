using HomeWithYou.Domain.Items;

namespace HomeWithYou.API.Converters
{
    internal static class ShoppingListItemConverter
    {
        public static ShoppingListItemAddRequest Convert(Client.Models.ShoppingListItemAddRequest value)
        {
            return new ShoppingListItemAddRequest
            {
                ItemId = value.ItemId!.Value,
                Amount = value.Amount,
                Unit = value.Unit
            };
        }
        
        public static ShoppingListItemCrossOutRequest Convert(Client.Models.ShoppingListItemCrossOutRequest value)
        {
            return new ShoppingListItemCrossOutRequest
            {
                ItemId = value.ItemId!.Value
            };
        }
    }
}