namespace SKAutoNew.Services.Mappers.OrderServiceMappers
{
    using SKAutoNew.Common.DtoModels.OrderDtos;
    using SKAutoNew.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class OrderIndexServiceMapper
    {
        public static IList<IndexOrderDto> Map(List<Order> ordersAll)
        {
            var ordersAllDtos = ordersAll.Select(x => new IndexOrderDto
            {
                Id = x.Id,
                IssuedOn = x.IssuedOn,
                OrderStatusName = x.OrderStatus.Name,
                RecipientName = $"{x.Recipient.FirstName} {x.Recipient.LastName}",
                OrderTotalPrice = Math.Round(x.Items.Select(y => y.Part.CustomerPrice * y.OrderedQuantity).Sum(), 2)
            })
                .OrderBy(x => x.RecipientName)
                .ThenBy(x => x.IssuedOn)
                .ToList();

            return ordersAllDtos;
        }
    }
}
