namespace SKAuto.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Order
    {
        public Order()
        {
            this.OrderParts = new List<OrderParts>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int RecipientId { get; set; }

        public Recipient Recipient { get; set; }

        [Required]
        public int OrderStatusId { get; set; } = 1;

        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderParts> OrderParts { get; set; }

        public decimal OrderPrice => this.OrderParts.Select(p => p.Part.CustomerPrice).Sum();
    }
}
