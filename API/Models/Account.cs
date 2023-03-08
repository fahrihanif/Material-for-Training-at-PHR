﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Models;

[Table("tb_m_accounts")]
public class Account
{
    [Key, Column("employee_nik", TypeName = "nchar(5)")]
    public string EmployeeNIK { get; set; }
    [Column("password", TypeName = "nvarchar(255)")]
    public string Password { get; set; }

    // Cardinality
    [JsonIgnore]
    [ForeignKey(nameof(EmployeeNIK))]
    public Employee? Employee { get; set; }
    [JsonIgnore]
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
