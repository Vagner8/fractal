using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        required public string Login { get; set; }
        [Required]
        required public string FirstName { get; set; }
        [Required]
        required public string LastName { get; set; }
        [Required]
        required public string Email { get; set; }
        [Required]
        required  public string Phone { get; set; }
        [Required]
        required public string Password { get; set; }
        [Required]
        required public DateTime Updated { get; set; }
        public DateTime DateTime { get; internal set; }
    }
}
