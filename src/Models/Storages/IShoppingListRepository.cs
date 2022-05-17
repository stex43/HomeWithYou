﻿using System;
using System.Threading.Tasks;
using HomeWithYou.Models.ShoppingLists;

namespace HomeWithYou.Models.Storages
{
    public interface IShoppingListRepository
    {
        Task<ShoppingList> CreateAsync(ShoppingListCreationRequest request);
        
        Task<ShoppingList> GetAsync(Guid id);
        
        Task RemoveAsync(Guid id);
    }
}