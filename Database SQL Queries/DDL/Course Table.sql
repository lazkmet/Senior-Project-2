DROP TABLE IF EXISTS Courses;
GO

CREATE TABLE Courses(
	CourseID int Identity(1,1)
		CONSTRAINT Courses_PK_CourseID PRIMARY KEY,
	FileGUID uniqueidentifier ROWGUIDCOL NOT NULL
		CONSTRAINT Courses_Default_FileGUID DEFAULT(NewID()) 
		CONSTRAINT Courses_Unique_FileGUID UNIQUE,
	OwnerID int NOT NULL 
		CONSTRAINT Courses_FK_OwnerID_Ref_UserID FOREIGN KEY REFERENCES Users(UserID),
	CourseCode char(6) NOT NULL
		CONSTRAINT Courses_Unique_CourseCode UNIQUE,
	CourseName varchar(100) NOT NULL,
	CourseDescription varchar(500),
	CourseImage varbinary(max) FILESTREAM NULL,
	LessonLimitType tinyint NOT NULL
		CONSTRAINT Courses_Default_LessonLimitType DEFAULT 0
		CONSTRAINT Courses_CheckValid_LessonLimitType CHECK(LessonLimitType IN (0, 1, 2)),
	DateCreated date NOT NULL
		CONSTRAINT Courses_Default_DateCreated DEFAULT(CURRENT_TIMESTAMP)
) 
FILESTREAM_ON Images;
GO

CREATE VIEW AllCourses AS(
	SELECT c.CourseID, c.CourseName, u.FirstName, u.LastName, (SELECT COUNT(*) FROM CourseStudents cs WHERE cs.CourseID = c.CourseID) AS NumStudents, (SELECT COUNT(*) FROM Videos v WHERE v.CourseID = c.CourseID) AS NumVideos, c.DateCreated
	FROM Courses c 
	JOIN Users u ON c.OwnerID = u.UserID
);
GO