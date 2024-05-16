using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData.Models;
using OneTrack_v2.DataModel;
using OneTrak_v2.DataModel;

namespace OneTrack_v2.DbData
{
    public partial class AppDataContext : DbContext
    {
        public AppDataContext()
        {
        }

        public AppDataContext(DbContextOptions<AppDataContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AgentExamActivity> AgentExamActivities { get; set; }
        public virtual DbSet<AgentMaster> AgentMasters { get; set; }
        public virtual DbSet<AgentStateTable> AgentStateTables { get; set; }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<AuditLogCe> AuditLogCes { get; set; }
        public virtual DbSet<Bif> Bifs { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<CombinedEmpLicType> CombinedEmpLicTypes { get; set; }
        public virtual DbSet<CombinedEmpLicTypeOld> CombinedEmpLicTypeOlds { get; set; }
        public virtual DbSet<Communication> Communications { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyRequirement> CompanyRequirements { get; set; }
        public virtual DbSet<ContEducation> ContEducations { get; set; }
        public virtual DbSet<ContEducationRequirement> ContEducationRequirements { get; set; }
        public virtual DbSet<ContEducationRequirementHistory> ContEducationRequirementHistories { get; set; }
        public virtual DbSet<Diary> Diaries { get; set; }
        public virtual DbSet<EducationRule> EducationRules { get; set; }
        public virtual DbSet<EducationRuleCriterion> EducationRuleCriteria { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeAppointment> EmployeeAppointments { get; set; }
        public virtual DbSet<EmployeeContEducation> EmployeeContEducations { get; set; }
        public virtual DbSet<EmployeeLicense> EmployeeLicenses { get; set; }
        public virtual DbSet<EmployeeLicensePreExam> EmployeeLicensePreExams { get; set; }
        public virtual DbSet<EmployeeLicenseePreEducation> EmployeeLicenseePreEducations { get; set; }
        public virtual DbSet<EmployeeSsn> EmployeeSsns { get; set; }
        public virtual DbSet<Employment> Employments { get; set; }
        public virtual DbSet<EmploymentCommunication> EmploymentCommunications { get; set; }
        public virtual DbSet<EmploymentCommunicationDetail> EmploymentCommunicationDetails { get; set; }
        public virtual DbSet<EmploymentCompanyRequirement> EmploymentCompanyRequirements { get; set; }
        public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }
        public virtual DbSet<EmploymentJobTitle> EmploymentJobTitles { get; set; }
        public virtual DbSet<EmploymentLicenseIncentive> EmploymentLicenseIncentives { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<LearningAsset> LearningAssets { get; set; }
        public virtual DbSet<Letter> Letters { get; set; }
        public virtual DbSet<LettersGenerated> LettersGenerateds { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<LicenseApplication> LicenseApplications { get; set; }
        public virtual DbSet<LicenseCompany> LicenseCompanies { get; set; }
        public virtual DbSet<LicenseExam> LicenseExams { get; set; }
        public virtual DbSet<LicensePreEducation> LicensePreEducations { get; set; }
        public virtual DbSet<LicenseProduct> LicenseProducts { get; set; }
        public virtual DbSet<LicenseTech> LicenseTeches { get; set; }
        public virtual DbSet<LineOfAuthority> LineOfAuthorities { get; set; }
        public virtual DbSet<LkpTypeStatus> LkpTypeStatuses { get; set; }
        public virtual DbSet<NewHirePackage> NewHirePackages { get; set; }
        public virtual DbSet<PreEducation> PreEducations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<RequiredLicense> RequiredLicenses { get; set; }
        public virtual DbSet<StateProvince> StateProvinces { get; set; }
        public virtual DbSet<StgActive> StgActives { get; set; }
        public virtual DbSet<StgAgentStateTable> StgAgentStateTables { get; set; }
        public virtual DbSet<StgContEducationRequirement> StgContEducationRequirements { get; set; }
        public virtual DbSet<StgEmploymentCompanyRequirement> StgEmploymentCompanyRequirements { get; set; }
        public virtual DbSet<StgHrx> StgHrxes { get; set; }
        public virtual DbSet<StgLettersGenerated> StgLettersGenerateds { get; set; }
        public virtual DbSet<StgOtincentive> StgOtincentives { get; set; }
        public virtual DbSet<StgTerm> StgTerms { get; set; }
        public virtual DbSet<Sysdtslog90> Sysdtslog90s { get; set; }
        public virtual DbSet<TempPeopleFailure> TempPeopleFailures { get; set; }
        public virtual DbSet<Tickler> Ticklers { get; set; }
        public virtual DbSet<TransferHistory> TransferHistories { get; set; }
        public virtual DbSet<VwAddress> VwAddresses { get; set; }
        public virtual DbSet<VwAddressTransformation> VwAddressTransformations { get; set; }
        public virtual DbSet<VwAgentExamActivity> VwAgentExamActivities { get; set; }
        public virtual DbSet<VwAgentMaster> VwAgentMasters { get; set; }
        public virtual DbSet<VwAgentStateTable> VwAgentStateTables { get; set; }
        public virtual DbSet<VwCommunication> VwCommunications { get; set; }
        public virtual DbSet<VwCompany> VwCompanies { get; set; }
        public virtual DbSet<VwCompanyRequirement> VwCompanyRequirements { get; set; }
        public virtual DbSet<VwContEducationRequirement> VwContEducationRequirements { get; set; }
        public virtual DbSet<VwContEducationRequirementHistory> VwContEducationRequirementHistories { get; set; }
        public virtual DbSet<VwDiary> VwDiaries { get; set; }
        public virtual DbSet<VwEducationRule> VwEducationRules { get; set; }
        public virtual DbSet<VwEducationRuleCriterion> VwEducationRuleCriteria { get; set; }
        public virtual DbSet<VwEmployee> VwEmployees { get; set; }
        public virtual DbSet<VwEmployeeAppointment> VwEmployeeAppointments { get; set; }
        public virtual DbSet<VwEmployeeContEducation> VwEmployeeContEducations { get; set; }
        public virtual DbSet<VwEmployeeLicense> VwEmployeeLicenses { get; set; }
        public virtual DbSet<VwEmployeeLicensePreExam> VwEmployeeLicensePreExams { get; set; }
        public virtual DbSet<VwEmployeeLicenseePreEducation> VwEmployeeLicenseePreEducations { get; set; }
        public virtual DbSet<VwEmployeeSsn> VwEmployeeSsns { get; set; }
        public virtual DbSet<VwEmployment> VwEmployments { get; set; }
        public virtual DbSet<VwEmploymentCommunication> VwEmploymentCommunications { get; set; }
        public virtual DbSet<VwEmploymentCommunicationDetail> VwEmploymentCommunicationDetails { get; set; }
        public virtual DbSet<VwEmploymentCompanyRequirement> VwEmploymentCompanyRequirements { get; set; }
        public virtual DbSet<VwEmploymentHistory> VwEmploymentHistories { get; set; }
        public virtual DbSet<VwEmploymentJobTitle> VwEmploymentJobTitles { get; set; }
        public virtual DbSet<VwEmploymentLicenseIncentive> VwEmploymentLicenseIncentives { get; set; }
        public virtual DbSet<VwExam> VwExams { get; set; }
        public virtual DbSet<VwJobTitle> VwJobTitles { get; set; }
        public virtual DbSet<VwLearningAsset> VwLearningAssets { get; set; }
        public virtual DbSet<VwLicense> VwLicenses { get; set; }
        public virtual DbSet<VwLicenseApplication> VwLicenseApplications { get; set; }
        public virtual DbSet<VwLicenseByStateTransformation> VwLicenseByStateTransformations { get; set; }
        public virtual DbSet<VwLicenseCompany> VwLicenseCompanies { get; set; }
        public virtual DbSet<VwLicenseExam> VwLicenseExams { get; set; }
        public virtual DbSet<VwLicensePreEducation> VwLicensePreEducations { get; set; }
        public virtual DbSet<VwLicenseProduct> VwLicenseProducts { get; set; }
        public virtual DbSet<VwLicenseTech> VwLicenseTeches { get; set; }
        public virtual DbSet<VwLicenseTransformation> VwLicenseTransformations { get; set; }
        public virtual DbSet<VwLineOfAuthority> VwLineOfAuthorities { get; set; }
        public virtual DbSet<VwNewHirePackage> VwNewHirePackages { get; set; }
        public virtual DbSet<VwPreEducation> VwPreEducations { get; set; }
        public virtual DbSet<VwProduct> VwProducts { get; set; }
        public virtual DbSet<VwRequiredLicense> VwRequiredLicenses { get; set; }
        public virtual DbSet<VwStateProvince> VwStateProvinces { get; set; }
        public virtual DbSet<VwTickler> VwTicklers { get; set; }
        public virtual DbSet<VwTransferHistory> VwTransferHistories { get; set; }
        public virtual DbSet<WorkList> WorkLists { get; set; }
        public virtual DbSet<WorkListDatum> WorkListData { get; set; }
        public virtual DbSet<XxxEdr> XxxEdrs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=FTSQLDVLP2;Database=License;Trusted_Connection=True;");

        #region STORED PROCEDURES
        //public virtual DbSet<OputEmployeeSearchResult> SPOUT_SearchEmployees { get; set; }
        //public virtual DbSet<UspAgentDetails_Result> UspAgentDetails_Results { get; set; }

        #endregion

        #region // Raw SQL Queries
        public virtual DbSet<OputEmployeeSearchResult> OputEmployeeSearchResult { get; set; }
        public virtual DbSet<OputAgentHiearchy> OputAgentHiearchy { get; set; }
        public virtual DbSet<OputVarDropDownList> OputVarDropDownList { get; set; }
        public virtual DbSet<OputVarDropDownList_v2> OputVarDropDownList_v2 { get; set; }
        public virtual DbSet<OputLicenseIncentiveInfo> OputLicenseIncentiveInfo { get; set; }
        public virtual DbSet<OputIncentiveRolloutGroup> OputIncentiveRolloutGroup { get; set; }
        public virtual DbSet<OputIncentiveBMMgr> OputIncentiveBMMgr { get; set; }
        public virtual DbSet<OputIncentiveTechName> OputIncentiveTechName { get; set; }
        public virtual DbSet<OputIncentiveDMMgr> OputIncentiveDMMgr { get; set; }
        public virtual DbSet<OputStockTickler> OputStockTickler { get; set; }
        public virtual DbSet<OputCompanyRequirement> OputCompanyRequirements { get; set; }
        public virtual DbSet<OputEducationRule> OputEducationRules { get; set; }
        public virtual DbSet<OputStateProvince> OputStateProvince { get; set; }
        public virtual DbSet<OputWorkListDataItem> OputWorkListDataItems { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.AddressId).HasName("PK__Address__0D0FEE32");

                entity.ToTable("Address", "dbo");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");
                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.AddressType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Country)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.Fax)
                    .HasMaxLength(13)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);
                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Zip)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<AgentExamActivity>(entity =>
            {
                entity.HasKey(e => new { e.ExamCode, e.ProductCode });

                entity.ToTable("AgentExamActivity", "dbo");

                entity.Property(e => e.ExamCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.ProductCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.AgentEmail)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.AgentName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.AgentPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.BestSyescore).HasColumnName("BestSYEScore");
                entity.Property(e => e.CourseExpirationDate).HasColumnType("datetime");
                entity.Property(e => e.Date100PctComplete).HasColumnType("date");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.FirstAccess).HasColumnType("datetime");
                entity.Property(e => e.LastLogin).HasColumnType("datetime");
                entity.Property(e => e.LicenseState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ManagerName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.Registered).HasColumnType("datetime");
                entity.Property(e => e.ResidentState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.TimeInCourse)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Time_in_Course");
                entity.Property(e => e.TimeInReading)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.TimeInStudyByTopicQuizzes)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.TimeInSye)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TimeInSYE");
            });
            modelBuilder.Entity<AgentMaster>(entity =>
            {
                entity.ToTable("AgentMaster", "dbo");

                entity.Property(e => e.AgentMasterId).HasColumnName("AgentMasterID");
                entity.Property(e => e.ChangeDate).HasColumnType("datetime");
                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseCurrentDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseExpirationDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseOriginalDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LineOfAuthorityAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ResidentCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<AgentStateTable>(entity =>
            {
                entity.ToTable("AgentStateTable", "dbo");

                entity.Property(e => e.AgentStateTableId).HasColumnName("AgentStateTableID");
                entity.Property(e => e.ChangeDate).HasColumnType("datetime");
                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseCurrentDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseExpirationDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseOriginalDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LineOfAuthorityAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ResidentCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(e => e.AuditLogId).HasName("PK__AuditLog__13BCEBC1");

                entity.ToTable("AuditLog", "dbo");

                entity.Property(e => e.AuditLogId).HasColumnName("AuditLogID");
                entity.Property(e => e.AuditAction)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.AuditFieldName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.AuditValueAfter)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.AuditValueBefore)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.BaseTableKeyValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.BaseTableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Geid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("GEID");
                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<AuditLogCe>(entity =>
            {
                entity.HasKey(e => e.AuditLogId).HasName("PK_AuditLog");

                entity.ToTable("AuditLogCE", "dbo");

                entity.Property(e => e.AuditLogId).HasColumnName("AuditLogID");
                entity.Property(e => e.EducationRule).IsUnicode(false);
                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.ExemptionRule).IsUnicode(false);
                entity.Property(e => e.ProcessName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.ProcessStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.StartDate).HasColumnType("datetime");
                entity.Property(e => e.StepName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Bif>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("BIF", "dbo");

                entity.Property(e => e.Address1)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Address2)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Bifid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("BIFID");
                entity.Property(e => e.City)
                    .HasMaxLength(21)
                    .IsUnicode(false);
                entity.Property(e => e.CloseDate)
                    .HasMaxLength(9)
                    .IsUnicode(false);
                entity.Property(e => e.Fax)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.HrDepartmentId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("HR_Department_ID");
                entity.Property(e => e.Name)
                    .HasMaxLength(55)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.ScoreNumber)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.UpdateDate).HasColumnType("datetime");
                entity.Property(e => e.ZipCode)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("Zip_Code");
            });
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.BranchCode).HasName("PK__Branch__72E607DB");

                entity.ToTable("Branch", "dbo");

                entity.HasIndex(e => e.Edr, "IDX_Branch_EDR");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.AcquisitionBranch)
                    .HasMaxLength(4)
                    .IsUnicode(false);
                entity.Property(e => e.AcquisitionIndDesc)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.AcquisitionIndicator)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.AcquisitionState)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.ActionLoanInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ActionbankInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Boxlabel)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                entity.Property(e => e.ClosedDate)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                entity.Property(e => e.CountryId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CountryID");
                entity.Property(e => e.CustomerPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.Edr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EDR");
                entity.Property(e => e.FacilityCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("facilityCode");
                entity.Property(e => e.FaxNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.InternalPhone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.LuType1)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                entity.Property(e => e.MaestroDesc)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.MaestroStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ManagerNamefirst)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.ManagerNamelast)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.ManagerNamemIddle)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ManagerNamemIDdle");
                entity.Property(e => e.ManagerNamematernal)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.OfficeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.OpenClosedInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.OpenClosedIndDesc)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.OpenDate)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                entity.Property(e => e.PoState)
                    .HasMaxLength(5)
                    .IsUnicode(false);
                entity.Property(e => e.Poboxdrawer)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.Pocity)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Pozip)
                    .HasMaxLength(9)
                    .IsUnicode(false);
                entity.Property(e => e.PuId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("PuID");
                entity.Property(e => e.StreetAddress1)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.StreetAddress2)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.StreetAddress3)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.StreetState)
                    .HasMaxLength(5)
                    .IsUnicode(false);
                entity.Property(e => e.Streetcity)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Streetzip)
                    .HasMaxLength(9)
                    .IsUnicode(false);
                entity.Property(e => e.SubType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.SubTypeDesc)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.SupervisoryCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.SupervisoryCodeDesc)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.TbranTerminalType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.TbranTypeInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.TypeDescription)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.TypeforNetworkComm)
                    .HasMaxLength(3)
                    .IsUnicode(false);
                entity.Property(e => e.UpDateDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("upDateDate");
                entity.Property(e => e.UpDateuserId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("upDateuserID");
                entity.Property(e => e.UrbanDesc)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.UrbanInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.VpsDest)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.EdrNavigation).WithMany(p => p.Branches)
                    .HasForeignKey(d => d.Edr)
                    .HasConstraintName("FK__Branch__EDR__7993056A");
            });
            modelBuilder.Entity<CombinedEmpLicType>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("CombinedEmpLicType", "dbo");

                entity.Property(e => e.AEmployeeNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("A_EmployeeNumber");
                entity.Property(e => e.ALicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("A_LicenseStateCode");
                entity.Property(e => e.ALicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("A_LicenseStatus");
                entity.Property(e => e.ALicenseTypeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("A_LicenseTypeCode");
                entity.Property(e => e.CEmployeeNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("C_EmployeeNumber");
                entity.Property(e => e.CLicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("C_LicenseStateCode");
                entity.Property(e => e.CLicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("C_LicenseStatus");
                entity.Property(e => e.CLicenseTypeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("C_LicenseTypeCode");
                entity.Property(e => e.CTerminationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("C_TerminationDate");
                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseTypeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.OEmployeeNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("O_EmployeeNumber");
                entity.Property(e => e.OLicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("O_LicenseStateCode");
                entity.Property(e => e.OLicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("O_LicenseStatus");
                entity.Property(e => e.OLicenseTypeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("O_LicenseTypeCode");
                entity.Property(e => e.OTerminationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("O_TerminationDate");
            });
            modelBuilder.Entity<CombinedEmpLicTypeOld>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("CombinedEmpLicTypeOld", "dbo");

                entity.Property(e => e.AEmployeeNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("A_EmployeeNumber");
                entity.Property(e => e.ALicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("A_LicenseStateCode");
                entity.Property(e => e.ALicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("A_LicenseStatus");
                entity.Property(e => e.ALicenseTypeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("A_LicenseTypeCode");
                entity.Property(e => e.CEmployeeNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("C_EmployeeNumber");
                entity.Property(e => e.CLicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("C_LicenseStateCode");
                entity.Property(e => e.CLicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("C_LicenseStatus");
                entity.Property(e => e.CLicenseTypeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("C_LicenseTypeCode");
                entity.Property(e => e.CTerminationDate)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("C_TerminationDate");
                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseTypeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.OEmployeeNumber)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("O_EmployeeNumber");
                entity.Property(e => e.OLicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("O_LicenseStateCode");
                entity.Property(e => e.OLicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("O_LicenseStatus");
                entity.Property(e => e.OLicenseTypeCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("O_LicenseTypeCode");
                entity.Property(e => e.OTerminationDate)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("O_TerminationDate");
            });
            modelBuilder.Entity<Communication>(entity =>
            {
                entity.ToTable("Communications", "dbo");

                entity.Property(e => e.CommunicationId).HasColumnName("CommunicationID");
                entity.Property(e => e.CommunicationName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.DocAppType)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.DocSubType)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.DocType)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.DocTypeAbv)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.EmailAttachments).IsUnicode(false);
            });
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId).HasName("PK__Company__40E497F3");

                entity.ToTable("Company", "dbo");

                entity.HasIndex(e => e.AddressId, "IDX_Company_AddressID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.AddressId).HasColumnName("AddressID");
                entity.Property(e => e.CompanyAbv)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.CompanyType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.Naicnumber).HasColumnName("NAICNumber");
                entity.Property(e => e.Tin).HasColumnName("TIN");
                entity.Property(e => e.XxxIsActive)
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("xxxIsActive");

                entity.HasOne(d => d.Address).WithMany(p => p.Companies)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__Company__Address__41D8BC2C");
            });
            modelBuilder.Entity<CompanyRequirement>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("CompanyRequirements", "dbo");

                entity.Property(e => e.CompanyRequirementId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CompanyRequirementID");
                entity.Property(e => e.Document).IsUnicode(false);
                entity.Property(e => e.RequirementType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.ResStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.StartAfterDate).HasColumnType("datetime");
                entity.Property(e => e.WorkStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<ContEducation>(entity =>
            {
                entity.HasKey(e => e.ContEducationId).HasName("PK__ContEducation__049AA3C2");

                entity.ToTable("ContEducation", "dbo");

                entity.Property(e => e.ContEducationId).HasColumnName("ContEducationID");
                entity.Property(e => e.DeliveryMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EducationName)
                    .HasMaxLength(75)
                    .IsUnicode(false);
                entity.Property(e => e.EducationProviderId).HasColumnName("EducationProviderID");
                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
                entity.Property(e => e.ExpireDate).HasColumnType("datetime");
                entity.Property(e => e.Fees).HasColumnType("money");
                entity.Property(e => e.Topic)
                    .HasMaxLength(75)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<ContEducationRequirement>(entity =>
            {
                entity.HasKey(e => e.ContEducationRequirementId).HasName("PK__ContEducationReq__0682EC34");

                entity.ToTable("ContEducationRequirement", "dbo");

                entity.HasIndex(e => e.EmploymentId, "IDX_ContEducationRequirement_EmploymentID");

                entity.HasIndex(e => e.EmploymentId, "XIF1ContEducationRequirement");

                entity.Property(e => e.ContEducationRequirementId).HasColumnName("ContEducationRequirementID");
                entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
                entity.Property(e => e.EducationStartDate).HasColumnType("datetime");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.RequiredCreditHours).HasColumnType("numeric(9, 2)");

                entity.HasOne(d => d.Employment).WithMany(p => p.ContEducationRequirements)
                    .HasForeignKey(d => d.EmploymentId)
                    .HasConstraintName("FK__ContEduca__Emplo__7EECB764");
            });
            modelBuilder.Entity<ContEducationRequirementHistory>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("ContEducationRequirement_History", "dbo");

                entity.Property(e => e.ContEducationRequirementId).HasColumnName("ContEducationRequirementID");
                entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
                entity.Property(e => e.EducationStartDate).HasColumnType("datetime");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.HistoryDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.RequiredCreditHours).HasColumnType("numeric(9, 2)");
            });
            modelBuilder.Entity<Diary>(entity =>
            {
                entity.HasKey(e => e.DiaryId).HasName("PK__Diary__1372D2FE");

                entity.ToTable("Diary", "dbo");

                entity.HasIndex(e => e.EmploymentId, "IDX_Diary_EmploymentID");

                entity.Property(e => e.DiaryId).HasColumnName("DiaryID");
                entity.Property(e => e.DiaryDate).HasColumnType("datetime");
                entity.Property(e => e.DiaryName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.Soeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SOEID");

                entity.HasOne(d => d.Employment).WithMany(p => p.Diaries)
                    .HasForeignKey(d => d.EmploymentId)
                    .HasConstraintName("FK__Diary__Employmen__7DF8932B");
            });
            modelBuilder.Entity<EducationRule>(entity =>
            {
                entity.HasKey(e => e.EducationRuleId).HasName("PK__EducationRule__522F1F86");

                entity.ToTable("EducationRule", "dbo");

                entity.HasIndex(e => e.EducationEndDateId, "IDX_EducationRule_EducationEndDateID");

                entity.HasIndex(e => e.EducationStartDateId, "IDX_EducationRule_EducationStartDateID");

                entity.Property(e => e.EducationRuleId).HasColumnName("EducationRuleID");
                entity.Property(e => e.EducationEndDateId).HasColumnName("EducationEndDateID");
                entity.Property(e => e.EducationStartDateId).HasColumnName("EducationStartDateID");
                entity.Property(e => e.ExceptionId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ExceptionID");
                entity.Property(e => e.ExemptionId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ExemptionID");
                entity.Property(e => e.LicenseType)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.RequiredCreditHours).HasColumnType("numeric(9, 2)");
                entity.Property(e => e.StateProvince)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.EducationEndDate).WithMany(p => p.EducationRuleEducationEndDates)
                    .HasForeignKey(d => d.EducationEndDateId)
                    .HasConstraintName("FK__Education__Educa__5BB889C0");

                entity.HasOne(d => d.EducationStartDate).WithMany(p => p.EducationRuleEducationStartDates)
                    .HasForeignKey(d => d.EducationStartDateId)
                    .HasConstraintName("FK__Education__Educa__5AC46587");
            });
            modelBuilder.Entity<EducationRuleCriterion>(entity =>
            {
                entity.HasKey(e => e.EducationCriteriaId).HasName("PK__EducationRuleCri__322C6448");

                entity.ToTable("EducationRuleCriteria", "dbo");

                entity.Property(e => e.EducationCriteriaId).HasColumnName("EducationCriteriaID");
                entity.Property(e => e.Criteria)
                    .HasMaxLength(7000)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.UsageType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__2E70E1FD");

                entity.ToTable("Employee", "dbo");

                entity.HasIndex(e => e.AddressId, "IDX_Employee_AddressID");

                entity.HasIndex(e => e.EmployeeSsnid, "IDX_Employee_EmployeeSSNID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.AddressId).HasColumnName("AddressID");
                entity.Property(e => e.Alias)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
                entity.Property(e => e.EmployeeSsnid).HasColumnName("EmployeeSSNID");
                entity.Property(e => e.ExcludeFromRpts).HasDefaultValueSql("((0))");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Geid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("GEID");
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LegalHold)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PurgeDate).HasColumnType("datetime");
                entity.Property(e => e.Soeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SOEID");
                entity.Property(e => e.Suffix)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Urccode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("URCCode");

                entity.HasOne(d => d.Address).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Addres__66B53B20");

                entity.HasOne(d => d.EmployeeSsn).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeSsnid)
                    .HasConstraintName("FK__Employee__Employ__67A95F59");
            });
            modelBuilder.Entity<EmployeeAppointment>(entity =>
            {
                entity.HasKey(e => e.EmployeeAppointmentId).IsClustered(false);

                entity.ToTable("EmployeeAppointment", "dbo");

                entity.HasIndex(e => e.AppointmentStatus, "IDX_EmployeeAppointment_AppointmentStatus");

                entity.HasIndex(e => e.CompanyId, "IDX_EmployeeAppointment_CompanyID");

                entity.HasIndex(e => new { e.EmployeeLicenseId, e.CompanyId }, "IDX_EmployeeAppointment_EmployeeLicenseID_CompanyID").IsClustered();

                entity.HasIndex(e => e.CompanyId, "XIF3EmployeeAppointment");

                entity.Property(e => e.EmployeeAppointmentId).HasColumnName("EmployeeAppointmentID");
                entity.Property(e => e.AppointmentEffectiveDate).HasColumnType("datetime");
                entity.Property(e => e.AppointmentExpireDate).HasColumnType("datetime");
                entity.Property(e => e.AppointmentStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.AppointmentTerminationDate).HasColumnType("datetime");
                entity.Property(e => e.CarrierDate).HasColumnType("datetime");
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.RetentionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company).WithMany(p => p.EmployeeAppointments)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__EmployeeA__Compa__43C1049E");

                entity.HasOne(d => d.EmployeeLicense).WithMany(p => p.EmployeeAppointments)
                    .HasForeignKey(d => d.EmployeeLicenseId)
                    .HasConstraintName("FK__EmployeeA__Emplo__2E5BD364");
            });
            modelBuilder.Entity<EmployeeContEducation>(entity =>
            {
                entity.HasKey(e => e.EmployeeEducationId).HasName("PK__EmployeeContEduc__086B34A6");

                entity.ToTable("EmployeeContEducation", "dbo");

                entity.HasIndex(e => e.ContEducationId, "IDX_EmployeeContEducation_ContEducationID");

                entity.HasIndex(e => e.ContEducationRequirementId, "IDX_EmployeeContEducation_ContEducationRequirementID");

                entity.Property(e => e.EmployeeEducationId).HasColumnName("EmployeeEducationID");
                entity.Property(e => e.AdditionalNotes).IsUnicode(false);
                entity.Property(e => e.ContEducationId).HasColumnName("ContEducationID");
                entity.Property(e => e.ContEducationRequirementId).HasColumnName("ContEducationRequirementID");
                entity.Property(e => e.ContEducationTakenDate).HasColumnType("datetime");
                entity.Property(e => e.CreditHoursTaken).HasColumnType("numeric(9, 2)");
                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.ContEducation).WithMany(p => p.EmployeeContEducations)
                    .HasForeignKey(d => d.ContEducationId)
                    .HasConstraintName("FK__EmployeeC__ContE__11007AA7");

                entity.HasOne(d => d.ContEducationRequirement).WithMany(p => p.EmployeeContEducations)
                    .HasForeignKey(d => d.ContEducationRequirementId)
                    .HasConstraintName("FK__EmployeeC__ContE__100C566E");
            });
            modelBuilder.Entity<EmployeeLicense>(entity =>
            {
                entity.HasKey(e => e.EmployeeLicenseId).HasName("PK__EmployeeLicense__2B7F66B9");

                entity.ToTable("EmployeeLicense", "dbo");

                entity.HasIndex(e => e.EmploymentId, "IDX_EmployeeLicense_EmploymentID");

                entity.HasIndex(e => e.LicenseId, "IDX_EmployeeLicense_LicenseID");

                entity.HasIndex(e => e.LicenseStatus, "IDX_EmployeeLicense_LicenseStatus");

                entity.HasIndex(e => e.EmploymentId, "XIF2EmployeeLicense");

                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.AscEmployeeLicenseId).HasColumnName("AscEmployeeLicenseID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.LicenseEffectiveDate).HasColumnType("datetime");
                entity.Property(e => e.LicenseExpireDate).HasColumnType("datetime");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
                entity.Property(e => e.LicenseIssueDate).HasColumnType("datetime");
                entity.Property(e => e.LicenseNote)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.LineOfAuthorityIssueDate).HasColumnType("datetime");
                entity.Property(e => e.NonResident).HasDefaultValueSql("((0))");
                entity.Property(e => e.Reinstatement).HasDefaultValueSql("((0))");
                entity.Property(e => e.Required).HasDefaultValueSql("((0))");
                entity.Property(e => e.RetentionDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employment).WithMany(p => p.EmployeeLicenses)
                    .HasForeignKey(d => d.EmploymentId)
                    .HasConstraintName("FK__EmployeeL__Emplo__7D046EF2");

                entity.HasOne(d => d.License).WithMany(p => p.EmployeeLicenses)
                    .HasForeignKey(d => d.LicenseId)
                    .HasConstraintName("FK__EmployeeL__Licen__4BA21D88");
            });
            modelBuilder.Entity<EmployeeLicensePreExam>(entity =>
            {
                entity.ToTable("EmployeeLicensePreExams", "dbo");

                entity.Property(e => e.EmployeeLicensePreExamId).HasColumnName("EmployeeLicensePreExamID");
                entity.Property(e => e.AdditionalNotes).IsUnicode(false);
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.ExamId).HasColumnName("ExamID");
                entity.Property(e => e.ExamScheduleDate).HasColumnType("datetime");
                entity.Property(e => e.ExamTakenDate).HasColumnType("datetime");
                entity.Property(e => e.Status)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<EmployeeLicenseePreEducation>(entity =>
            {
                entity.HasKey(e => e.EmployeeLicensePreEducationId);

                entity.ToTable("EmployeeLicenseePreEducation", "dbo");

                entity.Property(e => e.EmployeeLicensePreEducationId).HasColumnName("EmployeeLicensePreEducationID");
                entity.Property(e => e.AdditionalNotes).IsUnicode(false);
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
                entity.Property(e => e.EducationStartDate).HasColumnType("datetime");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.PreEducationId).HasColumnName("PreEducationID");
                entity.Property(e => e.Status)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company).WithMany(p => p.EmployeeLicenseePreEducations)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK_EmployeeLicensePreEducation_Company");

                entity.HasOne(d => d.EmployeeLicense).WithMany(p => p.EmployeeLicenseePreEducations)
                    .HasForeignKey(d => d.EmployeeLicenseId)
                    .HasConstraintName("FK_EmployeeLicensePreEducation_Employment");

                entity.HasOne(d => d.PreEducation).WithMany(p => p.EmployeeLicenseePreEducations)
                    .HasForeignKey(d => d.PreEducationId)
                    .HasConstraintName("FK_EmployeeLicensePreEducation_PreEducation");
            });
            modelBuilder.Entity<EmployeeSsn>(entity =>
            {
                entity.HasKey(e => e.EmployeeSsnid).HasName("PK__EmployeeSSN__361203C5");

                entity.ToTable("EmployeeSSN", "dbo");

                entity.Property(e => e.EmployeeSsnid).HasColumnName("EmployeeSSNID");
                entity.Property(e => e.EmployeeSsn1)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeSSN");
            });
            modelBuilder.Entity<Employment>(entity =>
            {
                entity.HasKey(e => e.EmploymentId).HasName("PK__Employment__7192BC46");

                entity.ToTable("Employment", "dbo");

                entity.HasIndex(e => e.EmployeeStatus, "IDX_Employment_EmployeeStatus");

                entity.HasIndex(e => e.EmployeeId, "IDX_NC_EmployeeID");

                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.Cerequired).HasColumnName("CERequired");
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.DirRptMgrTmnum)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DirRptMgrTMNum");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EmployeeStatus)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.H1employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H1EmploymentID");
                entity.Property(e => e.H2employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H2EmploymentID");
                entity.Property(e => e.H3employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H3EmploymentID");
                entity.Property(e => e.H4employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H4EmploymentID");
                entity.Property(e => e.H5employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H5EmploymentID");
                entity.Property(e => e.H6employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H6EmploymentID");
                entity.Property(e => e.IsRehire).HasDefaultValueSql("((0))");
                entity.Property(e => e.JobCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);
                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseIncentive)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NoIncentive')");
                entity.Property(e => e.LicenseLevel)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('{NeedsReview}')");
                entity.Property(e => e.RetentionDate).HasColumnType("datetime");
                entity.Property(e => e.Tmtype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TMType");
                entity.Property(e => e.WorkPhone)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company).WithMany(p => p.Employments)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__Employmen__Compa__783FB9D5");

                entity.HasOne(d => d.Employee).WithMany(p => p.Employments)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employmen__Emplo__7933DE0E");
            });
            modelBuilder.Entity<EmploymentCommunication>(entity =>
            {
                entity.ToTable("EmploymentCommunications", "dbo");

                entity.Property(e => e.EmploymentCommunicationId).HasColumnName("EmploymentCommunicationID");
                entity.Property(e => e.CommunicationId).HasColumnName("CommunicationID");
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.CompareXml)
                    .IsUnicode(false)
                    .HasColumnName("CompareXML");
                entity.Property(e => e.EmailAttachments).IsUnicode(false);
                entity.Property(e => e.EmailBodyHtml)
                    .IsUnicode(false)
                    .HasColumnName("EmailBodyHTML");
                entity.Property(e => e.EmailCreateDate).HasColumnType("datetime");
                entity.Property(e => e.EmailCreator)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EmailFrom)
                    .HasMaxLength(8000)
                    .IsUnicode(false);
                entity.Property(e => e.EmailSentDate).HasColumnType("datetime");
                entity.Property(e => e.EmailSubject)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.EmailTo)
                    .HasMaxLength(8000)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
            });
            modelBuilder.Entity<EmploymentCommunicationDetail>(entity =>
            {
                entity.ToTable("EmploymentCommunicationDetails", "dbo");

                entity.Property(e => e.EmploymentCommunicationDetailId).HasColumnName("EmploymentCommunicationDetailID");
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.CompanyRequirementId).HasColumnName("CompanyRequirementID");
                entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
                entity.Property(e => e.EmployeeAppointmentId).HasColumnName("EmployeeAppointmentID");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmploymentCommunicationId).HasColumnName("EmploymentCommunicationID");
                entity.Property(e => e.HireDate).HasColumnType("datetime");
                entity.Property(e => e.LicenseExpireDate).HasColumnType("datetime");
                entity.Property(e => e.RehireDate).HasColumnType("datetime");
                entity.Property(e => e.RequiredCreditHours).HasColumnType("numeric(9, 2)");
                entity.Property(e => e.SentToAgentDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<EmploymentCompanyRequirement>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("EmploymentCompanyRequirements", "dbo");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("asset_id");
                entity.Property(e => e.AssetSk).HasColumnName("asset_sk");
                entity.Property(e => e.EmploymentCompanyRequirementId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmploymentCompanyRequirementID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.LearningProgramCompletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("learning_program_completion_date");
                entity.Property(e => e.LearningProgramEnrollmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("learning_program_enrollment_date");
                entity.Property(e => e.LearningProgramStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("learning_program_status");
            });
            modelBuilder.Entity<EmploymentHistory>(entity =>
            {
                entity.HasKey(e => e.EmploymentHistoryId).HasName("PK__EmploymentHistor__75634D2A");

                entity.ToTable("EmploymentHistory", "dbo");

                entity.HasIndex(e => e.EmploymentId, "IDX_NC_EmploymentID");

                entity.Property(e => e.EmploymentHistoryId).HasColumnName("EmploymentHistoryID");
                entity.Property(e => e.BackGroundCheckNotes)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.BackgroundCheckStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((0))");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.ForCause).HasDefaultValueSql("((0))");
                entity.Property(e => e.HireDate).HasColumnType("datetime");
                entity.Property(e => e.HrtermCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("HRTermCode");
                entity.Property(e => e.HrtermDate)
                    .HasColumnType("datetime")
                    .HasColumnName("HRTermDate");
                entity.Property(e => e.NotifiedTermDate).HasColumnType("datetime");
                entity.Property(e => e.RehireDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employment).WithMany(p => p.EmploymentHistories)
                    .HasForeignKey(d => d.EmploymentId)
                    .HasConstraintName("FK__Employmen__Emplo__7A280247");
            });
            modelBuilder.Entity<EmploymentJobTitle>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("EmploymentJobTitle", "dbo");

                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.EmploymentJobTitleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmploymentJobTitleID");
                entity.Property(e => e.JobTitleDate).HasColumnType("datetime");
                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");
            });
            modelBuilder.Entity<EmploymentLicenseIncentive>(entity =>
            {
                entity.HasKey(e => e.EmploymentLicenseIncentiveId).HasName("PK__Employme__103B5E108FD72AFC");

                entity.ToTable("EmploymentLicenseIncentive", "dbo");

                entity.HasIndex(e => e.EmployeeLicenseId, "UQ__Employme__968482E4C116D84C").IsUnique();

                entity.Property(e => e.EmploymentLicenseIncentiveId).HasColumnName("EmploymentLicenseIncentiveID");
                entity.Property(e => e.Ccd2BmemploymentId).HasColumnName("CCd2BMEmploymentID");
                entity.Property(e => e.CcdBmemploymentId).HasColumnName("CCdBMEmploymentID");
                entity.Property(e => e.CcokToSellBmemploymentId).HasColumnName("CCOkToSellBMEmploymentID");
                entity.Property(e => e.Dm10daySentBySoeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DM10DaySentBySOEID");
                entity.Property(e => e.Dm10daySentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DM10DaySentDate");
                entity.Property(e => e.Dm20daySentBySoeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DM20DaySentBySOEID");
                entity.Property(e => e.Dm20daySentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DM20DaySentDate");
                entity.Property(e => e.DmapprovalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DMApprovalDate");
                entity.Property(e => e.Dmcomment)
                    .IsUnicode(false)
                    .HasColumnName("DMComment");
                entity.Property(e => e.DmdeclinedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DMDeclinedDate");
                entity.Property(e => e.DmemploymentId).HasColumnName("DMEmploymentID");
                entity.Property(e => e.DmsentBySoeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DMSentBySOEID");
                entity.Property(e => e.DmsentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DMSentDate");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.IncentiveStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.IncetivePeriodDate).HasColumnType("datetime");
                entity.Property(e => e.Notes).IsUnicode(false);
                entity.Property(e => e.RollOutGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Tm10daySentBySoeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TM10DaySentBySOEID");
                entity.Property(e => e.Tm10daySentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TM10DaySentDate");
                entity.Property(e => e.Tm45daySentBySoeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TM45DaySentBySOEID");
                entity.Property(e => e.Tm45daySentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TM45DaySentDate");
                entity.Property(e => e.TmapprovalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TMApprovalDate");
                entity.Property(e => e.Tmcomment)
                    .IsUnicode(false)
                    .HasColumnName("TMComment");
                entity.Property(e => e.TmdeclinedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TMDeclinedDate");
                entity.Property(e => e.Tmexception)
                    .IsUnicode(false)
                    .HasColumnName("TMException");
                entity.Property(e => e.TmexceptionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TMExceptionDate");
                entity.Property(e => e.TmokToSellSentBySoeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TMOkToSellSentBySOEID");
                entity.Property(e => e.TmokToSellSentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TMOkToSellSentDate");
                entity.Property(e => e.TmomsapprtoSendToHrdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TMOMSApprtoSendToHRDate");
                entity.Property(e => e.TmsentBySoeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TMSentBySOEID");
                entity.Property(e => e.TmsentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("TMSentDate");
                entity.Property(e => e.TmsentToHrdate)
                    .HasColumnType("datetime")
                    .HasColumnName("TMSentToHRDate");
            });
            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasKey(e => e.ExamId).HasName("PK__Exam__41E3A924");

                entity.ToTable("Exam", "dbo");

                entity.HasIndex(e => e.StateProvinceAbv, "IDX_Exam_StateProvinceAbv");

                entity.HasIndex(e => e.ExamProviderId, "XIF2Exam");

                entity.Property(e => e.ExamId).HasColumnName("ExamID");
                entity.Property(e => e.DeliveryMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ExamFees).HasColumnType("money");
                entity.Property(e => e.ExamName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.ExamProviderId).HasColumnName("ExamProviderID");
                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.XxxIsActive).HasColumnName("xxxIsActive");

                entity.HasOne(d => d.ExamProvider).WithMany(p => p.Exams)
                    .HasForeignKey(d => d.ExamProviderId)
                    .HasConstraintName("FK__Exam__ExamProvid__3CE9E9DD");

                entity.HasOne(d => d.StateProvinceAbvNavigation).WithMany(p => p.Exams)
                    .HasForeignKey(d => d.StateProvinceAbv)
                    .HasConstraintName("FK__Exam__StateProvi__44C015CF");
            });
            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.HasKey(e => e.JobTitleId).HasName("PK_JobTitleID");

                entity.ToTable("JobTitles", "dbo");

                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");
                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.JobCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);
                entity.Property(e => e.JobTitle1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("JobTitle");
                entity.Property(e => e.LicenseIncentive)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('NoIncentive')");
                entity.Property(e => e.LicenseLevel)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('{NeedsReview}')");
                entity.Property(e => e.Reviewed).HasColumnType("datetime");
            });
            modelBuilder.Entity<LearningAsset>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("LearningAsset", "dbo");

                entity.Property(e => e.AddedDate).HasColumnType("date");
                entity.Property(e => e.AssetId)
                    .HasMaxLength(36)
                    .IsUnicode(false);
                entity.Property(e => e.AssetTitle)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.ModifiedDate).HasColumnType("date");
            });
            modelBuilder.Entity<Letter>(entity =>
            {
                entity.HasKey(e => e.LetterName).HasName("PK__Letters__62BA8D0A");

                entity.ToTable("Letters", "dbo");

                entity.Property(e => e.LetterName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Fieldlist)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.SendMethod)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<LettersGenerated>(entity =>
            {
                entity.HasKey(e => e.LetterDataId).HasName("PK__LettersGenerated__6DF7358C");

                entity.ToTable("LettersGenerated", "dbo");

                entity.Property(e => e.LetterDataId).HasColumnName("LetterDataID");
                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.BranchCodeNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.DistrictTmnum)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DistrictTMNum");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.LetterData)
                    .HasMaxLength(8000)
                    .IsUnicode(false);
                entity.Property(e => e.LetterName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.RegionCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.LetterNameNavigation).WithMany(p => p.LettersGenerateds)
                    .HasForeignKey(d => d.LetterName)
                    .HasConstraintName("FK__LettersGe__Lette__41248F15");
            });
            modelBuilder.Entity<License>(entity =>
            {
                entity.HasKey(e => e.LicenseId).HasName("PK__License__46DD686B");

                entity.ToTable("License", "dbo");

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
                entity.Property(e => e.AppointmentFees).HasColumnType("money");
                entity.Property(e => e.Incentive2PlusMrpay)
                    .HasColumnType("money")
                    .HasColumnName("Incentive2_PlusMRPay");
                entity.Property(e => e.Incentive2PlusTmpay)
                    .HasColumnType("money")
                    .HasColumnName("Incentive2_PlusTMPay");
                entity.Property(e => e.LicIncentive3Mrpay)
                    .HasColumnType("money")
                    .HasColumnName("LicIncentive3MRPay");
                entity.Property(e => e.LicIncentive3Tmpay)
                    .HasColumnType("money")
                    .HasColumnName("LicIncentive3TMPay");
                entity.Property(e => e.LicenseAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseFees).HasColumnType("money");
                entity.Property(e => e.LicenseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.LineOfAuthorityId).HasColumnName("LineOfAuthorityID");
                entity.Property(e => e.PlsIncentive1Mrpay)
                    .HasColumnType("money")
                    .HasColumnName("PLS_Incentive1MRPay");
                entity.Property(e => e.PlsIncentive1Tmpay)
                    .HasColumnType("money")
                    .HasColumnName("PLS_Incentive1TMPay");
                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.StateProvinceAbvNavigation).WithMany(p => p.Licenses)
                    .HasForeignKey(d => d.StateProvinceAbv)
                    .HasConstraintName("FK__License__StatePr__48C5B0DD");
            });
            modelBuilder.Entity<LicenseApplication>(entity =>
            {
                entity.HasKey(e => e.LicenseApplicationId).HasName("PK__LicenseApplicati__6AC5C326");

                entity.ToTable("LicenseApplication", "dbo");

                entity.HasIndex(e => e.EmployeeLicenseId, "IDX_LicenseApplication_EmployeeLicenseID");

                entity.Property(e => e.LicenseApplicationId).HasColumnName("LicenseApplicationID");
                entity.Property(e => e.ApplicationStatus)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.ApplicationType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.RecFromAgentDate).HasColumnType("datetime");
                entity.Property(e => e.RecFromStateDate).HasColumnType("datetime");
                entity.Property(e => e.RenewalDate).HasColumnType("datetime");
                entity.Property(e => e.RenewalMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.SentToAgentDate).HasColumnType("datetime");
                entity.Property(e => e.SentToStateDate).HasColumnType("datetime");

                entity.HasOne(d => d.EmployeeLicense).WithMany(p => p.LicenseApplications)
                    .HasForeignKey(d => d.EmployeeLicenseId)
                    .HasConstraintName("FK__LicenseAp__Emplo__6BB9E75F");
            });
            modelBuilder.Entity<LicenseCompany>(entity =>
            {
                entity.HasKey(e => e.LicenseCompanyId).HasName("PK__LicenseC__57C3A2B458728E0E");

                entity.ToTable("LicenseCompany", "dbo");

                entity.Property(e => e.LicenseCompanyId).HasColumnName("LicenseCompanyID");
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");

                entity.HasOne(d => d.Company).WithMany(p => p.LicenseCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__LicenseCo__Compa__3A0426E8");

                entity.HasOne(d => d.License).WithMany(p => p.LicenseCompanies)
                    .HasForeignKey(d => d.LicenseId)
                    .HasConstraintName("FK__LicenseCo__Licen__3AF84B21");
            });
            modelBuilder.Entity<LicenseExam>(entity =>
            {
                entity.HasKey(e => e.LicenseExamId).HasName("PK__LicenseE__667CBE994FEE2CE4");

                entity.ToTable("LicenseExam", "dbo");

                entity.Property(e => e.LicenseExamId).HasColumnName("LicenseExamID");
                entity.Property(e => e.ExamId).HasColumnName("ExamID");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");

                entity.HasOne(d => d.Exam).WithMany(p => p.LicenseExams)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK__LicenseEx__ExamI__41A548B0");

                entity.HasOne(d => d.License).WithMany(p => p.LicenseExams)
                    .HasForeignKey(d => d.LicenseId)
                    .HasConstraintName("FK__LicenseEx__Licen__42996CE9");
            });
            modelBuilder.Entity<LicensePreEducation>(entity =>
            {
                entity.HasKey(e => e.LicensePreEducationId).HasName("PK__LicenseP__16AD0AC9E8D1B73C");

                entity.ToTable("LicensePreEducation", "dbo");

                entity.Property(e => e.LicensePreEducationId).HasColumnName("LicensePreEducationID");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
                entity.Property(e => e.PreEducationId).HasColumnName("PreEducationID");

                entity.HasOne(d => d.License).WithMany(p => p.LicensePreEducations)
                    .HasForeignKey(d => d.LicenseId)
                    .HasConstraintName("FK__LicensePr__Licen__3EC8DC05");

                entity.HasOne(d => d.PreEducation).WithMany(p => p.LicensePreEducations)
                    .HasForeignKey(d => d.PreEducationId)
                    .HasConstraintName("FK__LicensePr__PreEd__3DD4B7CC");
            });
            modelBuilder.Entity<LicenseProduct>(entity =>
            {
                entity.HasKey(e => e.LicenseProductId).HasName("PK__LicenseP__CF0B7ABFC5D4943B");

                entity.ToTable("LicenseProduct", "dbo");

                entity.Property(e => e.LicenseProductId).HasColumnName("LicenseProductID");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.License).WithMany(p => p.LicenseProducts)
                    .HasForeignKey(d => d.LicenseId)
                    .HasConstraintName("FK__LicensePr__Licen__3727BA3D");

                entity.HasOne(d => d.Product).WithMany(p => p.LicenseProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__LicensePr__Produ__36339604");
            });
            modelBuilder.Entity<LicenseTech>(entity =>
            {
                entity.HasKey(e => e.LicenseTechId).HasName("PK__LicenseTech__473C8FC7");

                entity.ToTable("LicenseTech", "dbo");

                entity.Property(e => e.LicenseTechId).HasColumnName("LicenseTechID");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseTechEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseTechFax)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseTechPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Soeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SOEID");
                entity.Property(e => e.TeamNum)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<LineOfAuthority>(entity =>
            {
                entity.HasKey(e => e.LineOfAuthorityId).HasName("PK__LineOfAu__5CF83696F127222A");

                entity.ToTable("LineOfAuthority", "dbo");

                entity.Property(e => e.LineOfAuthorityId).HasColumnName("LineOfAuthorityID");
                entity.Property(e => e.LineOfAuthorityAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LineOfAuthorityName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<LkpTypeStatus>(entity =>
            {
                entity.HasKey(e => new { e.LkpField, e.LkpValue }).HasName("PK__lkp_TypeStatus__60C757A0");

                entity.ToTable("lkp_TypeStatus", "dbo");

                entity.Property(e => e.LkpField)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.LkpValue)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<NewHirePackage>(entity =>
            {
                entity.HasKey(e => e.PackageId).HasName("PK__NewHirePackage__6809520C");

                entity.ToTable("NewHirePackage", "dbo");

                entity.Property(e => e.PackageId).HasColumnName("PackageID");
                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<PreEducation>(entity =>
            {
                entity.HasKey(e => e.PreEducationId).HasName("PK__PreEducation__43CBF196");

                entity.ToTable("PreEducation", "dbo");

                entity.Property(e => e.PreEducationId).HasColumnName("PreEducationID");
                entity.Property(e => e.DeliveryMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EducationName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.EducationProviderId).HasColumnName("EducationProviderID");
                entity.Property(e => e.Fees).HasColumnType("money");
                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.XxxIsActive).HasColumnName("xxxIsActive");

                entity.HasOne(d => d.EducationProvider).WithMany(p => p.PreEducations)
                    .HasForeignKey(d => d.EducationProviderId)
                    .HasConstraintName("FK__PreEducat__Educa__46A85E41");

                entity.HasOne(d => d.StateProvinceAbvNavigation).WithMany(p => p.PreEducations)
                    .HasForeignKey(d => d.StateProvinceAbv)
                    .HasConstraintName("FK__PreEducat__State__45B43A08");
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__Product__4B0D20AB");

                entity.ToTable("Product", "dbo");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
                entity.Property(e => e.ExpireDate).HasColumnType("datetime");
                entity.Property(e => e.ProductAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.XxxAgentMaster).HasColumnName("xxxAgentMaster");
                entity.Property(e => e.XxxIsActive).HasColumnName("xxxIsActive");
            });
            modelBuilder.Entity<RequiredLicense>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("RequiredLicenses", "dbo");

                entity.Property(e => e.BmincentiveAmt3)
                    .HasColumnType("money")
                    .HasColumnName("BMIncentiveAmt3");
                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.Incentive2Plus).HasColumnName("Incentive2_Plus");
                entity.Property(e => e.Incentive2PlusBmamt)
                    .HasColumnType("money")
                    .HasColumnName("Incentive2_PlusBMAmt");
                entity.Property(e => e.Incentive2PlusTmamt)
                    .HasColumnType("money")
                    .HasColumnName("Incentive2_PlusTMAmt");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
                entity.Property(e => e.PackageId).HasColumnName("PackageID");
                entity.Property(e => e.PlsIncentive1).HasColumnName("PLS_Incentive1");
                entity.Property(e => e.PlsIncentive1Bmamt)
                    .HasColumnType("money")
                    .HasColumnName("PLS_Incentive1BMAmt");
                entity.Property(e => e.PlsIncentive1Tmamt)
                    .HasColumnType("money")
                    .HasColumnName("PLS_Incentive1TMAmt");
                entity.Property(e => e.RenewalDocument).IsUnicode(false);
                entity.Property(e => e.RequiredLicenseId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RequiredLicenseID");
                entity.Property(e => e.RequirementType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.ResStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.StartDocument).IsUnicode(false);
                entity.Property(e => e.TmincentiveAmt3)
                    .HasColumnType("money")
                    .HasColumnName("TMIncentiveAmt3");
                entity.Property(e => e.WorkStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.License).WithMany()
                    .HasForeignKey(d => d.LicenseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RequiredL__Licen__2ACB39A2");

                entity.HasOne(d => d.Package).WithMany()
                    .HasForeignKey(d => d.PackageId)
                    .HasConstraintName("FK__RequiredL__Packa__68FD7645");
            });
            modelBuilder.Entity<StateProvince>(entity =>
            {
                entity.HasKey(e => e.StateProvinceAbv).HasName("PK__StateProvince__45943E77");

                entity.ToTable("StateProvince", "dbo");

                entity.HasIndex(e => e.DoiaddressId, "IDX_StateProvince_DOIAddressID");

                entity.HasIndex(e => e.LicenseTechId, "IDX_StateProvince_LicenseTechID");

                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.DoiaddressId).HasColumnName("DOIAddressID");
                entity.Property(e => e.Doiname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DOIName");
                entity.Property(e => e.LicenseTechId).HasColumnName("LicenseTechID");
                entity.Property(e => e.StateProvinceCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.StateProvinceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Doiaddress).WithMany(p => p.StateProvinces)
                    .HasForeignKey(d => d.DoiaddressId)
                    .HasConstraintName("FK__StateProv__DOIAd__4964CF5B");

                entity.HasOne(d => d.LicenseTech).WithMany(p => p.StateProvinces)
                    .HasForeignKey(d => d.LicenseTechId)
                    .HasConstraintName("FK__StateProv__Licen__4870AB22");
            });
            modelBuilder.Entity<StgActive>(entity =>
            {
                entity.HasKey(e => e.StgActivesId).HasName("PK_stg_ActivesID");

                entity.ToTable("stg_Actives", "dbo");

                entity.Property(e => e.StgActivesId).HasColumnName("stg_ActivesID");
                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Branch)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.DateofBirth)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.District)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FormerCo)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Geid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GEID");
                entity.Property(e => e.HireDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LeaveStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseState)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.NetworkFlag)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.Pru)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRU");
                entity.Property(e => e.Ssno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SSNO");
                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Zip)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<StgAgentStateTable>(entity =>
            {
                entity.HasKey(e => e.AgentStateTableId).HasName("PK_stg_AgentMaster");

                entity.ToTable("stg_AgentStateTable", "dbo");

                entity.Property(e => e.AgentStateTableId).HasColumnName("AgentStateTableID");
                entity.Property(e => e.ChangeDate).HasColumnType("datetime");
                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseCurrentDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseExpirationDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseOriginalDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LineOfAuthorityAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ResidentCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<StgContEducationRequirement>(entity =>
            {
                entity.HasKey(e => e.StgContEducationRequirementId).HasName("PK_stg_ContEducationRequirementID");

                entity.ToTable("stg_ContEducationRequirement", "dbo");

                entity.Property(e => e.StgContEducationRequirementId).HasColumnName("stg_ContEducationRequirementID");
                entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
                entity.Property(e => e.EducationStartDate).HasColumnType("datetime");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.RequiredCreditHours).HasColumnType("numeric(9, 2)");
            });
            modelBuilder.Entity<StgEmploymentCompanyRequirement>(entity =>
            {
                entity.HasKey(e => e.EmploymentCompanyRequirementId).HasName("PK__stg_Empl__07941AB0BB5650ED");

                entity.ToTable("stg_EmploymentCompanyRequirements", "dbo");

                entity.Property(e => e.EmploymentCompanyRequirementId).HasColumnName("EmploymentCompanyRequirementID");
                entity.Property(e => e.AssetId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("asset_id");
                entity.Property(e => e.AssetSk).HasColumnName("asset_sk");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.Geid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("GEID");
                entity.Property(e => e.LearningProgramCompletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("learning_program_completion_date");
                entity.Property(e => e.LearningProgramEnrollmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("learning_program_enrollment_date");
                entity.Property(e => e.LearningProgramStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("learning_program_status");
            });
            modelBuilder.Entity<StgHrx>(entity =>
            {
                entity.HasKey(e => e.StgHrxid).HasName("PK_stg_HRXID");

                entity.ToTable("stg_HRX", "dbo");

                entity.Property(e => e.StgHrxid).HasColumnName("stg_HRXID");
                entity.Property(e => e.Address1)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS1");
                entity.Property(e => e.Address2)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS2");
                entity.Property(e => e.City)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CITY");
                entity.Property(e => e.CompCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("COMP-CODE");
                entity.Property(e => e.DateOfBirth)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("DATE-OF-BIRTH");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");
                entity.Property(e => e.EmpStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("EMP-STATUS");
                entity.Property(e => e.EmployNbr)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("EMPLOY-NBR");
                entity.Property(e => e.Field)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("FIELD");
                entity.Property(e => e.Filler)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("FILLER");
                entity.Property(e => e.FirstMidName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRST-MID-NAME");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FIRST-NAME");
                entity.Property(e => e.HomePhone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("HOME-PHONE");
                entity.Property(e => e.HrDepartmentId)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("HR_Department_ID");
                entity.Property(e => e.JobDate)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("JOB-DATE");
                entity.Property(e => e.LastHireDate)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("LAST-HIRE-DATE");
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LAST-NAME");
                entity.Property(e => e.MgrEmployNbr)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("MGR-EMPLOY-NBR");
                entity.Property(e => e.MiddleName)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("MIDDLE-NAME");
                entity.Property(e => e.Nickname)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NICKNAME");
                entity.Property(e => e.OriginalHireDate)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("ORIGINAL-HIRE-DATE");
                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("SEX");
                entity.Property(e => e.SocsecNbr)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("SOCSEC-NBR");
                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("STATE");
                entity.Property(e => e.TerminateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("TERMINATE-CODE");
                entity.Property(e => e.TerminateDate)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("TERMINATE-DATE");
                entity.Property(e => e.Title)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("TITLE");
                entity.Property(e => e.TitleCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("TITLE-CODE");
                entity.Property(e => e.WorkState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("WORK-STATE");
                entity.Property(e => e.Zip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("ZIP");
                entity.Property(e => e.Zip4)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("ZIP-4");
            });
            modelBuilder.Entity<StgLettersGenerated>(entity =>
            {
                entity.HasKey(e => e.StgLettersGeneratedId).HasName("PK_stg_LettersGeneratedID");

                entity.ToTable("stg_LettersGenerated", "dbo");

                entity.Property(e => e.StgLettersGeneratedId).HasColumnName("stg_LettersGeneratedID");
                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.DistrictTmnum)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DistrictTMNum");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.LetterData)
                    .HasMaxLength(8000)
                    .IsUnicode(false);
                entity.Property(e => e.LetterName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<StgOtincentive>(entity =>
            {
                entity.HasKey(e => e.OtincentiveId).HasName("PK__stg_OTIn__CE267E8A6A03EBF2");

                entity.ToTable("stg_OTIncentive", "dbo");

                entity.Property(e => e.OtincentiveId).HasColumnName("OTIncentiveID");
                entity.Property(e => e.AgentName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.AppointmentEffDate).HasColumnType("datetime");
                entity.Property(e => e.Bmname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("BMName");
                entity.Property(e => e.CompletedIn90days).HasColumnType("datetime");
                entity.Property(e => e.DateDmapproved)
                    .HasColumnType("datetime")
                    .HasColumnName("DateDMApproved");
                entity.Property(e => e.DateDmdeclined)
                    .HasColumnType("datetime")
                    .HasColumnName("DateDMDeclined");
                entity.Property(e => e.DateReceivedTmapproval)
                    .HasColumnType("datetime")
                    .HasColumnName("DateReceivedTMApproval");
                entity.Property(e => e.DateReceivedTmdecline)
                    .HasColumnType("datetime")
                    .HasColumnName("DateReceivedTMDecline");
                entity.Property(e => e.DateSentToDm)
                    .HasColumnType("datetime")
                    .HasColumnName("DateSentToDM");
                entity.Property(e => e.DateSentToHr)
                    .HasColumnType("datetime")
                    .HasColumnName("DateSentToHR");
                entity.Property(e => e.DateSentToTm)
                    .HasColumnType("datetime")
                    .HasColumnName("DateSentToTM");
                entity.Property(e => e.DmapprovalStatus)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("DMApprovalStatus");
                entity.Property(e => e.Dmcomment)
                    .IsUnicode(false)
                    .HasColumnName("DMComment");
                entity.Property(e => e.Dmfollowup10Date)
                    .HasColumnType("datetime")
                    .HasColumnName("DMFollowup10Date");
                entity.Property(e => e.Dmfollowup20Date)
                    .HasColumnType("datetime")
                    .HasColumnName("DMFollowup20Date");
                entity.Property(e => e.Dmname)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("DMName");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.EmploymentLicenseIncentiveId).HasColumnName("EmploymentLicenseIncentiveID");
                entity.Property(e => e.EnrollDate).HasColumnType("datetime");
                entity.Property(e => e.ExceptionReason).IsUnicode(false);
                entity.Property(e => e.ExceptionStartDate).HasColumnType("datetime");
                entity.Property(e => e.IncentiveExpirationDate).HasColumnType("datetime");
                entity.Property(e => e.IncetivePeriodDate).HasColumnType("datetime");
                entity.Property(e => e.LicType)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.Notes).IsUnicode(false);
                entity.Property(e => e.OmsapprToSendToHr)
                    .HasColumnType("datetime")
                    .HasColumnName("OMSApprToSendToHR");
                entity.Property(e => e.ProgramEligible)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.RolloutGrp)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.SentToDmby)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("SentToDMby");
                entity.Property(e => e.SentToTmby)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("SentToTMBy");
                entity.Property(e => e.TmapprovalStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("TMApprovalStatus");
                entity.Property(e => e.Tmcomment)
                    .IsUnicode(false)
                    .HasColumnName("TMComment");
                entity.Property(e => e.Tmfollowup10Date)
                    .HasColumnType("datetime")
                    .HasColumnName("TMFollowup10Date");
                entity.Property(e => e.UpdtAh)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("UpdtAH");
                entity.Property(e => e.UpdtLi)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("UpdtLI");
                entity.Property(e => e.WkSt)
                    .HasMaxLength(2)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<StgTerm>(entity =>
            {
                entity.HasKey(e => e.StgTermsId).HasName("PK_stg_TermsID");

                entity.ToTable("stg_Terms", "dbo");

                entity.Property(e => e.StgTermsId).HasColumnName("stg_TermsID");
                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Branch)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.DateofBirth)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.FormerCo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Geid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("GEID");
                entity.Property(e => e.HireDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.HrtermDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HRTermDate");
                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LeaveStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseState)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Pru)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRU");
                entity.Property(e => e.Ssno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SSNO");
                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Zip)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Sysdtslog90>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__sysdtslog90__430CD787");

                entity.ToTable("sysdtslog90", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Computer)
                    .HasMaxLength(128)
                    .HasColumnName("computer");
                entity.Property(e => e.Databytes)
                    .HasColumnType("image")
                    .HasColumnName("databytes");
                entity.Property(e => e.Datacode).HasColumnName("datacode");
                entity.Property(e => e.Endtime)
                    .HasColumnType("datetime")
                    .HasColumnName("endtime");
                entity.Property(e => e.Event)
                    .HasMaxLength(128)
                    .HasColumnName("event");
                entity.Property(e => e.Executionid).HasColumnName("executionid");
                entity.Property(e => e.Message)
                    .HasMaxLength(2048)
                    .HasColumnName("message");
                entity.Property(e => e.Operator)
                    .HasMaxLength(128)
                    .HasColumnName("operator");
                entity.Property(e => e.Source)
                    .HasMaxLength(1024)
                    .HasColumnName("source");
                entity.Property(e => e.Sourceid).HasColumnName("sourceid");
                entity.Property(e => e.Starttime)
                    .HasColumnType("datetime")
                    .HasColumnName("starttime");
            });
            modelBuilder.Entity<TempPeopleFailure>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("Temp_People_failures", "dbo");

                entity.Property(e => e.Comment).HasMaxLength(25);
                entity.Property(e => e.Coursefailed)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("coursefailed");
                entity.Property(e => e.DateFailed).HasColumnType("datetime");
                entity.Property(e => e.DateRecorded).HasColumnType("datetime");
                entity.Property(e => e.Failurelevel).HasColumnName("failurelevel");
                entity.Property(e => e.Hoursmissed).HasColumnName("hoursmissed");
                entity.Property(e => e.Recordedby)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("recordedby");
                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<Tickler>(entity =>
            {
                entity.HasKey(e => e.TicklerId).HasName("PK__Tickler__93673B0F69A0583B");

                entity.ToTable("Tickler", "dbo");

                entity.Property(e => e.TicklerId).HasColumnName("TicklerID");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.LicenseTechId).HasColumnName("LicenseTechID");
                entity.Property(e => e.LkpValue)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.TicklerCloseByLicenseTechId).HasColumnName("TicklerCloseByLicenseTechID");
                entity.Property(e => e.TicklerCloseDate).HasColumnType("datetime");
                entity.Property(e => e.TicklerDate).HasColumnType("datetime");
                entity.Property(e => e.TicklerDueDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<TransferHistory>(entity =>
            {
                entity.HasKey(e => e.TransferHistoryId).HasName("PK__TransferHistory__774B959C");

                entity.ToTable("TransferHistory", "dbo");

                entity.HasIndex(e => e.BranchCode, "IDX_NC_BranchCode");

                entity.HasIndex(e => e.EmploymentId, "IDX_NC_EmploymentID");

                entity.Property(e => e.TransferHistoryId).HasColumnName("TransferHistoryID");
                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.ResStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Scorenumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SCORENumber");
                entity.Property(e => e.TransferDate).HasColumnType("datetime");
                entity.Property(e => e.WorkStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Employment).WithMany(p => p.TransferHistories)
                    .HasForeignKey(d => d.EmploymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TransferH__Emplo__7C104AB9");
            });
            modelBuilder.Entity<VwAddress>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_Address", "Audit");

                entity.Property(e => e.Address1)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Address2)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.AddressId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AddressID");
                entity.Property(e => e.AddressType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Country)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.Fax)
                    .HasMaxLength(13)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(13)
                    .IsUnicode(false);
                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Zip)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwAddressTransformation>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VW_AddressTransformation", "dbo");

                entity.Property(e => e.Address1)
                    .HasMaxLength(33)
                    .IsUnicode(false);
                entity.Property(e => e.Address1New)
                    .HasMaxLength(27)
                    .IsUnicode(false);
                entity.Property(e => e.Address2)
                    .HasMaxLength(28)
                    .IsUnicode(false);
                entity.Property(e => e.Address2New)
                    .HasMaxLength(37)
                    .IsUnicode(false);
                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.CityNew)
                    .HasMaxLength(17)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.PhoneNew)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.StateNew)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.Zip)
                    .HasMaxLength(9)
                    .IsUnicode(false);
                entity.Property(e => e.ZipNew)
                    .HasMaxLength(9)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwAgentExamActivity>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_AgentExamActivity", "Audit");

                entity.Property(e => e.AgentEmail)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.AgentName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.AgentPhone)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.BestSyescore).HasColumnName("BestSYEScore");
                entity.Property(e => e.CourseExpirationDate).HasColumnType("datetime");
                entity.Property(e => e.Date100PctComplete).HasColumnType("date");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.ExamCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.FirstAccess).HasColumnType("datetime");
                entity.Property(e => e.LastLogin).HasColumnType("datetime");
                entity.Property(e => e.LicenseState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ManagerName)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.ProductCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Registered).HasColumnType("datetime");
                entity.Property(e => e.ResidentState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.TimeInCourse)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Time_in_Course");
                entity.Property(e => e.TimeInReading)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.TimeInStudyByTopicQuizzes)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.TimeInSye)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TimeInSYE");
            });
            modelBuilder.Entity<VwAgentMaster>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_AgentMaster", "Audit");

                entity.Property(e => e.AgentMasterId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AgentMasterID");
                entity.Property(e => e.ChangeDate).HasColumnType("datetime");
                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseCurrentDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseExpirationDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseOriginalDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LineOfAuthorityAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ResidentCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<VwAgentStateTable>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_AgentStateTable", "Audit");

                entity.Property(e => e.AgentStateTableId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("AgentStateTableID");
                entity.Property(e => e.ChangeDate).HasColumnType("datetime");
                entity.Property(e => e.EmployeeNumber)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseCurrentDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseExpirationDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseOriginalDate)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStateCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LineOfAuthorityAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.ResidentCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<VwCommunication>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_Communications", "Audit");

                entity.Property(e => e.CommunicationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CommunicationID");
                entity.Property(e => e.CommunicationName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.DocAppType)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.DocSubType)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.DocType)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.DocTypeAbv)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.EmailAttachments).IsUnicode(false);
            });
            modelBuilder.Entity<VwCompany>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_Company", "Audit");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");
                entity.Property(e => e.CompanyAbv)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.CompanyId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CompanyID");
                entity.Property(e => e.CompanyName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.CompanyType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.Naicnumber).HasColumnName("NAICNumber");
                entity.Property(e => e.Tin).HasColumnName("TIN");
                entity.Property(e => e.XxxIsActive).HasColumnName("xxxIsActive");
            });
            modelBuilder.Entity<VwCompanyRequirement>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_CompanyRequirements", "Audit");

                entity.Property(e => e.CompanyRequirementId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("CompanyRequirementID");
                entity.Property(e => e.Document).IsUnicode(false);
                entity.Property(e => e.RequirementType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.ResStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.StartAfterDate).HasColumnType("datetime");
                entity.Property(e => e.WorkStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<VwContEducationRequirement>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_ContEducationRequirement", "Audit");

                entity.Property(e => e.ContEducationRequirementId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ContEducationRequirementID");
                entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
                entity.Property(e => e.EducationStartDate).HasColumnType("datetime");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.RequiredCreditHours).HasColumnType("numeric(9, 2)");
            });
            modelBuilder.Entity<VwContEducationRequirementHistory>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_ContEducationRequirement_History", "Audit");

                entity.Property(e => e.ContEducationRequirementId).HasColumnName("ContEducationRequirementID");
                entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
                entity.Property(e => e.EducationStartDate).HasColumnType("datetime");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.HistoryDate).HasColumnType("datetime");
                entity.Property(e => e.RequiredCreditHours).HasColumnType("numeric(9, 2)");
            });
            modelBuilder.Entity<VwDiary>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_Diary", "Audit");

                entity.Property(e => e.DiaryDate).HasColumnType("datetime");
                entity.Property(e => e.DiaryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DiaryID");
                entity.Property(e => e.DiaryName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.Notes)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.Soeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SOEID");
            });
            modelBuilder.Entity<VwEducationRule>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EducationRule", "Audit");

                entity.Property(e => e.EducationEndDateId).HasColumnName("EducationEndDateID");
                entity.Property(e => e.EducationRuleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EducationRuleID");
                entity.Property(e => e.EducationStartDateId).HasColumnName("EducationStartDateID");
                entity.Property(e => e.ExceptionId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ExceptionID");
                entity.Property(e => e.ExemptionId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("ExemptionID");
                entity.Property(e => e.LicenseType)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.RequiredCreditHours).HasColumnType("numeric(9, 2)");
                entity.Property(e => e.StateProvince)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwEducationRuleCriterion>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EducationRuleCriteria", "Audit");

                entity.Property(e => e.Criteria)
                    .HasMaxLength(7000)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.EducationCriteriaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EducationCriteriaID");
                entity.Property(e => e.UsageType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwEmployee>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_Employee", "Audit");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");
                entity.Property(e => e.Alias)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmployeeID");
                entity.Property(e => e.EmployeeSsnid).HasColumnName("EmployeeSSNID");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Geid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("GEID");
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LegalHold)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.PurgeDate).HasColumnType("datetime");
                entity.Property(e => e.Soeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SOEID");
                entity.Property(e => e.Suffix)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Urccode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("URCCode");
            });
            modelBuilder.Entity<VwEmployeeAppointment>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmployeeAppointment", "Audit");

                entity.Property(e => e.AppointmentEffectiveDate).HasColumnType("datetime");
                entity.Property(e => e.AppointmentExpireDate).HasColumnType("datetime");
                entity.Property(e => e.AppointmentStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.AppointmentTerminationDate).HasColumnType("datetime");
                entity.Property(e => e.CarrierDate).HasColumnType("datetime");
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.EmployeeAppointmentId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmployeeAppointmentID");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.RetentionDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<VwEmployeeContEducation>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmployeeContEducation", "Audit");

                entity.Property(e => e.AdditionalNotes).IsUnicode(false);
                entity.Property(e => e.ContEducationId).HasColumnName("ContEducationID");
                entity.Property(e => e.ContEducationRequirementId).HasColumnName("ContEducationRequirementID");
                entity.Property(e => e.ContEducationTakenDate).HasColumnType("datetime");
                entity.Property(e => e.CreditHoursTaken).HasColumnType("numeric(9, 2)");
                entity.Property(e => e.EmployeeEducationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmployeeEducationID");
                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwEmployeeLicense>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmployeeLicense", "Audit");

                entity.Property(e => e.AscEmployeeLicenseId).HasColumnName("AscEmployeeLicenseID");
                entity.Property(e => e.EmployeeLicenseId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.LicenseEffectiveDate).HasColumnType("datetime");
                entity.Property(e => e.LicenseExpireDate).HasColumnType("datetime");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
                entity.Property(e => e.LicenseIssueDate).HasColumnType("datetime");
                entity.Property(e => e.LicenseNote)
                    .HasMaxLength(250)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.LineOfAuthorityIssueDate).HasColumnType("datetime");
                entity.Property(e => e.RetentionDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<VwEmployeeLicensePreExam>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmployeeLicensePreExams", "Audit");

                entity.Property(e => e.AdditionalNotes).IsUnicode(false);
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmployeeLicensePreExamId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmployeeLicensePreExamID");
                entity.Property(e => e.ExamId).HasColumnName("ExamID");
                entity.Property(e => e.ExamScheduleDate).HasColumnType("datetime");
                entity.Property(e => e.ExamTakenDate).HasColumnType("datetime");
                entity.Property(e => e.Status)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwEmployeeLicenseePreEducation>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmployeeLicenseePreEducation", "Audit");

                entity.Property(e => e.AdditionalNotes).IsUnicode(false);
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
                entity.Property(e => e.EducationStartDate).HasColumnType("datetime");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmployeeLicensePreEducationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmployeeLicensePreEducationID");
                entity.Property(e => e.PreEducationId).HasColumnName("PreEducationID");
                entity.Property(e => e.Status)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwEmployeeSsn>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmployeeSSN", "Audit");

                entity.Property(e => e.EmployeeSsnid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmployeeSSNID");
                entity.Property(e => e.Last4EmployeeSsn)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("Last4EmployeeSSN");
            });
            modelBuilder.Entity<VwEmployment>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_Employment", "Audit");

                entity.Property(e => e.Cerequired).HasColumnName("CERequired");
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.DirRptMgrTmnum)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DirRptMgrTMNum");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EmployeeStatus)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EmploymentId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmploymentID");
                entity.Property(e => e.H1employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H1EmploymentID");
                entity.Property(e => e.H2employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H2EmploymentID");
                entity.Property(e => e.H3employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H3EmploymentID");
                entity.Property(e => e.H4employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H4EmploymentID");
                entity.Property(e => e.H5employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H5EmploymentID");
                entity.Property(e => e.H6employmentId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("H6EmploymentID");
                entity.Property(e => e.JobCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);
                entity.Property(e => e.JobTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseIncentive)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseLevel)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.RetentionDate).HasColumnType("datetime");
                entity.Property(e => e.Tmtype)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TMType");
                entity.Property(e => e.WorkPhone)
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwEmploymentCommunication>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmploymentCommunications", "Audit");

                entity.Property(e => e.CommunicationId).HasColumnName("CommunicationID");
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.CompareXml)
                    .IsUnicode(false)
                    .HasColumnName("CompareXML");
                entity.Property(e => e.EmailAttachments).IsUnicode(false);
                entity.Property(e => e.EmailBodyHtml)
                    .IsUnicode(false)
                    .HasColumnName("EmailBodyHTML");
                entity.Property(e => e.EmailCreateDate).HasColumnType("datetime");
                entity.Property(e => e.EmailCreator)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EmailFrom)
                    .HasMaxLength(8000)
                    .IsUnicode(false);
                entity.Property(e => e.EmailSentDate).HasColumnType("datetime");
                entity.Property(e => e.EmailSubject)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.EmailTo)
                    .HasMaxLength(8000)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.EmploymentCommunicationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmploymentCommunicationID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
            });
            modelBuilder.Entity<VwEmploymentCommunicationDetail>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmploymentCommunicationDetails", "Audit");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.CompanyRequirementId).HasColumnName("CompanyRequirementID");
                entity.Property(e => e.EducationEndDate).HasColumnType("datetime");
                entity.Property(e => e.EmployeeAppointmentId).HasColumnName("EmployeeAppointmentID");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmploymentCommunicationDetailId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmploymentCommunicationDetailID");
                entity.Property(e => e.EmploymentCommunicationId).HasColumnName("EmploymentCommunicationID");
                entity.Property(e => e.HireDate).HasColumnType("datetime");
                entity.Property(e => e.LicenseExpireDate).HasColumnType("datetime");
                entity.Property(e => e.RehireDate).HasColumnType("datetime");
                entity.Property(e => e.RequiredCreditHours).HasColumnType("numeric(9, 2)");
                entity.Property(e => e.SentToAgentDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<VwEmploymentCompanyRequirement>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmploymentCompanyRequirements", "Audit");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("asset_id");
                entity.Property(e => e.AssetSk).HasColumnName("asset_sk");
                entity.Property(e => e.EmploymentCompanyRequirementId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmploymentCompanyRequirementID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.LearningProgramCompletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("learning_program_completion_date");
                entity.Property(e => e.LearningProgramEnrollmentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("learning_program_enrollment_date");
                entity.Property(e => e.LearningProgramStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("learning_program_status");
            });
            modelBuilder.Entity<VwEmploymentHistory>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmploymentHistory", "Audit");

                entity.Property(e => e.BackGroundCheckNotes)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.BackgroundCheckStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EmploymentHistoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmploymentHistoryID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.HireDate).HasColumnType("datetime");
                entity.Property(e => e.HrtermCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("HRTermCode");
                entity.Property(e => e.HrtermDate)
                    .HasColumnType("datetime")
                    .HasColumnName("HRTermDate");
                entity.Property(e => e.NotifiedTermDate).HasColumnType("datetime");
                entity.Property(e => e.RehireDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<VwEmploymentJobTitle>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmploymentJobTitle", "Audit");

                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.EmploymentJobTitleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmploymentJobTitleID");
                entity.Property(e => e.JobTitleDate).HasColumnType("datetime");
                entity.Property(e => e.JobTitleId).HasColumnName("JobTitleID");
            });
            modelBuilder.Entity<VwEmploymentLicenseIncentive>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_EmploymentLicenseIncentive", "Audit");

                entity.Property(e => e.CcdBmemploymentId).HasColumnName("CCdBMEmploymentID");
                entity.Property(e => e.DmapprovalDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DMApprovalDate");
                entity.Property(e => e.DmdeclinedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DMDeclinedDate");
                entity.Property(e => e.DmemploymentId).HasColumnName("DMEmploymentID");
                entity.Property(e => e.DmsentBySoeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("DMSentBySOEID");
                entity.Property(e => e.DmsentDate)
                    .HasColumnType("datetime")
                    .HasColumnName("DMSentDate");
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmploymentLicenseIncentiveId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("EmploymentLicenseIncentiveID");
                entity.Property(e => e.IncentiveStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.IncetivePeriodDate).HasColumnType("datetime");
                entity.Property(e => e.RollOutGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwExam>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_Exam", "Audit");

                entity.Property(e => e.DeliveryMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ExamFees).HasColumnType("money");
                entity.Property(e => e.ExamId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ExamID");
                entity.Property(e => e.ExamName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.ExamProviderId).HasColumnName("ExamProviderID");
                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.XxxIsActive).HasColumnName("xxxIsActive");
            });
            modelBuilder.Entity<VwJobTitle>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_JobTitles", "Audit");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");
                entity.Property(e => e.JobCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);
                entity.Property(e => e.JobTitle)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.JobTitleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("JobTitleID");
                entity.Property(e => e.LicenseIncentive)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseLevel)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.Reviewed).HasColumnType("datetime");
            });
            modelBuilder.Entity<VwLearningAsset>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_LearningAsset", "Audit");

                entity.Property(e => e.AddedDate).HasColumnType("date");
                entity.Property(e => e.AssetId)
                    .HasMaxLength(36)
                    .IsUnicode(false);
                entity.Property(e => e.AssetTitle)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.ModifiedDate).HasColumnType("date");
            });
            modelBuilder.Entity<VwLicense>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_License", "Audit");

                entity.Property(e => e.AppointmentFees).HasColumnType("money");
                entity.Property(e => e.Incentive2PlusMrpay)
                    .HasColumnType("money")
                    .HasColumnName("Incentive2_PlusMRPay");
                entity.Property(e => e.Incentive2PlusTmpay)
                    .HasColumnType("money")
                    .HasColumnName("Incentive2_PlusTMPay");
                entity.Property(e => e.LicIncentive3Mrpay)
                    .HasColumnType("money")
                    .HasColumnName("LicIncentive3MRPay");
                entity.Property(e => e.LicIncentive3Tmpay)
                    .HasColumnType("money")
                    .HasColumnName("LicIncentive3TMPay");
                entity.Property(e => e.LicenseAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseFees).HasColumnType("money");
                entity.Property(e => e.LicenseId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LicenseID");
                entity.Property(e => e.LicenseName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.LineOfAuthorityId).HasColumnName("LineOfAuthorityID");
                entity.Property(e => e.PlsIncentive1Mrpay)
                    .HasColumnType("money")
                    .HasColumnName("PLS_Incentive1MRPay");
                entity.Property(e => e.PlsIncentive1Tmpay)
                    .HasColumnType("money")
                    .HasColumnName("PLS_Incentive1TMPay");
                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<VwLicenseApplication>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_LicenseApplication", "Audit");

                entity.Property(e => e.ApplicationStatus)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.ApplicationType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.LicenseApplicationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LicenseApplicationID");
                entity.Property(e => e.RecFromAgentDate).HasColumnType("datetime");
                entity.Property(e => e.RecFromStateDate).HasColumnType("datetime");
                entity.Property(e => e.RenewalDate).HasColumnType("datetime");
                entity.Property(e => e.RenewalMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.SentToAgentDate).HasColumnType("datetime");
                entity.Property(e => e.SentToStateDate).HasColumnType("datetime");
            });
            modelBuilder.Entity<VwLicenseByStateTransformation>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VW_LicenseByStateTransformation", "dbo");

                entity.Property(e => e.CurrentLicName)
                    .HasMaxLength(26)
                    .IsUnicode(false);
                entity.Property(e => e.NewLicName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(7)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwLicenseCompany>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_LicenseCompany", "Audit");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.LicenseCompanyId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LicenseCompanyID");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            });
            modelBuilder.Entity<VwLicenseExam>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_LicenseExam", "Audit");

                entity.Property(e => e.ExamId).HasColumnName("ExamID");
                entity.Property(e => e.LicenseExamId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LicenseExamID");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            });
            modelBuilder.Entity<VwLicensePreEducation>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_LicensePreEducation", "Audit");

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
                entity.Property(e => e.LicensePreEducationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LicensePreEducationID");
                entity.Property(e => e.PreEducationId).HasColumnName("PreEducationID");
            });
            modelBuilder.Entity<VwLicenseProduct>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_LicenseProduct", "Audit");

                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
                entity.Property(e => e.LicenseProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LicenseProductID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
            });
            modelBuilder.Entity<VwLicenseTech>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_LicenseTech", "Audit");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseTechEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseTechFax)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.LicenseTechId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LicenseTechID");
                entity.Property(e => e.LicenseTechPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Soeid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SOEID");
                entity.Property(e => e.TeamNum)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwLicenseTransformation>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("VW_LicenseTransformation", "dbo");

                entity.Property(e => e.CurrentLicName)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.NewLicName)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwLineOfAuthority>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_LineOfAuthority", "Audit");

                entity.Property(e => e.LineOfAuthorityAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.LineOfAuthorityId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LineOfAuthorityID");
                entity.Property(e => e.LineOfAuthorityName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwNewHirePackage>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_NewHirePackage", "Audit");

                entity.Property(e => e.PackageId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PackageID");
                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<VwPreEducation>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_PreEducation", "Audit");

                entity.Property(e => e.DeliveryMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.EducationName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.EducationProviderId).HasColumnName("EducationProviderID");
                entity.Property(e => e.Fees).HasColumnType("money");
                entity.Property(e => e.PreEducationId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PreEducationID");
                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.XxxIsActive).HasColumnName("xxxIsActive");
            });
            modelBuilder.Entity<VwProduct>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_Product", "Audit");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");
                entity.Property(e => e.ExpireDate).HasColumnType("datetime");
                entity.Property(e => e.ProductAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.ProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ProductID");
                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.XxxAgentMaster).HasColumnName("xxxAgentMaster");
                entity.Property(e => e.XxxIsActive).HasColumnName("xxxIsActive");
            });
            modelBuilder.Entity<VwRequiredLicense>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_RequiredLicenses", "Audit");

                entity.Property(e => e.BmincentiveAmt3)
                    .HasColumnType("money")
                    .HasColumnName("BMIncentiveAmt3");
                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.Incentive2Plus).HasColumnName("Incentive2_Plus");
                entity.Property(e => e.Incentive2PlusBmamt)
                    .HasColumnType("money")
                    .HasColumnName("Incentive2_PlusBMAmt");
                entity.Property(e => e.Incentive2PlusTmamt)
                    .HasColumnType("money")
                    .HasColumnName("Incentive2_PlusTMAmt");
                entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
                entity.Property(e => e.PackageId).HasColumnName("PackageID");
                entity.Property(e => e.PlsIncentive1).HasColumnName("PLS_Incentive1");
                entity.Property(e => e.PlsIncentive1Bmamt)
                    .HasColumnType("money")
                    .HasColumnName("PLS_Incentive1BMAmt");
                entity.Property(e => e.PlsIncentive1Tmamt)
                    .HasColumnType("money")
                    .HasColumnName("PLS_Incentive1TMAmt");
                entity.Property(e => e.RenewalDocument).IsUnicode(false);
                entity.Property(e => e.RequiredLicenseId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RequiredLicenseID");
                entity.Property(e => e.RequirementType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.ResStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.StartDocument).IsUnicode(false);
                entity.Property(e => e.TmincentiveAmt3)
                    .HasColumnType("money")
                    .HasColumnName("TMIncentiveAmt3");
                entity.Property(e => e.WorkStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<VwStateProvince>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_StateProvince", "Audit");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.DoiaddressId).HasColumnName("DOIAddressID");
                entity.Property(e => e.Doiname)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DOIName");
                entity.Property(e => e.LicenseTechId).HasColumnName("LicenseTechID");
                entity.Property(e => e.StateProvinceAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.StateProvinceCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.StateProvinceName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<VwTickler>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_Tickler", "Audit");

                entity.Property(e => e.EmployeeLicenseId).HasColumnName("EmployeeLicenseID");
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.LicenseTechId).HasColumnName("LicenseTechID");
                entity.Property(e => e.LkpValue)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.Message)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.TicklerCloseByLicenseTechId).HasColumnName("TicklerCloseByLicenseTechID");
                entity.Property(e => e.TicklerCloseDate).HasColumnType("datetime");
                entity.Property(e => e.TicklerDate).HasColumnType("datetime");
                entity.Property(e => e.TicklerDueDate).HasColumnType("datetime");
                entity.Property(e => e.TicklerId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TicklerID");
            });
            modelBuilder.Entity<VwTransferHistory>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("vw_TransferHistory", "Audit");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.EmploymentId).HasColumnName("EmploymentID");
                entity.Property(e => e.ResStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Scorenumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SCORENumber");
                entity.Property(e => e.TransferDate).HasColumnType("datetime");
                entity.Property(e => e.TransferHistoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("TransferHistoryID");
                entity.Property(e => e.WorkStateAbv)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<WorkList>(entity =>
            {
                entity.HasKey(e => e.WorkListName).HasName("PK__WorkList__7874C3FF");

                entity.ToTable("WorkList", "dbo");

                entity.Property(e => e.WorkListName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Fieldlist)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<WorkListDatum>(entity =>
            {
                entity.HasKey(e => e.WorkListDataId).HasName("PK__WorkListData__7A5D0C71");

                entity.ToTable("WorkListData", "dbo");

                entity.Property(e => e.WorkListDataId).HasColumnName("WorkListDataID");
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
                entity.Property(e => e.LicenseTech)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.ProcessDate).HasColumnType("datetime");
                entity.Property(e => e.ProcessedBy)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.WorkListData)
                    .HasMaxLength(8000)
                    .IsUnicode(false);
                entity.Property(e => e.WorkListName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.WorkListNameNavigation).WithMany(p => p.WorkListData)
                    .HasForeignKey(d => d.WorkListName)
                    .HasConstraintName("FK__WorkListD__WorkL__7B5130AA");
            });
            modelBuilder.Entity<XxxEdr>(entity =>
            {
                entity.HasKey(e => e.Edr).HasName("PK__EDR__76B698BF");

                entity.ToTable("xxxEDR", "dbo");

                entity.Property(e => e.Edr)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("EDR");
                entity.Property(e => e.BranchKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BranchKEY");
                entity.Property(e => e.ClosedDate)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                entity.Property(e => e.CountryId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CountryID");
                entity.Property(e => e.FacilityCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);
                entity.Property(e => e.Faxnumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FAXNumber");
                entity.Property(e => e.Lutype1)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("LUType1");
                entity.Property(e => e.ManagerNameFirst)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.ManagerNameLast)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.ManagerNameMaternal)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.ManagerNameMiddle)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.OpenClosedInd)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasColumnName("OpenClosedIND");
                entity.Property(e => e.OpenClosedInddesc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("OpenClosedINDDESC");
                entity.Property(e => e.OpenDate)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                entity.Property(e => e.PoboxDrawer)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("POBoxDrawer");
                entity.Property(e => e.PoboxLabel)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("POBoxLabel");
                entity.Property(e => e.Pocity)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("POCity");
                entity.Property(e => e.Postate)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("POState");
                entity.Property(e => e.Pozip)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("POZip");
                entity.Property(e => e.Puid)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("PUID");
                entity.Property(e => e.ReportingName)
                    .HasMaxLength(18)
                    .IsUnicode(false);
                entity.Property(e => e.StreetAddress1)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.StreetAddress2)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.StreetAddress3)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.StreetCity)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.StreetState)
                    .HasMaxLength(5)
                    .IsUnicode(false);
                entity.Property(e => e.StreetZip)
                    .HasMaxLength(9)
                    .IsUnicode(false);
                entity.Property(e => e.SupervisoryCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.SupervisoryDesc)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.Telephone)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.TypeDesc)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.TypeforNetworkComm)
                    .HasMaxLength(3)
                    .IsUnicode(false);
                entity.Property(e => e.UpdateDate)
                    .HasMaxLength(8)
                    .IsUnicode(false);
                entity.Property(e => e.UpdateUserId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("UpdateUserID");
                entity.Property(e => e.Vpsdest)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("VPSDest");
            });

            #region STORED PROCEDURES
            //modelBuilder.Entity<SPOUT_uspEmployeeGridSearchNew>().ToView("SPOUT_SearchEmployee", "dbo").HasNoKey();
            //modelBuilder.Entity<OputEmployeeSearchResult>().ToTable("SPOUT_SearchEmployee").HasNoKey();
            #endregion

            #region Raw SQL Queries
            modelBuilder.Entity<OputEmployeeSearchResult>().ToSqlQuery("OputEmployeeSearchResult").HasNoKey();
            modelBuilder.Entity<OputAgentHiearchy>().ToSqlQuery("OputAgentHiearchy").HasNoKey();
            modelBuilder.Entity<OputVarDropDownList>().ToSqlQuery("OputVarDropDownList").HasNoKey();
            modelBuilder.Entity<OputVarDropDownList_v2>().ToSqlQuery("OputVarDropDownList_v2").HasNoKey();
            modelBuilder.Entity<OputLicenseIncentiveInfo>().ToSqlQuery("OputLicenseIncentiveInfo").HasNoKey();
            modelBuilder.Entity<OputIncentiveRolloutGroup>().ToSqlQuery("OputIncentiveRolloutGroup").HasNoKey();
            modelBuilder.Entity<OputIncentiveBMMgr>().ToSqlQuery("OputIncentiveBMMgr").HasNoKey();
            modelBuilder.Entity<OputIncentiveTechName>().ToSqlQuery("OputIncentiveTechName").HasNoKey();
            modelBuilder.Entity<OputIncentiveDMMgr>().ToSqlQuery("OputIncentiveDMMgr").HasNoKey();
            modelBuilder.Entity<OputStockTickler>().ToSqlQuery("OputStockTickler").HasNoKey();
            modelBuilder.Entity<OputCompanyRequirement>().ToSqlQuery("OputCompanyRequirement").HasNoKey();
            modelBuilder.Entity<OputEducationRule>().ToSqlQuery("OputEducationRule").HasNoKey();
            modelBuilder.Entity<OputStateProvince>().ToSqlQuery("OputStateProvince").HasNoKey();
            modelBuilder.Entity<OputWorkListDataItem>().ToSqlQuery("OputWorkListDataItem").HasNoKey();
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
       
    }
}
