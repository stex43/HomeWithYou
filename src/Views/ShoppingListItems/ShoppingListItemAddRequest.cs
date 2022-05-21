using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Views
{
    public sealed class ShoppingListItemAddRequest
    {
        [Required]
        public Guid? ItemId { get; set; }
        
        [Required]
        public double Amount { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public string Unit { get; set; }
    }
}