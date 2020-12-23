namespace SKAutoNew.Services.Tests.ServicesTestHelpers
{
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;

    public static class GetBrandService
    {
        public static BrandService Return(SKAutoDbContext db)
        {
            var brandRepository = new Repository<Brand>(db);
            var brandService = new BrandService(brandRepository);

            return brandService;
        }
    }
}
