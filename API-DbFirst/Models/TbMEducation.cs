using System;
using System.Collections.Generic;

namespace API_DbFirst.Models;

public partial class TbMEducation
{
    public int Id { get; set; }

    public string Major { get; set; } = null!;

    public string Degree { get; set; } = null!;

    public string Gpa { get; set; } = null!;

    public int UniversityId { get; set; }

    public virtual ICollection<TbTrProfiling> TbTrProfilings { get; } = new List<TbTrProfiling>();

    public virtual TbMUniversity University { get; set; } = null!;
}
