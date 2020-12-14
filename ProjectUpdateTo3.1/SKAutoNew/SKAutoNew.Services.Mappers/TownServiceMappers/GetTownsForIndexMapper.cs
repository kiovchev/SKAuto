namespace SKAutoNew.Services.Mappers.TownServiceMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class GetTownsForIndexMapper
    {
        public static IList<TownIndexDtoModel> Map(List<Town> towns)
        {
            var townsDto = towns.Select(x => new TownIndexDtoModel
            {
                TownId = x.Id,
                TownName = x.Name
            })
                .OrderBy(x => x.TownName)
                .ToList();

            return townsDto;
        }
    }
}
