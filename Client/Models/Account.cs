using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models;

[Table("tb_m_accounts")]
public class Account
{
    
    public string EmployeeNIK { get; set; }
    
    public string Password { get; set; }

}
