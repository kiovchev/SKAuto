namespace SKAuto.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Recipient
    {
        public Recipient()
        {
            this.Orders = new HashSet<Order>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
