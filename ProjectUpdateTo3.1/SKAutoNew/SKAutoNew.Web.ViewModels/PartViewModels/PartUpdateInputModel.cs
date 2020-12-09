namespace SKAutoNew.Web.ViewModels.PartViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PartUpdateInputModel
    {
        [Required]
        public int PartId { get; set; }

        [Required]
        public string PartName { get; set; }

        [Required]
        public string BrandAndModelName { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string ManufactoryName { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
