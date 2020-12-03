namespace SKAuto.Web.ViewModels.ViewModels.OrderViewModel
{
    using System.ComponentModel.DataAnnotations;

    public class LastOrderParamViewModel
    {
        [Required]
        public int OrderId { get; set; }
    }
}
