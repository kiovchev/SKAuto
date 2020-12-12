namespace SKAutoNew.Web.ViewModels.OrderViewModels
{
    using System;
    using System.Collections.Generic;

    public class UpdateOutPutOrderViewModel
    {
        public UpdateOutPutOrderViewModel()
        {
            this.AllOrderStatusesNames = new List<string>();
        }

        public int Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public string RecipientName { get; set; }

        public string OrderStatusName { get; set; }

        public decimal OrderTotalPrice { get; set; }

        public IList<string> AllOrderStatusesNames { get; set; }
    }
}
