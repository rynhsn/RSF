SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_PMR02400_GET_REPORT')

USE BIMASAKTI_11
DECLARE @CCOMPANY_ID		VARCHAR(20)='BSI'
		, @CPROPERTY_ID	VARCHAR(20)='ASHMD'
		, @CLANGUAGE_ID	VARCHAR(2)='EN'	

BEGIN TRAN
	EXEC[dbo].[RSP_PMR02400_GET_REPORT]
		 @CCOMPANY_ID			
		,@CPROPERTY_ID			
		,'I'--@CREPORT_OPTION		
		,'S'--@CREPORT_TYPE			
		,'20241031'--@CCUT_OFF_DATE		
		,''--@CFR_PERIOD			
		,''--@CTO_PERIOD			
		,'T'--@CCURRENCY_TYPE		
		,'A004'--@CFR_CUSTOMER			
		,'YI001'--@CTO_CUSTOMER			
		,@CLANGUAGE_ID			

ROLLBACK

