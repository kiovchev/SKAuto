namespace SKAutoNew.HandMappers.OrderMappers
{
    using SKAutoNew.Common.DtoModels.OrderDtos;
    using SKAutoNew.Web.ViewModels.OrderViewModels;

    public static class OrderUpdateOutPutHandMapper
    {
        public static UpdateOutPutOrderViewModel Map(UpdateOutPutOrderDtoModel dtoModel)
        {
            var model = new UpdateOutPutOrderViewModel
            {
                Id = dtoModel.Id,
                IssuedOn = dtoModel.IssuedOn,
                RecipientName = dtoModel.RecipientName,
                OrderStatusName = dtoModel.OrderStatusName,
                OrderTotalPrice = dtoModel.OrderTotalPrice,
                AllOrderStatusesNames = dtoModel.AllOrderStatusesNames
            };

            return model;
        }
    }
}
