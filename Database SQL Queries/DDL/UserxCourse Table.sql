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

CREATE VIEW CourseStudents AS
	SELECT uc.CourseID, u.UserID, u.ProfilePicture, u.LastName, u.FirstName
	FROM Users u
		JOIN UserxCourse uc ON uc.UserID = u.UserID
GO