namespace SKAutoNew.HandMappers.RecipientMappers
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Web.ViewModels.RecipientViewModels;

    public static class RecipientUpdateParamsMapper
    {
        public static RecipientUpdateParamsViewModel Map(RecipientParamsDtoModel dtoModel)
        {
            var viewModel = new RecipientUpdateParamsViewModel
            {
                Id = dtoModel.Id,
                FirstName = dtoModel.FirstName,
                LastName = dtoModel.LastName,
                RecipientTown = dtoModel.RecipientTown,
                Address = dtoModel.Address,
                Phone = dtoModel.Phone
            };

            return viewModel;
        }
    }
}
