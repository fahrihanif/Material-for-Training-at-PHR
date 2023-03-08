using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;
[Table("tb_m_universities")]
public class University
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Column("name", TypeName = ("nvarchar(100)"))]
    public string Name { get; set; }

    //cardinality
    [JsonIgnore]
    public ICollection<Education>? Educations { get; set; }
}
