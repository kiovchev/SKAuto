using SKAutoNew.Common.DtoModels.UseFullCategoryDto;
using SKAutoNew.Web.ViewModels.UseFullCategoryViewModels;

namespace SKAutoNew.HandMappers.UseFullCategoryMapper
{
    public static class UseFullUpdatePostInputMapper
    {
        public static UseFullUpdatePostInputDtoModel Map(UseFullUpdatePostInputViewModel viewModel)
        {
            if (viewModel.ImageAddress == null || viewModel.ImageAddress == "")
            {
                viewModel.ImageAddress = "/Images/No picture.jpg";
            }

            var dtoModel = new UseFullUpdatePostInputDtoModel
            {
                UseFullCategoryId = viewModel.UseFullCategoryId,
                UseFullCategoryName = viewModel.UseFullCategoryName,
                ImageAddress = viewModel.ImageAddress
            };

            return dtoModel;
        }
    }
}
