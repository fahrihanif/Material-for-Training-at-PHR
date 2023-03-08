using System;
using System.Collections.Generic;

namespace API_DbFirst.Models;

public partial class TbMUniversity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TbMEducation> TbMEducations { get; } = new List<TbMEducation>();
}
