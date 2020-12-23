namespace SKAutoNew.Services.Tests.ServicesTestHelpers
{
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;

    public static class GetPartService
    {
        public static PartService Return(SKAutoDbContext db, 
                                         BrandService brandService,
                                         ModelService modelService,
                                         CategoryService categoryService,
                                         ManufactoryService manufactoryService)
        {
            var partRepository = new Repository<Part>(db);
            var partService = new PartService(partRepository, brandService, modelService, categoryService, manufactoryService);

            return partService;
        }
    }
}
