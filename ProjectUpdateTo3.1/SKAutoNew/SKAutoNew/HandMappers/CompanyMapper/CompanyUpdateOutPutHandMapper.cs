namespace SKAutoNew.HandMappers.CompanyMapper
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Web.ViewModels.CompanyViewModels;

    public static class CompanyUpdateOutPutHandMapper
    {
        public static CompanyUpdateOutPutViewModel Map(CompanyUpdateOutPutDtoModel dtoModel)
        {
            var viewModel = new CompanyUpdateOutPutViewModel
            {
                CompanyId = dtoModel.CompanyId,
                CompanyName = dtoModel.CompanyName,
                TownName = dtoModel.TownName,
                Address = dtoModel.Address,
                Phone = dtoModel.Phone,
                CategoryName = dtoModel.CategoryName,
                Towns = dtoModel.Towns,
                Categories = dtoModel.Categories
            };

            return viewModel;
        }
    }
}
