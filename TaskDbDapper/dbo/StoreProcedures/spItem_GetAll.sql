CREATE PROCEDURE [dbo].[spItem_GetAll]
AS
begin
	select *
	from dbo.[TodoItems];
end