namespace Domain.Modals.Wage
{
    public class WageToUpdate
    {
        public int Id { get; set; }
        public Decimal Tax { get; set; }
        public int? Insurance { get; set; }
        public string ListAllowance_Id { get; set; } = string.Empty;
        public int Total { get; set; }
        public string ListBonusId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Wage_TypeId { get; set; }
        public int MonthlySalaryId { get; set; }
        public Guid UserId { get; set; } = new();
    }
}
