using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string? AddressType { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Phone { get; set; }

    public string? Country { get; set; }

    public string? Zip { get; set; }

    public string? Fax { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
}
