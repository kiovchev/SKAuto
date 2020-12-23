namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.PartDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.PartServiceMappers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// business logic for Part
    /// </summary>
    public class PartService : IPartService
    {
        private readonly IRepository<Part> parts;
        private readonly IBrandService brandService;
        private readonly IModelService modelService;
        private readonly ICategoryService categoryService;
        private readonly IManufactoryService manufactoryService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="parts"></param>
        /// <param name="brandService"></param>
        /// <param name="modelService"></param>
        /// <param name="categoryService"></param>
        /// <param name="manufactoryService"></param>
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

        /// <summary>
        /// find part in database, add quantity to it and update it
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task AddQuantityAsync(PartAddInputDtoModel model)
        {
            var part = await this.parts.All().FirstOrDefaultAsync(x => x.Id == model.PartId);
            var totalQuantity = part.Quantity + model.Quantity;
            if (part.InComePrice != model.Price)
            {
                var averagePrice = Math.Round(((part.Quantity * part.InComePrice) + (model.Quantity * model.Price)) / totalQuantity, 2);
                part.InComePrice = averagePrice;
            }

            part.Quantity = totalQuantity;

            this.parts.Update(part);
            await this.parts.SaveAsync();
        }

        /// <summary>
        /// check in database if part exists and return boolean
        /// </summary>
        /// <param name="partName"></param>
        /// <param name="brandAndModelName"></param>
        /// <param name="categoryName"></param>
        /// <param name="manufactoryName"></param>
        /// <returns></returns>
        public async Task<bool> CheckIfPartExistsAsync(
                                                        string partName,
                                                        string brandAndModelName,
                                                        string categoryName,
                                                        string manufactoryName)
        {
            var carParams = this.TakeParmsFromModelName(brandAndModelName);
            var brandName = carParams[0];
            var modelName = carParams[1];
            var modelStartYear = int.Parse(carParams[2]);
            var modelEndYear = int.Parse(carParams[3]);
            var part = await this.parts.All()
                                       .Include(x => x.Model)
                                       .ThenInclude(x => x.Brand)
                                       .Include(x => x.Category)
                                       .Include(x => x.Manufactory)
                                       .FirstOrDefaultAsync(x => x.Name == partName
                                                                    && x.Model.Brand.Name == brandName
                                                                    && x.Model.Name == modelName
                                                                    && x.Model.StartYear == modelStartYear
                                                                    && x.Model.EndYear == modelEndYear
                                                                    && x.Category.Name == categoryName
                                                                    && x.Manufactory.Name == manufactoryName);

            if (part == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// create new part and add it to database
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        public async Task CreatePartAsync(PartCreateInputDtoModel partModel)
        {
            var allCarParams = this.TakeParmsFromModelName(partModel.ModelName);
            var brandName = allCarParams[0];
            var modelName = allCarParams[1];
            var modelStartYear = int.Parse(allCarParams[2]);
            var modelEndYear = int.Parse(allCarParams[3]);
            var categoryName = partModel.CategoryName;

            var model = await this.modelService.GetModelByNameStartAndEndYearsAsync(
                                                                                    modelName,
                                                                                    modelStartYear,
                                                                                    modelEndYear);
            var category = await this.categoryService.GetCategoryByNameAsync(categoryName);

            var part = PartServiceCreateMapper.Map(partModel, model, category);

            if (partModel.ManufactoryName != null)
            {
                var manufactory = await this.manufactoryService.GetManufactoryByNameAsync(partModel.ManufactoryName);

                if (manufactory == null)
                {
                    manufactory = await this.manufactoryService.CreateManufactoryAsync(partModel.ManufactoryName);
                }

                part.Manufactory = manufactory;
            }

            await this.parts.InsertAsync(part);
            await this.parts.SaveAsync();
        }

        /// <summary>
        /// find part by id and remove it from dsatabase
        /// </summary>
        /// <param name="partId"></param>
        /// <returns></returns>
        public async Task DeletePartAsync(int partId)
        {
            var part = await this.parts.All().FirstOrDefaultAsync(x => x.Id == partId);

            this.parts.Delete(part);
            await this.parts.SaveAsync();
        }

        /// <summary>
        /// get part from database by id
        /// </summary>
        /// <param name="partId"></param>
        /// <returns></returns>
        public async Task<PartAddOutputDtoModel> GetAddOutputModelByIdAsync(int partId)
        {
            var part = await this.parts.All()
                                       .Include(x => x.Model)
                                       .ThenInclude(x => x.Brand)
                                       .Include(x => x.Category)
                                       .Include(x => x.Manufactory)
                                       .FirstOrDefaultAsync(x => x.Id == partId);
            var partDto = PartServiceAddOutputMapper.Map(part);

            return partDto;
        }

        /// <summary>
        /// get all parts from database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<PartGetAllPartsForIndexDtoModel>> GetAllPartsAsync()
        {
            var allParts = await this.parts.All()
                                     .Include(x => x.Model)
                                     .ThenInclude(x => x.Brand)
                                     .Include(x => x.Category)
                                     .Include(x => x.Manufactory)
                                     .ToListAsync();

            var partAllModel = PartServiceIndexMapper.Map(allParts);

            return partAllModel;
        }

        /// <summary>
        /// get params needed for create view from database
        /// </summary>
        /// <returns></returns>
        public async Task<PartCreateOutPutDtoModel> GetPartCreateParams()
        {
            var brands = await this.brandService.GetAllBrandsWithModelsAsync();
            var categories = await this.categoryService.GetAllCategoryNamesAsync();

            var brandsWithModels = new List<string>();

            foreach (var brand in brands)
            {
                foreach (var model in brand.Models)
                {
                    string neededInfo = $"{brand.Name} {model.Name} {model.StartYear}-{model.EndYear}";
                    brandsWithModels.Add(neededInfo);
                }
            }

            var partCreate = PartGetPartCreateParamsMapper.Map(brandsWithModels, categories);

            return partCreate;
        }

        /// <summary>
        /// find and get part from database by id, thagt method is needed when we create new item 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Part> GetPartForItemByPartId(int id)
        {
            var part = await this.parts.All()
                                 .Include(x => x.Model)
                                 .ThenInclude(x => x.Brand)
                                 .Include(x => x.Category)
                                 .Include(x => x.Manufactory)
                                 .FirstOrDefaultAsync(x => x.Id == id);

            part.Quantity -= 1;
            this.parts.Update(part);
            await this.parts.SaveAsync();

            return part;
        }

        /// <summary>
        ///  get all parts for a model and for a category from database
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public async Task<IList<PartAllDtoModel>> GetPartsByModelAndCategoryAsync(string modelName, string categoryName)
        {
            var allCarParams = this.TakeParmsFromModelName(modelName);
            string currentBrandName = allCarParams[0];
            string currentModelName = allCarParams[1];
            int currentStartYear = int.Parse(allCarParams[2]);
            int currentEndYear = int.Parse(allCarParams[3]);

            var model = await this.modelService.GetModelByNameStartAndEndYearsAsync(
                                                                                     currentModelName,
                                                                                     currentStartYear,
                                                                                     currentEndYear);
            var category = await this.categoryService.GetCategoryByNameAsync(categoryName);

            var partsByParams = await this.parts.All().Where(x => x.ModelId == model.Id 
                                                             && x.CategoryId == category.Id)
                                                       .ToListAsync();

            var partsByCategoryAndModel = new List<PartAllDtoModel>();

            foreach (var part in partsByParams)
            {
                var manufactory = await this.manufactoryService.GetManufactoryById(part.ManufactoryId);
                string manufactoryName = GlobalConstants.ManufactoryName;

                if (manufactory != null)
                {
                    manufactoryName = manufactory.Name;
                }

                var currentPart = PartAllServiceMapper.Map(part, modelName, categoryName, manufactoryName);
                partsByCategoryAndModel.Add(currentPart);
            }

            return partsByCategoryAndModel;
        }

        /// <summary>
        /// get part model needed for update view
        /// </summary>
        /// <param name="partId"></param>
        /// <returns></returns>
        public async Task<PartUpdateOutputDtoModel> GetPartUpdateModel(int partId)
        {
            var part = await this.parts.All()
                                       .Include(x => x.Model)
                                       .ThenInclude(x => x.Brand)
                                       .Include(x => x.Category)
                                       .Include(x => x.Manufactory)
                                       .FirstOrDefaultAsync(x => x.Id == partId);
            var brands = await this.brandService.GetAllBrandsWithModelsAsync();
            var categories = await this.categoryService.GetAllCategoryNamesAsync();
            var brandsAndModelsNames = new List<string>();

            foreach (var brand in brands)
            {
                foreach (var model in brand.Models)
                {
                    string neededInfo = $"{brand.Name} {model.Name} {model.StartYear}-{model.EndYear}";

                    brandsAndModelsNames.Add(neededInfo);
                }
            }

            var partDto = PartServiceGetUpdateModelMapper.Map(part, brandsAndModelsNames, categories);

            return partDto;
        }

        /// <summary>
        /// check in database is there same part in it
        /// </summary>
        /// <param name="partName"></param>
        /// <param name="brandAndModelName"></param>
        /// <param name="categoryName"></param>
        /// <param name="manufactoryName"></param>
        /// <param name="partQuantity"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<bool> IsSamePartAsync(string partName, string brandAndModelName, string categoryName, string manufactoryName, int partQuantity, decimal price)
        {
            var carParams = this.TakeParmsFromModelName(brandAndModelName);
            var brandName = carParams[0];
            var modelName = carParams[1];
            var modelStartYear = int.Parse(carParams[2]);
            var modelEndYear = int.Parse(carParams[3]);
            var part = await this.parts.All()
                                       .Include(x => x.Model)
                                       .ThenInclude(x => x.Brand)
                                       .Include(x => x.Category)
                                       .Include(x => x.Manufactory)
                                       .FirstOrDefaultAsync(x => x.Name == partName
                                                                    && x.Model.Brand.Name == brandName
                                                                    && x.Model.Name == modelName
                                                                    && x.Model.StartYear == modelStartYear
                                                                    && x.Model.EndYear == modelEndYear
                                                                    && x.Category.Name == categoryName
                                                                    && x.Manufactory.Name == manufactoryName
                                                                    && x.Quantity == partQuantity
                                                                    && x.InComePrice == price);

            if (part == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// decrease part quantity in database, when order an item
        /// </summary>
        /// <param name="partId"></param>
        /// <returns></returns>
        public async Task RemoveQuantityAsync(int partId)
        {
            var part = await this.parts.All().FirstOrDefaultAsync(x => x.Id == partId);
            part.Quantity--;

            this.parts.Update(part);
        }

        /// <summary>
        /// increase part quantity in database when an item is deleted
        /// </summary>
        /// <param name="partId"></param>
        /// <param name="orderedQuantity"></param>
        /// <returns></returns>
        public async Task ReturnPartFromCartAsync(int partId, int orderedQuantity)
        {
            var part = await this.parts.All().FirstOrDefaultAsync(x => x.Id == partId);
            part.Quantity += orderedQuantity;

            this.parts.Update(part);
        }

        /// <summary>
        /// update part and save changes in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdatePartAsync(PartUpdateInputDtoModel model)
        {
            var allCarParams = this.TakeParmsFromModelName(model.BrandAndModelName);
            string currentBrandName = allCarParams[0];
            string currentModelName = allCarParams[1];
            int currentStartYear = int.Parse(allCarParams[2]);
            int currentEndYear = int.Parse(allCarParams[3]);

            var part = await this.parts.All().FirstOrDefaultAsync(x => x.Id == model.PartId);
            var currentModel = await this.modelService.GetModelByNameStartAndEndYearsAsync(
                                                                                           currentModelName,
                                                                                           currentStartYear,
                                                                                           currentEndYear);
            var category = await this.categoryService.GetCategoryByNameAsync(model.CategoryName);
            var manufactory = await this.manufactoryService.GetManufactoryByNameAsync(model.ManufactoryName);

            var currentPart = PartServiceUpdateMapper.Map(model, part, currentModel, category, manufactory);

            this.parts.Update(currentPart);
            await this.parts.SaveAsync();
        }

        /// <summary>
        /// take params from brand and model name (need to split string for that) - this is a help method
        /// </summary>
        /// <param name="brandWithModelName"></param>
        /// <returns></returns>
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
