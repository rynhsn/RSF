
EXEC RSP_PQ_GET_SERVICE 
''--@CSERVICE_ID




IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null) BEGIN
	select SP_Name=cast('' as varchar(50)), Err_Code=cast('' as varchar(20)), Err_Detail=cast('' as nvarchar(max)) into #__SP_ERR_Table where 0=1
end else begin
	truncate table #__SP_ERR_TABLE
end

begin try
	-- Jalankan script SP disini

	EXEC RSP_PQ_MAINTAIN_SERVICE 
'SRVC001',--@CSERVICE_ID
'SERVICE001',--@CSERVICE_NAME
'GHC',--@CUSER_ID
'A'--@CACTION_MODE

end try
begin catch
	select * from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch