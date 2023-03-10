using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models;

[Table("tb_m_roles")]
public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }

}
