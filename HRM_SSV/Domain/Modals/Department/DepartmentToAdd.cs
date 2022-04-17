namespace Domain.Modals.Department
{
    public class DepartmentToAdd
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CompanyId { get; set; }
    }
}
