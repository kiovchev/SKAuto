namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Common;
    using SKAuto.Common.DtoModels.ModelDto;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.ModelViewModels;

    public class ModelService : IModelService
    {
        private readonly IRepository<Model> models;
        private readonly IBrandService brandService;
        private readonly IRepository<Category> categories;
        private readonly IRepository<ModelCategories> modelCategories;

        public ModelService(
            IRepository<Model> models,
            IBrandService brandService,
            IRepository<Category> categories,
            IRepository<ModelCategories> modelCategories)
        {
            this.models = models;
            this.brandService = brandService;
            this.categories = categories;
            this.modelCategories = modelCategories;
        }

        public async Task CreateModel(ModelInputViewModel modelInputViewModel)
        {
            string imageAddress = modelInputViewModel.ImageAddress;
            var brand = await this.brandService.GetBrandByNameAsync(modelInputViewModel.BrandName);

            if (imageAddress == null)
            {
                imageAddress = GlobalConstants.ImageAddress;
            }

            Model model = new Model
            {
                Name = modelInputViewModel.Name,
                StartYear = modelInputViewModel.StartYear,
                EndYear = modelInputViewModel.EndYear,
                BrandId = brand.Id,
                Brand = brand,
                ImageAddress = imageAddress,
            };

            List<Category> allCategories = this.categories.All().ToList();

            for (int i = 0; i < allCategories.Count(); i++)
            {
                ModelCategories modelCategory = new ModelCategories
                {
                    Category = allCategories[i],
                    Model = model,
                };

                await this.modelCategories.AddAsync(modelCategory);
            }

            await this.models.AddAsync(model);
            await this.models.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(int id)
        {
            var neededModel = await this.models.All().FirstOrDefaultAsync(x => x.Id == id);
            this.models.Delete(neededModel);
            await this.models.SaveChangesAsync();
        }

        public async Task<IList<ModelWithBrandNameDtoModel>> GetAllModels()
        {
            var brandsWithModels = await this.models.All().Include(x => x.Brand).ToListAsync();
            var models = new List<ModelWithBrandNameDtoModel>();

            foreach (var item in brandsWithModels)
            {
                var currentModel = new ModelWithBrandNameDtoModel()
                {
                    ModelId = item.Id,
                    ModelName = item.Name,
                    StartYear = item.StartYear,
                    EndYear = item.EndYear,
                    BrandName = item.Brand.Name,
                };

                models.Add(currentModel);
            }

            return models;
        }

        public async Task<IList<ModelsWithImage>> GetAllModelsByBrandNameAsync(ModelKindInputModel kindInputModel)
        {
            var brandId = await this.brandService.GetBrandIdByNameAsync(kindInputModel.Name);
            List<Model> allModels = this.models.All().Where(x => x.BrandId == brandId).OrderBy(x => x.Name).ToList();

            List<ModelsWithImage> modelsByBrand = new List<ModelsWithImage>();

            foreach (var model in allModels)
            {
                ModelsWithImage modelsWithImage = new ModelsWithImage
                {
                    Name = $"{kindInputModel.Name} {model.Name}  {model.StartYear}-{model.EndYear}",
                    ModelImageAddress = model.ImageAddress,
                };

                modelsByBrand.Add(modelsWithImage);
            }

            return modelsByBrand;
        }

        public async Task<int> GetCountOfModelsByBrandIdAsync(int id)
        {
            var count = await this.models.All().CountAsync(x => x.BrandId == id);

            return count;
        }

        public async Task<ModelUpdateOutputDtoModel> GetModelByIdAsync(int id)
        {
            var currentModel = await this.models.All().Include(x => x.Brand).FirstOrDefaultAsync(x => x.Id == id);
            var allBrandsName = await this.brandService.GetBrandNamesAsync();

            var neededModel = new ModelUpdateOutputDtoModel()
            {
                ModelId = currentModel.Id,
                ModelName = currentModel.Name,
                StartYear = currentModel.StartYear,
                EndYear = currentModel.EndYear,
                ImageAddress = currentModel.ImageAddress,
                BrandName = currentModel.Brand.Name,
                AllBrandNames = allBrandsName,
            };

            return neededModel;
        }

        public async Task<bool> HaveModelCategoriesOrParts(int id)
        {
            var neededModel = await this.models.All()
                .Include(x => x.ModelCategories)
                .Include(x => x.Parts)
                .FirstOrDefaultAsync(x => x.Id == id);

            var countOfModelCategories = neededModel.ModelCategories.Count();
            var countOfModelParts = neededModel.Parts.Count();
            if (countOfModelCategories != 0 || countOfModelParts != 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IfModelExists(string brandName, string modelName, int startYear, int endYear)
        {
            int brandId = await this.brandService.GetBrandIdByNameAsync(brandName);
            var allModels = this.models.All().Where(x => x.BrandId == brandId);
            bool existModel = allModels.Any(x => x.Name.ToUpper() == modelName.ToUpper() && x.StartYear == startYear && x.EndYear == endYear);

            return existModel;
        }

        public async Task<bool> IsSameAsync(string brandName, string modelName, int startYear, int endYear, string imageAddress)
        {
            int brandId = await this.brandService.GetBrandIdByNameAsync(brandName);
            var allModels = this.models.All().Where(x => x.BrandId == brandId);
            bool existModel = allModels.Any(x => x.Name.ToUpper() == modelName.ToUpper() && x.StartYear == startYear && x.EndYear == endYear);

            return existModel;
        }

        public async Task UpdateModelAsync(ModelUpdateInputDtoModel model)
        {
            var brand = await this.brandService.GetBrandByNameAsync(model.BrandName);

            var modelForUpdate = new Model()
            {
                Id = model.Id,
                Name = model.Name,
                StartYear = model.StartYear,
                EndYear = model.EndYear,
                ImageAddress = model.ImageAddress,
                Brand = brand,
            };

            this.models.Update(modelForUpdate);
            await this.models.SaveChangesAsync();
        }
    }
}
