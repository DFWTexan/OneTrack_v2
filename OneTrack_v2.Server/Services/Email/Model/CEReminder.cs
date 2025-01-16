namespace OneTrak_v2.Services.Model
{
    public class CEReminder : ManagerInfo
    {
        public DateTime? CEExpireDate { get; set; }
        public decimal? RequiredCreditHours { get; set; }
    }
}
