using System;
using System.Collections.Generic;

namespace HomeWithYou.Models.ShoppingLists
{
    public sealed class ShoppingList
    {
        private readonly List<Guid> products = new();

        public Guid Id { get; }
        
        public string Name { get; }

        public ShoppingList(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public IReadOnlyCollection<Guid> Products => this.products.ToArray();

        public void AddProducts(IEnumerable<Guid> productIds)
        {
            this.products.AddRange(productIds);
        }
    }
}
