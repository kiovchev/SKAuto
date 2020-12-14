namespace SKAutoNew.Services.Mappers.TownServiceMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Data.Models;

    public static class TownUpdateGetOutputServiceMapper
    {
        public static TownUpdateGetOutPutDto Map(Town town)
        {
            var dtoModel = new TownUpdateGetOutPutDto
            {
                TownId = town.Id,
                TownName = town.Name
            };

            return dtoModel;
        }
    }
}
