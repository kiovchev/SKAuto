namespace SKAuto.Common.DtoModels.PartDtos
{
    public class PartUpdateInputDtoModel
    {
        public int PartId { get; set; }

        public string PartName { get; set; }

        public string BrandAndModelName { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public string ManufactoryName { get; set; }

        public int Quantity { get; set; }
    }
}
