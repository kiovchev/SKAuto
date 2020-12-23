namespace SKAutoNew.Data.Models
{
    using SKAutoNew.Common;
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
        public string Name { get; set; } = GlobalConstants.PendingBGStatus;

        public ICollection<Order> Orders { get; set; }
    }
}
