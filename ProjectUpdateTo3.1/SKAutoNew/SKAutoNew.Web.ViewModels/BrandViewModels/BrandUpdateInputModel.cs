namespace SKAutoNew.Web.ViewModels.BrandViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class BrandUpdateInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageAddress { get; set; }
    }
}
