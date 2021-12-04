CREATE PROCEDURE [dbo].[spItem_Updaet]
    @Id       UNIQUEIDENTIFIER,
    @Name     NVARCHAR(MAX),
    @DueDate  DATETIME2(7),    
    @Priority INT,              
    @Status   INT,              
    @City     NVARCHAR (MAX),   
    @AssignTo NVARCHAR (MAX)   

AS
begin
    update dbo.[TodoItems]
    set Name = @Name, DueDate = @DueDate, Priority = @Priority, Status = @Status, City = @City, AssignTo = @AssignTo
    where Id = @Id
end