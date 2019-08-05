namespace SKAuto.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Brand
    {
        public Brand()
        {
            this.Models = new List<Model>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }

        public IEnumerable<Model> Models { get; set; }
    }
}
