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
-- 1.0.0.1		20-MAC-2019			Tony			- a)  Residential Adm_SystemControl 
-- =======================================================================================================================================

DECLARE @TraceLog BIT=1
DECLARE @ShowResult BIT=0

-- Drop temporary table if exists
IF (OBJECT_ID ('tempdb..#Temp_Setting') IS NOT NULL) BEGIN
	DROP TABLE #Temp_Setting
END

-- Create temporary table
CREATE TABLE #Temp_Setting (
	Id INT IDENTITY(1,1) PRIMARY KEY
	,[Name] NVARCHAR(200)
	,[Value] NVARCHAR(2000)
	,[Description] NVARCHAR(2000)
	,[Active] BIT
	,[CreatedBy] INT
	,[CreatedOnUtc] DATETIME
	,[UpdatedBy] INT
	,[UpdatedOnUtc] DATETIME
	)

-- \/ Add setting here \/
INSERT INTO #Temp_Setting ([Name],[Value],[Description],[Active],[CreatedBy],[CreatedOnUtc],[UpdatedBy],[UpdatedOnUtc]) VALUES 
/* CLVN: 1.0.0.1(a) START */
 ('RES_DateFormat' ,'dd MMM yyyy' ,'Date Format' ,1 ,1 ,GETUTCDATE() ,1 ,GETUTCDATE())
,('RES_TimeFormat' ,'hh:mm tt' ,'Time Format' ,1 ,1 ,GETUTCDATE() ,1 ,GETUTCDATE())
,('RES_TimeFormatMobile' ,'hh:mm a' ,'Time Format for mobile time format' ,1 ,1 ,GETUTCDATE() ,1 ,GETUTCDATE()) 
,('RES_TimeFormatUTC' ,'HH:mm:ss' ,'Time Format UTC' ,1 ,1 ,GETUTCDATE() ,1 ,GETUTCDATE())
,('RES_DateFormatUTC' ,'yyyy-MM-dd' ,'Date Format UTC' ,1 ,1 ,GETUTCDATE() ,1 ,GETUTCDATE())
,('RES_GuestCustomerUsername' ,'searchengine' ,'Guest Customer Username' ,1 ,1 ,GETUTCDATE() ,1 ,GETUTCDATE())
,('RES_DomainAddress' ,'https://res.ggit2u.pw/' ,'Residential Domain Address' ,1 ,1 ,GETUTCDATE() ,1 ,GETUTCDATE())
,('RES_DefaultPageSize' ,'20' ,'Pagination Default Page Size' ,1 ,1 ,GETUTCDATE() ,1 ,GETUTCDATE())
/* CLVN: 1.0.0.1(a) END */

-- /\ Add setting here /\

IF (@TraceLog=1) PRINT CONCAT('/*INSERT INTO #Temp_Setting*/',CHAR(13)+CHAR(10))

DECLARE @Name NVARCHAR(MAX)=''
DECLARE @Value NVARCHAR(MAX)=''

DECLARE @Description NVARCHAR(MAX)=''
DECLARE @Active BIT=0
DECLARE @CreatedBy INT=0
DECLARE @CreatedOnUtc DATETIME = GETUTCDATE()
DECLARE @UpdatedBy INT=0
DECLARE @UpdatedOnUtc DATETIME = GETUTCDATE()


DECLARE @IsDelete BIT=0

DECLARE @Query NVARCHAR(MAX)=''
DECLARE @TotalRows INT=0
DECLARE @Loop INT=0

-- Cursor \/ --
DECLARE csTemp CURSOR FOR
	SELECT 
		[Name]
		,[Value]
		,[Description]
		,[Active]
		,[CreatedBy]
		,[CreatedOnUtc]
		,[UpdatedBy]
		,[UpdatedOnUtc]
	FROM [#Temp_Setting]
	ORDER BY [Name]
	
	SET @Loop=0
	OPEN csTemp 
	FETCH NEXT FROM csTemp INTO @Name,@Value,@Description,@Active,@CreatedBy,@CreatedOnUtc,@UpdatedBy,@UpdatedOnUtc
	SET @TotalRows=@@CURSOR_ROWS
	WHILE @@FETCH_STATUS=0 BEGIN--Looping records \/ --
		
		SET @Loop=@Loop+1
		IF (@TraceLog=1) PRINT CHAR(13)+CHAR(10)+CONCAT(@Loop,'/',@TotalRows,' Start')

		IF (@IsDelete=1) BEGIN
			SET @Query=CONCAT('DELETE [Adm_SystemControl] WHERE [Name]=''',@Name,'')		
		END
		ELSE BEGIN
			IF NOT EXISTS (
					SELECT 1 
					FROM Adm_SystemControl 
					WHERE [Name]=@Name
						
			) BEGIN
				SET @Query=CONCAT('INSERT INTO [Adm_SystemControl] ([Name],[Value],[Description],[Active],[CreatedBy],[CreatedOnUtc],[UpdatedBy],[UpdatedOnUtc]) 
									VALUES (N''',@Name,''',N''',@Value,''',N''',@Description,''',N''',@Active,''',N''',@CreatedBy,''',N''',@CreatedOnUtc,''',N''',@UpdatedBy,''',N''',@UpdatedOnUtc,''')')
			END ELSE BEGIN
				SET @Query=CONCAT('UPDATE [Adm_SystemControl] SET [Value]=N''',@Value,''', [Description]=N''',@Description,''' WHERE [Name]=''',@Name,'''')		
			END
		END
		
		EXEC SP_EXECUTESQL @Query
		IF (@TraceLog=1) PRINT @Query
		IF (@TraceLog=1) PRINT CONCAT(@Loop,'/',@TotalRows,' End')

		FETCH NEXT FROM csTemp INTO @Name,@Value,@Description,@Active,@CreatedBy,@CreatedOnUtc,@UpdatedBy,@UpdatedOnUtc
		
	END--Looping records /\ --
CLOSE csTemp
DEALLOCATE csTemp
-- Cursor /\ --

IF (@ShowResult=1) BEGIN
	SELECT * 
	FROM Adm_SystemControl
	WHERE [Name] IN (
			SELECT DISTINCT [Name]
			FROM #Temp_Setting
			GROUP BY [Name]
		)
	ORDER BY [Name]
	PRINT 'SELECT Setting'
END

-- Drop temporary table
IF (OBJECT_ID ('tempdb..#Temp_Setting') IS NOT NULL) BEGIN
	DROP TABLE #Temp_Setting
END
GO
