using System.ComponentModel.DataAnnotations;

public class RegisterRequest{
    [Required(ErrorMessage = "Username is required.")]
    [MinLength(5, ErrorMessage ="Username must be atleast 5 characters long.")]
    public required string UserName {get; set;}

    [Required(ErrorMessage ="Email is required")]
    [EmailAddress]
    public required string Email {get; set;}
    [Required(ErrorMessage ="Password is required")]
    [MinLength(5, ErrorMessage ="Password must be atleast 5 characters long.")]
    public required string Password {get; set;}
}