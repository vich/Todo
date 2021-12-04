CREATE PROCEDURE [dbo].[spItem_Get]
	@Id UNIQUEIDENTIFIER
AS
begin
	select *
	from dbo.[TodoItems]
	where Id = @Id;
end