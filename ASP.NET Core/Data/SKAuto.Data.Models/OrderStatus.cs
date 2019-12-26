namespace SKAuto.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderStatus
    {
        public OrderStatus()
        {
            this.Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "Pending";

        public ICollection<Order> Orders { get; set; }
    }
}
