namespace SKAutoNew.Services.Tests
{
    using Moq;
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class CategoryServiceTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task CreateCategoryAsyncShouldReteurnOne()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var categoriesAll = await categoryService.GetAllCategoriesAsync();
            var actual = categoriesAll.Count;
            var expected = 1;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteCategoryAsyncShouldReturnTrue()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var isDeleted = await categoryService.DeleteCategoryAsync(1);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task DeleteCategoryAsyncShouldReturnFalse()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var isDeleted = await categoryService.DeleteCategoryAsync(5);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task GetAllCategoriesAsyncShouldReturnOne()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var allCategories = await categoryService.GetAllCategoriesAsync();

            var expected = 1;
            var actual = allCategories.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAllCategoriesForModelAsyncShouldReturnOne()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var allCategories = await categoryService.GetAllCategoriesForModelAsync();

            var expected = 1;
            var actual = allCategories.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAllCategoriesForViewModelAsyncShouldReturnOne()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var allCategories = await categoryService.GetAllCategoriesForViewModelAsync();

            var expected = 1;
            var actual = allCategories.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAllCategoryNamesAsyncShouldReturnOne()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var allCategories = await categoryService.GetAllCategoryNamesAsync();

            var expected = 1;
            var actual = allCategories.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetCategoriesByNameAndYearssAsyncShouldReturnZero()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var allCategories = await categoryService.GetCategoriesByNameAndYears("Audi A6 1994-1998");

            var expected = 0;
            var actual = allCategories.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetCategoryByIdAsyncShouldReturnTestCategoryName()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var currentCategory = await categoryService.GetCategoryByIdAsync(1);

            var expected = "TestCategory";
            var actual = currentCategory.Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetCategoryByNameAsyncShouldReturnIdOne()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var currentCategory = await categoryService.GetCategoryByNameAsync("TestCategory");

            var expected = 1;
            var actual = currentCategory.Id;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task IfCategoryExistsShouldReturnTrue()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var ifExsits = await categoryService.IfCategoryExists("TestCategory");

            Assert.True(ifExsits);
        }

        [Fact]
        public async Task IfCategoryExistsShouldReturnFalse()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var ifExsits = await categoryService.IfCategoryExists("TestCategory1");

            Assert.False(ifExsits);
        }

        [Fact]
        public async Task IsSameCategoryAsyncShouldReturnTrue()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var ifExsits = await categoryService.IsSameCategoryAsync("TestCategory", "");

            Assert.True(ifExsits);
        }

        [Fact]
        public async Task IsSameCategoryAsyncShouldReturnFalse()
        {
            var categoryService = GetCategoryService.Return(db);

            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "TestCategory",
                ImageAddress = ""
            });

            var ifExsits = await categoryService.IsSameCategoryAsync("TestCategory1", "");

            Assert.False(ifExsits);
        }
    }
}
