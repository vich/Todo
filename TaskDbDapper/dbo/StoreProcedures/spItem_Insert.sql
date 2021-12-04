CREATE PROCEDURE [dbo].[spItem_Insert]
    @Id       UNIQUEIDENTIFIER,
    @Name     NVARCHAR(MAX),
    @DueDate  DATETIME2(7),    
    @Priority INT,              
    @Status   INT,              
    @City     NVARCHAR (MAX),   
    @AssignTo NVARCHAR (MAX)   

AS
begin
	insert into dbo.[TodoItems] (Id, Name, DueDate, Priority, Status, City, AssignTo)
    values (@Id, @Name, @DueDate, @Priority, @Status, @City, @AssignTo);
end