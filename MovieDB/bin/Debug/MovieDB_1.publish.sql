﻿/*
Script de déploiement pour MovieDB

Ce code a été généré par un outil.
La modification de ce fichier peut provoquer un comportement incorrect et sera perdue si
le code est régénéré.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "MovieDB"
:setvar DefaultFilePrefix "MovieDB"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Détectez le mode SQLCMD et désactivez l'exécution du script si le mode SQLCMD n'est pas pris en charge.
Pour réactiver le script une fois le mode SQLCMD activé, exécutez ce qui suit :
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Le mode SQLCMD doit être activé de manière à pouvoir exécuter ce script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Création de $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Impossible de modifier les paramètres de base de données. Vous devez être administrateur système pour appliquer ces paramètres.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'Impossible de modifier les paramètres de base de données. Vous devez être administrateur système pour appliquer ces paramètres.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Création de [dbo].[Actor]...';


GO
CREATE TABLE [dbo].[Actor] (
    [PersonID] INT          NOT NULL,
    [MovieID]  INT          NOT NULL,
    [Role]     VARCHAR (50) NOT NULL
);


GO
PRINT N'Création de [dbo].[Comment]...';


GO
CREATE TABLE [dbo].[Comment] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Content]  VARCHAR (MAX) NOT NULL,
    [PostDate] DATETIME2 (7) NOT NULL,
    [UserID]   INT           NULL,
    [MovieID]  INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de [dbo].[Movie]...';


GO
CREATE TABLE [dbo].[Movie] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Title]        VARCHAR (50)  NOT NULL,
    [Description]  VARCHAR (MAX) NOT NULL,
    [ReleaseYear]  INT           NOT NULL,
    [RealisatorID] INT           NULL,
    [ScenaristID]  INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de [dbo].[Person]...';


GO
CREATE TABLE [dbo].[Person] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [LastName]  VARCHAR (50) NOT NULL,
    [FirstName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Email]     VARCHAR (50)  NOT NULL,
    [Password]  VARCHAR (50)  NOT NULL,
    [FisrtName] VARCHAR (50)  NULL,
    [LastName]  VARCHAR (50)  NULL,
    [BIrthDate] DATETIME2 (7) NULL,
    [IsActive]  BIT           NOT NULL,
    [IsAdmin]   BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de contrainte sans nom sur [dbo].[User]...';


GO
ALTER TABLE [dbo].[User]
    ADD DEFAULT 1 FOR [IsActive];


GO
PRINT N'Création de contrainte sans nom sur [dbo].[User]...';


GO
ALTER TABLE [dbo].[User]
    ADD DEFAULT 0 FOR [IsAdmin];


GO
PRINT N'Création de [dbo].[FK_Person_Actor]...';


GO
ALTER TABLE [dbo].[Actor]
    ADD CONSTRAINT [FK_Person_Actor] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Person] ([Id]);


GO
PRINT N'Création de [dbo].[FK_Person_Movie]...';


GO
ALTER TABLE [dbo].[Actor]
    ADD CONSTRAINT [FK_Person_Movie] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movie] ([Id]);


GO
PRINT N'Création de [dbo].[FK_COMMENT_MOVIE]...';


GO
ALTER TABLE [dbo].[Comment]
    ADD CONSTRAINT [FK_COMMENT_MOVIE] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([Id]);


GO
PRINT N'Création de [dbo].[FK_COMMENT_USER]...';


GO
ALTER TABLE [dbo].[Comment]
    ADD CONSTRAINT [FK_COMMENT_USER] FOREIGN KEY ([MovieID]) REFERENCES [dbo].[Movie] ([Id]);


GO
PRINT N'Création de [dbo].[FK_Realistor]...';


GO
ALTER TABLE [dbo].[Movie]
    ADD CONSTRAINT [FK_Realistor] FOREIGN KEY ([RealisatorID]) REFERENCES [dbo].[Person] ([Id]);


GO
PRINT N'Création de [dbo].[FK_Scenarist]...';


GO
ALTER TABLE [dbo].[Movie]
    ADD CONSTRAINT [FK_Scenarist] FOREIGN KEY ([ScenaristID]) REFERENCES [dbo].[Person] ([Id]);


GO
-- Étape de refactorisation pour mettre à jour le serveur cible avec des journaux de transactions déployés

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '35623b57-cec1-4c41-83dc-4d205cd26f52')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('35623b57-cec1-4c41-83dc-4d205cd26f52')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'bc240416-187f-4d4b-a8fe-d5646d9eb68e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('bc240416-187f-4d4b-a8fe-d5646d9eb68e')

GO

GO
/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE MovieDB

INSERT INTO Person VALUES ('Hammil', 'Mark')
INSERT INTO Person VALUES ('Ford', 'Harisson')
INSERT INTO Person VALUES ('Fisher', 'Carrie')
INSERT INTO Person VALUES ('Lucas', 'George')
INSERT INTO Person VALUES ('Spielberg', 'Steven')
INSERT INTO Person VALUES ('Kershner', 'Irvin')
INSERT INTO Person VALUES ('Kasdan', 'Lawrence')

INSERT INTO Movie VALUES ('Star Wars - A New Hope', 'Space Opera', 1977, 4, 4)
INSERT INTO Movie VALUES ('Star Wars - Empire Strikes Back', 'Space Opera', 1979, 4, 6)
INSERT INTO Movie VALUES ('Indiana Jones - Cursed temple', 'Aventure', 1981, 5, 7)

INSERT INTO Actor VALUES (1, 1, 'Luke Skywalker')
INSERT INTO Actor VALUES (2, 1, 'Han Solo')
INSERT INTO Actor VALUES (3, 1, 'Leia Organa')
INSERT INTO Actor VALUES (1, 2, 'Luke Skywalker')
INSERT INTO Actor VALUES (2, 2, 'Han Solo')
INSERT INTO Actor VALUES (3, 2, 'Leia Organa')
INSERT INTO Actor VALUES (2, 3, 'Indiana Jones')

INSERT INTO [User] VALUES ('steve.lorent@bstorm.be', 'test1234', 'Steve', 'Lorent', '1983-06-28', 1, 1)
INSERT INTO [User] VALUES ('test@test.com', 'test1234', 'Test', 'Truc', '1992-03-12', 1, 0)

INSERT INTO Comment VALUES('Super film pour vieux geek', '2020-10-10', 1, 1)
INSERT INTO Comment VALUES('LE meilleur film de sci-fi de tout les temps', '2020-10-10', 1, 2)
INSERT INTO Comment VALUES('Je préfère le 3 :p', '2020-10-10', 2, 3)


GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Mise à jour terminée.';


GO
