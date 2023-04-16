create database LMS;
use LMS;


create table tblUsers(
id int primary key IDENTITY(1,1),
userName nvarchar(50) not null,
email nvarchar(30) not null,
password nvarchar(30) not null,
createdAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
isActive int,
userRole nvarchar(20) not null   /*Role can be super_admin,admin,student */ 
)

create table tblAuthors(
id int primary key IDENTITY(1,1),
authorName nvarchar(30)
)

create table tblBooks(
id int primary key IDENTITY(1,1),
bookTitle nvarchar(30) not null UNIQUE,
authorId int foreign key references tblAuthors(id),
bookType nvarchar(20) not null,
description TEXT,
quantity int not null,
createdAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
)



create table tblIssued(
id int primary key IDENTITY(1,1),
studentId int foreign key references tblUsers(id),
bookId int foreign key references tblBooks(id),
issuedAt  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
returned int default 0
)

create table tblReturned(
id int primary key IDENTITY(1,1),
studentId int foreign key references tblUsers(id),
bookId int foreign key references tblBooks(id),
returnedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
fine int not null
)