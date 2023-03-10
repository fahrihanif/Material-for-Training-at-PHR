using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Client.Models;
public class University
{   
    public int Id { get; set; } 
    [Required]
    public string Name { get; set; }

}
