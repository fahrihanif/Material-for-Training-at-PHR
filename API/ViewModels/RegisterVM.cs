using API.Models;

namespace API_CodeFirst.ViewModels;

public class RegisterVM
{
    public string NIK { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderEnum Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public string GPA { get; set; }
    public string UniversityName { get; set; }
    public string Password { get; set; }
}
