using System;
using System.Collections.Generic;

namespace API_DbFirst.Models;

public partial class TbMAccount
{
    public string EmployeeNik { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual TbMEmployee EmployeeNikNavigation { get; set; } = null!;

    public virtual ICollection<TbTrAccountRole> TbTrAccountRoles { get; } = new List<TbTrAccountRole>();
}
