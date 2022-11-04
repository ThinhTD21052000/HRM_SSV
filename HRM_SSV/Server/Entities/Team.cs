namespace Server.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manager { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; } = new();
        public virtual List<User> User { get; set; } = new();
    }
}
