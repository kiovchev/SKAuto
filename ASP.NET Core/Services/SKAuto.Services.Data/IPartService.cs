namespace SKAuto.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SKAuto.Common.DtoModels.PartDtos;
    using SKAuto.Data.Models;

    public interface IPartService
    {
        Task CreatePartAsync(PartCreateInputDtoModel partModel);

        Task DeletePartAsync(int partId);

        Task<bool> IsSamePartAsync(
                                    string partName,
                                    string brandAndModelName,
                                    string categoryName,
                                    string manufactoryName,
                                    int partQuantity,
                                    decimal price);

        Task<IList<PartAllDtoModel>> GetPartsByModelAndCategoryAsync(string modelName, string categoryName);

        Task<PartCreateOutPutDtoModel> GetPartCreateParams();

        Task<bool> CheckIfPartExistsAsync(
                                           string partName,
                                           string brandAndModelName,
                                           string categoryName,
                                           string manufactoryName);

        Task<IList<PartGetAllPartsForIndexDtoModel>> GetAllPartsAsync();

        Task<PartAddOutputDtoModel> GetAddOutputModelByIdAsync(int partId);

        Task AddQuantityAsync(PartAddInputDtoModel model);

        Task<PartUpdateOutputDtoModel> GetPartUpdateModel(int partId);

        Task<Part> GetPartByIdAsync(int partId);

        Task UpdatePartAsync(PartUpdateInputDtoModel model);

        Task<Part> GetPartForItemByPartId(int id);

        Task ReturnPartFromCartAsync(int partId, int orderedQuantity);
    }
}
