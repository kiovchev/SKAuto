namespace SKAutoNew.Services.Tests.ServicesTestHelpers
{
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;

    public static class GetItemService
    {
        public static ItemService Return(SKAutoDbContext db)
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            var itemRepository = new Repository<Item>(db);

            var itemService = new ItemService(partService, itemRepository);

            return itemService;
        }
    }
}
