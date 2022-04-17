namespace Domain.Modals.Team
{
    public class TeamToAdd
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
