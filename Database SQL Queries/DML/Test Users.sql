INSERT INTO Users (EmailAddress, FirstName, LastName, UserType, EmailVerified, EncryptedPassword)
VALUES
('fakeemail', 'Test', 'One', 2, 1, CONVERT(varbinary(max), 'password', 0)),
('lazfake', 'Test', 'Two', 1, 1, CONVERT(varbinary(max), 'password', 0)),
('alsofake', 'Test', 'Three', 1, 1, CONVERT(varbinary(max), 'password', 0)),
('fhgkhjnbm', 'Test', 'Four', 1, 1, CONVERT(varbinary(max), 'password', 0)),
('noemail', 'Test', 'Five', 1, 1, CONVERT(varbinary(max), 'password', 0));