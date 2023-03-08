using System;
using System.Collections.Generic;

namespace API_DbFirst.Models;

public partial class TbTrAccountRole
{
    public int Id { get; set; }

    public string AccountNik { get; set; } = null!;

    public int RoleId { get; set; }

    public virtual TbMAccount AccountNikNavigation { get; set; } = null!;

    public virtual TbMRole Role { get; set; } = null!;
}
