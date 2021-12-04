CREATE PROCEDURE [dbo].[spItem_Delete]
	@Id int
AS
begin
	delete
	from dbo.[TodoItems]
	where Id = @Id;
end