namespace SKAutoNew.Web.ViewModels.OrderViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class UpdateInputOrderViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string OrderStatusName { get; set; }
    }
}
