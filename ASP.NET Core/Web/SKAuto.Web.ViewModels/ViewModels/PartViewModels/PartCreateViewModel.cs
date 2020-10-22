namespace SKAuto.Web.ViewModels.ViewModels.PartViewModels
{
    using System.Collections.Generic;

    public class PartCreateViewModel
    {
        public PartCreateViewModel()
        {
            this.BrandWithModels = new List<string>();
            this.Categories = new List<string>();
        }

        public IList<string> BrandWithModels { get; set; }

        public IList<string> Categories { get; set; }
    }
}
