using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Views
{
    public sealed class ItemList
    {
        [Required]
        [MinLength(0)]
        [MaxLength(100)]
        public Item[] Items { get; set; }

        public static ItemList Empty()
        {
            return new ItemList
            {
                Items = Array.Empty<Item>()
            };
        }
    }
}
