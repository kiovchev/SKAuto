﻿namespace SKAutoNew.Web.ViewModels.PartViewModels
{
    using System.Collections.Generic;

    public class PartUpdateOutputModel
    {
        public PartUpdateOutputModel()
        {
            this.AllBrandsAndModelsNames = new List<string>();
            this.AllCategoriesNames = new List<string>();
        }

        public int PartId { get; set; }

        public string PartName { get; set; }

        public string BrandAndModelName { get; set; }

        public string CategoryName { get; set; }

        public string ManufactoryName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public IList<string> AllBrandsAndModelsNames { get; set; }

        public IList<string> AllCategoriesNames { get; set; }
    }
}
