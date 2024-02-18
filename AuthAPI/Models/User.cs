using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        required public string Name { get; set; }
        [Required]
        required public DateTime Updated { get; set; }
        [Required]
        required public DateTime Created { get; set; }
    }
}
