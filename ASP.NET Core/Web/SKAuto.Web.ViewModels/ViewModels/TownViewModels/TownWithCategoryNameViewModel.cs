namespace SKAuto.Web.ViewModels.ViewModels.TownViewModels
{
    using System.Collections.Generic;

    public class TownWithCategoryNameViewModel
    {
        public TownWithCategoryNameViewModel()
        {
            this.TownNames = new List<string>();
        }

        public string CategoryName { get; set; }

        public IList<string> TownNames { get; set; }
    }
}
