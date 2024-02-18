using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Models.Dto
{
    public class RegistrationRequestDto
    {
        required public string Name { get; set; }
        required public string Email { get; set; }
        required public string PhoneNumber { get; set; }
        required public string Password { get; set; }
        required public DateTime Updated { get; set; }
        required public DateTime Created { get; set; }
    }
}
