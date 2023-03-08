using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_m_employees")]
public class Employee
{
    [Key, Column("nik", TypeName = "nchar(5)")]
    public string NIK { get; set; }
    [Column("first_name", TypeName = "nvarchar(50)")]
    public string FirstName { get; set; }
    [Column("last_name", TypeName = "nvarchar(50)")]
    public string? LastName { get; set; }
    [Column("birthdate")]
    public DateTime BirthDate { get; set; }
    [Column("gender")]
    public GenderEnum Gender { get; set; }
    [Column("hiring_date")]
    public DateTime HiringDate { get; set; } = DateTime.Now;
    [Column("email", TypeName = "nvarchar(50)")]
    public string Email { get; set; }
    [Column("phone_number"), MaxLength(20)]
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }

    // Cardinality
    [JsonIgnore]
    public ICollection<Profiling>? Profilings { get; set; }
    [JsonIgnore]
    public Account? Account { get; set; }
}

public enum GenderEnum
{
    Male,
    Female
}
