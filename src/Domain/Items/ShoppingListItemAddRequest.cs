using System;

namespace HomeWithYou.Domain.Items
{
    public sealed class ShoppingListItemAddRequest
    {
        public Guid ItemId { get; set; }
        
        public double Amount { get; set; }
        
        public string Unit { get; set; } = null!;
    }
}