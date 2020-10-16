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
    using SKAuto.Services.Mapping.CategoryServiceMapper;

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

                await this.modelCategories.AddAsync(modelCategory);
            }

            await this.categories.AddAsync(category);
            await this.categories.SaveChangesAsync();
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

                await this.modelCategories.SaveChangesAsync();
            }

            this.categories.Delete(category);
            await this.categories.SaveChangesAsync();

            return true;
        }

        public async Task<IList<CategoryIndexDtoModel>> GetAllCategoriesAsync()
        {
            var categoriesAll = await this.categories.All().ToListAsync();
            var categories = GetAllCategoriesMapper.Map(categoriesAll);

            return categories;
        }

        public async Task<IList<CategoryAllDtoModel>> GetAllCategoriesForViewModelAsync()
        {
            var allCategories = await this.categories.All().ToListAsync();
            var categoriesWithImages = GetAllCategoriesForViewModelMapper.Map(allCategories);

            return categoriesWithImages;
        }

        public async Task<IList<GetCategoriesByNameAndYearsDtoModel>> GetCategoriesByNameAndYears(string modelName)
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

            var neededCategories = GetCategoriesByNameAndYearsMapper.Map(allCategories, modelName);

            return neededCategories;
        }

        public async Task<CategoryUpdateOutputDtoModel> GetCategoryByIdAsync(int categoryId)
        {
            var category = await this.categories.All().Where(x => x.Id == categoryId).FirstOrDefaultAsync();
            var neededCategory = GetCategoryByIdMapper.Map(category);

            return neededCategory;
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
            await this.categories.SaveChangesAsync();
        }
    }
}
