namespace SKAutoNew.HandMappers.TownMappers
{
    using SKAutoNew.Common.DtoModels.TownDtos;
    using SKAutoNew.Web.ViewModels.TownViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class TownIndexViewMapper
    {
        public static IList<TownIndexViewModel> Map(IList<TownIndexDtoModel> dtoModels)
        {
            var viewModels = dtoModels.Select(x => new TownIndexViewModel
            {
                TownId = x.TownId,
                TownName = x.TownName
            }).ToList();

            return viewModels;
        }
    }
}
