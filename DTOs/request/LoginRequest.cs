using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class LoginRequest
{
    [Required(ErrorMessage = "Username is required.")]
    public required string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public required string Password { get; set; }
}