using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Views
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
