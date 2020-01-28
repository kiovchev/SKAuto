namespace SKAuto.Web.ViewModels.ViewModels.RecipientViewModels
{
    using SKAuto.Web.ViewModels.ViewModels.PartViewModels;

    public class RecipientOrderIpnutModel
    {
        public PartByCategoryAndModelViewModel PartModel { get; set; }

        public int RecipientId { get; set; }
    }
}
