using System;
using System.Threading.Tasks;
using HomeWithYou.Models.Items;

namespace HomeWithYou.Models.Storages
{
    public sealed class ItemRepository : IItemRepository
    {
        private readonly SqlContext sqlContext;

        public ItemRepository(SqlContext sqlContext)
        {
            this.sqlContext = sqlContext;
        }

        public async Task<Item?> GetAsync(Guid id)
        {
            return await this.sqlContext.FindAsync<Item>(id);
        }
    }
}