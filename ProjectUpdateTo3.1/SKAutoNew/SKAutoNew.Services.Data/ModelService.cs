namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.ModelDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.ModelServiceMappers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ModelService : IModelService
    {
        private readonly IRepository<Model> models;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly IRepository<ModelCategories> modelCategories;

        public ModelService(
            IRepository<Model> models,
            IBrandService brandService,
            ICategoryService categoryService,
            IRepository<ModelCategories> modelCategories)
        {
            this.models = models;
            this.brandService = brandService;
            this.categoryService = categoryService;
            this.modelCategories = modelCategories;
        }

        public async Task CreateModel(ModelCreateDtoModel modelToCreate)
        {
            if (modelToCreate.ImageAddress == null)
            {
                modelToCreate.ImageAddress = GlobalConstants.ImageAddress;
            }

            var brand = await this.brandService.GetBrandByNameAsync(modelToCreate.BrandName);
            var model = ModelServiceCreateMapper.Map(modelToCreate, brand);
            var allCategories = await this.categoryService.GetAllCategoriesForModelAsync();

            for (int i = 0; i < allCategories.Count(); i++)
            {
                var modelCategory = new ModelCategories
                {
                    Category = allCategories[i],
                    Model = model,
                };

                await this.modelCategories.InsertAsync(modelCategory);
            }

            await this.models.InsertAsync(model);
            await this.models.SaveAsync();
        }

        public async Task DeleteModelAsync(int id)
        {
            var neededModel = await this.models.All()
                                               .Include(x => x.ModelCategories)
                                               .FirstOrDefaultAsync(x => x.Id == id);

            if (neededModel.ModelCategories.Count > 0)
            {
                foreach (var item in neededModel.ModelCategories)
                {
                    this.modelCategories.Delete(item);
                }

                await this.modelCategories.SaveAsync();
            }

            this.models.Delete(neededModel);
            await this.models.SaveAsync();
        }

        public async Task<IList<ModelWithBrandNameDtoModel>> GetAllModels()
        {
            var brandsWithModels = await this.models.All().Include(x => x.Brand).ToListAsync();
            var models = GetAllModelsMapper.Map(brandsWithModels);

            return models;
        }

        public async Task<IList<ModelWithImageDtoModel>> GetAllModelsByBrandNameAsync(string kindInputModel)
        {
            var brandId = await this.brandService.GetBrandIdByNameAsync(kindInputModel);
            var allModels = await this.models.All().Where(x => x.BrandId == brandId).OrderBy(x => x.Name).ToListAsync();
            var modelsByBrand = GetAllModelsByBrandMapper.Map(allModels, kindInputModel);

            return modelsByBrand;
        }

        public async Task<ModelUpdateOutputDtoModel> GetModelByIdAsync(int id)
        {
            var currentModel = await this.models.All().Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);
            var allBrandsName = await this.brandService.GetBrandNamesAsync();
            var neededModel = GetModelByIdMapper.Map(currentModel, allBrandsName);

            return neededModel;
        }

        public async Task<Model> GetModelByNameStartAndEndYearsAsync(string name, int startYear, int endYear)
        {
            var model = await this.models.All().FirstOrDefaultAsync(x => x.Name == name
                                                                && x.StartYear == startYear
                                                                && x.EndYear == endYear);

            return model;
        }

        public async Task<bool> HavePartsAsync(int id)
        {
            var neededModel = await this.models.All()
                .Include(x => x.Parts)
                .FirstOrDefaultAsync(x => x.Id == id);

            var countOfModelParts = neededModel.Parts.Count();
            if (countOfModelParts != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IfModelExistsAsync(string brandName, string modelName, int startYear, int endYear)
        {
            int brandId = await this.brandService.GetBrandIdByNameAsync(brandName);
            var allModels = this.models.All().Where(x => x.BrandId == brandId);
            bool existModel = allModels.Any(x => x.Name.ToUpper() == modelName.ToUpper() 
                                            && x.StartYear == startYear 
                                            && x.EndYear == endYear);

            return existModel;
        }

        public async Task<bool> IsSameAsync(string brandName, string modelName, int startYear, int endYear, string imageAddress)
        {
            int brandId = await this.brandService.GetBrandIdByNameAsync(brandName);
            var allModels = this.models.All().Where(x => x.BrandId == brandId);
            bool existModel = allModels.Any(x => x.Name.ToUpper() == modelName.ToUpper() 
                                            && x.StartYear == startYear 
                                            && x.EndYear == endYear
                                            && x.ImageAddress == imageAddress);

            return existModel;
        }

        public async Task UpdateModelAsync(ModelUpdateInputDtoModel model)
        {
            var brand = await this.brandService.GetBrandByNameAsync(model.BrandName);

            var modelForUpdate = ModelServiceUpdateMapper.Map(model, brand);

            this.models.Update(modelForUpdate);
            await this.models.SaveAsync();
        }
    }
}
