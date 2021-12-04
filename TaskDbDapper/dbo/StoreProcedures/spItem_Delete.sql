CREATE PROCEDURE [dbo].[spItem_Delete]
	@Id UNIQUEIDENTIFIER
AS
begin
	delete
	from dbo.[TodoItems]
	where Id = @Id;
end