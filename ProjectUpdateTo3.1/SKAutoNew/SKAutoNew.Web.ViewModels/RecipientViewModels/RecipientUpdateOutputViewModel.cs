namespace SKAutoNew.Web.ViewModels.RecipientViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RecipientUpdateOutputViewModel
    {
        [Required]
        public int RecipientId { get; set; }
    }
}
