SELECT COUNT(CASE WHEN dbo.UDF_CompletionPercentage(1, c.CourseID) = 100 THEN 1 ELSE null END) FROM Courses c

SELECT COUNT(*) FROM Courses c WHERE dbo.UDF_CompletionPercentage(1, c.CourseID) = 100;