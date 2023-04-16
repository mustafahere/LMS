use LMS;

create trigger _issueBookTrigger
on tblIssued
instead of insert
as
begin

if (select count(studentId) from tblIssued where studentId = (select studentId from inserted) and returned = 0 and bookId = (select bookId from inserted)) > 0
throw 60001, 'You already issued that book', 1; 
else 
begin
declare @noOfBooks int = (select count(studentId) as issuedBooks from tblIssued i where returned = 0 and studentId = (select studentId from inserted) group by studentId)

if @noOfBooks >=3  
throw 60002, 'Cannot issue more than 3 books', 1; 
else
begin
insert into tblIssued select studentId,bookId,issuedAt,returned  from inserted
update tblBooks set quantity = quantity - 1 where id = (select bookId from inserted)
end
end
end



create trigger _returnBookTrigger
on tblReturned
instead of insert
as
begin
update tblBooks set quantity = quantity + 1 where id = (select bookId from inserted)
update tblIssued set returned = 1 where bookId = (select bookId from inserted) and studentId = (select studentId from inserted)
insert into tblReturned select studentId,bookId,returnedAt,fine from inserted
end




create trigger _signup
on tblUsers
instead of insert
as
begin
declare @count int = (select count(*) from tblUsers where email = (select email from inserted))
if @count = 0
begin
insert into tblUsers select userName,email,password,createdAt,isActive,userRole  from inserted
end
else
throw 50000, 'Email already exist!', 1; 
end