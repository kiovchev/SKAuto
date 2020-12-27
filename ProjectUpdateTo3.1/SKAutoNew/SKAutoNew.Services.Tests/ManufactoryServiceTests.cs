namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Threading.Tasks;
    using Xunit;

    public class ManufactoryServiceTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task CreateManufactoryAsyncShouldReturnName()
        {
            var manufactoryRepository = new Repository<Manufactory>(db);
            var manufactoryService = new ManufactoryService(manufactoryRepository);

            var manufactory = await manufactoryService.CreateManufactoryAsync("Kaih");

            var expected = "Kaih";
            var actual = manufactory.Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetManufactoryByIdShouldReturnName()
        {
            var manufactoryRepository = new Repository<Manufactory>(db);
            var manufactoryService = new ManufactoryService(manufactoryRepository);

            var manufactoryCreate = await manufactoryService.CreateManufactoryAsync("Kaih");
            var manufactory = await manufactoryService.GetManufactoryById(1);

            var expected = "Kaih";
            var actual = manufactory.Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetManufactoryByNameAsyncShouldReturnId()
        {
            var manufactoryRepository = new Repository<Manufactory>(db);
            var manufactoryService = new ManufactoryService(manufactoryRepository);

            var manufactoryCreate = await manufactoryService.CreateManufactoryAsync("Kaih");
            var manufactory = await manufactoryService.GetManufactoryByNameAsync("Kaih");

            var expected = 1;
            var actual = manufactory.Id;

            Assert.Equal(expected, actual);
        }
    }
}
