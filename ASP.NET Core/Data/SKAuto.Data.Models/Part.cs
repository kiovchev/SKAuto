namespace SKAuto.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Part
    {
        public Part()
        {
            this.OrderParts = new List<OrderParts>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        [Required]
        public int ModelId { get; set; }

        public Model Model { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        public decimal InComePrice { get; set; }

        public decimal CustomerPrice => this.InComePrice * 1.2m;

        public int? ManufactoryId { get; set; }

        public Manufactory Manufactory { get; set; }

        [Required]
        public int Quantity { get; set; }

        public ICollection<OrderParts> OrderParts { get; set; }
    }
}
