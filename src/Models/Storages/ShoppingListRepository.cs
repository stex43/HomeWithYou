using System;
using System.Threading.Tasks;
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

        public async Task<ShoppingList> SaveAsync(ShoppingListCreateRequest request)
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

        public async Task<ShoppingList?> GetAsync(Guid id)
        {
            var shoppingList = await this.sqlContext.FindAsync<ShoppingList>(id);

            if (shoppingList == null)
            {
                return null;
            }
            
            await this.sqlContext.Entry(shoppingList)
                .Collection(x => x.ShoppingListItems)
                .LoadAsync();

            foreach (var shoppingListItem in shoppingList.ShoppingListItems)
            {
                await this.sqlContext.Entry(shoppingListItem)
                    .Reference(x => x.Item)
                    .LoadAsync();
            }

            return shoppingList;
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
            this.sqlContext.Dispose();
        }
    }
}