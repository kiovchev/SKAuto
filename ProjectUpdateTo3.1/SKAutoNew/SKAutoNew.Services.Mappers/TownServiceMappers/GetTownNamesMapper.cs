namespace SKAutoNew.Services.Mappers.TownServiceMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class GetTownNamesMapper
    {
        public static AllTownsViewDtoModel Map(List<Town> towns)
        {
            var all = new AllTownsViewDtoModel();
            all.TownsNames = towns.Select(x => x.Name).ToList();

            return all;
        }
    }
}
