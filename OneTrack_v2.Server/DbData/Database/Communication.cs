using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class Communication
{
    public int CommunicationId { get; set; }

    public string? CommunicationName { get; set; }

    public string? DocTypeAbv { get; set; }

    public string? DocType { get; set; }

    public string? DocSubType { get; set; }

    public string? DocAppType { get; set; }

    public string? EmailAttachments { get; set; }

    public bool HasNote { get; set; }

    public bool IsActive { get; set; }
}
