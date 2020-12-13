namespace SKAutoNew.Services.Mappers.RecipientServiceMappers
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Data.Models;

    public static class RecipientUpdateServiceMapper
    {
        public static Recipient Map(Recipient recipientForUpdate, RecipientUpdateInputDtoModel dtoModel) 
        {
            recipientForUpdate.FirstName = dtoModel.FirstName;
            recipientForUpdate.LastName = dtoModel.LastName;
            recipientForUpdate.RecipientTown = dtoModel.RecipientTown;
            recipientForUpdate.Address = dtoModel.Address;
            recipientForUpdate.Phone = dtoModel.Phone;

            return recipientForUpdate;
        }
    }
}
