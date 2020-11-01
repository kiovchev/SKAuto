namespace SKAuto.Web.ViewModels.ViewModels.RecipientViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RecipientFindInputViewModel
    {
        [Required]
        public int PartId { get; set; }

        [Required]
        public string PartName { get; set; }

        [Required]
        public string BrandAndModelName { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string ManufactoryName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
