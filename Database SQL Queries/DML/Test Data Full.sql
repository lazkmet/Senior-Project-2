INSERT INTO Users (EmailAddress, FirstName, LastName, UserType, EmailVerified, EncryptedPassword)
VALUES
('fakeemail@email.com', 'Test', 'One', 2, 1, CONVERT(Varbinary(max), '0xB132FA3FAA0F7681C0CA6A857693DD7F96AD1A4F0B9B4BAF84DF6BBDF96BA114B996D9DE', 1)),
('anotherfake@email.com', 'Test', 'Two', 1, 1, CONVERT(Varbinary(max), '0xB132FA3FAA0F7681C0CA6A857693DD7F96AD1A4F0B9B4BAF84DF6BBDF96BA114B996D9DE', 1)),
('alsofake@email.com', 'Test', 'Three', 1, 1, CONVERT(Varbinary(max), '0xB132FA3FAA0F7681C0CA6A857693DD7F96AD1A4F0B9B4BAF84DF6BBDF96BA114B996D9DE', 1)),
('asdf@email.com', 'Test', 'Four', 1, 1, CONVERT(Varbinary(max), '0xB132FA3FAA0F7681C0CA6A857693DD7F96AD1A4F0B9B4BAF84DF6BBDF96BA114B996D9DE', 1)),
('noemail@email.com', 'Test', 'Five', 1, 1, CONVERT(Varbinary(max), '0xB132FA3FAA0F7681C0CA6A857693DD7F96AD1A4F0B9B4BAF84DF6BBDF96BA114B996D9DE', 1));
GO

INSERT INTO Courses (OwnerID, CourseCode, CourseName, CourseDescription, LessonLimitType)
VALUES
(1, 'ABC123', 'testCourse1', 'This is a test course for the purposes of testing. It contains three youtube videos of How It''s Made.', 0),
(3, '7GRAND', 'Several Siivagunner Videos', 'This is another test course. It contains twelve music videos by my favorite youtuber', 2),
(3, '123456', 'testCourse3', 'This is a course to show multiple owned courses by one user. It has a single video in it', 1)
GO

INSERT INTO Videos(CourseID, VideoTitle, VideoDescription, OrderInCourse, VideoType, YTVideoID, YTUseDescription)
VALUES
(1, 'How It''s Made - Swords', 'This video teaches you how swords are manufactured.', 1, 0, 'uPm3wHJdyGs', 0),
(1, 'How It''s Made - Parachutes', 'This video teaches you how parachutes are manufactured.', 2,0,'7TfxLzwtGHE', 0),
(1, 'How It''s Made - Seatbelts', NULL, 3, 0, 'Wqbqd8LC5UQ', 1),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 1, 0, 'XD08Dg4JxFM', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 2, 0, 'rP0E8E20HQ4', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 3, 0, '_yt5VjSn8l0', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 4, 0, 'XB7TsvBQUMQ', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 5, 0, 'm_z9mTpgdxY', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 6, 0, 'lwU2ficlHrM', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 7, 0, 'ku9ACY6lzww', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 8, 0, 'N1zglmNQ8Tk', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 9, 0, 'XD08Dg4JxFM', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 10, 0, 'w7E1rVBt9K4', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 11, 0, 'bIOOT_gIcmg', 0),
(2, 'Video Title', 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Et tortor consequat id porta nibh venenatis cras sed. Vel facilisis volutpat est velit egestas dui id ornare.', 12, 0, '4jTWgAqOJSA', 0),
(3, 'Nonexistent Video', 'This video contains a broken link', 1, 0, 'lajkjjjkjkj', 0);
GO

INSERT INTO UserxCourse (UserID, CourseID)
VALUES
(2, 1),
(3, 1),
(4, 1),
(1, 2),
(2, 2),
(4, 2),
(1, 3),
(2, 3)
GO

INSERT INTO UserxVideo (UserID, VideoID, VideoCompleted)
	VALUES
	(1, 4, 0),
	(1, 16, 1),
	(1, 12, 1),
	(2, 1, 1),
	(4, 4, 1),
	(4, 5, 1),
	(4, 6, 1),
	(4, 7, 1),
	(4, 8, 1),
	(4, 9, 1)
GO