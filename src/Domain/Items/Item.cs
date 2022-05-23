using System;

namespace HomeWithYou.Domain.Items
{
    public sealed class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        
        public ItemType Type { get; set; }
    }
}
