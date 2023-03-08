using System;
using System.Collections.Generic;

namespace API_DbFirst.Models;

public partial class TbTrProfiling
{
    public int Id { get; set; }

    public string EmployeeNik { get; set; } = null!;

    public int EducationId { get; set; }

    public virtual TbMEducation Education { get; set; } = null!;

    public virtual TbMEmployee EmployeeNikNavigation { get; set; } = null!;
}
