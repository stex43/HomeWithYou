using System;
using System.Collections.Generic;
using HomeWithYou.Domain.Items;

namespace HomeWithYou.Domain.ShoppingLists
{
    public sealed class ShoppingList
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>(0);
    }
}
