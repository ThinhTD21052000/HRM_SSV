﻿namespace Domain.Modals.Timekeeping
{
    public class TimekeepingToAdd
    {
        public int Status { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string ListVM_Id { get; set; } = string.Empty;
        public int OvertimeHours { get; set; }
        public string OverTimeDescription { get; set; } = string.Empty;
        public Guid UserId { get; set; }
    }
}
