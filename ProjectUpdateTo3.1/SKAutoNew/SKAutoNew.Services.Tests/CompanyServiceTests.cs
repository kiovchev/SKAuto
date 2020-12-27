namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Threading.Tasks;
    using Xunit;

    public class CompanyServiceTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task DeleteAsyncShouldReturnTrue()
        {
            var companyRepo = new Repository<Company>(db);
            var townRepo = new Repository<Town>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullcategoryRepo = new Repository<UseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullcategoryRepo,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);
            var companyService = new CompanySrvice(companyRepo, townService, useFullCategoryService);

            await companyService.CreateCompanyAsync(new CompanyInputViewDtoModel
            {
                Name = "Test",
                TownName = "TestTown",
                Address = "Test 12",
                CategoryName = "Test Service",
                Phone = "0000 000 000"
            });

            var isDeleted = await companyService.DeleteAsync(1);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteAsyncShouldReturnFalse()
        {
            var companyRepo = new Repository<Company>(db);
            var townRepo = new Repository<Town>(db);
            var townUseFullCategoriesRepo = new Repository<TownUseFullCategory>(db);
            var useFullcategoryRepo = new Repository<UseFullCategory>(db);
            var useFullCategoryService = new UseFullCategoryService(useFullcategoryRepo,
                                                                    townRepo,
                                                                    townUseFullCategoriesRepo, companyRepo);
            var townService = new TownService(townRepo, townUseFullCategoriesRepo, useFullCategoryService, companyRepo);
            var companyService = new CompanySrvice(companyRepo, townService, useFullCategoryService);

            await companyService.CreateCompanyAsync(new CompanyInputViewDtoModel
            {
                Name = "Test",
                TownName = "TestTown",
                Address = "Test 12",
                CategoryName = "Test Service",
                Phone = "0000 000 000"
            });

            var isDeleted = await companyService.DeleteAsync(2);

            Assert.False(isDeleted);
        }
    }
}
