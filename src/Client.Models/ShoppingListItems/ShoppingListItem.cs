using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Client.Models
{
    public sealed class ShoppingListItem
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        
        [Required]
        public double Amount { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Unit { get; set; } = null!;
    }
}