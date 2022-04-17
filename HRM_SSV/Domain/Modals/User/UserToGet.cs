namespace Domain.Modals.User
{
    public class UserToGet
    {
        public Guid Id { get; set; }
        public byte[]? Avatar { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public byte Sex { get; set; }
        public DateTime DoB{ get; set; }
        public DateTime DoJ { get; set; }
        public string Address { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public int TeamId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
