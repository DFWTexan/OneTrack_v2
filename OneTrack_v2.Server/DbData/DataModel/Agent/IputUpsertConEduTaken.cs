namespace OneTrak_v2.DataModel
{
    public class IputUpsertConEduTaken
    {
        public int EmployeeEducationID { get; set; } = 0;
        public int ContEducationID { get; set; } = 0;
        public int ContEducationRequirementID { get; set; } = 0;

        public DateTime ContEducationTakenDate { get; set; }
        public decimal CreditHoursTaken { get; set; }
        public string? AdditionalNotes { get; set; }
        public string UserSOEID { get; set; }
    }
}
