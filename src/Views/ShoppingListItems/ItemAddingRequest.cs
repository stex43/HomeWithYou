using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Views
{
    public sealed class ItemAddingRequest
    {
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public double Amount { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public string Unit { get; set; }
    }
}