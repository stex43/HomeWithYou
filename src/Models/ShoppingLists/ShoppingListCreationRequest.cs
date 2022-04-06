using System;

namespace HomeWithYou.Models.ShoppingLists
{
    public class ShoppingListCreationRequest
    {
        public ShoppingListCreationRequest(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}
