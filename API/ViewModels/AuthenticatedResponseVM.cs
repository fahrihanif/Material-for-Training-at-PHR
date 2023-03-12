namespace API_CodeFirst.ViewModels;

public class AuthenticatedResponseVM
{
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
}
