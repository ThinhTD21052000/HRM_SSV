namespace Domain.Modals.Team
{
    public class TeamToGet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
    }
}
