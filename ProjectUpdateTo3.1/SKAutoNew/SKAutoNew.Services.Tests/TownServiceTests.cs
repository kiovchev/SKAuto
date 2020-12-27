namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Threading.Tasks;
    using Xunit;

    public class TownServiceTests
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

            await townService.CreateTownByNameAsync("Test");

            var ifExists = await townService.CheckIfExistsAsync("Test");

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

            await townService.CreateTownByNameAsync("Test");

            var ifExists = await townService.CheckIfExistsAsync("Test1");

            Assert.False(ifExists);
        }

        [Fact]
        public async Task DeleteTownAsyncShouldReturnTrue()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await townService.CreateTownByNameAsync("Test");

            var isDeleted = await townService.DeleteTownAsync(new TownDeleteDtoModel 
            {
                TownId = 1,
            });

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteTownAsyncShouldReturnFalse()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await townService.CreateTownByNameAsync("Test");

            var isDeleted = await townService.DeleteTownAsync(new TownDeleteDtoModel
            {
                TownId = 2,
            });

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task GetAllTownsAsyncShouldReturnOne()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await townService.CreateTownByNameAsync("Test");

            var allTowns = await townService.GetAllTownsAsync();
            
            var expected = 1;
            var actual = allTowns.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetTownNamesAsyncShouldReturnOne()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await townService.CreateTownByNameAsync("Test");

            var allTowns = await townService.GetTownNamesAsync();

            var expected = 1;
            var actual = allTowns.TownsNames.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetTownsByCategoryNameAsyncShouldReturnZero()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await townService.CreateTownByNameAsync("Test");

            var allTowns = await townService.GetTownsByCategoryNameAsync("Test Category");

            var expected = 0;
            var actual = allTowns.TownNames.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetTownsForIndexAsyncShouldReturnOne()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await townService.CreateTownByNameAsync("Test");

            var allTowns = await townService.GetTownsForIndexAsync();

            var expected = 1;
            var actual = allTowns.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task UpdateTownAsyncShouldReturnTrue()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await townService.CreateTownByNameAsync("Test");

            var isUpdated = await townService.UpdateTownAsync(new TownUpdatePostInputDtoModel
            {
                TownId = 1,
                TownName = "Test1"
            });

            Assert.True(isUpdated);
        }

        [Fact]
        public async Task UpdateTownAsyncShouldReturnFalse()
        {
            var townRepo = new Repository<Town>(db);
            var useFullCategory = new Repository<UseFullCategory>(db);
            var companyRepo = new Repository<Company>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullCategory,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);

            await townService.CreateTownByNameAsync("Test");

            var isUpdated = await townService.UpdateTownAsync(new TownUpdatePostInputDtoModel
            {
                TownId = 1,
                TownName = "Test"
            });

            Assert.False(isUpdated);
        }
    }
}
