namespace SKAutoNew.Web.ViewModels.TownViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class TownDeleteViewModel
    {
        [Required]
        public int TownId { get; set; }
    }
}
