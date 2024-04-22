USE DotNetCourseDatabase
GO 


SELECT [UserId], 
    [FirstName],
    [LastName],
    [Email],
    [Gender],
    [Active] 
FROM TutorialAppSchema.Users
ORDER BY UserId DESC

INSERT INTO TutorialAppSchema.Users(
    [FirstName],
    [LastName],
    [Email],
    [Gender],
    [Active]
) VALUES(
    [FirstName],
    [LastName],
    [Email],
    [Gender],
    [Active]
)

UPDATE TutorialAppSchema.Users
    SET [FirstName] = 'Albertina',
    [LastName] = 'ivan',
    [Email] = 'aofinan0@blogspot.com',
    [Gender] = 'Female',
    [Active] = 1
    WHERE UserId = 1



-- SELECT [UserId], 
-- [JobTitle],
-- [Department] FROM TutorialAppSchema.UserJobInfo

-- SELECT[UserId], 
-- [Salary]  FROM TutorialAppSchema.UserSalary
