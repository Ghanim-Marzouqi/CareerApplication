namespace CareerApplication.Admin.Models;

public class ForgotPasswordModel
{
    [Required(ErrorMessage = "Please enter email address")]
    [EmailAddress(ErrorMessage = "Email address is invalid")]
    public string Email { get; set; } = string.Empty;
}
