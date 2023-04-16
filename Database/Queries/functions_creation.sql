use LMS;

create function _countUsers(
@role nvarchar(20)
)
returns int
as
begin
return (select count(*) from tblUsers where userRole = @role)
end



create function _countBooks()
returns int
as
begin
return (select count(*) from tblBooks)
end




create function _countBooksIssued()
returns int
as
begin
return (select count(*) from tblIssued)
end




create function studentDashboard(
@stdId int, 
@type nvarchar(20)
)
returns int
as
begin
declare @count int = 0;
if @type = 'total_books'
set @count = (select count(*) from tblBooks)
else if @type = 'total_fine'
set @count = (select ISNULL(SUM(fine),0) from tblReturned where studentId = @stdId)
else if @type = 'total_issued'
set @count = (select count(studentId) from tblIssued where studentId = @stdId and returned = 0)
else if @type = 'total_returned'
set @count = (select count(studentId) from tblIssued where studentId = @stdId and returned = 1)
return @count;
end