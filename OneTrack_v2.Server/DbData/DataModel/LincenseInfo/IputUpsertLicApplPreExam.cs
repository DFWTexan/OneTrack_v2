namespace OneTrak_v2.DataModel
{
    public class IputUpsertLicApplPreExam
    {
        public int? EmployeeLicensePreExamID { get; set; }  
        public int? ExamID { get; set; }
        public int? EmployeeLicenseID { get; set; }
        public DateTime? ExamTakenDate { get; set; }
        public string? Status { get; set; }
        public DateTime? ExamScheduleDate { get; set; }
        public string? AdditionalNotes { get; set; }
        public string UserSOEID { get; set; }
    }
}
