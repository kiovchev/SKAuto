namespace SKAuto.Web.ViewModels.ViewModels.CategoryViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryWithImageViewModel
    {
        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }
    }
}
