namespace OneTrak_v2.DataModel
{
    public class IputUpsertLicensePreEducation
    {
        public int EmployeeLicensePreEducationID { get; set; }
        public string? Status { get; set; }
        public DateTime? EducationEndDate { get; set; }
        public int? PreEducationID { get; set; }
        public DateTime? EducationStartDate { get; set; }
        public int? EmployeeLicenseID { get; set; }
        public string? AdditionalNotes { get; set; }
        public string UserSOEID { get; set; }
    }
}
