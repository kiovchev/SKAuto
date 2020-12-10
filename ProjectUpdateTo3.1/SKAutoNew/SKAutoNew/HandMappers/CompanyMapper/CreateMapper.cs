namespace SKAutoNew.HandMappers.CompanyMapper
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Web.ViewModels.CompanyViewModels;

    public static class CreateMapper
    {
        public static CompanyCreateViewModel Map(CompanyCreateViewDtoModel viewModelDto)
        {
            var viewModel = new CompanyCreateViewModel
            {
                CategoryNames = viewModelDto.CategoryNames,
                TownNames = viewModelDto.TownNames
            };

            return viewModel;
        }
    }
}
