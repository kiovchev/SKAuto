namespace SKAuto.Common.DtoModels.CartDtos
{
    public class ItemForLastOrderDto
    {
        public int ItemId { get; set; }

        public int PartId { get; set; }

        public string PartName { get; set; }

        public string BrandAndModel { get; set; }

        public decimal CustomerPrice { get; set; }

        public int OrderedQuantity { get; set; }
    }
}
