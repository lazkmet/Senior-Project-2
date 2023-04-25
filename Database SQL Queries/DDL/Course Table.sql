DROP TABLE IF EXISTS Courses;
GO

CREATE TABLE Courses(
	CourseID int Identity(1,1)
		CONSTRAINT Courses_PK_CourseID PRIMARY KEY,
	FileGUID uniqueidentifier ROWGUIDCOL NOT NULL
		CONSTRAINT Courses_Default_FileGUID DEFAULT(NewID()) 
		CONSTRAINT Courses_Unique_FileGUID UNIQUE,
	OwnerID int NOT NULL 
		CONSTRAINT Courses_FK_OwnerID_Ref_UserID FOREIGN KEY REFERENCES Users(UserID)
		CONSTRAINT Courses_Unique_OwnerID UNIQUE,
	CourseCode char(6) NOT NULL
		CONSTRAINT Courses_Unique_CourseCode UNIQUE,
	CourseName varchar(100) NOT NULL,
	CourseDescription varchar(500),
	CourseImage varbinary(max) FILESTREAM NULL,
	LessonLimitType tinyint
		CONSTRAINT Courses_CheckValid_LessonLimitType CHECK(LessonLimitType IN (0, 1, 2)),
	DateCreated date NOT NULL
		CONSTRAINT Courses_Default_DateCreated DEFAULT(CURRENT_TIMESTAMP)
) 
FILESTREAM_ON Images;
GO

CREATE INDEX Courses_idx_CourseCode ON Courses(CourseCode);
CREATE INDEX Courses_idx_CourseName ON Courses(CourseName);
GO