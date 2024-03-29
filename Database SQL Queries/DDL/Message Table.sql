DROP TABLE IF EXISTS [Messages];
GO

CREATE TABLE [Messages](
	MessageID int IDENTITY(1, 1)
		CONSTRAINT Messages_PK_MessageID PRIMARY KEY,
	MessageType int NOT NULL
		CONSTRAINT Messages_Default_MessageType DEFAULT(0)
		CONSTRAINT Messages_CheckValid_MessageType CHECK(MessageType IN (0, 1, 2)),
	RecipientEmail varchar(254) NOT NULL,
	DateCreated date NOT NULL
		CONSTRAINT Messages_Default_DateCreated DEFAULT(CURRENT_TIMESTAMP),
	TimeSent datetime NULL,
	CourseCode char(6),
	AdditionalText varchar(8000)
);
GO

CREATE INDEX Messages_idx_DateCreated ON [Messages](DateCreated)
GO