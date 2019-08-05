namespace SKAuto.Data.Models
{
    public class TownUseFullCategory
    {
        public int TownId { get; set; }

        public Town Town { get; set; }

        public int UseFullCategoryId { get; set; }

        public UseFullCategory UseFullCategory { get; set; }
    }
}
