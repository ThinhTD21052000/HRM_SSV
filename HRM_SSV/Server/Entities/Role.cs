using Microsoft.AspNetCore.Identity;

namespace Server.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;
    }
}
