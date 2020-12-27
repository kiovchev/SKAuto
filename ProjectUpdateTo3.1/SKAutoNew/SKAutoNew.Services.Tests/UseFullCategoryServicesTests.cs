namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Threading.Tasks;
    using Xunit;

    public class UseFullCategoryServicesTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task CheckIfExistsAsyncShouldReturnTrue()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var ifExists = await useFullCategoryService.CheckIfExistsAsync("Test Category");

            Assert.True(ifExists);
        }

        [Fact]
        public async Task CheckIfExistsAsyncShouldReturnFalse()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var ifExists = await useFullCategoryService.CheckIfExistsAsync("Test Category1");

            Assert.False(ifExists);
        }

        [Fact]
        public async Task DeleteUseFullCategoryAsyncShouldReturnFalse()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var isDeleted = await useFullCategoryService.DeleteUseFullCategoryAsync(new UseFullDeleteDtoModel
            {
                UseFullCategoryId = 1,
            });

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteUseFullCategoryAsyncShouldReturnTrue()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var isDeleted = await useFullCategoryService.DeleteUseFullCategoryAsync(new UseFullDeleteDtoModel
            {
                UseFullCategoryId = 2,
            });

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task GetAllCategoriesForIndexAsyncShouldReturnOne()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var allCategories = await useFullCategoryService.GetAllCategoriesForIndexAsync();

            var expected = 1;
            var actual = allCategories.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAlluseFullCategoriesAsyncShouldReturnOne()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var allCategories = await useFullCategoryService.GetAlluseFullCategoriesAsync();

            var expected = 1;
            var actual = allCategories.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAllUseFullCategoriesWithParamsAsyncShouldReturnOne()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var allCategories = await useFullCategoryService.GetAllUseFullCategoriesWithParamsAsync();

            var expected = 1;
            var actual = allCategories.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetDtoModelForUpdateOutputModelAsyncShouldReturnName()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var currentCategory = await useFullCategoryService.GetDtoModelForUpdateOutputModelAsync(new UseFullUpdateGetInputDtoModel
            {
                UseFullCategoryId = 1
            });

            var expected = "Test Category";
            var actual = currentCategory.UseFullCategoryName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateUseFullCategoryAsyncShouldReturnFalse()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var isUpdated = await useFullCategoryService.UpdateUseFullCategoryAsync(new UseFullUpdatePostInputDtoModel
            {
                UseFullCategoryId = 1,
                UseFullCategoryName = "Test Category1",
                ImageAddress = "/No Image"
            });

            Assert.False(isUpdated);
        }

        [Fact]
        public async Task UpdateUseFullCategoryAsyncShouldReturnTrue()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await useFullCategoryService.CreateUseFullCategoryByNameAsync("Test Category", "/No Image");

            var isUpdated = await useFullCategoryService.UpdateUseFullCategoryAsync(new UseFullUpdatePostInputDtoModel
            {
                UseFullCategoryId = 1,
                UseFullCategoryName = "Test Category",
                ImageAddress = "/No Image"
            });

            Assert.True(isUpdated);
        }
    }
}
