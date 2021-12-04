if not exists (select 1 from dbo.[TodoItems])
begin
	insert into dbo.[TodoItems] (Id, Name, DueDate, Priority, Status, City, AssignTo)
	values (CONVERT(uniqueidentifier, '36a12bfa-d2c4-4dc9-9906-eae63b4319a0'), 'Todo 1', CURRENT_TIMESTAMP, 2, 1, 'city1', 'a1@gmail.com'),
		   (CONVERT(uniqueidentifier, '36a12bfa-1111-2222-3333-eae63b4319a0'), 'Todo 2', CURRENT_TIMESTAMP, 0, 0, 'city2', 'a2@gmail.com');
end
