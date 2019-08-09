namespace SKAuto.Data.Models
{
    public class OrderParts
    {
        public int OrderId { get; set; }

        public Order Order { get; set; }

        public int PartId { get; set; }

        public Part Part { get; set; }
    }
}
