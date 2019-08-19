namespace SKAuto.Web.ViewModels.ViewModels.PartViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PartCreateInputModel
    {
        [Required]
        public string PartName { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ManufactoryName { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
