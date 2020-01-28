namespace SKAuto.Web.ViewModels.ViewModels.RecipientViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RecipientFindInputModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
