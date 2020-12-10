namespace SKAutoNew.Common.DtoModels.TownDtos
{
    using System.Collections.Generic;

    public class AllTownsViewDtoModel
    {
        public AllTownsViewDtoModel()
        {
            this.TownsNames = new List<string>();
        }

        public IList<string> TownsNames { get; set; }
    }
}
