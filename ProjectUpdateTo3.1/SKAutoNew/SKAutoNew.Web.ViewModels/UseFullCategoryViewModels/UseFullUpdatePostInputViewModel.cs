namespace SKAutoNew.Web.ViewModels.UseFullCategoryViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UseFullUpdatePostInputViewModel
    {
        [Required]
        public int UseFullCategoryId { get; set; }

        [Required]
        public string UseFullCategoryName { get; set; }

        public string ImageAddress { get; set; }
    }
}
