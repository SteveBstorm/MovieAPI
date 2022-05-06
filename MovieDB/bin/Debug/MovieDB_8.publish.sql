/*
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
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL12.TFTIC2014\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL12.TFTIC2014\MSSQL\DATA\"

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
USE [$(DatabaseName)];


GO
PRINT N'L''opération de refactorisation de changement de nom avec la clé 35623b57-cec1-4c41-83dc-4d205cd26f52 est ignorée, l''élément [dbo].[Comment].[Date] (SqlSimpleColumn) ne sera pas renommé en PostDate';


GO
PRINT N'L''opération de refactorisation de changement de nom avec la clé bc240416-187f-4d4b-a8fe-d5646d9eb68e est ignorée, l''élément [dbo].[Actor].[Id] (SqlSimpleColumn) ne sera pas renommé en PersonID';


GO
PRINT N'Création de [dbo].[FK_Realistor]...';


GO
ALTER TABLE [dbo].[Movie] WITH NOCHECK
    ADD CONSTRAINT [FK_Realistor] FOREIGN KEY ([RealisatorID]) REFERENCES [dbo].[Person] ([Id]) ON DELETE SET NULL ON UPDATE CASCADE;


GO
PRINT N'Création de [dbo].[FK_Scenarist]...';


GO
ALTER TABLE [dbo].[Movie] WITH NOCHECK
    ADD CONSTRAINT [FK_Scenarist] FOREIGN KEY ([ScenaristID]) REFERENCES [dbo].[Person] ([Id]) ON DELETE SET NULL ON UPDATE CASCADE;


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
GO
INSERT INTO Person VALUES ('Hammil', 'Mark')
INSERT INTO Person VALUES ('Ford', 'Harisson')
INSERT INTO Person VALUES ('Fisher', 'Carrie')
INSERT INTO Person VALUES ('Lucas', 'George')
INSERT INTO Person VALUES ('Spielberg', 'Steven')
INSERT INTO Person VALUES ('Kershner', 'Irvin')
INSERT INTO Person VALUES ('Kasdan', 'Lawrence')
GO

INSERT INTO Movie VALUES ('Star Wars - A New Hope', 'Space Opera', 1977, 4, 4)
INSERT INTO Movie VALUES ('Star Wars - Empire Strikes Back', 'Space Opera', 1979, 4, 6)
INSERT INTO Movie VALUES ('Indiana Jones - Cursed temple', 'Aventure', 1981, 5, 7)
GO

INSERT INTO Actor VALUES (1, 1, 'Luke Skywalker')
INSERT INTO Actor VALUES (2, 1, 'Han Solo')
INSERT INTO Actor VALUES (3, 1, 'Leia Organa')
INSERT INTO Actor VALUES (1, 2, 'Luke Skywalker')
INSERT INTO Actor VALUES (2, 2, 'Han Solo')
INSERT INTO Actor VALUES (3, 2, 'Leia Organa')
INSERT INTO Actor VALUES (2, 3, 'Indiana Jones')
GO

INSERT INTO [User] VALUES ('steve.lorent@bstorm.be', 'test1234', 'Steve', 'Lorent', '1983-06-28', 1, 1)
INSERT INTO [User] VALUES ('test@test.com', 'test1234', 'Test', 'Truc', '1992-03-12', 1, 0)
GO

INSERT INTO Comment VALUES('Super film pour vieux geek', '2020-10-10', 1, 1)
INSERT INTO Comment VALUES('LE meilleur film de sci-fi de tout les temps', '2020-10-10', 1, 2)
INSERT INTO Comment VALUES('Je préfère le 3 :p', '2020-10-10', 2, 3)
GO

GO
PRINT N'Vérification de données existantes par rapport aux nouvelles contraintes';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[Movie] WITH CHECK CHECK CONSTRAINT [FK_Realistor];

ALTER TABLE [dbo].[Movie] WITH CHECK CHECK CONSTRAINT [FK_Scenarist];


GO
PRINT N'Mise à jour terminée.';


GO
