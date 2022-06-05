namespace Server.Entities
{
    public class MonthlySalary
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Total { get; set; }
        public int OvertimeHours { get; set; }
        public string OvertimeDescription { get; set; } = string.Empty;
        public int NumberofWorkSeparately { get; set; }
        public int BonusWage { get; set; }
        public string BonusWageDescription { get; set; } = string.Empty;
        public Decimal Tax { get; set; }
        public int? Insurance { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
