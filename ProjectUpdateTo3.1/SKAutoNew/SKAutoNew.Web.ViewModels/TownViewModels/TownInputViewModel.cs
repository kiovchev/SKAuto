namespace SKAutoNew.Web.ViewModels.TownViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class TownInputViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
