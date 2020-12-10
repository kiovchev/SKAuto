namespace SKAutoNew.Common.DtoModels.TownDtos
{
    using System.Collections.Generic;

    public class TownWithCategoryNameViewDtoModel
    {
        public TownWithCategoryNameViewDtoModel()
        {
            this.TownNames = new List<string>();
        }

        public string CategoryName { get; set; }

        public IList<string> TownNames { get; set; }
    }
}
