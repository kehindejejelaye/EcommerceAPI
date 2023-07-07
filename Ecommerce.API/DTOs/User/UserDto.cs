using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.DTOs.User;

public class UserDto
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Jwt { get; set; }
}
