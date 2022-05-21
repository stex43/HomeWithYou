using System;
using System.ComponentModel.DataAnnotations;

namespace HomeWithYou.Views
{
    public sealed class ShoppingListItemCrossOutRequest
    {
        [Required]
        public Guid? ItemId { get; set; }
    }
}