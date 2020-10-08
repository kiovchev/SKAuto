namespace SKAuto.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Common;
    using SKAuto.Common.DtoModels.CategoryDtos;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.CategoryViewModels;

    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categories;
        private readonly IRepository<Model> models;
        private readonly IRepository<ModelCategories> modelCategories;

        public CategoryService(
                               IRepository<Category> categories,
                               IRepository<Model> models,
                               IRepository<ModelCategories> modelCategories)
        {
            this.categories = categories;
            this.models = models;
            this.modelCategories = modelCategories;
        }

        public async Task CreateCategory(string name, string imageAddress)
        {
            if (imageAddress == null)
            {
                imageAddress = GlobalConstants.ImageAddress;
            }

            Category category = new Category
            {
                Name = name,
                ImageAddress = imageAddress,
            };

            List<Model> allModels = this.models.All().ToList();

            for (int i = 0; i < allModels.Count(); i++)
            {
                var modelCategory = new ModelCategories
                {
                    Category = category,
                    Model = allModels[i],
                };

                await this.modelCategories.AddAsync(modelCategory);
            }

            await this.categories.AddAsync(category);
            await this.categories.SaveChangesAsync();
        }

        public async Task<IList<CategoryIndexDtoModel>> GetAllCategories()
        {
            var categoriesAll = this.categories.All();
            List<CategoryIndexDtoModel> categories = await categoriesAll.Select(x => new CategoryIndexDtoModel
            {
                CategoryId = x.Id,
                CategoryName = x.Name,
                ImageAddress = x.ImageAddress,
            }).ToListAsync();

            return categories;
        }

        public async Task<IList<CategoruAllDtoModel>> GetAllCategoriesForViewModel()
        {
            var allCategories = await this.categories.All().ToListAsync();
            IList<CategoruAllDtoModel> categoryWithImages = allCategories.Select(x => new CategoruAllDtoModel
                {
                    CategoryName = x.Name,
                    ImageAddress = x.ImageAddress,
                }).ToList();

            return categoryWithImages;
        }

        public async Task<IList<CategoryWithModelViewModel>> GetCategoriesByNameAndYears(string modelName)
        {
            string[] nameAsArr = modelName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            string name = string.Join(" ", nameAsArr.Skip(1).Take(nameAsArr.Count() - 2));

            string[] years = nameAsArr[nameAsArr.Length - 1].Split(new char[] { '-' }).ToArray();

            int startYear = int.Parse(years[0]);
            int endYear = int.Parse(years[1]);

            int modelId = await this.models.All()
                                     .Where(x => x.Name == name && x.StartYear == startYear && x.EndYear == endYear)
                                     .Select(y => y.Id)
                                     .FirstOrDefaultAsync();

            var allCategories = this.categories.All()
                                               .Where(x => x.ModelCategories
                                               .Any(y => y.Model.Name == name &&
                                               y.Model.StartYear == startYear &&
                                               y.Model.EndYear == endYear));

            IList<CategoryWithModelViewModel> neededCategory = new List<CategoryWithModelViewModel>();

            foreach (var item in allCategories)
            {
                string catName = item.Name;
                string catImage = item.ImageAddress;

                CategoryWithModelViewModel category = new CategoryWithModelViewModel
                {
                    Name = catName,
                    ImageAdsress = catImage,
                    ModelName = modelName,
                };

                neededCategory.Add(category);
            }

            return neededCategory;
        }

        public async Task<bool> IfCategoryExists(string name)
        {
            var allCategories = await this.categories.All().ToListAsync();
            bool existCategogy = allCategories.Any(x => x.Name.ToUpper() == name.ToUpper());

            return existCategogy;
        }
    }
}
