namespace SKAuto.Web.ViewModels.ViewModels.RecipientViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RecipientParamsViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
