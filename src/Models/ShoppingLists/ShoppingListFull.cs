using System;
using System.Collections.Generic;
using HomeWithYou.Models.Items;

namespace HomeWithYou.Models.ShoppingLists
{
    public sealed class ShoppingListFull
    {
        public Guid Id { get; }

        public string Name { get; }
        
        public IReadOnlyCollection<Item> Products { get; }

        public ShoppingListFull(Guid id, string name, IReadOnlyCollection<Item> products)
        {
            this.Id = id;
            this.Name = name;
            this.Products = products;
        }
    }
}
