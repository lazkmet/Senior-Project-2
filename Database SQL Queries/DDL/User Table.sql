DROP TABLE IF EXISTS Users;
GO

CREATE TABLE Users(
	UserID int IDENTITY(1,1) 
		CONSTRAINT Users_PK_UserID PRIMARY KEY,
	FileGUID uniqueidentifier ROWGUIDCOL NOT NULL 
		CONSTRAINT Users_Default_FileGUID DEFAULT(NewID()) 
		CONSTRAINT Users_Unique_FileGUID UNIQUE,
	EmailAddress varchar(254) NOT NULL
		CONSTRAINT Users_Unique_EmailAddress UNIQUE,
	EncryptedPassword varbinary(max) NOT NULL,
	FirstName varchar(50),
	LastName varchar(50),
	ProfilePicture varbinary(max) FILESTREAM NULL,
	UserType tinyint NOT NULL 
		CONSTRAINT Users_Default_UserType DEFAULT(0),
	DateCreated date NOT NULL 
		CONSTRAINT Users_Default_DateCreated DEFAULT(CURRENT_TIMESTAMP),
	EmailVerified bit NOT NULL 
		CONSTRAINT Users_Default_EmailVerified DEFAULT 0,
	LatestLogin datetime,
	WebsiteTheme tinyint NOT NULL 
		CONSTRAINT Users_Default_Theme DEFAULT 0 
		CONSTRAINT Users_CheckValid_Theme CHECK(WebsiteTheme IN (0, 1)),
	MinTextSize tinyint NOT NULL 
		CONSTRAINT Users_Default_MinTextSize DEFAULT 0,
	CourseOrdering tinyint NOT NULL 
		CONSTRAINT Users_Default_CourseOrdering DEFAULT 2 
		CONSTRAINT Users_CheckValid_CourseOrdering CHECK(CourseOrdering IN (0, 1, 2, 3)),
	StudentOrdering tinyint NOT NULL 
		CONSTRAINT Users_Default_StudentOrdering DEFAULT 1 
		CONSTRAINT Users_CheckValid_StudentOrdering CHECK(StudentOrdering IN (0, 1, 3))
)
FILESTREAM_ON Images;
GO

CREATE INDEX Users_idx_EmailAddress ON Users(EmailAddress);
GO