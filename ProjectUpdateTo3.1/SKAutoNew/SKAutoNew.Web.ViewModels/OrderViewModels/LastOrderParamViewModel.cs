namespace SKAutoNew.Web.ViewModels.OrderViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class LastOrderParamViewModel
    {
        [Required]
        public int OrderId { get; set; }
    }
}
