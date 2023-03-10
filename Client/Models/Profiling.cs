using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models;

[Table("tb_tr_profilings")]
public class Profiling
{
    public int Id { get; set; }
    public string EmployeeNIK { get; set; }
    public int EducationId { get; set; }

}