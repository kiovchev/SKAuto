namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using SKAuto.Common;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.UseFullCategoryViewModels;

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

        public bool CheckIfExists(string name)
        {
            bool checkUseFullCategory = this.useFullCategories.All().Any(x => x.Name.ToUpper() == name.ToUpper());

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

                await this.townUseFullCategories.AddAsync(townUseFullCategory);
            }

            await this.useFullCategories.AddAsync(useFullCategory);
            await this.useFullCategories.SaveChangesAsync();
        }

        public IQueryable<UseFullCategory> GetAllUseFullCategories()
        {
            var allUseFullCategories = this.useFullCategories.All();

            return allUseFullCategories;
        }

        public List<UseFullCategoryWithImageViewModel> GetAllUseFullCategoriesWithParams()
        {
            var allUseFullCategories = this.useFullCategories.All();
            List<UseFullCategoryWithImageViewModel> useFullCategoryWithImages = new List<UseFullCategoryWithImageViewModel>();

            foreach (var item in allUseFullCategories)
            {
                UseFullCategoryWithImageViewModel viewModel = new UseFullCategoryWithImageViewModel
                {
                    Name = item.Name,
                    ImageAdsress = item.ImageAddress,
                };

                useFullCategoryWithImages.Add(viewModel);
            }

            return useFullCategoryWithImages;
        }
    }
}
