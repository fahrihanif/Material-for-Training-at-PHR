using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_m_educations")]
public class Education
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("major", TypeName = "nvarchar(100)")]
    public string Major { get; set; }
    [Column("degree", TypeName = "nvarchar(10)")]
    public string Degree { get; set; }
    [Column("gpa", TypeName = "nvarchar(5)")]
    public string GPA { get; set; }
    [Column("university_id")]
    public int UniversityId { get; set; }

    //relation and cardinality
    [JsonIgnore]
    [ForeignKey(nameof(UniversityId))]
    public University? University { get; set; }
    [JsonIgnore]
    public ICollection<Profiling>? Profilings { get; set; }
}
