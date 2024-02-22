using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class EmployeeSsn
{
    public int EmployeeSsnid { get; set; }

    public string? EmployeeSsn1 { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
