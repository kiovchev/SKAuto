namespace SKAutoNew.Services.Tests.ServicesTestHelpers
{
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;

    public static class GetModelService
    {
        public static ModelService Return(SKAutoDbContext db, BrandService brandService)
        {
            var modelRepository = new Repository<Model>(db);
            var categoryRepository = new Repository<Category>(db);
            var modelCategoryRepository = new Repository<ModelCategories>(db);
            var caretegoryService = new CategoryService(categoryRepository, modelRepository, modelCategoryRepository);
            var modelService = new ModelService(modelRepository, brandService, caretegoryService, modelCategoryRepository);

            return modelService;
        }
    }
}
