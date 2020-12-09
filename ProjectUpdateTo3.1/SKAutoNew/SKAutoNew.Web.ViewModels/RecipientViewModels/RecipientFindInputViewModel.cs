namespace SKAutoNew.Web.ViewModels.RecipientViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RecipientFindInputViewModel
    {
        [Required]
        public string Phone { get; set; }
    }
}
