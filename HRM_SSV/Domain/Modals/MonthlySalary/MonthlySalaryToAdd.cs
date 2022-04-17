namespace Domain.Modals.MonthlySalary
{
    public class MonthlySalaryToAdd
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Total { get; set; }
        public string ListTimekeepingId { get; set; } = string.Empty;
        public int OvertimeHours { get; set; }
        public Guid UserId { get; set; }
    }
}
