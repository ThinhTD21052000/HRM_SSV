namespace Domain.Modals.Bonus_Wage
{
    public class Bonus_WageToUpdate
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int BonusTotal { get; set; }
        public int BPP { get; set; }
    }
}
