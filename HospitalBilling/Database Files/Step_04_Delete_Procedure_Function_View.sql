SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
*******************************************************************************
-------------------------------------------------------------------------------
===============================================================================
Name: Delete all Procedure
Description: Delete all Procedure
Author: Sumon
-------------------------------------------------------------------------------
Modification Log: 
********************************************************************************
Description                  		Date         		Changed By
===========							==========			==========
Created procedure            		05/26/2023 
================================================================================
--------------------------------------------------------------------------------
********************************************************************************
*/
DECLARE @outParamValue INT, @intCount INT=1, @strSelected VARCHAR(MAX), @intTotal INT
DECLARE @tempProcedureTable TABLE(Id INT IDENTITY(1,1), ProcedureName VARCHAR(MAX))			
INSERT INTO @tempProcedureTable 
			SELECT N' DROP PROCEDURE '+QUOTENAME(SCHEMA_NAME(SCHEMA_ID))+ N'.'+ QUOTENAME(NAME) 
			FROM SYS.OBJECTS WHERE TYPE='P' OR TYPE_DESC LIKE '%SQL_STORED_PROCEDURE%'

SELECT @intTotal=count(1) FROM @tempProcedureTable
WHILE @intCount<=@intTotal
BEGIN
	SELECT @strSelected= ProcedureName FROM @tempProcedureTable where Id=@intCount
	EXEC (@strSelected)
	SET @intCount=@intCount+1
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
*******************************************************************************
-------------------------------------------------------------------------------
===============================================================================
Name: Delete all view
Description: Delete all user define view table
Author: Sumon
-------------------------------------------------------------------------------
Modification Log: 
********************************************************************************
Description                  		Date         		Changed By
===========							==========			==========
Created procedure            		05/26/2023 
================================================================================
--------------------------------------------------------------------------------
********************************************************************************
*/
DECLARE @str_View_Drop_Query NVARCHAR(MAX)=N'';

SELECT @str_View_Drop_Query = @str_View_Drop_Query + N' DROP VIEW ' 
							+ QUOTENAME(SCHEMA_NAME(SCHEMA_ID)) 
							+ N'.' + QUOTENAME(NAME)
	FROM SYS.VIEWS WHERE TYPE='V' OR TYPE_DESC LIKE '%VIEW%';

IF @str_View_Drop_Query<>N''
BEGIN
	EXEC SP_EXECUTESQL @str_View_Drop_Query
END
GO





SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
*******************************************************************************
-------------------------------------------------------------------------------
===============================================================================
Name: Delete all funtion
Description: Delete all user define function
Author: Sumon
-------------------------------------------------------------------------------
Modification Log: 
********************************************************************************
Description                  		Date         		Changed By
===========							==========			==========
Created procedure            		05/26/2023 
================================================================================
--------------------------------------------------------------------------------
********************************************************************************
*/
DECLARE @str_FN_Drop_Query NVARCHAR(MAX)=N'';

SELECT @str_FN_Drop_Query = @str_FN_Drop_Query + N' DROP FUNCTION ' 
							+ QUOTENAME(SCHEMA_NAME(SCHEMA_ID)) 
							+ N'.' + QUOTENAME(NAME)
	FROM SYS.OBJECTS WHERE TYPE IN('FN','TF','IF') OR TYPE_DESC LIKE '%FUNCTION%';

IF @str_FN_Drop_Query<>N''
BEGIN
	EXEC SP_EXECUTESQL @str_FN_Drop_Query
END
GO
