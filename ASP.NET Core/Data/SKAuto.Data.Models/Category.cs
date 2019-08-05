namespace SKAuto.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        public Category()
        {
            this.Parts = new List<Part>();
            this.ModelCategories = new List<ModelCategories>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }

        public ICollection<Part> Parts { get; set; }

        public ICollection<ModelCategories> ModelCategories { get; set; }
    }
}
