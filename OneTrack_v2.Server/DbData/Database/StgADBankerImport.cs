﻿namespace OneTrack_v2.DbData.Models
{
    public class StgADBankerImport
    {
        public int EmployeeId { get; set; }
        public string? CourseState { get; set; }
        public string? StudentName { get; set; }
        public string? CourseTitle { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? ReportedDate { get; set; }
        public int? TotalCredits { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsImportComplete { get; set; }
    }
}
