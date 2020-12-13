namespace SKAutoNew.HandMappers.RecipientMappers
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Web.ViewModels.RecipientViewModels;

    public static class RecipientUpdateOutputViewMapper
    {
        public static RecipientUpdateOutputDtoModel Map(RecipientUpdateOutputViewModel viewModel)
        {
            var dtoModel = new RecipientUpdateOutputDtoModel
            {
                RecipientId = viewModel.RecipientId
            };

            return dtoModel;
        }
    }
}
