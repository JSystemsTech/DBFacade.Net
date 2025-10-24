CREATE PROCEDURE [dbo].[UpdateEmployee]
	@Guid UNIQUEIDENTIFIER = NULL,
	@FirstName NVARCHAR(50) = NULL,
	@MiddleName NVARCHAR(50) = NULL,
	@LastName NVARCHAR(50) = NULL,
	@AddressLine1 NVARCHAR(50) = NULL,
	@AddressLine2 NVARCHAR(20) = NULL,
	@AddressCity NVARCHAR(20) = NULL,
	@AddressState NVARCHAR(20) = NULL,	
	@AddressZIP NVARCHAR(5) = NULL,	
	@HomePhone NVARCHAR(10) = NULL,
	@MobilePhone NVARCHAR(10) = NULL,
	@Email NVARCHAR(256) = NULL,
	@ResponseMessage NVARCHAR(MAX) OUTPUT
AS

DECLARE @ReturnCode INT = 0

BEGIN TRY
	DECLARE @EmployeeId INT

	SELECT @EmployeeId = [Id],
	@TypeName = [Name]
	FROM [dbo].[Employee] 
	WHERE [Guid] = @Guid

	UPDATE [E]
	SET
	[FirstName] = ISNULL(@FirstName,[FirstName]),
	[MiddleName] = ISNULL(@MiddleName,[MiddleName]),
	[LastName] = ISNULL(@LastName,[LastName]),
	[UpdateDate] = GETDATE()
	FROM [dbo].[Employee] [E]	 
	WHERE [Id] = @EmployeeId
	
	IF @@ROWCOUNT = 0
	BEGIN
		INSERT INTO [dbo].[Employee]([FirstName],[MiddleName],[LastName])
		VALUES(@FirstName,@MiddleName,@LastName)

		SET @EmployeeId = SCOPE_IDENTITY()
	END

	UPDATE [CI]
	SET
	[AddressLine1] = ISNULL(@AddressLine1,[AddressLine1]),
	[AddressLine2] = ISNULL(@AddressLine2,[AddressLine2]),
	[AddressCity] = ISNULL(@AddressCity,[AddressCity]),
	[AddressState] = ISNULL(@AddressState,[AddressState]),	
	[AddressZIP] = ISNULL(@AddressZIP,[AddressZIP]),	
	[HomePhone] = ISNULL(@HomePhone,[HomePhone]),
	[MobilePhone] = ISNULL(@MobilePhone,[MobilePhone]),
	[Email] = ISNULL(@Email,[Email]),
	[UpdateDate] = GETDATE() 
	FROM [dbo].[ContactInfo] [CI]	 
	WHERE [EmployeeId] = @EmployeeId
	
	IF @@ROWCOUNT = 0
	BEGIN
		INSERT INTO [dbo].[ContactInfo]([EmployeeId],[AddressLine1],[AddressLine2],[AddressCity],[AddressState],[AddressZIP],[HomePhone],[MobilePhone],[Email])
		VALUES(@AddressLine1,@AddressLine2,@AddressCity,@AddressState,@AddressZIP,@HomePhone,@MobilePhone,@Email)
	END

	SET @ReturnCode = 1
	SET @ResponseMessage = 'Successfully updated employee'
END TRY

BEGIN CATCH
	SET @ReturnCode = 0
	SET @ResponseMessage = ERROR_MESSAGE()
END CATCH

RETURN @ReturnCode