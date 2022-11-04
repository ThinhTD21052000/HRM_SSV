namespace Server.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } = new();
        public virtual List<Team> Teams { get; set; } = new();
    }
}
