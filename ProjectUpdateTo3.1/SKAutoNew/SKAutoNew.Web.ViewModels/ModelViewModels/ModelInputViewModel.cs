namespace SKAutoNew.Web.ViewModels.ModelViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ModelInputViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int StartYear { get; set; }

        [Required]
        public int EndYear { get; set; }

        [Required]
        public string BrandName { get; set; }

        public string ImageAddress { get; set; }
    }
}
