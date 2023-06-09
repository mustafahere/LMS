USE [master]
GO
/****** Object:  Database [LMS]    Script Date: 09/01/2022 8:46:24 pm ******/
CREATE DATABASE [LMS]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LMS', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MUSTAFA\MSSQL\DATA\LMS.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LMS_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MUSTAFA\MSSQL\DATA\LMS_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [LMS] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LMS].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LMS] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LMS] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LMS] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LMS] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LMS] SET ARITHABORT OFF 
GO
ALTER DATABASE [LMS] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LMS] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LMS] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LMS] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LMS] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LMS] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LMS] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LMS] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LMS] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LMS] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LMS] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LMS] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LMS] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LMS] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LMS] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LMS] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LMS] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LMS] SET RECOVERY FULL 
GO
ALTER DATABASE [LMS] SET  MULTI_USER 
GO
ALTER DATABASE [LMS] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LMS] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LMS] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LMS] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LMS] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LMS', N'ON'
GO
ALTER DATABASE [LMS] SET QUERY_STORE = OFF
GO
USE [LMS]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [LMS]
GO
/****** Object:  UserDefinedFunction [dbo].[_countBooks]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[_countBooks]()
returns int
as
begin
return (select count(*) from tblBooks)
end

GO
/****** Object:  UserDefinedFunction [dbo].[_countBooksIssued]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[_countBooksIssued]()
returns int
as
begin
return (select count(*) from tblIssued)
end
GO
/****** Object:  UserDefinedFunction [dbo].[_countUsers]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[_countUsers](
@role nvarchar(20)
)
returns int
as
begin
return (select count(*) from tblUsers where userRole = @role)
end

GO
/****** Object:  UserDefinedFunction [dbo].[studentDashboard]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[studentDashboard](
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

GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[email] [nvarchar](30) NOT NULL,
	[password] [nvarchar](30) NOT NULL,
	[createdAt] [datetime] NOT NULL,
	[isActive] [int] NULL,
	[userRole] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblAuthors]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAuthors](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[authorName] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblBooks]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBooks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bookTitle] [nvarchar](30) NOT NULL,
	[authorId] [int] NULL,
	[bookType] [nvarchar](20) NOT NULL,
	[description] [text] NULL,
	[quantity] [int] NOT NULL,
	[createdAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[bookTitle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tblIssued]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblIssued](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [int] NULL,
	[bookId] [int] NULL,
	[issuedAt] [datetime] NOT NULL,
	[returned] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[_issued]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[_issued] 
as
select i.id as issuedId, i.studentId, u.userName as studentName, i.bookId, b.bookTitle,a.authorName, i.issuedAt
from tblIssued i  
inner join tblUsers u on i.studentId = u.id 
inner join tblBooks b on i.bookId = b.id 
inner join tblAuthors a on b.authorId = a.id
GO
/****** Object:  View [dbo].[_admins]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[_admins]
as 
select * from tblUsers where userRole = 'admin'
GO
/****** Object:  View [dbo].[_books]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[_books]
as
select b.id as bookId, b.bookTitle, b.bookType, b.createdAt, b.quantity, a.authorName
from tblBooks b 
inner join tblAuthors a on b.authorId = a.id
GO
/****** Object:  View [dbo].[_NotIssuedBooks]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[_NotIssuedBooks] 
as 
select b.id from tblBooks b left join tblIssued i on b.id = i.bookId where (i.returned is NULL or i.returned = 1) and b.quantity > 0

GO
/****** Object:  Table [dbo].[tblReturned]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblReturned](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[studentId] [int] NULL,
	[bookId] [int] NULL,
	[returnedAt] [datetime] NOT NULL,
	[fine] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[_returned]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[_returned] 
as
select r.id as returnedId, r.studentId, u.userName as studentName, r.bookId, b.bookTitle,a.authorName, r.returnedAt,r.fine
from tblReturned r  
inner join tblUsers u on r.studentId = u.id 
inner join tblBooks b on r.bookId = b.id 
inner join tblAuthors a on b.authorId = a.id
GO
/****** Object:  View [dbo].[_students]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[_students]
as 
select u.id,u.userName,u.email,u.password,u.createdAt,u.isActive,u.userRole,
(select count(studentId) from tblIssued where returned=0 and studentId=u.id) as booksIssued,
(select count(studentId) from tblIssued where returned=1 and studentId=u.id) as booksReturned 
from tblUsers u where userRole = 'student'
GO
ALTER TABLE [dbo].[tblBooks] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[tblIssued] ADD  DEFAULT (getdate()) FOR [issuedAt]
GO
ALTER TABLE [dbo].[tblIssued] ADD  DEFAULT ((0)) FOR [returned]
GO
ALTER TABLE [dbo].[tblReturned] ADD  DEFAULT (getdate()) FOR [returnedAt]
GO
ALTER TABLE [dbo].[tblUsers] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[tblBooks]  WITH CHECK ADD FOREIGN KEY([authorId])
REFERENCES [dbo].[tblAuthors] ([id])
GO
ALTER TABLE [dbo].[tblIssued]  WITH CHECK ADD FOREIGN KEY([bookId])
REFERENCES [dbo].[tblBooks] ([id])
GO
ALTER TABLE [dbo].[tblIssued]  WITH CHECK ADD FOREIGN KEY([studentId])
REFERENCES [dbo].[tblUsers] ([id])
GO
ALTER TABLE [dbo].[tblReturned]  WITH CHECK ADD FOREIGN KEY([bookId])
REFERENCES [dbo].[tblBooks] ([id])
GO
ALTER TABLE [dbo].[tblReturned]  WITH CHECK ADD FOREIGN KEY([studentId])
REFERENCES [dbo].[tblUsers] ([id])
GO
/****** Object:  StoredProcedure [dbo].[_Login]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[_Login] @email nvarchar(20), @password nvarchar(30)
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

GO
/****** Object:  StoredProcedure [dbo].[_returnBook]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[_returnBook] 
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
GO
/****** Object:  StoredProcedure [dbo].[studentIssuedBooks]    Script Date: 09/01/2022 8:46:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[studentIssuedBooks](
@stdId int
)
as
begin
select (select bookTitle from tblBooks where id = bookId) as bookTitle,issuedAt, DATEADD(day,3,issuedAt) as returnTime from tblIssued  where returned = 0 and studentId = @stdId
end
GO
USE [master]
GO
ALTER DATABASE [LMS] SET  READ_WRITE 
GO
