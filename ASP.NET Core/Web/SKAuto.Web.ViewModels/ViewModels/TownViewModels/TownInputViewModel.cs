namespace SKAuto.Web.ViewModels.ViewModels.TownViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class TownInputViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
