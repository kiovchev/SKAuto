namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Common;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.ModelViewModels;

    public class ModelService : IModelService
    {
        private readonly IRepository<Model> models;
        private readonly IBrandService brandService;
        private readonly IRepository<Brand> brands;
        private readonly IRepository<Category> categories;
        private readonly IRepository<ModelCategories> modelCategories;

        public ModelService(
            IRepository<Model> models,
            IBrandService brandService,
            IRepository<Brand> brands,
            IRepository<Category> categories,
            IRepository<ModelCategories> modelCategories)
        {
            this.models = models;
            this.brandService = brandService;
            this.brands = brands;
            this.categories = categories;
            this.modelCategories = modelCategories;
        }

        public async Task CreateModel(ModelInputViewModel modelInputViewModel)
        {
            string imageAddress = modelInputViewModel.ImageAddress;
            var brand = await this.brands.All().FirstOrDefaultAsync(x => x.Name == modelInputViewModel.BrandName);

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

        public async Task<IList<ModelsWithImage>> GetAllModelsAsync(ModelKindInputModel kindInputModel)
        {
            var brandId = await this.brandService.GetBrandIdByNameAsync(kindInputModel.Name);
            List<Model> allModels = this.models.All().Where(x => x.BrandId == brandId).ToList();

            List<ModelsWithImage> modelsByBrand = new List<ModelsWithImage>();

            foreach (var item in allModels)
            {
                ModelsWithImage modelsWithImage = new ModelsWithImage
                {
                    Name = kindInputModel.Name + " " + item.Name + " " + item.StartYear + "-" + item.EndYear,
                    ModelImageAddress = item.ImageAddress,
                };

                modelsByBrand.Add(modelsWithImage);
            }

            return modelsByBrand;
        }

        public async Task<bool> IfModelExists(string brandName, string modelName, int startYear, int endYear)
        {
            int brandId = await this.brandService.GetBrandIdByNameAsync(brandName);
            var allModels = this.models.All().Where(x => x.BrandId == brandId);
            bool existModel = allModels.Any(x => x.Name.ToUpper() == modelName.ToUpper() && x.StartYear == startYear && x.EndYear == endYear);

            return existModel;
        }
    }
}
