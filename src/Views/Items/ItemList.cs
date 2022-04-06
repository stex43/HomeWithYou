using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Views
{
    public sealed class ItemList
    {
        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public Guid[] Ids { get; set; }
    }
}
