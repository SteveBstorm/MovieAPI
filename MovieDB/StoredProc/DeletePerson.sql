CREATE PROCEDURE [dbo].[DeletePerson]
	@Id int
AS
BEGIN
	DELETE FROM Person WHERE Id = @Id AND @Id NOT IN (SELECT DISTINCT PersonID From Actor)
									  AND @Id NOT IN (SELECT DISTINCT RealisatorId FROM Movie)
									  AND @Id NOT IN (SELECT DISTINCT ScenaristId FROM Movie)
END
