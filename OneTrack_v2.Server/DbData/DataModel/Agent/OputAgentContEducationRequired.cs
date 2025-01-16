namespace OneTrack_v2.DataModel
{
    public class OputAgentContEducationRequired
    {
        public int ContEducationRequirementID { get; set; }
        public DateTime? EducationStartDate { get; set; }
        public DateTime? EducationEndDate { get; set; }
        public decimal? RequiredCreditHours { get; set; } 
        public bool? IsExempt { get; set; }
    }
}
