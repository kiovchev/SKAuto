namespace SKAutoNew.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Brand
    {
        public Brand()
        {
            this.Models = new HashSet<Model>();
            this.Parts = new HashSet<Part>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }

        public ICollection<Model> Models { get; set; }

        public ICollection<Part> Parts { get; set; }
    }
}
