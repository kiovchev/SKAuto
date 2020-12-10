namespace SKAutoNew.HandMappers.CompanyMapper
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Web.ViewModels.CompanyViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public static class AllByTownAndCategoryHandMapper
    {
        public static List<CompanyInputViewModel> Map(IList<CompanyInputViewDtoModel> viewModelDto)
        {
            var viewModel = viewModelDto.Select(x => new CompanyInputViewModel
            {
                Name = x.Name,
                TownName = x.TownName,
                Address = x.Address,
                CategoryName = x.CategoryName,
                Phone = x.Phone
            }).ToList();

            return viewModel;
        }
    }
}
