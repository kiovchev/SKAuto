namespace SKAutoNew.HandMappers.RecipientMappers
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Web.ViewModels.RecipientViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class RecipientIndexViewMapper
    {
        public static IList<RecipientIndexViewModel> Map(List<RecipientIndexDto> dtoModels)
        {
            var viewModels = dtoModels.Select(x => new RecipientIndexViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                RecipientTown = x.RecipientTown,
                Address = x.Address,
                Phone = x.Phone
            }).ToList();

            return viewModels;
        }
    }
}
