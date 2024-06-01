namespace OneTrak_v2.DataModel
{
    public class IputUpsertConEduTaken
    {
        public int EmployeeEducationID { get; set; }
        public int ContEducationID { get; set; }
        public int ContEducationRequirementID { get; set; }

        public DateTime ContEducationTakenDate { get; set; }
        public decimal CreditHoursTaken { get; set; }
        public string? AdditionalNotes { get; set; }
        public string UserSOEID { get; set; }
    }
}
