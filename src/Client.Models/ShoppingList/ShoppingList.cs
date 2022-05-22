using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Client.Models
{
    public sealed class ShoppingList
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        public ShoppingListItemList Items { get; set; } = ShoppingListItemList.Empty();
    }
}
