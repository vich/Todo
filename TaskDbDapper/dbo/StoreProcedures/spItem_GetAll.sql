CREATE PROCEDURE [dbo].[spItem_GetAll]
	@Id int
AS
begin
	select *
	from dbo.[TodoItems];
end