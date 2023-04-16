use LMS;

create view _issued 
as
select i.id as issuedId, i.studentId, u.userName as studentName, i.bookId, b.bookTitle,a.authorName, i.issuedAt
from tblIssued i  
inner join tblUsers u on i.studentId = u.id 
inner join tblBooks b on i.bookId = b.id 
inner join tblAuthors a on b.authorId = a.id


create view _returned 
as
select r.id as returnedId, r.studentId, u.userName as studentName, r.bookId, b.bookTitle,a.authorName, r.returnedAt,r.fine
from tblReturned r  
inner join tblUsers u on r.studentId = u.id 
inner join tblBooks b on r.bookId = b.id 
inner join tblAuthors a on b.authorId = a.id


create view _books
as
select b.id as bookId, b.bookTitle, b.bookType, b.createdAt, b.quantity, a.authorName
from tblBooks b 
inner join tblAuthors a on b.authorId = a.id


create view _students
as 
select u.id,u.userName,u.email,u.password,u.createdAt,u.isActive,u.userRole,
(select count(studentId) from tblIssued where returned=0 and studentId=u.id) as booksIssued,
(select count(studentId) from tblIssued where returned=1 and studentId=u.id) as booksReturned 
from tblUsers u where userRole = 'student'


create view _admins
as 
select * from tblUsers where userRole = 'admin'


create view _NotIssuedBooks 
as 
select b.id from tblBooks b left join tblIssued i on b.id = i.bookId where (i.returned is NULL or i.returned = 1) and b.quantity > 0
