namespace SKAutoNew.Common.DtoModels.ModelDtos
{
    using System.Collections.Generic;

    public class ModelUpdateOutputDtoModel
    {
        public ModelUpdateOutputDtoModel()
        {
            this.AllBrandNames = new List<string>();
        }

        public int ModelId { get; set; }

        public string ModelName { get; set; }

        public int StartYear { get; set; }

        public int EndYear { get; set; }

        public string ImageAddress { get; set; }

        public string BrandName { get; set; }

        public IList<string> AllBrandNames { get; set; }
    }
}
