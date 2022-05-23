using System;
using HomeWithYou.Domain.Storages;
using LightInject;
using Microsoft.EntityFrameworkCore;

namespace HomeWithYou.Domain
{
    public sealed class DomainCompositionRoot : ICompositionRoot
    {
        private readonly string connectionString;

        public DomainCompositionRoot(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), $"{nameof(connectionString)} can't be null or empty");
            }
            
            this.connectionString = connectionString;
        }

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IShoppingListRepository, ShoppingListRepository>(new PerScopeLifetime());
            serviceRegistry.Register<IItemRepository, ItemRepository>(new PerScopeLifetime());
            serviceRegistry.Register<IShoppingListItemRepository, ShoppingListItemRepository>(new PerScopeLifetime());
            
            var optionsBuilder = new DbContextOptionsBuilder<SqlContext>().UseSqlServer(this.connectionString);
            var options = optionsBuilder.Options;

            serviceRegistry.Register(x => new SqlContext(options), new PerScopeLifetime());
        }
    }
}