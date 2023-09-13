CREATE TABLE [dbo].[Game]
(
	[Id] INT NOT NULL IDENTITY,
	[Name] NVARCHAR(50),
	[Description] NVARCHAR(1000) NULL,
	[Nb_Player_Min] INT NOT NULL,
	[Nb_Player_Max] INT NOT NULL,
	[Age] INT NULL,
	[Coop] BIT NOT NULL DEFAULT 0,

	CONSTRAINT PK_Game PRIMARY KEY ([Id]),
	CONSTRAINT UK_Game_Name UNIQUE ([Name]),
	CONSTRAINT CK_Game_Nb_Player CHECK ([Nb_Player_Max] >= [Nb_Player_Min]),
	CONSTRAINT CK_Game_Age CHECK ([Age] >= 0)
)
