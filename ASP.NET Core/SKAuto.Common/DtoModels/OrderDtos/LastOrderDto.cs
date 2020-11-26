namespace SKAuto.Common.DtoModels.OrderDtos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.CartDtos;

    public class LastOrderDto
    {
        public int Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public string RecipientName { get; set; }

        public string OrderStatus { get; set; }

        public ICollection<ItemForLastOrderDto> Items { get; set; }

        public decimal OrderPrice => this.Items.Select(p => p.CustomerPrice * p.OrderedQuantity).Sum();
    }
}
