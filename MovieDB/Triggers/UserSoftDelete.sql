CREATE TRIGGER [UserSoftDelete]
ON [dbo].[User]
INSTEAD OF DELETE
AS
BEGIN 
	UPDATE [User] SET IsActive = 0 WHERE Id = (SELECT Id FROM deleted) 
END