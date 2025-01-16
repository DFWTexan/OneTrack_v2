using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class AuditLog
{
    public int AuditLogId { get; set; }

    public string? BaseTableName { get; set; }

    public string? BaseTableKeyValue { get; set; }

    public DateTime? ModifyDate { get; set; }

    public string? ModifiedBy { get; set; }

    public string? Geid { get; set; }

    public string? AuditFieldName { get; set; }

    public string? AuditAction { get; set; }

    public string? AuditValueBefore { get; set; }

    public string? AuditValueAfter { get; set; }
}
