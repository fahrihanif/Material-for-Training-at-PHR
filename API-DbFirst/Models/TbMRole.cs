using System;
using System.Collections.Generic;

namespace API_DbFirst.Models;

public partial class TbMRole
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TbTrAccountRole> TbTrAccountRoles { get; } = new List<TbTrAccountRole>();
}
