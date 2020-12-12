using SKAutoNew.Common.DtoModels.OrderDtos;
using SKAutoNew.Web.ViewModels.OrderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKAutoNew.HandMappers.OrderMappers
{
    public static class OrderIndexMapper
    {
        public static IList<IndexOrderViewModel> Map(IList<IndexOrderDto> ordersAllDtos)
        {
            var ordersAllViewModels = ordersAllDtos.Select(x => new IndexOrderViewModel
            {
                Id = x.Id,
                IssuedOn = x.IssuedOn,
                OrderStatusName = x.OrderStatusName,
                RecipientName = x.RecipientName,
                OrderTotalPrice = x.OrderTotalPrice
            })
                .OrderBy(x => x.RecipientName)
                .ThenBy(x => x.IssuedOn)
                .ToList();

            return ordersAllViewModels;
        }
    }
}
