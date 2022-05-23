using System;

namespace HomeWithYou.Domain.Items
{
    public sealed class ShoppingListItem
    {
        public Guid ShoppingListId { get; set; }
        
        public Guid ItemId { get; set; }
        
        public double Amount { get; set; }
        
        public string Unit { get; set; } = null!;

        public Item Item { get; set; } = null!;
    }
}