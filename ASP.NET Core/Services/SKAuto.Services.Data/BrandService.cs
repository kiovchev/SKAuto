﻿namespace SKAuto.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SKAuto.Data.Common.Repositories;
    using SKAuto.Data.Models;
    using SKAuto.Web.ViewModels.ViewModels.BrandViewModels;

    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> brands;

        public BrandService(IRepository<Brand> brands) // Must to use IRespository
        {
            this.brands = brands;
        }

        public async Task CreateBrand(BrandCreateInputModel brandCreateInputModel)
        {
            if (brandCreateInputModel.ImageAddress == null)
            {
                brandCreateInputModel.ImageAddress = "/Images/No picture.jpg";
            }

            Brand brand = new Brand
            {
                Name = brandCreateInputModel.Name,
                ImageAddress = brandCreateInputModel.ImageAddress,
            };

            await this.brands.AddAsync(brand);
            await this.brands.SaveChangesAsync();
        }

        public IQueryable<Brand> GetAllBrands()
        {
            var allBrands = this.brands.All().Include(x => x.Models);

            return allBrands;
        }

        public async Task<int> GetBrandIdByNameAsync(string name)
        {
            var currentBrand = await this.brands.All().FirstOrDefaultAsync(x => x.Name == name);
            int brandId = currentBrand.Id;

            return brandId;
        }

        public async Task<IList<string>> GetBrandNamesAsync()
        {
            List<string> brandNames = await this.brands.All().Select(b => b.Name).ToListAsync();

            return brandNames;
        }

        public async Task<bool> IfBrandExistsAsync(string name)
        {
            bool checkBrand = await this.brands.All().AnyAsync(x => x.Name == name);

            return checkBrand;
        }
    }
}
