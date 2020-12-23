namespace SKAutoNew.Services.Tests.ServicesTestHelpers
{
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;

    public static class GetCategoryService
    {
        public static CategoryService Return(SKAutoDbContext db)
        {
            var modelRepository = new Repository<Model>(db);
            var modelCategoryRepo = new Repository<ModelCategories>(db);
            var categoryRepo = new Repository<Category>(db);
            var categoryService = new CategoryService(categoryRepo, modelRepository, modelCategoryRepo);

            return categoryService;
        }
    }
}
