﻿namespace Domain.Modals.MonthTimekeeping
{
    public class MonthTimekeepingToGet
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime CreateDate { get; set; }
        public int Status { get; set; }
    }
}