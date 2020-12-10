namespace SKAutoNew.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SKAutoNew.Common;
    using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data.Contractcs;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UseFullCategoryService : IUseFullCategoryService
    {
        private readonly IRepository<UseFullCategory> useFullCategories;
        private readonly IRepository<Town> towns;
        private readonly IRepository<TownUseFullCategory> townUseFullCategories;

        public UseFullCategoryService(
            IRepository<UseFullCategory> useFullCategories,
            IRepository<Town> towns,
            IRepository<TownUseFullCategory> townUseFullCategories)
        {
            this.useFullCategories = useFullCategories;
            this.towns = towns;
            this.townUseFullCategories = townUseFullCategories;
        }

        public async Task<bool> CheckIfExistsAsync(string name)
        {
            bool checkUseFullCategory = await this.useFullCategories
                                                              .All()
                                                              .AnyAsync(x => x.Name.ToUpper() == name.ToUpper());

            return checkUseFullCategory;
        }

        public async Task CreateUseFullCategoryByNameAsync(string name, string imageAddress)
        {
            var towns = this.towns.All();

            if (imageAddress == null)
            {
                imageAddress = GlobalConstants.ImageAddress;
            }

            UseFullCategory useFullCategory = new UseFullCategory()
            {
                Name = name,
                ImageAddress = imageAddress,
            };

            foreach (var town in towns)
            {
                TownUseFullCategory townUseFullCategory = new TownUseFullCategory()
                {
                    UseFullCategory = useFullCategory,
                    Town = town,
                };

                await this.townUseFullCategories.InsertAsync(townUseFullCategory);
            }

            await this.useFullCategories.InsertAsync(useFullCategory);
            await this.useFullCategories.SaveAsync();
        }

        public async Task<IList<UseFullCategory>> GetAlluseFullCategoriesAsync()
        {
            var allCategories = await this.useFullCategories.AllAsNoTracking().ToListAsync();

            return allCategories;
        }

        public async Task<IList<UseFullCategoryWithImageViewDtoModel>> GetAllUseFullCategoriesWithParamsAsync()
        {
            var allUseFullCategories = await this.useFullCategories.All().ToListAsync();
            var useFullCategoryWithImages = allUseFullCategories
                                            .Select(x => new UseFullCategoryWithImageViewDtoModel
            {
                Name = x.Name,
                ImageAddress = x.ImageAddress
            }).ToList();

            return useFullCategoryWithImages;
        }
    }
}
