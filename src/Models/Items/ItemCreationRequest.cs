using System;

namespace HomeWithYou.Models.Items
{
    public sealed class ItemCreationRequest
    {
        public Guid Id { get; }

        public string Name { get; }

        public ItemCreationRequest(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }
    }
}
