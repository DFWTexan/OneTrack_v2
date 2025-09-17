USE [License]
GO


INSERT INTO [dbo].[Communications]
           ([CommunicationName]
           ,[DocTypeAbv]
           ,[DocType]
           ,[DocSubType]
           ,[DocAppType]
           ,[EmailAttachments]
           ,[HasNote]
           ,[IsActive])
VALUES
           ('Licensing Offer Letter'
           , 'APP'
           , 'Application'
           , 'Licensing Offer Letter'
           , 'Template'
           , NULL
           , 0
           , 1),
           ('AD Banker Registration Confirmation'
           , 'APP'
           , 'Application'
           , 'AD Banker Registration Confirmation'
           , 'Template'
           , NULL
           , 0
           , 1),
           ('Universal Pending License Notice'
           , 'APP'
           , 'Application'
           , 'Universal Pending License'
           , 'OneTrakEmail'
           , NULL
           , 0
           , 1)
GO
