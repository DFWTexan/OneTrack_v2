using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class VwEmployeeAppointment
{
    public int EmployeeAppointmentId { get; set; }

    public DateTime? AppointmentEffectiveDate { get; set; }

    public string? AppointmentStatus { get; set; }

    public int? EmployeeLicenseId { get; set; }

    public DateTime? CarrierDate { get; set; }

    public DateTime? AppointmentExpireDate { get; set; }

    public DateTime? AppointmentTerminationDate { get; set; }

    public int? CompanyId { get; set; }

    public DateTime? RetentionDate { get; set; }
}
