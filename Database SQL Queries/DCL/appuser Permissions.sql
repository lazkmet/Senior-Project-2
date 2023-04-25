EXEC sp_addrolemember @rolename = 'db_DataReader', @membername = 'appuser'
EXEC sp_addrolemember @rolename = 'db_DataWriter', @membername = 'appuser'
GRANT EXECUTE ON SCHEMA::dbo TO appuser
GRANT VIEW DEFINITION ON SCHEMA::dbo TO appuser