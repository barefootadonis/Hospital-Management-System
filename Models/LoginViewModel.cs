using System.ComponentModel.DataAnnotations;

public class LoginViewModel
{
    [Required]
    public string EmailOrId { get; set; }

    [Required]
    public string Password { get; set; }
}
