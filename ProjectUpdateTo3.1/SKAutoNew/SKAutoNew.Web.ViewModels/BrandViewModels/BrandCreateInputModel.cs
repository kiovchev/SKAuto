namespace SKAutoNew.Web.ViewModels.BrandViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class BrandCreateInputModel
    {
        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }
    }
}
