namespace SKAuto.Data.Models
{
    using System.Collections.Generic;

    public class UseFullCategory
    {
        public UseFullCategory()
        {
            this.Companies = new List<Company>();
            this.Towns = new List<TownUseFullCategory>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TownUseFullCategory> Towns { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
