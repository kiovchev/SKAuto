namespace SKAuto.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int UseFullCategoryId { get; set; }

        public UseFullCategory UseFullCategory { get; set; }

        [Required]
        public int TownId { get; set; }

        public Town Town { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
