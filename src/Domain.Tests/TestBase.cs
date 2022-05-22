using System.Threading.Tasks;
using FluentAssertions;
using HomeWithYou.Domain.ShoppingLists;
using HomeWithYou.Domain.Storages;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace HomeWithYou.Domain.Tests
{
    public class TestBase
    {
        private const string TestSqlConnection =
            "Server=(localdb)\\mssqllocaldb;Database=TestDataBase;Trusted_Connection=True;";

        private ShoppingListRepository shoppingListRepository;
        private SqlContext sqlContext;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlContext>().UseSqlServer(TestSqlConnection);
            var options = optionsBuilder.Options;
            
            this.sqlContext = new SqlContext(options);
            this.sqlContext.Database.EnsureDeleted();
            this.sqlContext.Database.EnsureCreated();
            
            this.shoppingListRepository = new ShoppingListRepository(this.sqlContext);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            this.shoppingListRepository.Dispose();
        }

        [Test]
        public async Task CreateTest()
        {
            var creationRequest = new ShoppingListCreateRequest
            {
                Name = "test_list"
            };
            
            var expected = await this.shoppingListRepository.SaveAsync(creationRequest);

            var actual = await this.shoppingListRepository.GetAsync(expected.Id);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task RemoveTest()
        {
            var creationRequest = new ShoppingListCreateRequest
            {
                Name = "test_list"
            };
            
            var created = await this.shoppingListRepository.SaveAsync(creationRequest);
            await this.shoppingListRepository.RemoveAsync(created.Id);

            var actual = await this.shoppingListRepository.GetAsync(created.Id);

            actual.Should().BeNull();
        }
    }
}