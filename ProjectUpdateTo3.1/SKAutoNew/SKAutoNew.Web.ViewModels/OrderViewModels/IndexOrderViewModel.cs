namespace SKAutoNew.Web.ViewModels.OrderViewModels
{
    using System;

    public class IndexOrderViewModel
    {
        public int Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public string RecipientName { get; set; }

        public string OrderStatusName { get; set; }

        public decimal OrderTotalPrice { get; set; }
    }
}
