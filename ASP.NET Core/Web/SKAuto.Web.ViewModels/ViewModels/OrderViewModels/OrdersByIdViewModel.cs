namespace SKAuto.Web.ViewModels.ViewModels.OrderViewModels
{
    using System;
    using System.Collections.Generic;

    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public class OrdersByIdViewModel
    {
        public OrdersByIdViewModel()
        {
            this.OrderParts = new HashSet<PartCreateInputModel>();
        }

        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        public string StatusName { get; set; }

        public ICollection<PartCreateInputModel> OrderParts { get; set; }
    }
}
