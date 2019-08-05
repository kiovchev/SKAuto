namespace SKAuto.Web.ViewModels.ViewModels.BrandViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class BrandCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageAddress { get; set; }
    }
}
