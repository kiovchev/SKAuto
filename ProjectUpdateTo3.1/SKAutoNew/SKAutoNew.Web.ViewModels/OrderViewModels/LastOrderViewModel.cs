namespace SKAutoNew.Web.ViewModels.OrderViewModels
{
    using SKAutoNew.Web.ViewModels.CartViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class LastOrderViewModel
    {
        public int Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public string RecipientName { get; set; }

        public string OrderStatus { get; set; }

        public IList<ItemForLastOrder> Items { get; set; }

        public decimal OrderPrice => this.Items.Select(p => p.CustomerPrice * p.OrderedQuantity).Sum();
    }
}
