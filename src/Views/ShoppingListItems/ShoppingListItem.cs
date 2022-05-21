using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Views
{
    public sealed class ShoppingListItem
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        public double Amount { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Unit { get; set; }
    }
}