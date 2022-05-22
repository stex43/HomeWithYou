using HomeWithYou.Domain.Storages;
using LightInject;

namespace HomeWithYou.Domain
{
    public sealed class DomainCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IShoppingListRepository, ShoppingListRepository>(new PerScopeLifetime());
            serviceRegistry.Register<IItemRepository, ItemRepository>(new PerScopeLifetime());
            serviceRegistry.Register<IShoppingListItemRepository, ShoppingListItemRepository>(new PerScopeLifetime());
        }
    }
}