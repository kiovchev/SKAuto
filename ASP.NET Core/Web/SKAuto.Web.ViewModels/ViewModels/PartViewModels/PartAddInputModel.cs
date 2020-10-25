namespace SKAuto.Web.ViewModels.ViewModels.PartViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class PartAddInputModel
    {
        [Required]
        public int PartId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
