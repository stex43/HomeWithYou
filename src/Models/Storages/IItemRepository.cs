using System;
using System.Threading.Tasks;
using HomeWithYou.Models.Items;

namespace HomeWithYou.Models.Storages
{
    public interface IItemRepository
    {
        Task<Item?> GetAsync(Guid id);
    }
}