namespace SKAutoNew.Common.DtoModels.OrderDtos
{
    using System;
    using System.Collections.Generic;

    public class UpdateOutPutOrderDtoModel
    {
        public UpdateOutPutOrderDtoModel()
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
