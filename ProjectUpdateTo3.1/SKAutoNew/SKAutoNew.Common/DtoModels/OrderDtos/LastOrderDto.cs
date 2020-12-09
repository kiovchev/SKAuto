namespace SKAutoNew.Common.DtoModels.OrderDtos
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
