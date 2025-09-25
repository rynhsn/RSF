

BEGIN TRANSACTION
IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null) BEGIN
	select SP_Name=cast('' as varchar(50)), Err_Code=cast('' as varchar(20)), Err_Detail=cast('' as nvarchar(max)) into #__SP_ERR_Table where 0=1
end else begin
	truncate table #__SP_ERR_TABLE
end

begin try
EXEC	[RSP_PM_MAINTAIN_SLA_CALL_TYPE]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CCALL_TYPE_ID = 'test',
		@CCALL_TYPE_NAME = 'test1',
		@CCATEGORY_ID = 'GS',
		@IDAYS = 0,
		@IHOURS = 0,
		@IMINUTES = 0,
		@LPRIORITY = false,
		@CACTION = 'ADD',
		@CUSER_ID = 'hmc'
end try
begin catch
	select * from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

ROLLBACK


USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_SLA_CALL_TYPE_LIST]
		@CCOMPANY_ID = NULL,
		@CPROPERTY_ID = NULL,
		@CCALL_TYPE_ID = null
SELECT	'Return Value' = @return_value
GO

USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_SLA_CALL_TYPE]
		@CCOMPANY_ID = NULL,
		@CPROPERTY_ID = NULL,
		@CCALL_TYPE_ID = NULL,
		@CCALL_TYPE_NAME = NULL,
		@CCATEGORY_ID = NULL,
		@IDAYS = NULL,
		@IHOURS = NULL,
		@IMINUTES = NULL,
		@LPRIORITY = NULL,
		@CACTION = NULL,
		@CUSER_ID = NULL

SELECT	'Return Value' = @return_value

GO

SELECT * FROM PMM_SLA_CALL_TYPE


RSP_TX_GET_BODY_EMAIL

RSP_PM_UPLOAD_SLA_PROCESS 



USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_SLA_CATEGORY]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CCATEGORY_ID = NULL,
		@CCATEGORY_NAME = NULL,
		@CPARENT_CATEGORY_ID = NULL,
		@ILEVEL = NULL,
		@CNOTES = NULL,
		@CACTION = NULL,
		@CUSER_ID = NULL

SELECT	'Return Value' = @return_value

GO

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_SLA_CATEGORY]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CCATEGORY_ID = ''
SELECT	'Return Value' = @return_value
GO

['RCD', '', '30', 'HMC']

EXEC RSP_PM_GET_TENANT_CATEGORY_LIST 'RCD', 'ASHMD', 'ERC'

begin transaction

EXEC	[RSP_PM_MAINTAIN_SLA_CALL_TYPE]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CCALL_TYPE_ID = 'test',
		@CCALL_TYPE_NAME = 'test',
		@CCATEGORY_ID = 'PL',
		@IDAYS = 0,
		@IHOURS = 0,
		@IMINUTES = 0,
		@LPRIORITY = false,
		@CACTION = 'EDIT',
		@CUSER_ID = 'hmc'

rollback

exec [RSP_PM_GET_CALL_TYPE_PRICELIST_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CCALL_TYPE_ID = 'test',
		@CLANG_ID = 'en'
		

exec [RSP_PM_GET_CALL_TYPE_ASSIGNED_PRICELIST_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CCALL_TYPE_ID = 'EL-GS-001',
		@CLANG_ID = 'en'

begin transaction
exec RSP_PM_UNASSIGN_PRICELIST 
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CCALL_TYPE_ID = 'EL-GS-001',
		@CPRICELIST_ID = 'PRC-003,PRC-004',
		@CUSER_ID = 'hmc'
		commit
begin transaction
exec RSP_PM_ASSIGN_PRICELIST 
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CCALL_TYPE_ID = 'EL-GS-001',
		@CPRICELIST_ID = 'PRC-003,PRC-004',
		@CUSER_ID = 'hmc'



EXEC RSP_PM_GET_HANDOVER_CHECKLIST_MAPPING 'RCD', 'ASHMD', 'CHO01', false
EXEC RSP_PM_GET_HANDOVER_CHECKLIST_MAPPING 'RCD', 'ASHMD', 'CHO02', false
