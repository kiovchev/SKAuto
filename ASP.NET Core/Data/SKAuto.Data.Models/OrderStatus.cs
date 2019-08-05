namespace SKAuto.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "Pending";
    }
}
