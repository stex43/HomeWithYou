using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Client.Models
{
    public sealed class ShoppingListItemList
    {
        [Required]
        [MinLength(0)]
        [MaxLength(100)]
        public ShoppingListItem[] Items { get; set; }

        public static ShoppingListItemList Empty()
        {
            return new ShoppingListItemList
            {
                Items = Array.Empty<ShoppingListItem>()
            };
        }
    }
}
