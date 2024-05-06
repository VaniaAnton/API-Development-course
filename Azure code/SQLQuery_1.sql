USE DotNetCourseDatabase
GO 

SELECT * FROM TutorialAppSchema.Auth

SELECT  [PostId],
[UserId],
[PostTitle],
[PostContent],
[PostCreated],
[PostUpdated],FROM TutorialAppSchema.Posts

CREATE TABLE TutorialAppSchema.Posts (
    PostId INT IDENTITY(1,1)
    , UserId INT
    , PostTitle NVARCHAR(255)
    , PostContent NVARCHAR(MAX)
    , PostCreated DATETIME
    , PostUpdated DATETIME
)

CREATE CLUSTERED INDEX cix_Posts_UserId_PostId ON TutorialAppSchema.Posts(UserId, PostId)

SELECT [Email],
[PasswordHash],
[PasswordSalt] FROM TutorialAppSchema.Auth WHERE Email = ''
INSERT INTO TutorialAppSchema.Auth ([Email],
[PasswordHash],
[PasswordSalt]) VALUES ('email', 'pasword', 'dwwd')
-- INSERT INTO TutorialAppSchema.Users(
--     [FirstName],
--     [LastName],
--     [Email],
--     [Gender],
--     [Active]
-- ) VALUES(
--     [FirstName],
--     [LastName],
--     [Email],
--     [Gender],
--     [Active]
-- )

-- UPDATE TutorialAppSchema.Users
--     SET [FirstName] = 'Albertina',
--     [LastName] = 'ivan',
--     [Email] = 'aofinan0@blogspot.com',
--     [Gender] = 'Female',
--     [Active] = 1
--     WHERE UserId = 1



-- SELECT [UserId], 
-- [JobTitle],
-- [Department] FROM TutorialAppSchema.UserJobInfo

-- SELECT[UserId], 
-- [Salary]  FROM TutorialAppSchema.UserSalary
