namespace SKAuto.Web.ViewModels.ViewModels.PartViewModels
{
    using System.Collections.Generic;

    using SKAuto.Data.Models;

    public class PartCreateViewModel
    {
        public PartCreateViewModel()
        {
            this.BrandWithModels = new List<string>();
            this.Categories = new List<Category>();
        }

        public List<string> BrandWithModels { get; set; }

        public List<Category> Categories { get; set; }
    }
}
