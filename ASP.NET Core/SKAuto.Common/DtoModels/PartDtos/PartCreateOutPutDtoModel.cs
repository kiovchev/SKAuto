namespace SKAuto.Common.DtoModels.PartDtos
{
    using System.Collections.Generic;

    public class PartCreateOutPutDtoModel
    {
        public PartCreateOutPutDtoModel()
        {
            this.BrandWithModels = new List<string>();
            this.Categories = new List<string>();
        }

        public IList<string> BrandWithModels { get; set; }

        public IList<string> Categories { get; set; }
    }
}
