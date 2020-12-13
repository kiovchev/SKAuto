namespace SKAutoNew.HandMappers.RecipientMappers
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Web.ViewModels.RecipientViewModels;

    public static class RecipientUpdateInputMapper
    {
        public static RecipientUpdateInputDtoModel Map(RecipientUpdateInputViewModel viewModel)
        {
            var dtoModel = new RecipientUpdateInputDtoModel
            {
                RecipientId = viewModel.RecipientId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                RecipientTown = viewModel.RecipientTown,
                Address = viewModel.Address,
                Phone = viewModel.Phone
            };

            return dtoModel;
        }
    }
}
