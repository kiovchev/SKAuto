namespace SKAuto.Web.ViewModels.ViewModels.CompanyViewModels
{
    using System.Collections.Generic;

    public class CompanyCreateViewModel
    {
        public CompanyCreateViewModel()
        {
            this.CategoryNames = new List<string>();
            this.TownNames = new List<string>();
        }

        public List<string> TownNames { get; set; }

        public List<string> CategoryNames { get; set; }
    }
}
