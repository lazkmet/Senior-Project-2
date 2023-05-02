DROP FUNCTION IF EXISTS UDF_CourseMostRecentVisit

CREATE FUNCTION UDF_CourseMostRecentVisit(@UserID int, @CourseID int)
RETURNS time
AS
BEGIN
	RETURN (SELECT MAX(uv.LastVisited) FROM UserxVideo uv JOIN Videos v ON uv.VideoID = v.VideoID WHERE uv.UserID = @UserID AND v.CourseID = @CourseID)
END
GO