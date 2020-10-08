namespace SKAuto.Web.ViewModels.ViewModels.BrandViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class BrandUpdateInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }
    }
}
