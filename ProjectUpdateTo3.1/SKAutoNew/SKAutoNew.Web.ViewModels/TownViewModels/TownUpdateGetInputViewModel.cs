namespace SKAutoNew.Web.ViewModels.TownViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class TownUpdateGetInputViewModel
    {
        [Required]
        public int TownId { get; set; }
    }
}
