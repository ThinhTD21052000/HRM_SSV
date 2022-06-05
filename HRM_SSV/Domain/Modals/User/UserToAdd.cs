using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Modals.User
{
    public class UserToAdd
    {
        public byte[]? Avatar { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Sex { get; set; }
        public DateTime DoB{ get; set; }
        public DateTime DoJ { get; set; }
        public string Address { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public int TeamId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [NotMapped]
        public string RoleName { get; set; } = string.Empty;
    }
}
