namespace SKAuto.Web.ViewModels.ViewModels.ModelViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ModelUpdateInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int StartYear { get; set; }

        [Required]
        public int EndYear { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        public string ImageAddress { get; set; }
    }
}
