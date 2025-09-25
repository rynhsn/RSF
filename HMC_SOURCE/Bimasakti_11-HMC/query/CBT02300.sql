---BANK IN CHEQUE
select * from SAT_LOCKING
--Password: CD-r5fdbsvr1#
rollback
delete from SAT_LOCKING where CUSER_ID='vfm'

--LIST Bank in cheque
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_CB_GET_BANK_IN_CHEQUE_LIST]
		@CCOMPANY_ID = 'bsi',
		@CUSER_ID = 'RAM',
		@CTRANS_TYPE = 'BANK_IN', --BANK_IN, HOLD, BOUNCE, CLEAR, UNDO_CLEAR
		@CDEPT_CODE = '02',
		@CCB_CODE = 'BBYB',
		@CCB_ACCOUNT_NO = 'BBYB',
		@CDATE = '20241122',
		@CLANGUAGE_ID = 'en'
SELECT	'Return Value' = @return_value
GO

select CBANK_IN_DATE from CBT_CHEQUE_HD

--LIST DETAIL
USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_CB_GET_CHEQUE_HD]
		@CCOMPANY_ID = 'rcd',
		@CUSER_ID = 'hmc',
		@CREC_ID = '6734BDA7-992E-4A64-9244-6A6B013413AA',
		@CLANGUAGE_ID = 'en'

SELECT	'Return Value' = @return_value

GO


---LOOKUP
RSP_GS_GET_CASH_BANK_LOOKUP 'rcd', 'HRD', 'B', 'I'
RSP_GS_GET_CASH_BANK_LOOKUP '', '', '', ''

RSP_GS_GET_CB_ACCOUNT_LOOKUP
		@CCOMPANY_ID = N'rcd',
		@CDEPT_CODE = N'hrd',
		@CCB_TYPE = N'B',
		@CBANK_TYPE = N'I',
		@CCB_CODE = N'jtm'

GO


---PROCESSS
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_CB_PROCESS_BANK_IN_CHEQUE]
		@CCOMPANY_ID = 'rcd',
		@CUSER_ID = 'hmc',
		@CACTION = 'UNDO_CLEAR',
		@CPROCESS_DATE = '20240830',
		@CREASON = 'undo clear',
		@CREC_ID_LIST = 'E6592482-EB61-4790-967F-B01D19D5D1CA'
SELECT	'Return Value' = @return_value
GO

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_CB_GET_BANK_IN_CHEQUE_LIST]
		@CCOMPANY_ID = 'rcd',
		@CUSER_ID = 'hmc',
		@CTRANS_TYPE = 'BANK_IN', --BANK_IN, HOLD, BOUNCE, CLEAR, UNDO_CLEAR
		@CDEPT_CODE = 'ACC',
		@CCB_CODE = 'BANK2',
		@CCB_ACCOUNT_NO = '20241505',
		@CDATE = '20240830',
		@CLANGUAGE_ID = 'en'
SELECT	'Return Value' = @return_value
GO

--LIST Bank in cheque
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_CB_GET_BANK_IN_CHEQUE_LIST]
		@CCOMPANY_ID = 'bsi',
		@CUSER_ID = 'RAM',
		@CTRANS_TYPE = 'BANK_IN', --BANK_IN, HOLD, BOUNCE, CLEAR, UNDO_CLEAR
		@CDEPT_CODE = '02',
		@CCB_CODE = 'BBYB',
		@CCB_ACCOUNT_NO = 'BBYB',
		@CDATE = '20241122',
		@CLANGUAGE_ID = 'en'
SELECT	'Return Value' = @return_value
GO

select * from GST_UPLOAD_ERROR_STATUS   ---"b6403da1c4d94612b911973dc373ed40"
where CUSER_ID = 'hmc'

select * from GST_UPLOAD_PROCESS_STATUS
where CUSER_ID ='hmc'