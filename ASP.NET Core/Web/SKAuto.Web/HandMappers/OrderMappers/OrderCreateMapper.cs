namespace SKAuto.Web.HandMappers.OrderMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.CartDtos;
    using SKAuto.Web.ViewModels.ViewModels.CartViewModels;

    public static class OrderCreateMapper
    {
        public static CartOrderCreateDtoModel Map(int recipientId, List<CartViewModel> cartViewModels)
        {
            var itemsIds = cartViewModels.Select(x => x.ItemId).ToList();

            var model = new CartOrderCreateDtoModel
            {
                RecipientId = recipientId,
                ItemsIds = itemsIds,
            };

            return model;
        }
    }
}
