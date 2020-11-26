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
            this.Items = new HashSet<Item>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow.AddHours(2);

        [Required]
        public int RecipientId { get; set; }

        public Recipient Recipient { get; set; }

        [Required]
        public int OrderStatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ICollection<Item> Items { get; set; }

        public decimal OrderPrice => this.Items.Select(p => p.Part.CustomerPrice * p.OrderedQuantity).Sum();
    }
}
