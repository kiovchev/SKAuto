namespace SKAuto.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UseFullCategory
    {
        public UseFullCategory()
        {
            this.Companies = new List<Company>();
            this.Towns = new List<TownUseFullCategory>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageAddress { get; set; }

        public ICollection<TownUseFullCategory> Towns { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
