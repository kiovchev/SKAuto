namespace SKAutoNew.Web.ViewModels.CategoryViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CategoryUpdateInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ImageAddress { get; set; }
    }
}
