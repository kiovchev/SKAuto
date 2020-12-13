namespace SKAutoNew.HandMappers.RecipientMappers
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Data.Models;

    public static class RecipientParamsDtoMapper
    {
        public static RecipientParamsDtoModel Map(Recipient recipientModel)
        {
            var dtoModel = new RecipientParamsDtoModel
            {
                Id = recipientModel.Id,
                FirstName = recipientModel.FirstName,
                LastName = recipientModel.LastName,
                RecipientTown = recipientModel.RecipientTown,
                Address = recipientModel.Address,
                Phone = recipientModel.Phone
            };

            return dtoModel;
        }
    }
}
