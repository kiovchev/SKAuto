﻿namespace SKAuto.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Town
    {
        public Town()
        {
            this.UseFullCategories = new HashSet<TownUseFullCategory>();
            this.Companies = new HashSet<Company>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<TownUseFullCategory> UseFullCategories { get; set; }

        public ICollection<Company> Companies { get; set; }
    }
}
