namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class ItemServiceTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task GetCartDtoByPartIdAsyncShouldReturnOne()
        {
            var partService = await ReturnPartService(db);
            var itemRepository = new Repository<Item>(db);
            var itemService = new ItemService(partService, itemRepository);

            await itemService.GetCartDtoByPartIdAsync(1);
            var allItems = await itemService.GetAllItemsAsync();

            var expected = 1;
            var actual = allItems.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetItemByPartIdAsyncShouldReturnOne()
        {
            var partService = await ReturnPartService(db);
            var itemRepository = new Repository<Item>(db);
            var itemService = new ItemService(partService, itemRepository);

            await itemService.GetItemByPartIdAsync(1);
            var allItems = await itemService.GetAllItemsAsync();

            var expected = 1;
            var actual = allItems.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetItemsByItemsIdsAsyncShouldReturnOne()
        {
            var partService = await ReturnPartService(db);
            var itemRepository = new Repository<Item>(db);
            var itemService = new ItemService(partService, itemRepository);

            await itemService.GetItemByPartIdAsync(1);
            var allItems = await itemService.GetItemsByItemsIdsAsync(new List<int>{ 1});

            var expected = 1;
            var actual = allItems.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteShouldReturnZero()
        {
            var partService = await ReturnPartService(db);
            var itemRepository = new Repository<Item>(db);
            var itemService = new ItemService(partService, itemRepository);

            await itemService.GetItemByPartIdAsync(1);
            await itemService.Delete(1);
            var allItems = await itemService.GetAllItemsAsync();

            var expected = 0;
            var actual = allItems.Count;

            Assert.Equal(expected, actual);
        }

        private static async Task<PartService> ReturnPartService(SKAutoDbContext db)
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);

            await brandService.CreateBrand(new BrandCreateDtoModel
            {
                Name = "Audi",
                ImageAddress = "/Images/CarLogos/Audi-logo.png"
            });

            await modelService.CreateModel(new ModelCreateDtoModel
            {
                BrandName = "Audi",
                Name = "A6",
                StartYear = 1994,
                EndYear = 1998,
                ImageAddress = "/Images/AUDI/AUDI A6 1994-1998-.jpg"
            });


            await categoryService.CreateCategoryAsync(new CategoryCreateDtoModel
            {
                CategoryName = "Едрогабаритни части",
                ImageAddress = ""
            });

            await manufactoryService.CreateManufactoryAsync("Kaih");

            await partService.CreatePartAsync(new PartCreateInputDtoModel
            {
                PartName = "Калник",
                CategoryName = "Едрогабаритни части",
                ManufactoryName = "Kaih",
                ModelName = "Audi A6 1994-1998",
                Price = 10,
                Quantity = 1
            });

            return partService;
        }
    }
}
