﻿namespace OneTrak_v2.DataModel
{
    public class IputAddLicenseExam
    {
        public int LicenseID { get; set; }
        public int ExamID { get; set; }
        public string UserSOEID { get; set; }
    }

    public class IputDeleteLicenseExam
    {
        public int LicenseExamID { get; set; }
        public int LicenseID { get; set; }
        public string UserSOEID { get; set; }
    }
}
