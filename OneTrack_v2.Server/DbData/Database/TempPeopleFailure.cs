using System;
using System.Collections.Generic;

namespace OneTrack_v2.DbData.Models;

public partial class TempPeopleFailure
{
    public int PeopleKey { get; set; }

    public string State { get; set; } = null!;

    public int Failurelevel { get; set; }

    public DateTime? DateRecorded { get; set; }

    public DateTime? DateFailed { get; set; }

    public string Coursefailed { get; set; } = null!;

    public string? Recordedby { get; set; }

    public int? Hoursmissed { get; set; }

    public string? Comment { get; set; }
}
