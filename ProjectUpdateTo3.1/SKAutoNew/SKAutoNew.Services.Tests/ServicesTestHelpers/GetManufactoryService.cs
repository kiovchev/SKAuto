namespace SKAutoNew.Services.Tests.ServicesTestHelpers
{
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;

    public static class GetManufactoryService
    {
        public static ManufactoryService Return(SKAutoDbContext db)
        {
            var manyfactoryRepository = new Repository<Manufactory>(db);
            var manufactoryService = new ManufactoryService(manyfactoryRepository);

            return manufactoryService;
        }
    }
}
