DROP TABLE IF EXISTS Videos;
GO

CREATE TABLE Videos(
	VideoID int IDENTITY(1,1)
		CONSTRAINT Videos_PK_VideoID PRIMARY KEY,
	FileGUID uniqueidentifier ROWGUIDCOL NOT NULL
		CONSTRAINT Videos_Default_FileGUID DEFAULT(NewID()) 
		CONSTRAINT Videos_Unique_FileGUID UNIQUE,
	CourseID int NOT NULL
		CONSTRAINT Videos_FK_CourseID_Ref_CourseID FOREIGN KEY REFERENCES Courses(CourseID),
	VideoTitle varchar(50) NOT NULL,
	VideoDescription varchar(300),
	OrderInCourse smallint NOT NULL,
	VideoType tinyint NOT NULL
		CONSTRAINT Videos_Default_VideoType DEFAULT(0),
		CONSTRAINT Videos_CheckValid_VideoType CHECK(VideoType IN (0, 1)),
	YTVideoURL varchar(255),
	YTUseDescription bit NOT NULL
		CONSTRAINT Videos_Default_YTUseDescription DEFAULT(0),
	VideoData varbinary(max) FILESTREAM NULL,
	CONSTRAINT Videos_NotNull_YTVideoURL CHECK(VideoType != 0 OR YTVideoURL IS NOT NULL),
	CONSTRAINT Videos_NotNull_VideoData CHECK(VideoType != 1 OR VideoData IS NOT NULL)
) FILESTREAM_ON Videos;
GO

CREATE INDEX Videos_idx_CourseID ON Videos(CourseID);
GO