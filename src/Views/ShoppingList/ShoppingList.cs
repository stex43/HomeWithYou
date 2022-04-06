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
        public string Name { get; set; }

        public IReadOnlyCollection<Guid> Products { get; set; }
    }
}
