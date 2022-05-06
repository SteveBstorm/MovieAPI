CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Email] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [FisrtName] VARCHAR(50) NULL, 
    [LastName] VARCHAR(50) NULL, 
    [BIrthDate] DATETIME2 NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsAdmin] BIT NOT NULL DEFAULT 0 
)
