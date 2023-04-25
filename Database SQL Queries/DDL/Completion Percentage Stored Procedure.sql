DROP PROCEDURE IF EXISTS CompletionPercentage;
GO

CREATE PROCEDURE CompletionPercentage @UserID int, @CourseID int AS
	SELECT COUNT(CASE uv.VideoCompleted WHEN 1 THEN 1 ELSE null END) / COUNT(*)
		FROM UserxVideo uv 
			INNER JOIN Videos v ON uv.VideoID = v.VideoID
		WHERE uv.UserID = @UserID
			AND v.CourseID = @CourseID;
GO