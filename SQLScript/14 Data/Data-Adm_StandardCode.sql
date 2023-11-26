/* 
	- For insert/update/delete records
	- File Name Format: "Data-<table_name>", eg."Data-XXX"
	- Do not remove any history records.
	- Always append new changes.
*/
------------------------------------------------------------------------------------------------------------------------------------------
-- UPDATE HISTORY 
------------------------------------------------------------------------------------------------------------------------------------------
-- Rev. No.		Date(dd-MMM-yyyy)	By					Description
------------------------------------------------------------------------------------------------------------------------------------------
-- 1.0.0.1		03-APR-2019			Name				- a) Create initiate insert script for Adm_StandardCode
-- =======================================================================================================================================

DECLARE @TraceLog BIT=1
DECLARE @ShowResult BIT=0

-- Drop temporary table if exists
IF (OBJECT_ID ('tempdb..#Temp_StandardCode') IS NOT NULL) BEGIN
	DROP TABLE #Temp_StandardCode
END

-- Create temporary table
CREATE TABLE #Temp_StandardCode (
	 [Name] 		VARCHAR(50)
	,[Key] 			VARCHAR(15)
	,[Code]			VARCHAR(15)
	,[Description] 	VARCHAR(100)
	,[CreatedBy] 	INT
	,[CreatedOnUtc]	DATETIME
	,[UpdatedBy] 	INT
	,[UpdatedOnUtc] DATETIME
	)
 
 

INSERT INTO #Temp_StandardCode ([Name],[Key],[Code],[Description],[CreatedBy],[CreatedOnUtc],[UpdatedBy],[UpdatedOnUtc]) VALUES 
-- \/ Add setting here \/
('Account type', 'ACCT_TYPE', 'P', 'Principal Account/Master/Owner', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Account type', 'ACCT_TYPE', 'T', 'Master Tenant', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Account type', 'ACCT_TYPE', 'S', 'Supplementary Account', 1, GETUTCDATE(), 1, GETUTCDATE())

,('Occupancy type', 'OCPY_TYPE', 'F', 'Family Member', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Occupancy type', 'OCPY_TYPE', 'B', 'Sub-tenant', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Occupancy type', 'OCPY_TYPE', 'V', 'Vacant/Airbnb', 1, GETUTCDATE(), 1, GETUTCDATE())

,('User account status type', 'UACC_STS', 'PAPV', 'Pending Approve', 1, GETUTCDATE(), 1, GETUTCDATE())
,('User account status type', 'UACC_STS', 'ACTV', 'Active', 1, GETUTCDATE(), 1, GETUTCDATE())
,('User account status type', 'UACC_STS', 'TERM', 'Terminated', 1, GETUTCDATE(), 1, GETUTCDATE())
,('User account status type', 'UACC_STS', 'SPND', 'Suspended', 1, GETUTCDATE(), 1, GETUTCDATE())
,('User account status type', 'UACC_STS', 'REJC', 'Rejected', 1, GETUTCDATE(), 1, GETUTCDATE())
,('User account status type', 'UACC_STS', 'PACT', 'Pending Activation', 1, GETUTCDATE(), 1, GETUTCDATE())

,('Visitor timestamp type', 'VT_TYPE', 'CI', 'Timestamp Clock In', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Visitor timestamp type', 'VT_TYPE', 'CO', 'Timestamp Clock Out', 1, GETUTCDATE(), 1, GETUTCDATE())

,('Visitor vehicle type', 'VV_TYPE', 'B', 'Bicycle', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Visitor vehicle type', 'VV_TYPE', 'C', 'Car', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Visitor vehicle type', 'VV_TYPE', 'M', 'Motorcycle', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Visitor vehicle type', 'VV_TYPE', 'L', 'Lorry', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Visitor vehicle type', 'VV_TYPE', 'T', 'Truck', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Visitor vehicle type', 'VV_TYPE', 'V', 'Van', 1, GETUTCDATE(), 1, GETUTCDATE())

,('Facility status type', 'FAC_ST', 'O', 'Open', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Facility status type', 'FAC_ST', 'C', 'Close', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Facility status type', 'FAC_ST', 'M', 'Maintenance', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Facility status type', 'FAC_ST', 'T', 'Terminated', 1, GETUTCDATE(), 1, GETUTCDATE())

,('Module visible type', 'MODSUB_TYPE', 'S', 'Show', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Module visible type', 'MODSUB_TYPE', 'D', 'Dim', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Module visible type', 'MODSUB_TYPE', 'H', 'Hide', 1, GETUTCDATE(), 1, GETUTCDATE())

,('Visitor transaction status type', 'VTRX_STS', 'A', 'Approved', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Visitor transaction status type', 'VTRX_STS', 'P', 'Pending', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Visitor transaction status type', 'VTRX_STS', 'R', 'Rejected', 1, GETUTCDATE(), 1, GETUTCDATE())
,('Visitor transaction status type', 'VTRX_STS', 'X', 'Canceled', 1, GETUTCDATE(), 1, GETUTCDATE())
-- /\ Add setting here /\

IF (@TraceLog=1) PRINT CONCAT('/*INSERT INTO #Temp_StandardCode*/',CHAR(13)+CHAR(10))

DECLARE @Name			VARCHAR(50) = ''
DECLARE @Key			VARCHAR(15) = ''
DECLARE @Code			VARCHAR(15) = ''
DECLARE @Description	VARCHAR(100)= ''
DECLARE @CreatedBy		INT			= 0
DECLARE @CreatedOnUtc	DATETIME
DECLARE @UpdatedBy		INT			= 0
DECLARE @UpdatedOnUtc	DATETIME
DECLARE @IsDelete		BIT			= 0

DECLARE @Query NVARCHAR(MAX)=''
DECLARE @TotalRows INT=0
DECLARE @Loop INT=0

-- Cursor \/ --
DECLARE csTemp CURSOR FOR
	SELECT 
		 [Name]
		,[Key]
		,[Code]
		,[Description]
		,[CreatedBy]
		,[CreatedOnUtc]
		,[UpdatedBy]
		,[UpdatedOnUtc]
	FROM [#Temp_StandardCode]
	
	SET @Loop=0
	OPEN csTemp 
	FETCH NEXT FROM csTemp INTO @Name,@Key,@Code,@Description,@CreatedBy,@CreatedOnUtc,@UpdatedBy,@UpdatedOnUtc		
	
	SET @TotalRows=@@CURSOR_ROWS
	WHILE @@FETCH_STATUS=0 BEGIN--Looping records \/ --
		
		SET @Loop=@Loop+1
		IF (@TraceLog=1) PRINT CHAR(13)+CHAR(10)+CONCAT(@Loop,'/',@TotalRows,' Start')

		IF (@IsDelete=1) BEGIN
			SET @Query=CONCAT('DELETE [Adm_StandardCode] WHERE [Key]=''',@Key,''' AND [Code]=''',@Code,'''')		
		END
		ELSE BEGIN
			IF NOT EXISTS (
					SELECT 1 
					FROM Adm_StandardCode 
					WHERE [Key]=@Key
					AND [Code]=@Code
			) BEGIN
				SET @Query=CONCAT('INSERT INTO [Adm_StandardCode]([Name],[Key],[Code],[Description],[CreatedBy],[CreatedOnUtc],[UpdatedBy],[UpdatedOnUtc]) VALUES (N''',@Name,''',N''',@Key,''',N''',@Code,''',N''',@Description,''',N''',@CreatedBy,''',N''', @CreatedOnUtc ,''',N''',@UpdatedBy,''',N''',@UpdatedOnUtc,''')')
			END
		END
		
		EXEC SP_EXECUTESQL @Query
		IF (@TraceLog=1) PRINT @Query
		IF (@TraceLog=1) PRINT CONCAT(@Loop,'/',@TotalRows,' End')

		FETCH NEXT FROM csTemp INTO @Name,@Key,@Code,@Description,@CreatedBy,@CreatedOnUtc,@UpdatedBy,@UpdatedOnUtc	
		
	END--Looping records /\ --
CLOSE csTemp
DEALLOCATE csTemp
-- Cursor /\ --

IF (@ShowResult=1) BEGIN
	SELECT * 
	FROM Adm_StandardCode
	WHERE [Key] IN (
			SELECT DISTINCT [Key]
			FROM #Temp_StandardCode
			GROUP BY [Key]
		)
	ORDER BY [Key]
	PRINT 'SELECT StandardCode'
END

-- Drop temporary table
IF (OBJECT_ID ('tempdb..#Temp_StandardCode') IS NOT NULL) BEGIN
	DROP TABLE #Temp_StandardCode
END
GO

