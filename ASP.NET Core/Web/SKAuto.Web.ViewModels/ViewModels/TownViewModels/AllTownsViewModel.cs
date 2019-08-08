namespace SKAuto.Web.ViewModels.ViewModels.TownViewModels
{
    using System.Collections.Generic;

    public class AllTownsViewModel
    {
        public AllTownsViewModel()
        {
            this.TownsNames = new List<string>();
        }

        public ICollection<string> TownsNames { get; set; }
    }
}
