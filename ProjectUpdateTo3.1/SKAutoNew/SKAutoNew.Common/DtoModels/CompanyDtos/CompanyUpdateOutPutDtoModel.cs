namespace SKAutoNew.Common.DtoModels.CompanyDtos
{
    using System.Collections.Generic;

    public class CompanyUpdateOutPutDtoModel
    {
        public CompanyUpdateOutPutDtoModel()
        {
            this.Towns = new List<string>();
            this.Categories = new List<string>();
        }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string TownName { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string CategoryName { get; set; }

        public IList<string> Towns { get; set; }

        public IList<string> Categories { get; set; }
    }
}
