using System.ComponentModel.DataAnnotations;

namespace SKAuto.Web.ViewModels.ViewModels.BrandViewModels
{
    public class BrandUpdateInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }
    }
}
