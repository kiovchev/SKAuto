namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Common;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public class PartService : IPartService
    {
        private readonly IRepository<Part> parts;
        private readonly IRepository<Brand> brands;
        private readonly IRepository<Model> models;
        private readonly IRepository<Category> categories;
        private readonly IRepository<Manufactory> manufactories;

        public PartService(
            IRepository<Part> parts,
            IRepository<Brand> brands,
            IRepository<Model> models,
            IRepository<Category> categories,
            IRepository<Manufactory> manufactories)
        {
            this.parts = parts;
            this.brands = brands;
            this.models = models;
            this.categories = categories;
            this.manufactories = manufactories;
        }

        public async Task CreatePartAsync(PartCreateInputModel partModel)
        {
            List<string> allCarParams = this.TakeParmsFromModelName(partModel.ModelName);

            Brand brand = await this.brands.All().FirstOrDefaultAsync(x => x.Name == allCarParams[0]);
            Model model = await this.models.All().FirstOrDefaultAsync(x => x.Name == allCarParams[1]
                                                                      && x.StartYear == int.Parse(allCarParams[2])
                                                                      && x.EndYear == int.Parse(allCarParams[3]));
            Category category = await this.categories.All().FirstOrDefaultAsync(x => x.Name == partModel.CategoryName);

            if (partModel.ManufactoryName == null)
            {
                Part part = new Part
                {
                    Name = partModel.PartName,
                    Brand = brand,
                    Model = model,
                    Category = category,
                    InComePrice = partModel.Price,
                    Quantity = partModel.Quantity,
                };

                await this.parts.AddAsync(part);
                await this.parts.SaveChangesAsync();
            }
            else
            {
                Manufactory manufactory = await this.manufactories.All().FirstOrDefaultAsync(x => x.Name == partModel.ManufactoryName);

                if (manufactory == null)
                {
                    manufactory = new Manufactory
                    {
                        Name = partModel.ManufactoryName,
                    };

                    await this.manufactories.AddAsync(manufactory);
                }

                Part part = new Part
                {
                    Name = partModel.PartName,
                    Brand = brand,
                    Model = model,
                    Category = category,
                    InComePrice = partModel.Price,
                    Manufactory = manufactory,
                    Quantity = partModel.Quantity,
                };

                await this.parts.AddAsync(part);
                await this.parts.SaveChangesAsync();
            }
        }

        public PartCreateViewModel GetPartCreateParams()
        {
            List<Brand> brands = this.brands.All().Include(x => x.Models).ToList();
            List<string> categories = this.categories.All().Select(x => x.Name).ToList();

            List<string> brandsWithModels = new List<string>();

            foreach (var brand in brands)
            {
                foreach (var model in brand.Models)
                {
                    string neededInfo = brand.Name + " " + model.Name + " " + model.StartYear + "-" + model.EndYear;

                    brandsWithModels.Add(neededInfo);
                }
            }

            PartCreateViewModel partCreate = new PartCreateViewModel
            {
                BrandWithModels = brandsWithModels,
                Categories = categories,
            };

            return partCreate;
        }

        public async Task<List<PartByCategoryAndModelViewModel>> GetPartsByModelAndCategoryAsync(string modelName, string categoryName)
        {
            List<string> allCarParams = this.TakeParmsFromModelName(modelName);
            string currentBrandName = allCarParams[0];
            string currentModelName = allCarParams[1];
            int currentStartYear = int.Parse(allCarParams[2]);
            int currentEndYear = int.Parse(allCarParams[3]);

            Brand brand = await this.brands.All().FirstOrDefaultAsync(x => x.Name == currentBrandName);
            Model model = await this.models.All().FirstOrDefaultAsync(x => x.Name == currentModelName
                                                                      && x.StartYear == currentStartYear
                                                                      && x.EndYear == currentEndYear);
            Category category = await this.categories.All().FirstOrDefaultAsync(x => x.Name == categoryName);

            List<Part> partsByParams = await this.parts.All().Where(x => x.BrandId == brand.Id &&
                                                                   x.ModelId == model.Id &&
                                                                   x.CategoryId == category.Id).ToListAsync();

            List<PartByCategoryAndModelViewModel> partsByCategoryAndModel = new List<PartByCategoryAndModelViewModel>();

            foreach (var part in partsByParams)
            {
                Manufactory manufactory = await this.manufactories.All().FirstOrDefaultAsync(x => x.Id == part.ManufactoryId);
                string manufactoryName = GlobalConstants.ManufactoryName;

                if (manufactory != null)
                {
                    manufactoryName = manufactory.Name;
                }

                PartByCategoryAndModelViewModel currentPart = new PartByCategoryAndModelViewModel
                {
                    PartName = part.Name,
                    BrandAndModelName = modelName,
                    CategoryName = categoryName,
                    ManufactoryName = manufactoryName,
                    Quantity = part.Quantity,
                    SellPrice = part.CustomerPrice.ToString(format: GlobalConstants.PartPriceFormat),
                };

                partsByCategoryAndModel.Add(currentPart);
            }

            return partsByCategoryAndModel;
        }

        private List<string> TakeParmsFromModelName(string brandWithModelName)
        {
            string[] carParams;
            string carModel;
            string startYear, endYear;
            carParams = brandWithModelName.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries).ToArray();
            string brandName = carParams[0];
            carModel = string.Join(" ", carParams.Skip(1).Take(carParams.Length - 2).ToArray());
            string[] carYears = carParams[carParams.Length - 1].Split(new char[] { '-' }, System.StringSplitOptions.RemoveEmptyEntries).ToArray();
            startYear = carYears[0];
            endYear = carYears[1];

            List<string> carParamsArr = new List<string> { brandName, carModel, startYear, endYear };

            return carParamsArr;
        }
    }
}
