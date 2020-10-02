namespace SKAuto.Web.ViewModels.ViewModels.ModelViewModels
{
    using System.Collections.Generic;

    public class ModelUpdateOutputModel
    {
        public ModelUpdateOutputModel()
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
