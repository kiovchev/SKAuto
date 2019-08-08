namespace SKAuto.Web.ViewModels.ViewModels.CompanyViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CompanyInputViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string TownName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
