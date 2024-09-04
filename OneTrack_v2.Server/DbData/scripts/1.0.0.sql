USE [License]
GO

/****** Object:  Table [dbo].[stg_ADBankerImport]    Script Date: 6/12/2024 5:51:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[stg_ADBankerImport](
	[EmployeeID] [int] NULL,
	[CourseState] [nvarchar](2) NULL,
	[StudentName] [nvarchar](50) NULL,
	[CourseTitle] [nvarchar](150) NULL,
	[CompletionDate] [date] NULL,
	[ReportedDate] [date] NULL,
	[TotalCredits] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[IsImportComplete] [bit] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[stg_ADBankerImport] ADD  CONSTRAINT [DF_stg_ADBankerImport_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[stg_ADBankerImport] ADD  CONSTRAINT [DF_stg_ADBankerImport_IsImportComplete]  DEFAULT ((0)) FOR [IsImportComplete]
GO


