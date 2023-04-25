DROP TABLE IF EXISTS Attachments;
GO

CREATE TABLE Attachments(
	VideoID int
		CONSTRAINT Attachments_FK_VideoID_Ref_VideoID FOREIGN KEY REFERENCES Videos(VideoID),
	[FileName] varchar(255),
	FileGUID uniqueidentifier ROWGUIDCOL NOT NULL
		CONSTRAINT Attachments_Default_FileGUID DEFAULT(NewID()) 
		CONSTRAINT Attachments_Unique_FileGUID UNIQUE,
	FileData varbinary(max) FILESTREAM NULL,
	UploadDate date NOT NULL
		CONSTRAINT Attachments_Default_UploadDate DEFAULT(CURRENT_TIMESTAMP),
	CONSTRAINT Attachments_PK_VideoID_FileName PRIMARY KEY(VideoID, [FileName])
) FILESTREAM_ON FilestreamDefault;
GO