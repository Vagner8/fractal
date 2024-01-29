using System.ComponentModel.DataAnnotations;

namespace UsersAPI.Models.User
{
    public abstract class UserAbstract
    {
        [Key]
        required public Guid UserId { get; set; }
        [Required]
        required public string Login { get; set; }
        [Required]
        required public string FirstName { get; set; }
        [Required]
        required public string LastName { get; set; }
        [Required]
        required public string Email { get; set; }
        [Required]
        required public string Phone { get; set; }
        [Required]
        required public DateTime Updated { get; set; }
        [Required]
        required public DateTime Created { get; set; }
        [Required]
        required public string Password { get; set; }
    }
}
