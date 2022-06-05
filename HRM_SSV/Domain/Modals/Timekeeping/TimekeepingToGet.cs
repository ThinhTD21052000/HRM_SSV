using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Modals.Timekeeping
{
    public class TimekeepingToGet
    {
        public int Id { get; set; }
        public int Status { get; set; } = 1;
        public string Note { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string ListVM_Id { get; set; } = string.Empty;
        public int OvertimeHours { get; set; }
        public string OverTimeDescription { get; set; } = string.Empty; 
        public string UserId { get; set; } = string.Empty;
        public int MTKId { get; set; }
        [NotMapped]
        public string FullName { get; set; } = string.Empty;
        [NotMapped]
        public string PositionName { get; set; } = string.Empty;
        [NotMapped]
        public string TeamName { get; set; } = string.Empty;
    }
}
