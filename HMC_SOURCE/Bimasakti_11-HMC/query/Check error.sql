BEGIN TRANSACTION

IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null) BEGIN
	select SP_Name=cast('' as varchar(50)), Err_Code=cast('' as varchar(20)), Err_Detail=cast('' as nvarchar(max)) into #__SP_ERR_Table where 0=1
end else begin
	truncate table #__SP_ERR_TABLE
end

begin try
	
DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT_DEPT]
		@CCOMPANY_ID = N'rcd',
		@CPROPERTY_ID = N'ashmd',
		@CJRNGRP_TYPE = N'12',
		@CJRNGRP_CODE = N'DEPO',
		@CGOA_CODE = N'DEPO',
		@CDEPT_CODE = N'FIN',
		@CGLACCOUNT_NO = N'0A153',
		@CACTION = N'ADD',
		@CUSER_ID = N'hmc'

SELECT	'Return Value' = @return_value




end try
begin catch
	select * from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

ROLLBACK