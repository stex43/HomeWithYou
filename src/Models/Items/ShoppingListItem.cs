using System;

namespace HomeWithYou.Models.Items
{
    public sealed class ShoppingListItem
    {
        public Guid ShoppingListId { get; set; }
        
        public Guid ItemId { get; set; }
        
        public double Amount { get; set; }
        
        public string Unit { get; set; }
        
        public Item Item { get; set; }
    }
}