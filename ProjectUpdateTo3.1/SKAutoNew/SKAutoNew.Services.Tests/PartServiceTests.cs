namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Threading.Tasks;
    using Xunit;

    public class PartServiceTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task CheckIfPartExistsAsyncShouldReturnTrue()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            var ifExists = await partService.CheckIfPartExistsAsync("Калник",
                                                                    "Audi A6 1994-1998",
                                                                    "Едрогабаритни части",
                                                                    "Kaih");

            Assert.True(ifExists);
        }

        [Fact]
        public async Task CheckIfPartExistsAsyncShouldReturnFalse()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            var ifExists = await partService.CheckIfPartExistsAsync("Броня",
                                                                    "Audi A6 1994-1998",
                                                                    "Едрогабаритни части",
                                                                    "Kaih");

            Assert.False(ifExists);
        }

        [Fact]
        public async Task CreatePartAsyncShouldReturnTwo()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            await partService.CreatePartAsync(new PartCreateInputDtoModel
            {
                PartName = "Броня",
                CategoryName = "Едрогабаритни части",
                ManufactoryName = "Kaih",
                ModelName = "Audi A6 1994-1998",
                Price = 10,
                Quantity = 1
            });

            var allParts = await partService.GetAllPartsAsync();

            var expected = 2;
            var actual = allParts.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeletePartAsyncShouldReturnZero()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            await partService.DeletePartAsync(1);

            var allParts = await partService.GetAllPartsAsync();

            var expected = 0;
            var actual = allParts.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAddOutputModelByIdAsyncShouldReturnKaih()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            var currentPart = await partService.GetAddOutputModelByIdAsync(1);


            var expected = "Kaih";
            var actual = currentPart.ManufactoryName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetAllPartsAsyncShouldReturnOne()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            var allParts = await partService.GetAllPartsAsync();


            var expected = 1;
            var actual = allParts.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetPartCreateParamsShouldReturnOneBrandWithModelAndOneVategory()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            var partParamsmodel = await partService.GetPartCreateParams();


            var expectedBM = 1;
            var actualBM = partParamsmodel.BrandWithModels.Count;

            var expectedC = 1;
            var actualC = partParamsmodel.Categories.Count;

            Assert.Equal(expectedBM, actualBM);
            Assert.Equal(expectedC, actualC);
        }

        [Fact]
        public async Task GetPartsByModelAndCategoryAsyncShouldReturnOne()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            var partsByModelAndCategory = await partService.GetPartsByModelAndCategoryAsync("Audi A6 1994-1998", "Едрогабаритни части");


            var expected = 1;
            var actual = partsByModelAndCategory.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetPartUpdateModelShouldReturnIdOne()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            var partModel = await partService.GetPartUpdateModel(1);


            var expected = 1;
            var actual = partModel.PartId;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task IsSamePartAsyncShouldReturnTrue()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            var isSame = await partService.IsSamePartAsync("Калник", "Audi A6 1994-1998", "Едрогабаритни части", "Kaih", 1, 10);


            Assert.True(isSame);
        }

        [Fact]
        public async Task IsSamePartAsyncShouldReturnFalse()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            var categoryService = GetCategoryService.Return(db);
            var manufactoryService = GetManufactoryService.Return(db);
            var partService = GetPartService.Return(db, brandService,
                                                        modelService,
                                                        categoryService, manufactoryService);
            await AddBrandModelCategoryManufactoryAndPart(brandService, modelService, categoryService, manufactoryService, partService);

            var isSame = await partService.IsSamePartAsync("Калник1", "Audi A6 1994-1998", "Едрогабаритни части", "Kaih", 1, 10);


            Assert.False(isSame);
        }

        private static async Task AddBrandModelCategoryManufactoryAndPart(Data.BrandService brandService, Data.ModelService modelService, Data.CategoryService categoryService, Data.ManufactoryService manufactoryService, Data.PartService partService)
        {
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
        }
    }
}
