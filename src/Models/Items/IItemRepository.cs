using System;
using System.Threading.Tasks;

namespace HomeWithYou.Models.Items
{
    public interface IItemRepository
    {
        Task CreateAsync(ItemCreationRequest creationRequest);

        Task<Item> GetAsync(Guid productId);
    }
}
