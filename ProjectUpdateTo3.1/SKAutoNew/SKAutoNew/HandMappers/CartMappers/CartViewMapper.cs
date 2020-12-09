namespace SKAutoNew.HandMappers.CartMappers
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Web.ViewModels.CartViewModels;

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
