using System;
using System.Threading.Tasks;
using HomeWithYou.Domain.Items;
using JetBrains.Annotations;

namespace HomeWithYou.Domain.Storages
{
    [UsedImplicitly]
    internal sealed class ItemRepository : IItemRepository
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