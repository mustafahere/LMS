create database LMS;
use LMS;

insert into tblUsers values('root','root@lms.com','123456',CURRENT_TIMESTAMP ,1,'super_admin');
select * from tblUsers
update tblUsers set isActive = 1 where id = 1
Exec _Login 'root@lms.com','123456'
insert into tblUsers values('mustafa','root@lms.com','123',CURRENT_TIMESTAMP ,1,'admin')
drop trigger _signup
drop procedure _Login
select * from tblIssued
select * from tblReturned
select dbo._countBooks()
select * from _books
select * from tblBooks
select * from tblUsers where userRole = 'student' and isActive = 1
select * from _issued;
select * from _returned;
select * from tblIssued
select * from tblReturned;
insert into tblIssued values(2,8,CURRENT_TIMESTAMP,0)
truncate table tblIssued;
select count(studentId) as issuedBooks from tblIssued i where returned = 0 and studentId = 2 group by studentId;
drop view _NotIssuedBooks
select * from _NotIssuedBooks
select dbo.studentDashboard(2,'total_fine')
exec studentIssuedBooks 2
select * from tblBooks where id=6
select bookId from tblIssued where studentId = 2 and returned = 0








select * from tblReturned
select * from tblIssued

truncate table tblIssued;
truncate table tblReturned;

exec _returnBook 2,8

select id,userName,email,createdAt,isActive from _admins

select id,userName,email,createdAt,isActive, booksIssued, booksReturned
from _students