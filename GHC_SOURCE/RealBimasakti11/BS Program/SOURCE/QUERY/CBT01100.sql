
EXEC RSP_GS_GET_COMPANY_INFO 'rcd'
EXEC RSP_GL_GET_SYSTEM_PARAM 'rcd','en'
EXEC RSP_CB_GET_SYSTEM_PARAM 'BSI','en'
EXEC RSP_GS_GET_TRANS_CODE_INFO 'rcd','990030'
EXEC RSP_GS_GET_PERIOD_YEAR_RANGE 'rcd','',''
EXEC RSP_GS_GET_GSB_CODE_LIST 'BIMASAKTI','rcd','_CB_JOURNAL_STATUS','en'
EXEC RSP_CB_SEARCH_TRANS_HD_LIST 'BSI','ghc','990030','02','202406','','','en'
EXEC RSP_CB_GET_SYSTEM_PARAM 'BSI','en'

EXEC RSP_CB_GET_TRANS_HD 'bsi','ghc','FF8C20CD-5856-466F-95F2-6077976BA4C5','en','','',''
delete from sat_locking where CUSER_ID= 'ghc'

EXEC RSP_CB_UPDATE_TRANS_HD_STATUS 'BSI','VFM','VFM','FF8C20CD-5856-466F-95F2-6077976BA4C5','00',0,0



IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null) BEGIN
	select SP_Name=cast('' as varchar(50)), Err_Code=cast('' as varchar(20)), Err_Detail=cast('' as nvarchar(max)) into #__SP_ERR_Table where 0=1
end else begin
	truncate table #__SP_ERR_TABLE
end

begin try
	-- Jalankan script SP disini

	exec RSP_CB_SAVE_CA_WT_JOURNAL 
		'ram',
		'NEW',
		'',
		'rcd',
		'ACC',
		'990030',
		'WT',
		'',
		'20240830',
		'test input sql',
		'20240423',
		'MANDIRI',
		'Account01',
		'Test SQL Server',
		'IDR',
		'1000',
		'1',
		'1',
		'1',
		'1'

exec RSP_GS_GET_CB_ACCOUNT_LOOKUP 'RCD','ACC','B','I','MANDIRI'

end try
begin catch
	select * from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

