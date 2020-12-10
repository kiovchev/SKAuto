namespace SKAutoNew.HandMappers.CompanyMapper
{
    using SKAutoNew.Common.DtoModels.CompanyDtos;
    using SKAutoNew.Web.ViewModels.CompanyViewModels;

    public static class CompanyCreateInputHandMapper
    {
        public static CompanyInputViewDtoModel Map(CompanyInputViewModel companyModel)
        {
            var companyModelDto = new CompanyInputViewDtoModel
            {
                Name = companyModel.Name,
                TownName = companyModel.TownName,
                Address = companyModel.Address,
                CategoryName = companyModel.CategoryName,
                Phone = companyModel.Phone
            };

            return companyModelDto;
        }
    }
}
