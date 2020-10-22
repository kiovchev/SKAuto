namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Common;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public class PartService : IPartService
    {
        private readonly IRepository<Part> parts;
        private readonly IBrandService brandService;
        private readonly IModelService modelService;
        private readonly ICategoryService categoryService;
        private readonly IManufactoryService manufactoryService;

        public PartService(
            IRepository<Part> parts,
            IBrandService brandService,
            IModelService modelService,
            ICategoryService categoryService,
            IManufactoryService manufactoryService)
        {
            this.parts = parts;
            this.brandService = brandService;
            this.modelService = modelService;
            this.categoryService = categoryService;
            this.manufactoryService = manufactoryService;
        }

        public async Task<bool> CheckIfPartExistsAsync(PartCreateInputModel model)
        {
            var part = await this.parts.All().FirstOrDefaultAsync(x => x.Name == model.PartName
                                                                    && x.Model.Name == model.ModelName
                                                                    && x.Category.Name == model.CategoryName
                                                                    && x.Manufactory.Name == model.ModelName);

            if (part == null)
            {
                return false;
            }

            return true;
        }

        public async Task CreatePartAsync(PartCreateInputModel partModel)
        {
            var allCarParams = this.TakeParmsFromModelName(partModel.ModelName);
            var brandName = allCarParams[0];
            var modelName = allCarParams[1];
            var modelStartYear = int.Parse(allCarParams[2]);
            var modelEndYear = int.Parse(allCarParams[3]);
            var categoryName = partModel.CategoryName;

            var brand = await this.brandService.GetBrandByNameAsync(brandName);
            var model = await this.modelService.GetModelByNameStartAndEndYearsAsync(
                                                                                    modelName,
                                                                                    modelStartYear,
                                                                                    modelEndYear);
            var category = await this.categoryService.GetCategoryByNameAsync(categoryName);

            var part = new Part
            {
                Name = partModel.PartName,
                Brand = brand,
                Model = model,
                Category = category,
                InComePrice = partModel.Price,
                Quantity = partModel.Quantity,
            };

            if (partModel.ManufactoryName != null)
            {
                var manufactory = await this.manufactoryService.GetManufactoryByNameAsync(partModel.ManufactoryName);

                if (manufactory == null)
                {
                    manufactory = await this.manufactoryService.CreateManufactoryAsync(partModel.ManufactoryName);
                }

                part.Manufactory = manufactory;
            }

            await this.parts.AddAsync(part);
            await this.parts.SaveChangesAsync();
        }

        public async Task<PartCreateViewModel> GetPartCreateParams()
        {
            var brands = await this.brandService.GetAllBrandsWithModelsAsync();
            var categories = await this.categoryService.GetAllCategoryNamesAsync();

            var brandsWithModels = new List<string>();

            foreach (var brand in brands)
            {
                foreach (var model in brand.Models)
                {
                    string neededInfo = brand.Name + " " + model.Name + " " + model.StartYear + "-" + model.EndYear;

                    brandsWithModels.Add(neededInfo);
                }
            }

            var partCreate = new PartCreateViewModel
            {
                BrandWithModels = brandsWithModels.OrderBy(x => x).ToList(),
                Categories = categories.OrderBy(x => x).ToList(),
            };

            return partCreate;
        }

        public async Task<List<PartByCategoryAndModelViewModel>> GetPartsByModelAndCategoryAsync(string modelName, string categoryName)
        {
            var allCarParams = this.TakeParmsFromModelName(modelName);
            string currentBrandName = allCarParams[0];
            string currentModelName = allCarParams[1];
            int currentStartYear = int.Parse(allCarParams[2]);
            int currentEndYear = int.Parse(allCarParams[3]);

            var brand = await this.brandService.GetBrandByNameAsync(currentBrandName);
            var model = await this.modelService.GetModelByNameStartAndEndYearsAsync(
                                                                                     currentModelName,
                                                                                     currentStartYear,
                                                                                     currentEndYear);
            var category = await this.categoryService.GetCategoryByNameAsync(categoryName);

            var partsByParams = await this.parts.All().Where(x => x.BrandId == brand.Id &&
                                                                   x.ModelId == model.Id &&
                                                                   x.CategoryId == category.Id).ToListAsync();

            var partsByCategoryAndModel = new List<PartByCategoryAndModelViewModel>();

            foreach (var part in partsByParams)
            {
                var manufactory = await this.manufactoryService.GetManufactoryById(part.ManufactoryId);
                string manufactoryName = GlobalConstants.ManufactoryName;

                if (manufactory != null)
                {
                    manufactoryName = manufactory.Name;
                }

                var currentPart = new PartByCategoryAndModelViewModel
                {
                    PartName = part.Name,
                    BrandAndModelName = modelName,
                    CategoryName = categoryName,
                    ManufactoryName = manufactoryName,
                    Quantity = part.Quantity,
                    SellPrice = part.CustomerPrice.ToString(format: GlobalConstants.PartPriceFormat),
                };

                partsByCategoryAndModel.Add(currentPart);
            }

            return partsByCategoryAndModel;
        }

        private List<string> TakeParmsFromModelName(string brandWithModelName)
        {
            var carParams = brandWithModelName
                .Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            string brandName = carParams[0];
            var carModel = string.Join(" ", carParams.Skip(1)
                .Take(carParams.Length - 2)
                .ToArray());
            var carYears = carParams[carParams.Length - 1]
                .Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            var startYear = carYears[0];
            var endYear = carYears[1];

            var carParamsArr = new List<string> { brandName, carModel, startYear, endYear };

            return carParamsArr;
        }
    }
}
