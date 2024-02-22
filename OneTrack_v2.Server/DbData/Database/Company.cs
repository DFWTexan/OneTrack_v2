using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string? CompanyAbv { get; set; }

    public string? CompanyType { get; set; }

    public string? CompanyName { get; set; }

    public int? Tin { get; set; }

    public int? Naicnumber { get; set; }

    public int? AddressId { get; set; }

    public bool? XxxIsActive { get; set; }

    public bool LetterRequired { get; set; }

    public virtual Address? Address { get; set; }

    public virtual ICollection<EmployeeAppointment> EmployeeAppointments { get; set; } = new List<EmployeeAppointment>();

    public virtual ICollection<EmployeeLicenseePreEducation> EmployeeLicenseePreEducations { get; set; } = new List<EmployeeLicenseePreEducation>();

    public virtual ICollection<Employment> Employments { get; set; } = new List<Employment>();

    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    public virtual ICollection<LicenseCompany> LicenseCompanies { get; set; } = new List<LicenseCompany>();

    public virtual ICollection<PreEducation> PreEducations { get; set; } = new List<PreEducation>();
}
