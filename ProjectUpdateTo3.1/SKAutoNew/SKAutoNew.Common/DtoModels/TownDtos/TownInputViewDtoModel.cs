namespace SKAutoNew.Common.DtoModels.TownDtos
{
    using System.ComponentModel.DataAnnotations;

    public class TownInputViewDtoModel
    {
        [Required]
        public string Name { get; set; }
    }
}
