USE [License]
GO

/****** Object:  Table [dbo].[stg_ADBankerImport]    Script Date: 10/7/2024 4:13:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[stg_ADBankerImport](
	[TeamMemberID] [int] NULL,
	[CourseState] [nvarchar](2) NULL,
	[StudentName] [nvarchar](50) NULL,
	[CourseTitle] [nvarchar](150) NULL,
	[CompletionDate] [date] NULL,
	[ReportedDate] [date] NULL,
	[TotalCredits] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsImportComplete] [bit] NOT NULL,
	[SortOrder] [nchar](1) NOT NULL,
	[ModifiedBy] [nvarchar](50) NULL,
	[ModifyDate] [datetime] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[stg_ADBankerImport] ADD  CONSTRAINT [DF_stg_ADBankerImport_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[stg_ADBankerImport] ADD  CONSTRAINT [DF_stg_ADBankerImport_IsImportComplete]  DEFAULT ((0)) FOR [IsImportComplete]
GO

ALTER TABLE [dbo].[stg_ADBankerImport] ADD  CONSTRAINT [DF_stg_ADBankerImport_SortOrder]  DEFAULT ((1)) FOR [SortOrder]
GO

ALTER TABLE [dbo].[stg_ADBankerImport] ADD  CONSTRAINT [DF_stg_ADBankerImport_ModifyDate]  DEFAULT (NULL) FOR [ModifyDate]
GO