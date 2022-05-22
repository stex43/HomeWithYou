using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Client.Models
{
    public sealed class ShoppingListCreateRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
