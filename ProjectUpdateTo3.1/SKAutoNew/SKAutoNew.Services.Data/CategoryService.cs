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

    /// <summary>
    /// business logic for Category
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> categories;
        private readonly IRepository<Model> models;
        private readonly IRepository<ModelCategories> modelCategories;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="categories"></param>
        /// <param name="models"></param>
        /// <param name="modelCategories"></param>
        public CategoryService(
                               IRepository<Category> categories,
                               IRepository<Model> models,
                               IRepository<ModelCategories> modelCategories)
        {
            this.categories = categories;
            this.models = models;
            this.modelCategories = modelCategories;
        }

        /// <summary>
        /// method - create category and modelcategory and add them to database
        /// </summary>
        /// <param name="categoryCreateDto"></param>
        /// <returns></returns>
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

        /// <summary>
        /// method - delete category and it modelcategories if there are no any parts for that category 
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await this.categories.All()
                .Include(x => x.ModelCategories)
                .Include(x => x.Parts)
                .FirstOrDefaultAsync(x => x.Id == categoryId);

            if (category == null)
            {
                return false;
            }

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

        /// <summary>
        /// method - get all categories from database
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CategoryIndexDtoModel>> GetAllCategoriesAsync()
        {
            var categoriesAll = await this.categories.All().ToListAsync();
            var categories = GetAllCategoriesMapper.Map(categoriesAll);

            return categories;
        }

        /// <summary>
        /// method - get all categories from database and use them for model business logic
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Category>> GetAllCategoriesForModelAsync()
        {
            var categories = await this.categories.All().ToListAsync();

            return categories;
        }

        /// <summary>
        /// method - get all categories from datbase and use it in category all 
        /// </summary>
        /// <returns></returns>
        public async Task<IList<CategoryAllDtoModel>> GetAllCategoriesForViewModelAsync()
        {
            var allCategories = await this.categories.All().ToListAsync();
            var categoriesWithImages = GetAllCategoriesForViewModelMapper.Map(allCategories);

            return categoriesWithImages;
        }

        /// <summary>
        /// method - get all categories from database and return model which contains their names
        /// </summary>
        /// <returns></returns>
        public async Task<IList<string>> GetAllCategoryNamesAsync()
        {
            var categoriesNames = await this.categories.All().Select(x => x.Name).ToListAsync();

            return categoriesNames;
        }

        /// <summary>
        /// method - get all categories from database by model name , start and end years
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// method - get category using id for param to find it
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<CategoryUpdateOutputDtoModel> GetCategoryByIdAsync(int categoryId)
        {
            var category = await this.categories.All().Where(x => x.Id == categoryId).FirstOrDefaultAsync();
            var neededCategory = GetCategoryByIdMapper.Map(category);

            return neededCategory;
        }

        /// <summary>
        /// method - get category using name for param to find it
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            var category = await this.categories.All().FirstOrDefaultAsync(x => x.Name == categoryName);

            return category;
        }

        /// <summary>
        /// method - check if there is a category with same name in database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> IfCategoryExists(string name)
        {
            var allCategories = await this.categories.All().ToListAsync();
            bool existCategogy = allCategories.Any(x => x.Name.ToUpper() == name.ToUpper());

            return existCategogy;
        }

        /// <summary>
        /// method - check if there is a category with same name and image address in database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="imageAddress"></param>
        /// <returns></returns>
        public async Task<bool> IsSameCategoryAsync(string name, string imageAddress)
        {
            var allCategories = await this.categories.AllAsNoTracking().ToListAsync();
            bool existCategogy = allCategories.Any(x => x.Name.ToUpper() == name.ToUpper()
            && x.ImageAddress == imageAddress);

            return existCategogy;
        }

        /// <summary>
        /// update category in databse
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        public async Task UpdateCategoryAsync(CategoryUpdateInputDtoModel categoryDto)
        {
            var catetory = CategoryUpdateMapper.Map(categoryDto);
            this.categories.Update(catetory);
            await this.categories.SaveAsync();
        }
    }
}
