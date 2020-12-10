namespace SKAutoNew.Common.DtoModels.CompanyDtos
{
    using System.Collections.Generic;

    public class CompanyCreateViewDtoModel
    {
        public CompanyCreateViewDtoModel()
        {
            this.CategoryNames = new List<string>();
            this.TownNames = new List<string>();
        }

        public List<string> TownNames { get; set; }

        public List<string> CategoryNames { get; set; }
    }
}
