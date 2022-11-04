namespace Server.Entities
{
    public class Bonus_Wage
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int BonusTotal { get; set; }
        /// bonus per person
        public int BPP { get; set; }
    }
}
