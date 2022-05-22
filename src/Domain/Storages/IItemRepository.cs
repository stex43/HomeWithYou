using System;
using System.Threading.Tasks;
using HomeWithYou.Domain.Items;

namespace HomeWithYou.Domain.Storages
{
    public interface IItemRepository
    {
        Task<Item?> GetAsync(Guid id);
    }
}