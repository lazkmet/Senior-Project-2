DROP TABLE IF EXISTS UserxVideo;
GO

CREATE TABLE UserxVideo(
	UserID int
		CONSTRAINT UserxVideo_FK_UserID_Ref_UserID FOREIGN KEY REFERENCES Users(UserID),
	VideoID int
		CONSTRAINT UserxVideo_FK_VideoID_Ref_VideoID FOREIGN KEY REFERENCES Videos(VideoID),
	VideoCompleted bit NOT NULL
		CONSTRAINT UserxVideo_Default_VideoCompleted DEFAULT(0),
	CurrentTime int NOT NULL
		CONSTRAINT UserxVideo_Default_CurrentTime DEFAULT(0),
	CONSTRAINT UserxVideo_PK_UserID_VideoID PRIMARY KEY(UserID, VideoID)
);
GO