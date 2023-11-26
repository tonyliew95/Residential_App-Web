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
-- 1.0.0.1		05-MAR-2019			WKK					- a)RDT-63 [API] Account - login
-- =======================================================================================================================================

/* CLVN: 1.0.0.1(a) \/ */
IF NOT EXISTS (
		-- \/ Change query at below \/
		SELECT * 
		FROM [ActivityLogType]
		where [SystemKeyword] ='PublicStore.MobileLogin'
		-- /\ Change query at below /\
	) BEGIN
	PRINT 'Data NOT Exists/Found.'
	-- \/ Create/Delete query start at below \/
	   Insert into [ActivityLogType](
      [SystemKeyword]
      ,[Name]
      ,[Enabled]
	  ) values ('PublicStore.MobileLogin','Public store. Mobile Login',1)
	-- /\ Create/Delete query end /\
END
ELSE BEGIN
	PRINT 'Data Exists/Found.'
	-- \/ Modify query start at below \/
	
	-- /\ Modify query end /\
END
PRINT ''
/* CLVN: 1.0.0.1(a) /\ */


GO

/*
SELECT * FROM Temp_Table 
*/