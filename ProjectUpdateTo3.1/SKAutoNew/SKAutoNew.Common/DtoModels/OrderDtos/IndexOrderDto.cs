using System;
using System.Collections.Generic;
using System.Text;

namespace SKAutoNew.Common.DtoModels.OrderDtos
{
    public class IndexOrderDto
    {
        public int Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public string RecipientName { get; set; }

        public string OrderStatusName { get; set; }

        public decimal OrderTotalPrice { get; set; }
    }
}
