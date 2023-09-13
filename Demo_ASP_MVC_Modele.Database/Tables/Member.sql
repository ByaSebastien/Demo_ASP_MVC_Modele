CREATE TABLE [dbo].[Member]
(
	[Id] INT NOT NULL IDENTITY,
	[Pseudo] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(150) NOT NULL,
	[Password_Hash] CHAR(97) NOT NULL,
	[Role] BIT DEFAULT 0


	CONSTRAINT PK_Member PRIMARY KEY ([Id]),
	CONSTRAINT UK_Member_Pseudo Unique ([Pseudo]),
	CONSTRAINT UK_Member_Email UNIQUE ([Email])
);
