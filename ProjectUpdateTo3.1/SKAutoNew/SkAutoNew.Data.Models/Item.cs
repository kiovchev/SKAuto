namespace SKAutoNew.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Item
    {
        public Item()
        {
            this.IsOrdered = false;
            this.OrderedAt = DateTime.UtcNow.AddHours(2);
        }

        [Key]
        public int ItemId { get; set; }

        [Required]
        public int PartId { get; set; }

        public Part Part { get; set; }

        [Required]
        public int OrderedQuantity { get; set; }

        public bool IsOrdered { get; set; }

        public DateTime OrderedAt { get; set; }

        public int? OrderId { get; set; }

        public Order Order { get; set; }
    }
}
