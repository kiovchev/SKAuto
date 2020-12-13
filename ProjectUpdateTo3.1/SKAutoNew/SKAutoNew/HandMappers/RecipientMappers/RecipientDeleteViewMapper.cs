namespace SKAutoNew.HandMappers.RecipientMappers
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Web.ViewModels.RecipientViewModels;

    public static class RecipientDeleteViewMapper
    {
        public static RecipientDeleteDtoModel Map(RecipientDeleteViewModel viewModel)
        {
            var dtoModel = new RecipientDeleteDtoModel
            {
                RecipientId = viewModel.RecipientId
            };

            return dtoModel;
        }
    }
}
