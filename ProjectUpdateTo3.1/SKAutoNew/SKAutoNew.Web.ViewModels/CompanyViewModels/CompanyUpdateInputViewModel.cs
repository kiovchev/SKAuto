namespace SKAutoNew.Web.ViewModels.CompanyViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CompanyUpdateInputViewModel
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set; }

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
