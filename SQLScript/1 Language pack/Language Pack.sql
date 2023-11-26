/*
    - For language pack
    - Do not remove any history records.
    - Always append new changes.
    - LanguageId - 1(English) | 2(Chinese (中文))
*/
------------------------------------------------------------------------------------------------------------------------------------------
-- UPDATE HISTORY
------------------------------------------------------------------------------------------------------------------------------------------
-- Rev. No.     Date(dd-MMM-yyyy)   By                  Description
------------------------------------------------------------------------------------------------------------------------------------------
-- 1.0.0.1      01-MAR-2019			Tony Liew           - a) MSP-45 Backend Function: Show user profiles
-- 1.0.0.2      05-MAR-2019			WKK					- a) RDT-63 [API] Account - login
-- 1.0.0.3		18-MAR-2019			wailiang			- a) RDT-127 [Web Admin] Incident listing
-- =======================================================================================================================================

DECLARE @DebugMode BIT=0

-- Drop temporary table if exists
IF (OBJECT_ID ('tempdb..#Temp_LanguagePack') IS NOT NULL) BEGIN
    DROP TABLE #Temp_LanguagePack
END

-- Create temporary table
CREATE TABLE #Temp_LanguagePack
    (
    Id INT IDENTITY(1,1) PRIMARY KEY
    ,LanguageId INT
    ,ResourceName NVARCHAR(MAX)
    ,ResourceValue NVARCHAR(MAX)
    )

-- \/ Add language pack here \/
INSERT INTO #Temp_LanguagePack (ResourceName,LanguageId,ResourceValue) VALUES
--------------------------------\/ User Front-End \/-------------------------------
/* CLVN: 1.0.0.1(a) START */
-- Page Title
 ('User.Incident.Title.IncidentReport',1,N'Incident Report')
-- Page Field Placeholder
--,('User.Incident.Field.Placeholder.IncidentTitle',1,N'e.g: pipe leakage')
--,('User.Incident.Field.Placeholder.IncidentDescription',1,N'Optional')
--,('User.Incident.Field.Placeholder.IncidentLocation',1,N'Please Provide Location')
--,('User.Incident.Field.Placeholder.IncidentType',1,N'Incident Type')

-- Page Field 
,('User.Incident.Field.IncidentTitle',1,N'Title')
,('User.Incident.Field.IncidentDescription',1,N'Description')
,('User.Incident.Field.IncidentLocation',1,N'Location')
,('User.Incident.Field.IncidentType',1,N'Incident Type')
,('User.Incident.Field.LocationType',1,N'Location Type')
,('User.Incident.Field.CreatedOnUtc',1,N'Date')

-- Page Field Validator
,('User.Incident.Field.Validator.IncidentTitle',1,N'Please Enter Title')
,('User.Incident.Field.Validator.IncidentDescription',1,N'Please Enter Description')
,('User.Incident.Field.Validator.IncidentLocation',1,N'Please Enter Location')
,('User.Incident.Field.Validator.IncidentLocationType',1,N'Please Enter Location Type')
,('User.Incident.Field.Validator.IncidentType',1,N'Please Enter Incident Type')
,('User.Incident.Field.Validator.CreatedOnUtc',1,N'Please Enter Date')
/* CLVN: 1.0.0.1(a) END */

/* CLVN: 1.0.0.2(a) START */
,('ActivityLog.PublicStore.MobileLogin',1,N'Mobile Login')
/* CLVN: 1.0.0.2(a) END */

/* CLVN: 1.0.0.3(a) START */
-- Searching Criteria
,('Incidents.IncidentsList.IncidentsItems.List.Title',1,N'Title')
,('Incidents.IncidentsList.IncidentsItems.List.Inc_DateFrom',1,N'Incident Date From')
,('Incidents.IncidentsList.IncidentsItems.List.Inc_DateTo',1,N'Incident Date To')
,('Incidents.IncidentsList.IncidentsItems.List.Status',1,N'Status')
,('Incidents.IncidentsList.IncidentsItems.List.Type',1,N'Type')
,('Incidents.IncidentsList.IncidentsItems.List.Category',1,N'Category')
,('Incidents.IncidentsItems.Status.All',1,N'All')
,('Incidents.IncidentsItems.Category.All',1,N'All')
,('Incidents.IncidentsItems.Type.All',1,N'All')

-- Result Sets
,('Incidents.IncidentsList.IncidentsItems.Fields.Inc_Date',1,N'Incident Date')
,('Incidents.IncidentsList.IncidentsItems.Fields.Desc',1,N'Description')

-- ActivityLog
,('ActivityLog.AddNewIncidents',1,N'Added a new incidents (ID = {0})')
,('ActivityLog.EditIncidents',1,N'Edited an incidents (ID = {0})')
,('ActivityLog.DeleteIncidents',1,N'Deleted incidents an incidents (ID = {0})')

--Messages
,('Incidents.IncidentsList',1,N'Incidents List')
,('Incidents.IncidentsList.IncidentsItems.Added',1,N'The incidents item has been added successfully')
,('Incidents.IncidentsList.IncidentsItems.Updated',1,N'The incidents item has been updated successfully')
,('Incidents.IncidentsList.IncidentsItems.Deleted',1,N'The incidents item has been deleted successfully')
,('Incidents.IncidentsList.IncidentsItems.EditIncidentsDetails',1,N'Edit incidents item details')
,('Incidents.IncidentsList.IncidentsItems.Info',1,N'Info')
,('Incidents.IncidentsList.IncidentsItems.AddNew',1,N'Add a new incidents item')
,('Incidents.IncidentsList.IncidentsItems.BackToList',1,N'back to incidents item list')
,('DateTimeFormat.NoEmpty',1,N'Date should not be empty')
,('DateTimeFormat.UnusualDateRange',1,N'Unusual searching date range')
,('Admin.Incidents',1,N'Incident')
,('Incidents.IncidentsList.IncidentsItems',1,N'Incident')
,('Incidents.Title.Required',1,N'Enter title')
,('Incidents.Description.Required',1,N'Enter description')
,('Incidents.Title.MaxLengthValidation',1,N'Max length of title is {0} chars')
,('Incidents.Description.MaxLengthValidation',1,N'Max length of description is {0} chars')
/* CLVN: 1.0.0.3(a) END */



--------------------------------/\ User Front-End /\-------------------------------


PRINT CONCAT('/*INSERT INTO #Temp_LanguagePack*/',CHAR(13)+CHAR(10))

DECLARE @Query NVARCHAR(MAX)=''
DECLARE @ValidationMessage NVARCHAR(MAX)=''
DECLARE @LanguageId INT=0
DECLARE @LanguageName NVARCHAR(MAX)=''
DECLARE @ResourceName NVARCHAR(MAX)=''
DECLARE @ResourceValue NVARCHAR(MAX)=''
DECLARE @TotalRows INT=0
DECLARE @Loop INT=0
-- Cursor \/ --
DECLARE csTemp CURSOR FOR
    SELECT
        LanguageId
        ,(CASE LanguageId WHEN 1 THEN 'English' WHEN 2 THEN N'Chinese' ELSE 'Error' END)
        ,LTRIM(RTRIM(ResourceName))
        ,REPLACE(LTRIM(RTRIM(ResourceValue)),'''','''''')
    FROM [#Temp_LanguagePack]
    --ORDER BY ResourceName,LanguageId,ResourceValue
    ORDER BY ResourceName,LanguageId,Id

    SET @Loop=0
    OPEN csTemp
    FETCH NEXT FROM csTemp INTO @LanguageId,@LanguageName,@ResourceName,@ResourceValue
    SET @TotalRows=@@CURSOR_ROWS
    WHILE @@FETCH_STATUS=0
    BEGIN
        -- Looping records \/ --
        SET @Loop=@Loop+1
        IF (@DebugMode=1) PRINT CHAR(13)+CHAR(10)+CONCAT(@Loop,'/',@TotalRows,' Start')

        IF (UPPER(LTRIM(RTRIM(@LanguageName)))='ERROR') BEGIN
            SET @ValidationMessage=@ValidationMessage+(CASE WHEN @ValidationMessage='' THEN '' ELSE CHAR(13)+CHAR(10) END)+CHAR(9)+CONCAT('Wrong Language Id ',@LanguageId,' for Resource Name [',@ResourceName,'].')
        END
        ELSE IF (LTRIM(RTRIM(@ResourceValue))='') BEGIN
            SET @ValidationMessage=@ValidationMessage+(CASE WHEN @ValidationMessage='' THEN '' ELSE CHAR(13)+CHAR(10) END)+CHAR(9)+CONCAT('Resource Value for Resource Name [',@ResourceName,'] for Language ',@LanguageId,'(',@LanguageName,') cannot be empty.')
        END ELSE BEGIN
            IF NOT EXISTS (SELECT * FROM LocaleStringResource WHERE ResourceName=@ResourceName AND LanguageId=@LanguageId) BEGIN
                SET @Query=CONCAT('INSERT INTO LocaleStringResource (LanguageId,ResourceName,ResourceValue) VALUES (',@LanguageId,',N''',@ResourceName,''',N''',@ResourceValue,''')')
                IF (@DebugMode=1) PRINT @Query
                EXEC SP_EXECUTESQL @Query
                IF (@DebugMode=1) PRINT '/*Insert data*/'
            END ELSE BEGIN
                SET @Query=CONCAT('UPDATE LocaleStringResource SET ResourceValue=N''',@ResourceValue,''' WHERE ResourceName=N''',@ResourceName,''' AND LanguageId=',@LanguageId)
                IF (@DebugMode=1) PRINT @Query
                EXEC SP_EXECUTESQL @Query
                IF (@DebugMode=1) PRINT '/*Update data*/'
            END
        END

        IF (@DebugMode=1) PRINT CONCAT(@Loop,'/',@TotalRows,' End')

        FETCH NEXT FROM csTemp INTO @LanguageId,@LanguageName,@ResourceName,@ResourceValue
        -- Looping records /\ --
    END
CLOSE csTemp
DEALLOCATE csTemp
-- Cursor /\ --

IF (@ValidationMessage<>'') BEGIN
    PRINT CHAR(13)+CHAR(10)+'Error:'
    DECLARE @TextLength INT=LEN(@ValidationMessage)
    IF @TextLength<=3500 BEGIN
        PRINT @ValidationMessage
    END
    ELSE BEGIN
        DECLARE @TextLength_Temp INT=LEN(@ValidationMessage)
        DECLARE @StartPoint INT=0
        WHILE @TextLength_Temp>0
        BEGIN
            PRINT SUBSTRING(@ValidationMessage,@StartPoint,3500)
            SET @TextLength_Temp=@TextLength_Temp-3500
            SET @StartPoint=@StartPoint+3500
        END
    END
END

IF (@DebugMode=1) BEGIN
    SELECT *
    FROM LocaleStringResource
    WHERE ResourceName IN (
            SELECT ResourceName
            FROM #Temp_LanguagePack
            GROUP BY ResourceName
        )
    ORDER BY ResourceName,LanguageId,ResourceValue
END

-- Drop temoporary table
IF (OBJECT_ID ('tempdb..#Temp_LanguagePack') IS NOT NULL) BEGIN
    DROP TABLE #Temp_LanguagePack
END
