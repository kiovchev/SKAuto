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

    /// <summary>
    /// business logic for Model
    /// </summary>
    public class ModelService : IModelService
    {
        private readonly IRepository<Model> models;
        private readonly IBrandService brandService;
        private readonly ICategoryService categoryService;
        private readonly IRepository<ModelCategories> modelCategories;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="models"></param>
        /// <param name="brandService"></param>
        /// <param name="categoryService"></param>
        /// <param name="modelCategories"></param>
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

        /// <summary>
        /// create new model and new modelcategories and add them all to database
        /// </summary>
        /// <param name="modelToCreate"></param>
        /// <returns></returns>
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

        /// <summary>
        /// find model by id and delete it from databse 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// get all models from database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<ModelWithBrandNameDtoModel>> GetAllModels()
        {
            var brandsWithModels = await this.models.All().Include(x => x.Brand).ToListAsync();
            var models = GetAllModelsMapper.Map(brandsWithModels);

            return models;
        }

        /// <summary>
        /// find all models for a brand and get them from database
        /// </summary>
        /// <param name="kindInputModel"></param>
        /// <returns></returns>
        public async Task<IList<ModelWithImageDtoModel>> GetAllModelsByBrandNameAsync(string kindInputModel)
        {
            var brandId = await this.brandService.GetBrandIdByNameAsync(kindInputModel);
            var allModels = await this.models.All().Where(x => x.BrandId == brandId).OrderBy(x => x.Name).ToListAsync();
            var modelsByBrand = GetAllModelsByBrandMapper.Map(allModels, kindInputModel);

            return modelsByBrand;
        }

        /// <summary>
        /// find model by id and get it from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ModelUpdateOutputDtoModel> GetModelByIdAsync(int id)
        {
            var currentModel = await this.models.All().Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);
            var allBrandsName = await this.brandService.GetBrandNamesAsync();
            var neededModel = GetModelByIdMapper.Map(currentModel, allBrandsName);

            return neededModel;
        }

        /// <summary>
        /// find model by name, start year, end year and get it from database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public async Task<Model> GetModelByNameStartAndEndYearsAsync(string name, int startYear, int endYear)
        {
            var model = await this.models.All().FirstOrDefaultAsync(x => x.Name == name
                                                                && x.StartYear == startYear
                                                                && x.EndYear == endYear);

            return model;
        }

        /// <summary>
        /// check is there any parts for a model and return boolean
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// check if model exists in databse and return boolean
        /// </summary>
        /// <param name="brandName"></param>
        /// <param name="modelName"></param>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <returns></returns>
        public async Task<bool> IfModelExistsAsync(string brandName, string modelName, int startYear, int endYear)
        {
            int brandId = await this.brandService.GetBrandIdByNameAsync(brandName);
            var allModels = this.models.All().Where(x => x.BrandId == brandId);
            bool existModel = allModels.Any(x => x.Name.ToUpper() == modelName.ToUpper() 
                                            && x.StartYear == startYear 
                                            && x.EndYear == endYear);

            return existModel;
        }

        /// <summary>
        /// check is there in database same model and return boolean
        /// </summary>
        /// <param name="brandName"></param>
        /// <param name="modelName"></param>
        /// <param name="startYear"></param>
        /// <param name="endYear"></param>
        /// <param name="imageAddress"></param>
        /// <returns></returns>
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

        /// <summary>
        /// update model in database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateModelAsync(ModelUpdateInputDtoModel model)
        {
            var brand = await this.brandService.GetBrandByNameAsync(model.BrandName);

            var modelForUpdate = ModelServiceUpdateMapper.Map(model, brand);

            this.models.Update(modelForUpdate);
            await this.models.SaveAsync();
        }
    }
}
