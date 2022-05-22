using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Client.Models
{
    public sealed class ShoppingListItemCrossOutRequest
    {
        [Required]
        public Guid? ItemId { get; set; }
    }
}