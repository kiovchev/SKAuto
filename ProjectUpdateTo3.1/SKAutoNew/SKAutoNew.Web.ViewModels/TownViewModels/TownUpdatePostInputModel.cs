namespace SKAutoNew.Web.ViewModels.TownViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class TownUpdatePostInputModel
    {
        [Required]
        public int TownId { get; set; }

        [Required]
        public string TownName { get; set; }
    }
}
