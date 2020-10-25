namespace SKAuto.Common.DtoModels.PartDtos
{
    public class PartCreateInputDtoModel
    {
        public string PartName { get; set; }

        public string ModelName { get; set; }

        public string CategoryName { get; set; }

        public decimal Price { get; set; }

        public string ManufactoryName { get; set; }

        public int Quantity { get; set; }
    }
}
