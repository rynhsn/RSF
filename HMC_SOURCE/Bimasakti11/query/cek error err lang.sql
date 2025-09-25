

BEGIN TRANSACTION
IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null) BEGIN
	select SP_Name=cast('' as varchar(50)), Err_Code=cast('' as varchar(20)), Err_Detail=cast('' as nvarchar(max)) into #__SP_ERR_Table where 0=1
end else begin
	truncate table #__SP_ERR_TABLE
end

begin try
exec RSP_PM_GET_AGREEMENT_UTILITIES_DT
@CCOMPANY_ID = 'bsi',
@CPROPERTY_ID ='ashmd',
@CDEPT_CODE = '00',
@CTRANS_CODE = '802043',
@CREF_NO = 'LOO-E-00-2025020001',
@CUNIT_ID = 'BZ015',
@CFLOOR_ID = 'GF',
@CBUILDING_ID ='TW-1',
@CCHARGES_TYPE ='01',
@CCHARGES_ID ='EC01',
@CSEQ_NO ='001',
@CUSER_ID ='hmc'
end try
begin catch
	select * from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

ROLLBACK


rollback
