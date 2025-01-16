using OneTrack_v2.DataModel;

namespace OneTrak_v2.DataModel
{
    public class OputAgentLicenseAppointments : OputAgentLicenses
    {
        public List<OputAgentAppointments> LicenseAppointments { get; set; }
        public OputAgentLicenseAppointments()
        {
            LicenseAppointments = new List<OputAgentAppointments>();
        }
    }
}
