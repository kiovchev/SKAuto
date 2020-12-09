namespace SKAutoNew.Common.DtoModels.CartDtos
{
    using System.Collections.Generic;

    public class CartOrderCreateDtoModel
    {
        public CartOrderCreateDtoModel()
        {
            this.ItemsIds = new List<int>();
        }

        public int RecipientId { get; set; }

        public List<int> ItemsIds { get; set; }
    }
}
