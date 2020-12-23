namespace SKAutoNew.Services.Tests
{
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Threading.Tasks;
    using Xunit;

    public class ModelServiceTests
    {
        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task CreateModelShoudReturnOne()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);

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

            var allModels = await modelService.GetAllModels();
            var count = allModels.Count;

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task DeleteModelAsyncShouldReturnZero()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            
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


            await modelService.DeleteModelAsync(1);

            var allModels = await modelService.GetAllModels();
            var count = allModels.Count;

            Assert.Equal(0, count);
        }

        [Fact]
        public async Task GetModelByIdAsyncShouldReturnNameA6()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            
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

            var currentModel = await modelService.GetModelByIdAsync(1);
            var actual = currentModel.ModelName;
            var expected = "A6";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetModelByNameStartAndEndYearsAsyncShouldReturnModelWithNameA6StartYear1994EndYear1996()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            
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

            var currentModel = await modelService.GetModelByNameStartAndEndYearsAsync("A6", 1994, 1998);
            var actual = currentModel.Name;
            var expected = "A6";

            Assert.Equal(expected, actual);
            Assert.Equal(1994, currentModel.StartYear);
            Assert.Equal(1998, currentModel.EndYear);
        }

        [Fact]
        public async Task HavePartsAsyncShouldReturnFalse()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            
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

            var haveParts = await modelService.HavePartsAsync(1);

            Assert.False(haveParts);
        }

        [Fact]
        public async Task IfModelExistsAsyncShoulReturnTrue()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            
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

            var ifExist = await modelService.IfModelExistsAsync("Audi", "A6", 1994, 1998);

            Assert.True(ifExist);
        }

        [Fact]
        public async Task IfModelExistsAsyncShoulReturnFalse()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            
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

            var ifExist = await modelService.IfModelExistsAsync("Audi", "A6", 1999, 2004);

            Assert.False(ifExist);
        }

        [Fact]
        public async Task IsSameAsyncShouldReturnTrue()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            
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

            var isSame = await modelService.IsSameAsync("Audi", "A6", 1994, 1998, "/Images/AUDI/AUDI A6 1994-1998-.jpg");

            Assert.True(isSame);
        }

        [Fact]
        public async Task IsSameAsyncShouldReturnFalse()
        {
            var brandService = GetBrandService.Return(db);
            var modelService = GetModelService.Return(db, brandService);
            
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

            var isSame = await modelService.IsSameAsync("Audi", "A6", 1994, 1998, "");

            Assert.False(isSame);
        }
    }
}
