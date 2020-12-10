namespace SKAutoNew.Web.ViewModels.UseFullCategoryViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UseFullCategoryWithImageViewModel
    {
        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }
    }
}
