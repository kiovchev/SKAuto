namespace SKAutoNew.Web.ViewModels.BrandViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class BrandUpdateInputModel
    {
        [Required]
        public int BrandId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageAddress { get; set; }
    }
}
