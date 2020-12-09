namespace SKAutoNew.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Model
    {
        public Model()
        {
            this.ModelCategories = new HashSet<ModelCategories>();
            this.Parts = new HashSet<Part>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int StartYear { get; set; }

        [Required]
        public int EndYear { get; set; }

        [Required]
        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public string ImageAddress { get; set; }

        public ICollection<ModelCategories> ModelCategories { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}
