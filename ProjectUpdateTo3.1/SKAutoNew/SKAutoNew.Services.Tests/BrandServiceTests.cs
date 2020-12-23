namespace SKAutoNew.Services.Tests
{
    using Moq;
    using SKAutoNew.Common.DtoModels.BrandDtos;
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Tests.ServicesTestHelpers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class BrandServiceTests
    {
        private Mock<IRepository<Brand>> brandRepository = new Mock<IRepository<Brand>>();
        private List<Brand> brandsAll = new List<Brand>
            {
               new Brand
               {
                   Id = 1,
                   Name = "Audi",
                   ImageAddress = "/Images/CarLogos/Audi-logo.png",
                   Models = new List<Model>{new Model
                   {
                       Id = 1,
                       BrandId = 1,
                       Name = "A5",
                       StartYear = 1995,
                       EndYear = 2000,
                       ModelCategories = new List<ModelCategories>(),
                       Parts = new List<Part>(),
                       ImageAddress = ""
                   }},
                   Parts = new List<Part>()
               },
               new Brand
               {
                   Id = 2,
                   Name = "BMW",
                   ImageAddress = "/Images/CarLogos/BMW-logo.png",
                   Models = new List<Model>(),
                   Parts = new List<Part>()
               },
               new Brand
               {
                   Id = 3,
                   Name = "Ford",
                   ImageAddress = "/Images/CarLogos/Ford-logo.png",
                   Models = new List<Model>(),
                   Parts = new List<Part>()
               },
               new Brand
               {
                   Id = 4,
                   Name = "Citroen",
                   ImageAddress = "/Images/CarLogos/Citroen-logo.png",
                   Models = new List<Model>(),
                   Parts = new List<Part>()
               }
            };

        private SKAutoDbContext db = DbHelper.GetDb();

        [Fact]
        public async Task GetBrandNamesAsyncShouldreturnFour()
        {
            brandRepository.Setup(r => r.All()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);

            var result = await service.GetAllBrandsWithImageAsync();
            var currentCount = result.Count;

            Assert.Equal(4, currentCount);
            brandRepository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetAllBrandsWithModelsAsyncShouldReturnOneBrandsAndOneModel()
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

            var result = await brandService.GetAllBrandsWithModelsAsync();
            var currentCount = result.Count;

            Assert.Equal(1, currentCount);

            var expected = 1;
            var actual = result[0].Models.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetBrandByIdAsyncShouldReturnBrandUpdateDtoModel() 
        {
            brandRepository.Setup(r => r.All()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);
            var result = await service.GetBrandByIdAsync(1);
            
            var expected = "BrandUpdateDtoModel";
            var actual = result.GetType().Name;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GetBrandByNameAsyncShouldReturnBrandWithNameAudiAndBrandIdIsOne()
        {
            brandRepository.Setup(r => r.All()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);
            var result = await service.GetBrandByNameAsync("Audi");

            Assert.True(result.Name == "Audi");
            Assert.True(result.Id == 1);
        }

        [Fact]
        public async Task GetBrandIdByNameAsyncShouldReturnOne()
        {
            brandRepository.Setup(r => r.All()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);

            var result = await service.GetBrandIdByNameAsync("Audi");
            var expected = 1;

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetBrandNamesAsyncShoulReturnFourAndBMW()
        {
            brandRepository.Setup(r => r.All()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);
            var result = await service.GetBrandNamesAsync();

            var expected = 4;
            var actual = result.Count;

            Assert.Equal(expected, actual);
            Assert.True(result[1] == "BMW");
        }

        [Fact]
        public async Task GetBrandsWithLogosShouldReturnFourThirdIsCitroen() 
        {
            brandRepository.Setup(r => r.All()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);
            var result = await service.GetBrandsWithLogos();

            var expected = 4;
            var actual = result.Count;

            Assert.Equal(expected, actual);
            Assert.True(result[2].BrandName == "Citroen");
        }

        [Fact]
        public async Task IfBrandExistsAsyncShouldReturnTrue()
        {
            brandRepository.Setup(r => r.All()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);
            var result = await service.IfBrandExistsAsync("BMW");

            Assert.True(result);
        }

        [Fact]
        public async Task IfBrandExistsAsyncShouldReturnFalse()
        {
            brandRepository.Setup(r => r.All()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);
            var result = await service.IfBrandExistsAsync("Mazda");

            Assert.False(result);
        }

        [Fact]
        public async Task SameBrandCountShouldReturnZero()
        {
            var model = new BrandUpdateDtoModel
            {
                BrandId = 5,
                BrandName = "Suzuki",
                ImageAddress = "/Images/CarLogos/Suzuki-logo.png"
            };

            brandRepository.Setup(r => r.AllAsNoTracking()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);
            var result = await service.SameBrandCount(model);

            var expected = 0;
            var actual = result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task SameBrandCountShouldReturnOne()
        {
            var model = new BrandUpdateDtoModel
            {
                BrandId = 1,
                BrandName = "Audi",
                ImageAddress = "/Images/CarLogos/Audi-logo.png"
            };

            brandRepository.Setup(r => r.AllAsNoTracking()).Returns(brandsAll.AsQueryable());

            var service = new BrandService(brandRepository.Object);
            var result = await service.SameBrandCount(model);

            var expected = 1;
            var actual = result;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task DeleteBrandAsyncShouldReturnFalse()
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

            var isDeleted = await brandService.DeleteBrandAsync(1);

            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteBrandAsyncShouldReturnTrue()
        {
            var brandRepo = new Repository<Brand>(db);
            var brandService = new BrandService(brandRepo);

            await brandService.CreateBrand(new BrandCreateDtoModel
            {
                Name = "Audi",
                ImageAddress = "/Images/CarLogos/Audi-logo.png"
            });

            var isDeleted = await brandService.DeleteBrandAsync(1);

            Assert.True(isDeleted);
        }

        [Fact]
        public async Task UpdateBrandAsyncShouldReturnTrue()
        {
            var brandRepo = new Repository<Brand>(db);
            var brandService = new BrandService(brandRepo);

            await brandService.CreateBrand(new BrandCreateDtoModel
            {
                Name = "Audi",
                ImageAddress = "/Images/CarLogos/Audi-logo.png"
            });

            var isUpdated = await brandService.UpdateBrandAsync(new BrandUpdateDtoModel
            {
                BrandId = 1,
                BrandName = "Audi",
                ImageAddress = "/Images/CarLogos/Audi-logo.png"
            });

            Assert.True(isUpdated);
        }
    }
}
