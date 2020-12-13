namespace SKAutoNew.HandMappers.CompanyMapper
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Web.ViewModels.CompanyViewModels;

    public static class CompanyUpdateInputHandMapper
    {
        public static CompanyUpdateInputDtoModel Map(CompanyUpdateInputViewModel viewModel)
        {
            var dtoModel = new CompanyUpdateInputDtoModel
            {
                CompanyId = viewModel.CompanyId,
                CompanyName = viewModel.CompanyName,
                TownName = viewModel.TownName,
                Address = viewModel.Address,
                Phone = viewModel.Phone,
                CategoryName = viewModel.CategoryName
            };

            return dtoModel;
        }
    }
}
