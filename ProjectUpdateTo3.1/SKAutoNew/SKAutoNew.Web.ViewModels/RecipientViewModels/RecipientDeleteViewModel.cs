namespace SKAutoNew.Web.ViewModels.RecipientViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RecipientDeleteViewModel
    {
        [Required]
        public int RecipientId { get; set; }
    }
}
