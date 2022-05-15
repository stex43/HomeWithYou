using System;
using System.Threading.Tasks;
using HomeWithYou.Models.EntityFramework;
using HomeWithYou.Models.ShoppingLists;

namespace HomeWithYou.Models.Storages
{
    public sealed class ShoppingListRepository : IShoppingListRepository, IDisposable
    {
        private readonly SqlContext sqlContext;

        public ShoppingListRepository(SqlContext sqlContext)
        {
            this.sqlContext = sqlContext ?? throw new ArgumentNullException(nameof(sqlContext));
        }

        public async Task<ShoppingList> CreateAsync(ShoppingListCreationRequest request)
        {
            var shoppingList = new ShoppingList
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            await this.sqlContext.AddAsync(shoppingList);
            await this.sqlContext.SaveChangesAsync();

            return shoppingList;
        }

        public ValueTask<ShoppingList> GetAsync(Guid id)
        {
            return this.sqlContext.FindAsync<ShoppingList>(id);
        }

        public async Task RemoveAsync(Guid id)
        {
            var shoppingList = await this.sqlContext.FindAsync<ShoppingList>(id);

            if (shoppingList == null)
            {
                return;
            }
            
            this.sqlContext.Remove(shoppingList);
            await this.sqlContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            sqlContext.Dispose();
        }
    }
}