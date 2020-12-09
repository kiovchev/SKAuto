namespace SKAutoNew.Common.DtoModels.PartDtos
{
    public class PartGetAllPartsForIndexDtoModel
    {
        public int PartId { get; set; }

        public string PartName { get; set; }

        public string BrandAndModelName { get; set; }

        public string CategoryName { get; set; }

        public string ManufactoryName { get; set; }

        public int Quantity { get; set; }

        public decimal IncomePrice { get; set; }
    }
}
