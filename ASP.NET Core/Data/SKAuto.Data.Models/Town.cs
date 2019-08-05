namespace SKAuto.Data.Models
{
    using System.Collections.Generic;

    public class Town
    {
        public Town()
        {
            this.UseFullCategories = new List<TownUseFullCategory>();
            this.Companies = new List<Company>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TownUseFullCategory> UseFullCategories { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
