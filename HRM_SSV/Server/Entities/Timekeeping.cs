﻿namespace Server.Entities
{
    public class Timekeeping
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        //Violation Money
        public string ListVM_Id { get; set; } = string.Empty;
        public int OvertimeHours { get; set; }
        public string OverTimeDescription { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int MTKId { get; set; }
    }
}
