namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
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

        public async Task CreateModel(ModelInputViewModel modelInputViewModel, int brandId)
        {
            string imageAddress = modelInputViewModel.ImageAddress;
            var brand = await this.brands.All().FirstOrDefaultAsync(x => x.Name == modelInputViewModel.BrandName);

            if (imageAddress == null)
            {
                imageAddress = "/Images/No picture.jpg";
            }

            Model model = new Model
            {
                Name = modelInputViewModel.Name,
                StartYear = modelInputViewModel.StartYear,
                EndYear = modelInputViewModel.EndYear,
                BrandId = brandId,
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

        public IQueryable<Model> GetAllModels(int id)
        {
            var allModels = this.models.All();

            return allModels;
        }

        public IQueryable<Model> GetAllModelsByBrandId(int id)
        {
            var allModels = this.models.All().Where(x => x.BrandId == id);

            return allModels;
        }
    }
}
