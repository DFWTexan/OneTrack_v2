using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmploymentCommunicationDetail
{
    public int EmploymentCommunicationDetailId { get; set; }

    public int? EmploymentCommunicationId { get; set; }

    public int? EmployeeLicenseId { get; set; }

    public int? EmployeeAppointmentId { get; set; }

    public DateTime? EducationEndDate { get; set; }

    public decimal? RequiredCreditHours { get; set; }

    public DateTime? LicenseExpireDate { get; set; }

    public DateTime? SentToAgentDate { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? HireDate { get; set; }

    public DateTime? RehireDate { get; set; }

    public int? CompanyRequirementId { get; set; }
}
