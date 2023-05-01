DROP FUNCTION IF EXISTS UDF_CompletionPercentage;
GO

CREATE FUNCTION UDF_CompletionPercentage (@UserID int, @CourseID int)
RETURNS int
AS
BEGIN
	DECLARE @NumCourses int
	SET @NumCourses = (SELECT (CASE WHEN COUNT(*) = 0 THEN 1 ELSE COUNT(*) END) FROM Videos WHERE CourseID = @CourseID)
	RETURN (SELECT FLOOR((COUNT(CASE uv.VideoCompleted WHEN 1 THEN 1 ELSE null END) / CAST(@NumCourses AS float)) * 100) 			
				FROM UserxVideo uv 
					RIGHT JOIN Videos v ON uv.VideoID = v.VideoID
				WHERE uv.UserID = @UserID
					AND v.CourseID = @CourseID)
END
GO