﻿namespace OneTrak_v2.Server.Services.Email.Templates
{
    public interface IEmailTemplateService
    {
        public Tuple<string, string, string, string> GetMessageHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetCourtDocHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetIncompleteHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetApplNotRecievedHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetAPPLicCopyDisplayGaKyMtWaWyHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetAPPLicenseCopyHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetEmploymentHistoryHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExamScheduledHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExamScheduledNoCertHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetRENLicenseCopyCAHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetRENLicenseCopyHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetRENLicenseCopyGaKyMtWaWyHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExamFxRegLifeHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetUmonitoredHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetMonitoredHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetMonitoredInHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetWebinarIlHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetLifePLSHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetLifePlsPlusHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetOkToSELLHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetBackgroundReleaseHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetBackgroundDisclosureLinkHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExamScheduledCreditHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetClearenceLetterHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetLifePLsPlusILHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetChildSupportHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetCitizenDocumentHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetNotoryMissingHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetNotoryMissingTnHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetApplicationRequiredHIHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetApplicationRequiredTNHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintRequiredAZHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintRequiredLAHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintRequiredCreditHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintScheduledALHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintScheduledTNHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintScheduledNMHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintScheduledPAHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetComplianceCertificateHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetComplianceCertificateEndHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetLifePlsILHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintRequiredUTHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetADBankerRegistrationILHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExamScheduledPAHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetAddressChangeHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetNameChangeILHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintScheduledGAHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetNameChangeHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetNameChangeAzLaMiNmWvHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetNameChangeALHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExpiredCertificateMDHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExpiredCertificateALHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExpiredCertificateTNHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExamFXCourseRenewalHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetStateExamExceptionHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetApplicationRequiredWIHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetExamScheduledNDHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetFingerprintScheduledWVHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetCreditMembershipSalesTrainingHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetNonCreditTrainingHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetADBankerRegistrationFLHTML(int vEmploymentID);
        //public Tuple<string, string, string, string> GetExamFXRegistrationHealthHTML(int vEmploymentID);
        //public Tuple<string, string, string, string> GetExamFXRegistrationLifeAndHealthHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetPLSLicensingIncentiveExpiredHTML(int vEmploymentID);
        public Tuple<string, string, string, string> GetLifeHealthPLSHTML(int vEmploymentID);
        //public Tuple<string, string, string, string> GetLifeHealthPLSPlusHTML(int vEmploymentID);
        //public Tuple<string, string, string, string> GetBackgroundDisclosureLinkCAHTML(int vEmploymentID);
        //public Tuple<string, string, string, string> GetPLSSrAmDLSIncentiveExpiredHTML(int vEmploymentID);
        //public Tuple<string, string, string, string> GetADBankerRegistrationHealthHTML(int vEmploymentID);
        //public Tuple<string, string, string, string> GetADBankerRegistrationLifeHTML(int vEmploymentID);
        //public Tuple<string, string, string, string> GetADBankerRegistrationLifeHealthHTML(int vEmploymentID);
    }
}
