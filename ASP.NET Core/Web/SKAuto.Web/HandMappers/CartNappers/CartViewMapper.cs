namespace SKAuto.Web.HandMappers.CartMappers
{
    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Web.ViewModels.ViewModels.CartViewModels;

    public static class CartViewMapper
    {
        public static CartViewModel Map(CartDtoModel cartDto)
        {
            var model = new CartViewModel
            {
                ItemId = cartDto.ItemId,
                PartId = cartDto.PartId,
                BrandAndModel = cartDto.BrandAndModel,
                PartName = cartDto.PartName,
                CustomerPrice = cartDto.CustomerPrice,
                OrderedQuantity = cartDto.OrderedQuantity,
            };

            return model;
        }
    }
}
