using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_tr_profilings")]
public class Profiling
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("employee_nik", TypeName = "nchar(5)")]
    public string EmployeeNIK { get; set; }
    [Column("education_id")]
    public int EducationId { get; set; }

    //relation & cardinality
    [JsonIgnore]
    [ForeignKey(nameof(EducationId))]
    public Education? Education { get; set; }
    [JsonIgnore]
    [ForeignKey(nameof(EmployeeNIK))]
    public Employee? Employee { get; set; }
}