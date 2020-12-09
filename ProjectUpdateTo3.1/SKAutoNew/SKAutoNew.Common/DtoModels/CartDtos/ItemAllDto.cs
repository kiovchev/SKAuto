namespace SKAutoNew.Common.DtoModels.CartDtos
{
    using System;

    public class ItemAllDto
    {
        public int ItemId { get; set; }

        public string PartName { get; set; }

        public string BrandAndModelName { get; set; }

        public string OrderStatus { get; set; }

        public int OrderedQuantity { get; set; }

        public DateTime OrderedAt { get; set; }

        public decimal CustomerPrice { get; set; }

        public string RecipientFullName { get; set; }
    }
}
