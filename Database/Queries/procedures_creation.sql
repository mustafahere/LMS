use LMS;


create procedure _Login @email nvarchar(20), @password nvarchar(30)
as
begin
declare @isValid int
set @isValid = 0
if(select count(*) from tblUsers where email = @email and password = @password) = 0 
	throw 50001, 'Email or Password is incorrect!', 1; 
else
begin
if(select count(*) from tblUsers where email = @email and isActive=1) = 0 
throw 50002, 'Account is not active!', 1; 
else
set @isValid = 1
end
if @isValid = 1
begin
select userRole,id,concat(upper(substring(userName,1,1)),substring(userName,2,Len(userName))) as userName from tblUsers where email = @email
end
end



create procedure studentIssuedBooks(
@stdId int
)
as
begin
select (select bookTitle from tblBooks where id = bookId) as bookTitle,issuedAt, DATEADD(day,3,issuedAt) as returnTime from tblIssued  where returned = 0 and studentId = @stdId
end



create procedure _returnBook 
(@stdId int,@bookId int)
as
begin
declare @returnedTime datetime = (select dateadd(day,3,issuedAt) from tblIssued where studentId = @stdId and returned = 0 and bookId = @bookId) 
declare @diff int = (select DATEDIFF(DAY,@returnedTime,CURRENT_TIMESTAMP))
declare @fine int = 0
if @diff > 0
set @fine = @diff * 100;
insert into tblReturned values(@stdId,@bookId,CURRENT_TIMESTAMP,@fine)
end