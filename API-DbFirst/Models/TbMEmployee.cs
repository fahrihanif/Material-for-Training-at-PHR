using System;
using System.Collections.Generic;

namespace API_DbFirst.Models;

public partial class TbMEmployee
{
    public string Nik { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateTime Birthdate { get; set; }

    public int Gender { get; set; }

    public DateTime HiringDate { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool? IsActive { get; set; }

    public virtual TbMAccount? TbMAccount { get; set; }

    public virtual ICollection<TbTrProfiling> TbTrProfilings { get; } = new List<TbTrProfiling>();
}
