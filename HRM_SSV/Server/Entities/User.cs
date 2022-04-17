using Microsoft.AspNetCore.Identity;

namespace Server.Entities
{
    public class User : IdentityUser<Guid>
    {
        public byte[]? Avatar { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte Sex { get; set; }
        //Date of birth
        public DateTime DoB{ get; set; }
        //Date of joining the company
        public DateTime DoJ { get; set; }
        public string Address { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public int TeamId { get; set; }
        public virtual Position Position { get; set; } = new();
        public virtual Team Team { get; set; } = new();
        public virtual List<LaborContract> LaborContracts { get; set; } = new();
        public virtual List<Wage> Wages { get; set; } = new();
        public virtual List<MonthlySalary> MonthlySalaries { get; set; } = new();
        public virtual List<Timekeeping> Timekeepings { get; set; } = new();
    }
}
