using System.Threading.Tasks;
using FluentAssertions;
using HomeWithYou.Models.EntityFramework;
using HomeWithYou.Models.ShoppingLists;
using HomeWithYou.Models.Storages;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Models.Tests
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
            var creationRequest = new ShoppingListCreationRequest
            {
                Name = "test_list"
            };
            
            var expected = await this.shoppingListRepository.CreateAsync(creationRequest);

            var actual = await this.shoppingListRepository.GetAsync(expected.Id);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public async Task RemoveTest()
        {
            var creationRequest = new ShoppingListCreationRequest
            {
                Name = "test_list"
            };
            
            var created = await this.shoppingListRepository.CreateAsync(creationRequest);
            await this.shoppingListRepository.RemoveAsync(created.Id);

            var actual = await this.shoppingListRepository.GetAsync(created.Id);

            actual.Should().BeNull();
        }
    }
}