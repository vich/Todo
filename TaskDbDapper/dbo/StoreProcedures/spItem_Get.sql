CREATE PROCEDURE [dbo].[spItem_Get]
	@Id int
AS
begin
	select *
	from dbo.[TodoItems]
	where Id = @Id;
end