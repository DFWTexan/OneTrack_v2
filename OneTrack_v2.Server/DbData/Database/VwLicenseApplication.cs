using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwLicenseApplication
{
    public int LicenseApplicationId { get; set; }

    public DateTime? SentToAgentDate { get; set; }

    public DateTime? RecFromAgentDate { get; set; }

    public DateTime? SentToStateDate { get; set; }

    public DateTime? RecFromStateDate { get; set; }

    public int? EmployeeLicenseId { get; set; }

    public string? ApplicationStatus { get; set; }

    public string? ApplicationType { get; set; }

    public DateTime? RenewalDate { get; set; }

    public string? RenewalMethod { get; set; }
}
