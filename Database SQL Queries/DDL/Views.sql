DROP VIEW IF EXISTS ALLUsers
GO

CREATE VIEW AllUsers AS (
	SELECT u.UserID, u.EmailAddress, u.FirstName, u.LastName, u.DateCreated AS DateJoined, (SELECT COUNT(*) FROM Courses c WHERE c.OwnerID = u.UserID) AS CoursesOwned, (SELECT COUNT(*) FROM UserXCourse uc WHERE uc.UserID = u.UserID) AS CoursesJoined
	FROM Users u
);

GO

DROP VIEW IF EXISTS AllCourses
GO

CREATE VIEW AllCourses AS(
	SELECT c.CourseID, c.CourseName, u.FirstName, u.LastName, (SELECT COUNT(*) FROM CourseStudents cs WHERE cs.CourseID = c.CourseID) AS NumStudents, (SELECT COUNT(*) FROM Videos v WHERE v.CourseID = c.CourseID) AS NumVideos, c.DateCreated
	FROM Courses c 
	JOIN Users u ON c.OwnerID = u.UserID
);
GO

DROP VIEW IF EXISTS CourseStudents
GO

CREATE VIEW CourseStudents AS
	SELECT uc.CourseID, u.UserID, u.FileGUID, u.ProfilePicture, u.LastName, u.FirstName, dbo.UDF_CompletionPercentage(uc.UserID, uc.CourseID) AS CompletionPercentage
	FROM Users u
		JOIN UserxCourse uc ON uc.UserID = u.UserID
GO