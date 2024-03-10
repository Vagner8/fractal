using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Models.User
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }
    }
}
