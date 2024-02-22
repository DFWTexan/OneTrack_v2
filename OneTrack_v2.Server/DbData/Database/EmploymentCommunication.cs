using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class EmploymentCommunication
{
    public int EmploymentCommunicationId { get; set; }

    public int? EmployeeId { get; set; }

    public int? EmploymentId { get; set; }

    public int? CompanyId { get; set; }

    public string? EmailTo { get; set; }

    public string? EmailFrom { get; set; }

    public string? EmailSubject { get; set; }

    public string? EmailBodyHtml { get; set; }

    public string? CompareXml { get; set; }

    public string? EmailAttachments { get; set; }

    public string? EmailCreator { get; set; }

    public DateTime? EmailCreateDate { get; set; }

    public DateTime? EmailSentDate { get; set; }

    public int? CommunicationId { get; set; }
}
