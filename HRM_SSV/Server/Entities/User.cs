using Microsoft.AspNetCore.Identity;

namespace Server.Entities
{
    public class User : IdentityUser<Guid>
    {
        public byte[]? Avatar { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Sex { get; set; }
        //Date of birth
        public DateTime DoB{ get; set; }
        //Date of joining the company
        public DateTime DoJ { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public int TeamId { get; set; }
    }
}
