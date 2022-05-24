using System;

namespace HomeWithYou.API.Infrastructure
{
    public sealed class ConflictException : Exception
    {
        public ConflictException(string message)
            : base(message)
        {
        }

        public static ConflictException ShoppingListItemAlreadyAdded(Guid shoppingListId, Guid itemId)
        {
            return new ConflictException($"There is already item={itemId} in shopping list={shoppingListId}");
        }
    }
}