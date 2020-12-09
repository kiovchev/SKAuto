namespace SKAutoNew.HandMappers.OrderMappers
{
    using SKAutoNew.Common.DtoModels.CartDtos;
    using SKAutoNew.Web.ViewModels.CartViewModels;
    using System.Collections.Generic;
    using System.Linq;

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
