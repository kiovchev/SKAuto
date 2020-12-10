namespace SKAutoNew.Web.ViewModels.TownViewModels
{
    using System.Collections.Generic;

    public class AllTownsViewModel
    {
        public AllTownsViewModel()
        {
            this.TownsNames = new HashSet<string>();
        }

        public ICollection<string> TownsNames { get; set; }
    }
}
