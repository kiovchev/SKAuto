namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.CategoryDtos;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Mappers.CategoryServiceMappers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task CreateCategoryAsync(CategoryCreateDtoModel categoryCreateDto)
        {
            if (categoryCreateDto.ImageAddress == null)
            {
                categoryCreateDto.ImageAddress = GlobalConstants.ImageAddress;
            }

            var category = CategoryServiceCreateMapper.Map(categoryCreateDto);
            var allModels = this.models.All().ToList();

            for (int i = 0; i < allModels.Count(); i++)
            {
                var modelCategory = new ModelCategories
                {
                    Category = category,
                    Model = allModels[i],
                };

                await this.modelCategories.InsertAsync(modelCategory);
            }

            await this.categories.InsertAsync(category);
            await this.categories.SaveAsync();
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await this.categories.All()
                .Include(x => x.ModelCategories)
                .Include(x => x.Parts)
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            if (category.Parts.Count > 0)
            {
                return false;
            }

            if (category.ModelCategories.Count > 0)
            {
                foreach (var item in category.ModelCategories)
                {
                    this.modelCategories.Delete(item);
                }

                await this.modelCategories.SaveAsync();
            }

            this.categories.Delete(category);
            await this.categories.SaveAsync();

            return true;
        }

        public async Task<IList<CategoryIndexDtoModel>> GetAllCategoriesAsync()
        {
            var categoriesAll = await this.categories.All().ToListAsync();
            var categories = GetAllCategoriesMapper.Map(categoriesAll);

            return categories;
        }

        public async Task<IList<Category>> GetAllCategoriesForModelAsync()
        {
            var categories = await this.categories.All().ToListAsync();

            return categories;
        }

        public async Task<IList<CategoryAllDtoModel>> GetAllCategoriesForViewModelAsync()
        {
            var allCategories = await this.categories.All().ToListAsync();
            var categoriesWithImages = GetAllCategoriesForViewModelMapper.Map(allCategories);

            return categoriesWithImages;
        }

        public async Task<IList<string>> GetAllCategoryNamesAsync()
        {
            var categoriesNames = await this.categories.All().Select(x => x.Name).ToListAsync();

            return categoriesNames;
        }

        public async Task<IList<GetCategoriesByNameAndYearsDtoModel>> GetCategoriesByNameAndYears(string modelName)
        {
            string[] nameAsArr = modelName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            string name = string.Join(" ", nameAsArr.Skip(1).Take(nameAsArr.Count() - 2));
            string[] years = nameAsArr[nameAsArr.Length - 1].Split(new char[] { '-' }).ToArray();
            int startYear = int.Parse(years[0]);
            int endYear = int.Parse(years[1]);

            var allCategories = await this.categories.All()
                                               .Where(x => x.ModelCategories
                                               .Any(y => y.Model.Name == name &&
                                               y.Model.StartYear == startYear &&
                                               y.Model.EndYear == endYear)).ToListAsync();

            var neededCategories = GetCategoriesByNameAndYearsMapper.Map(allCategories, modelName);

            return neededCategories;
        }

        public async Task<CategoryUpdateOutputDtoModel> GetCategoryByIdAsync(int categoryId)
        {
            var category = await this.categories.All().Where(x => x.Id == categoryId).FirstOrDefaultAsync();
            var neededCategory = GetCategoryByIdMapper.Map(category);

            return neededCategory;
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            var category = await this.categories.All().FirstOrDefaultAsync(x => x.Name == categoryName);

            return category;
        }

        public async Task<bool> IfCategoryExists(string name)
        {
            var allCategories = await this.categories.All().ToListAsync();
            bool existCategogy = allCategories.Any(x => x.Name.ToUpper() == name.ToUpper());

            return existCategogy;
        }

        public async Task<bool> IsSameCategoryAsync(string name, string imageAddress)
        {
            var allCategories = await this.categories.AllAsNoTracking().ToListAsync();
            bool existCategogy = allCategories.Any(x => x.Name.ToUpper() == name.ToUpper()
            && x.ImageAddress == imageAddress);

            return existCategogy;
        }

        public async Task UpdateCategoryAsync(CategoryUpdateInputDtoModel categoryDto)
        {
            var catetory = CategoryUpdateMapper.Map(categoryDto);
            this.categories.Update(catetory);
            await this.categories.SaveAsync();
        }
    }
}
