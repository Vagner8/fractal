using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Models.User
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
    }
}
