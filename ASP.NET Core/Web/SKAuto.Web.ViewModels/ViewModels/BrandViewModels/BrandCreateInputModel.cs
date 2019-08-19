namespace SKAuto.Web.ViewModels.ViewModels.BrandViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class BrandCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }
    }
}
