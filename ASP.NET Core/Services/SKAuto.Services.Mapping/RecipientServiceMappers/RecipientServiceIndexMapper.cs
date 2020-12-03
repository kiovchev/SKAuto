namespace SKAuto.Services.Mapping.RecipientServiceMappers
{
    using System.Collections.Generic;
    using System.Linq;

    using SKAuto.Common.DtoModels.RecipientDtos;
    using SKAuto.Data.Models;

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
