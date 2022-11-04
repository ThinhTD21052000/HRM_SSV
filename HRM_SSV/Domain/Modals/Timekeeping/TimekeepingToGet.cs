namespace Domain.Modals.Timekeeping
{
    public class TimekeepingToGet
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string ListVM_Id { get; set; } = string.Empty;
        public int OvertimeHours { get; set; }
        public string OverTimeDescription { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
