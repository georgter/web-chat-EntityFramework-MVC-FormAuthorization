CREATE DATABASE WebChatFinal
GO
USE WebChatFinal
GO
CREATE TABLE Users(
	Id INT CONSTRAINT PK_Users_Id PRIMARY KEY IDENTITY(1,1),
	[Login] NVARCHAR(100)  NOT NULL,
	Pass NVARCHAR(100) NOT NULL,
	CONSTRAINT UQ_Users_Login UNIQUE([Login])
 )
GO
CREATE TABLE LogChats
(
Id INT IDENTITY(1,1) CONSTRAINT PK_LogChats_Id PRIMARY KEY,
UserId INT CONSTRAINT FK_LogChats_UserId FOREIGN KEY REFERENCES dbo.Users(Id),
LogMessage NVARCHAR(MAX) NOT NULL,
LogDate DATETIME default getdate(),
)