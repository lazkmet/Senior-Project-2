DROP TABLE IF EXISTS UserxCourse;
GO

CREATE TABLE UserxCourse(
	UserID int
		CONSTRAINT UserxCourse_FK_UserID_Ref_UserID FOREIGN KEY REFERENCES Users(UserID),
	CourseID int
		CONSTRAINT UserxCourse_FK_CourseID_Ref_CourseID FOREIGN KEY REFERENCES Courses(CourseID),
	DateJoined date NOT NULL
		CONSTRAINT UserxCourse_Default_DateJoined DEFAULT(CURRENT_TIMESTAMP),
	CONSTRAINT UserxCourse_PK_UserID_CourseID PRIMARY KEY(UserID, CourseID)
);
GO