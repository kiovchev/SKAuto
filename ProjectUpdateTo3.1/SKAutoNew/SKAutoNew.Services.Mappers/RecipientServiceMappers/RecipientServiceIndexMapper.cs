namespace SKAutoNew.Services.Mappers.RecipientServiceMappers
{
    using SKAutoNew.Common.DtoModels.RecipientDtos;
    using SKAutoNew.Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class RecipientServiceIndexMapper
    {
        public static List<RecipientIndexDto> Map(List<Recipient> recipientsAll)
        {
            var recipients = recipientsAll.Select(x => new RecipientIndexDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                RecipientTown = x.Phone,
                Address = x.Address,
                Phone = x.Phone,
            })
                .ToList();

            return recipients;
        }
    }
}
