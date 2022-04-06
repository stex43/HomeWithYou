using System;

namespace HomeWithYou.Models.Items
{
    public sealed class Item
    {
        public Item(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}
