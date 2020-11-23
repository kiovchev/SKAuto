namespace SKAuto.Web.ViewModels.ViewModels.CartViewModels
{
    public class CartViewModel
    {
        public int ItemId { get; set; }

        public int PartId { get; set; }

        public string PartName { get; set; }

        public string BrandAndModel { get; set; }

        public decimal CustomerPrice { get; set; }

        public int OrderedQuantity { get; set; }

        public decimal TotalPerItem => this.CustomerPrice * this.OrderedQuantity;
    }
}
