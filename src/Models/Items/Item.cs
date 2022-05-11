using System;

namespace HomeWithYou.Models.Items
{
    public sealed class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public ItemType Type { get; set; }
    }
}
