using System;
using System.Collections.Generic;
using HomeWithYou.Models.Items;

namespace HomeWithYou.Models.ShoppingLists
{
    public sealed class ShoppingList
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public ICollection<ShoppingListItem> ShoppingListItems { get; set; } = new List<ShoppingListItem>(0);
    }
}
