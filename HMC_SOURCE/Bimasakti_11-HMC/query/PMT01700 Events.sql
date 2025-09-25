--EXEC RSP_GS_GET_OTHER_UNIT_LIST 'Login Company Id', 'Selected Property Id', 'Login User Id'
delete from SAT_LOCKING where CUSER_ID='ram'
select * from SAT_LOCKING
---EXEC RSP_PM_GET_AGREEMENT_LIST 'Login Company Id', 'Selected Property Id', VAR_GSM_TRANSACTION_CODE.CTRANS_CODE, 'Login User Id'

--------------------OTHER LIST___________________
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_GS_GET_OTHER_UNIT_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@LEVENT = 1,
		@CUSER_ID = 'hmc'RSP_GET_REPORT_TEMPLATE_LIST
SELECT	'Return Value' = @return_value
GO

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_GS_GET_BUILDING_UNIT_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO
------------------TAB LOO OFFER LIST
	--Agreement List
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_LIST] --RSP_PM_GET_AGREEMENT_LIST-
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CTRANS_CODE = '802063',
		@CFROM_REF_DATE = '',
		@CTRANS_STATUS_LIST = '',
		@CUSER_ID = 'aoc'
SELECT	'Return Value' = @return_value
GO
 ---SUBMIT LOO
 USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_UPDATE_AGREEMENT_TRANS_STS]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'ACC',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'A-001-00',
		@CUSER_ID = 'hmc',
		@CNEW_STATUS = '10'
SELECT	'Return Value' = @return_value
GO



------DETAIL
--CEK TEST 
select * from PMT_AGREEMENT
where CCOMPANY_ID = 'rcd' 
and CREF_NO= 'PMM-00027-202407'
AND CTRANS_CODE = '802043'
/*

                var loDbParam = loCommand.Parameters.Cast<DbParameter>()
*/

--MAINTAIN @IHOURS
USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_AGREEMENT]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'ACC',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'A-001-01',
		@CREF_DATE = '20240624',
		@CBUILDING_ID = 'TW-A',
		@CDOC_NO = '',
		@CDOC_DATE = '',
		@CSTART_DATE = '20240624',
		@CEND_DATE = '20250623',
		@IDAYS = 0,
		@IMONTHS = 0,
		@IYEARS = 1,
		@CSALESMAN_ID = 'C099',
		@CTENANT_ID = 'TNT08',
		@CUNIT_DESCRIPTION = '',
		@CNOTES = '',
		@CCURRENCY_CODE = 'IDR',
		@CLEASE_MODE = '',
		@CCHARGE_MODE = '',
		@CACTION = 'ADD',
		@CUSER_ID = 'hmc',
		@CORIGINAL_REF_NO = '',
		@CFOLLOW_UP_DATE = '',
		@CEXPIRED_DATE = '',
		@LWITH_FO = 0,
		@CHAND_OVER_DATE = '',
		@CBILLING_RULE_CODE = '',
		@NBOOKING_FEE = 0,
		@CTC_CODE = 'TC001',
		@CLINK_TRANS_CODE = '',
		@CLINK_REF_NO = ''

SELECT	'Return Value' = @return_value

GO
--RSP_PM_MAINTAIN_AGREEMENT_UNIT


USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_AGREEMENT_UNIT]
		@CCOMPANY_ID = N'rcd',
		@CPROPERTY_ID = N'ashmd',
		@CDEPT_CODE = N'acc',
		@CTRANS_CODE = N'802043',
		@CREF_NO = N'A-001-00',
		@CUNIT_ID = N'123126',
		@CFLOOR_ID = N'L04',
		@CBUILDING_ID = N'TW-A',
		@NACTUAL_AREA_SIZE = 0,
		@NCOMMON_AREA_SIZE = 0,
		@CACTION = N'add',
		@CUSER_ID = N'hmc'

SELECT	'Return Value' = @return_value

GO

 
NTOTAL_GROSS_AREA
0.00

------------------TAB OFFER
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_DETAIL]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'A-001-00',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO



select * from PMT_AGREEMENT
where CTRANS_CODE = '802053'

select * from PMT_AGREEMENT
where CTRANS_CODE = '802043'





CCOMPANY_ID	CPROPERTY_ID	CREF_NO	CCHARGE_MODE	CCHARGE_MODE_DESCR	CUNIT_ID	CUNIT_NAME	CFLOOR_ID	CFLOOR_NAME	CBUILDING_ID	CBUILDING_NAME	NACTUAL_AREA_SIZE	NCOMMON_AREA_SIZE	NGROSS_AREA_SIZE	NNET_AREA_SIZE	CUNIT_TYPE_ID	CUNIT_TYPE_NAME	CUPDATE_BY	DUPDATE_DATE	CCREATE_BY	DCREATE_DATE


--Load Other Unit Info List

USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_AGREEMENT_UNIT]
		@CCOMPANY_ID = N'rcd',
		@CPROPERTY_ID = N'ashmd',
		@CDEPT_CODE = N'acc',
		@CTRANS_CODE = N'802043',
		@CREF_NO = N'A-001-00',
		@CUNIT_ID = N'A0102',
		@CFLOOR_ID = N'L01',
		@CBUILDING_ID = N'TW-A',
		@NACTUAL_AREA_SIZE = 0,
		@NCOMMON_AREA_SIZE = 0,
		@CACTION = N'add',
		@CUSER_ID = N'hmc'

SELECT	'Return Value' = @return_value

GO

select * from PMT_AGREEMENT_UNIT
where CTRANS_CODE = '802043' --SM_PROPERTY_UNIT

select * from GSM_PROPERTY_UNIT
where CPROPERTY_ID= 'ashmd'

select * from GSM_PROPERTY_UNIT_TYPE
where CPROPERTY_ID= 'ashmd'


USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_UNIT_INFO_LIST]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = '00',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'A-001-00',
		@CUSER_ID = 'hmc'

SELECT	'Return Value' = @return_value

GO

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_UNIT_INFO_LIST]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'LOO-E-00-2024090001',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

----•	Load Other Unit Info Detail 
USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_UNIT_INFO_DT]
		@CCOMPANY_ID = NULL,
		@CPROPERTY_ID = NULL,
		@CDEPT_CODE = NULL,
		@CTRANS_CODE = NULL,
		@CREF_NO = NULL,
		@CUNIT_ID = NULL,
		@CFLOOR_ID = NULL,
		@CBUILDING_ID = NULL,
		@CUSER_ID = NULL

SELECT	'Return Value' = @return_value

GO

EXEC RSP_PM_GET_AGREEMENT_UNIT_INFO_LIST 'Login Company Id', 'Selected Property Id', 'Selected Dept Code', 'Selected Transaction Code', 'Selected Ref No', 'Login User Id'
EXEC RSP_PM_GET_AGREEMENT_UNIT_INFO_DT 'Login Company Id', 'Selected Property Id', 'Selected Dept Code', 'Selected Transaction Code', 'Selected Ref No', 'Selected Unit', 'Selected Floor', 'Selected Building', 'Login User Id'

USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_AGREEMENT]
		@CCOMPANY_ID = NULL,
		@CPROPERTY_ID = NULL,
		@CDEPT_CODE = NULL,
		@CTRANS_CODE = NULL,
		@CREF_NO = NULL,
		@CREF_DATE = NULL,
		@CBUILDING_ID = NULL,
		@CDOC_NO = NULL,
		@CDOC_DATE = NULL,

		@CSTART_DATE = NULL,
		@CEND_DATE = NULL,
		@IDAYS = NULL,
		@IMONTHS = NULL,
		@IYEARS = NULL,
		@CSALESMAN_ID = NULL,
		@CTENANT_ID = NULL,
		@CUNIT_DESCRIPTION = NULL,
		@CNOTES = NULL,
		@CCURRENCY_CODE = NULL,
		@CLEASE_MODE = NULL,
		@CCHARGE_MODE = NULL,
		@CACTION = NULL,
		@CUSER_ID = NULL,

		@CORIGINAL_REF_NO = NULL,
		@CFOLLOW_UP_DATE = NULL,
		@CEXPIRED_DATE = NULL,
		@LWITH_FO = NULL,
		@CHAND_OVER_DATE = NULL,
		@CBILLING_RULE_CODE = NULL,
		@NBOOKING_FEE = NULL,
		@CTC_CODE = NULL

SELECT	'Return Value' = @return_value

GO

-------------------DEPOSITTTT

EXEC RSP_PM_MAINTAIN_AGREEMENT_DEPOSIT


--------CHARGESSSS
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'A-001-00',
		@CCHARGE_MODE = '',
		@CBUILDING_ID = 'TW-A',
		@CFLOOR_ID = 'L01',
		@CUNIT_ID = 'A0103',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

EXEC RSP_PM_GET_AGREEMENT_CHARGES_DT 'Login Company Id', 'Selected Property Id', 
'Selected Dept Code', 'VAR_GSM_TRANSACTION_CODE.CTRANS_CODE', 'Selected Ref No', 'Selected Seq No', 'Login User Id'

SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO 
('BIMASAKTI', 'Login Company Id', '_BS_FEE_METHOD', '', 'Login Language Id')

SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO 
('BIMASAKTI', 'Login Company Id', '_BS_FEE_METHOD', '', 'Login Language Id')



----------------
USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_AGREEMENT_CHARGES] --@CCURRENCY_CODE
		@CCOMPANY_ID='',
@CPROPERTY_ID		='',
@CDEPT_CODE			='',
@CTRANS_CODE		='',
@CREF_NO			='',
@CSEQ_NO			='',
@CCHARGE_MODE		='',
@CBUILDING_ID		='',
@CFLOOR_ID			='',
@CUNIT_ID			='',
@CCHARGES_TYPE		='',
@CCHARGES_ID		='',
@CTAX_ID			='',
@CSTART_DATE		='',
@IYEAR				=0,
@IMONTH				=0,
@IDAY				=0,
@CEND_DATE			='',
@CCURRENCY_CODE		='',
@CBILLING_MODE		='',
@CFEE_METHOD		='',
@NFEE_AMT			=0,
@CINVOICE_PERIOD	='',
@NINVOICE_AMT		=0,
@CDESCRIPTION		='',
@LCAL_UNIT			=0,
@LBASED_OPEN_DATE	=0,
@LPRORATE			=0,
@CACTION			='',
@CUSER_ID			=''
SELECT	'Return Value' = @return_value
GO

pagi Fiz, ini rsp untuk get items di charges
EXEC RSP_PM_GET_AGREEMENT_CHARGES_ITEMS 'Login Company Id', 'Selected Property Id', 'Selected Dept Code', 'Selected Trans Code', 'Selected Ref No', 'Selected Charges Seq No', 'Login User Id'

USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_ITEMS]
		@CCOMPANY_ID = NULL,
		@CPROPERTY_ID = NULL,
		@CDEPT_CODE = NULL,
		@CTRANS_CODE = NULL,
		@CREF_NO = NULL,
		@CCHARGES_SEQ_NO = NULL,
		@CUSER_ID = NULL

SELECT	'Return Value' = @return_value

GO


									-------------LOC----------------
RSP_GS_GET_TRANS_CODE_INFO  'rcd', '802043'
---	VAR_COMPANY_ID
---	VAR_TRANS_CODE === 802043

------------ LOC LIST -------------
--GET LIST
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_LIST]
		@CCOMPANY_ID = N'bsi',
		@CPROPERTY_ID = N'ASHMD',
		@CTRANS_CODE = N'802043',
		@CUSER_ID = N'hmc'
SELECT	'Return Value' = @return_value
GO


CCOMPANY_ID	CPROPERTY_ID	CDEPT_CODE	CDEPT_NAME	CTRANS_CODE	CTRANS_NAME	CREF_NO	CREF_DATE	CBUILDING_ID	CBUILDING_NAME	CDOC_NO	CDOC_DATE	CSTART_DATE	CEND_DATE	IMONTHS	IYEARS	IDAYS	IHOURS	CSALESMAN_ID	CSALESMAN_NAME	CTENANT_ID	CTENANT_NAME	CUNIT_DESCRIPTION	CEVENT_NAME	CNOTES	CCURRENCY_CODE	CCURRENCY_NAME	CLEASE_MODE	CLEASE_MODE_DESCR	CCHARGE_MODE	CCHARGE_MODE_DESCR	CTRANS_STATUS	CTRANS_STATUS_DESCR	CAGREEMENT_STATUS	CAGREEMENT_STATUS_DESCR	CDOC_TYPE	CORIGINAL_REF_NO	CFOLLOW_UP_DATE	CHO_ACTUAL_DATE	COPEN_DATE	LPAID	CUPDATE_BY	DUPDATE_DATE	CCREATE_BY	DCREATE_DATE
rcd	ashmd	ACC	ACCOUNTING	802043	LOO Event	A-001-00	20240624	TW-A	Tower A		        	20240624	20250623	0	0	0	0	NY004	Nayla	TNT08	tenant8	new desc	new desc	new Note		NULL	  	NULL	  	NULL	00	Draft	01	Signed			        		20240624	0	hmc	2024-06-24 09:18:19.863	hmc	2024-06-24 09:18:19.863
--GET UNIT  LIST
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_UNIT_INFO_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802063',
		@CREF_NO = 'A-001-03',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value

--GET DETAIL
GO
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_UNIT_INFO_DT]
			@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802063',
		@CREF_NO = 'LOI-E-2024070002',
		@CUNIT_ID = 'A0104',
		@CFLOOR_ID = 'L01',
		@CBUILDING_ID = 'TW-A',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

--Utilities List--
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_UTILITIES_LIST]
			@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'A-001-03',
		@CUNIT_ID = 'A0103',
		@CFLOOR_ID = 'l01',
		@CBUILDING_ID = 'TW-A',
		@CUSER_ID = 'hmc'

SELECT	'Return Value' = @return_value

GO




------------ LOC ------------------
------------ UNIT -----------------

USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_UNIT_INFO_LIST]
		@CCOMPANY_ID = N'rcd',
		@CPROPERTY_ID = N'ashmd',
		@CDEPT_CODE = N'Acc',
		@CTRANS_CODE = N'802053',
		@CREF_NO = N'A-001-03',
		@CUSER_ID = N'hmc'

SELECT	'Return Value' = @return_value

GO
--Utilities List--
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_UTILITIES_LIST]
		@CCOMPANY_ID = NULL,
		@CPROPERTY_ID = NULL,
		@CDEPT_CODE = NULL,
		@CTRANS_CODE = NULL,
		@CREF_NO = NULL,
		@CUNIT_ID = NULL,
		@CFLOOR_ID = NULL,
		@CBUILDING_ID = NULL,
		@CUSER_ID = NULL

SELECT	'Return Value' = @return_value

GO

------------ CHARGES --------------

/*
			@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'A-001-03',
		@CUNIT_ID = 'A0103',
		@CFLOOR_ID = 'l01',
		@CBUILDING_ID = 'TW-A',
		@CUSER_ID = 'hmc'
*/
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_LIST]
			@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'A-001-03',
		@CCHARGE_MODE = '', -- KONFIRMASI
		@CUNIT_ID = 'A0103',
		@CFLOOR_ID = 'l01',
		@CBUILDING_ID = 'TW-A',
		@CUSER_ID = 'hmc'		
SELECT	'Return Value' = @return_value
GO


USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_ITEMS]
			@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'A-001-03',
		@CCHARGES_SEQ_NO = NULL,
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

---CHARGE
SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO 
('BIMASAKTI', 'rcd', '_BS_FEE_METHOD', '', 'en')

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_LIST]
@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'fin',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'PMM-00017-202409',
		@CCHARGE_MODE = '01',
		@CBUILDING_ID = '',
		@CFLOOR_ID = '',
		@CUNIT_ID = 'OU002',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value --CINVOICE_PERIOD_DESCR
GO

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_DT]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'FIN',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'PMM-00017-202409',
		@CSEQ_NO = '001',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_AGREEMENT_CHARGES]
		@CCOMPANY_ID = NULL,
		@CPROPERTY_ID = NULL,
		@CDEPT_CODE = NULL,
		@CTRANS_CODE = NULL,
		@CREF_NO = NULL,
		@CSEQ_NO = NULL,
		@CCHARGE_MODE = NULL,
		@CBUILDING_ID = NULL,
		@CFLOOR_ID = NULL,
		@CUNIT_ID = NULL,
		@CCHARGES_TYPE = NULL,
		@CCHARGES_ID = NULL,
		@CTAX_ID = NULL,
		@CSTART_DATE = NULL,
		@IYEAR = NULL,
		@IMONTH = NULL,
		@CEND_DATE = NULL,
		@CCURRENCY_CODE = NULL,
		@CBILLING_MODE = NULL,
		@CFEE_METHOD = NULL,
		@NFEE_AMT = NULL,
		@CINVOICE_PERIOD = NULL,
		@NINVOICE_AMT = NULL,
		@CDESCRIPTION = NULL,
		@LCAL_UNIT = NULL,
		@CACTION = NULL,
		@CUSER_ID = NULL

SELECT	'Return Value' = @return_value

GO


USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_ITEMS]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'A-001-03',
		@CCHARGES_SEQ_NO = NULL,
		@CUSER_ID = NULL

SELECT	'Return Value' = @return_value

GO

------------ DEPOSIT --------------
--HEADER
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_DETAIL]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'FIN',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'PMM-00022-202407',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO


--DEPOSIT LIST
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_DEPOSIT_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'ACC',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'A-001-03',
		@CCHARGE_MODE = '', --''
		@CBUILDING_ID = 'TW-A', 
		@CFLOOR_ID = '', --''
		@CUNIT_ID = '', --''
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

RSP_PM_MAINTAIN_AGREEMENT_CHARGES
delete user_\\\ from SAT_LOCKING where use

BEGIN TRANSACTION

IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null) BEGIN
	select SP_Name=cast('' as varchar(50)), Err_Code=cast('' as varchar(20)), Err_Detail=cast('' as nvarchar(max)) into #__SP_ERR_Table where 0=1
end else begin
	truncate table #__SP_ERR_TABLE
end

begin try
EXEC	[dbo].[RSP_PM_UPDATE_AGREEMENT_TRANS_STS]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'ACC',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'LMACC-20240600001-PM',
		@CUSER_ID = 'hmc',
		@CNEW_STATUS = '10'

end try
begin catch
	select * from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

ROLLBACK


BEGIN TRANSACTION

IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null) BEGIN
	select SP_Name=cast('' as varchar(50)), Err_Code=cast('' as varchar(20)), Err_Detail=cast('' as nvarchar(max)) into #__SP_ERR_Table where 0=1
end else begin
	truncate table #__SP_ERR_TABLE
end

begin try
	
	--HAPIS ANJING DISINI CODENAY LU PASTE


end try
begin catch
	select * from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

ROLLBACK

select * from PMT_AGREEMENT_UNIT

SELECT * FROM  PMT_AGREEMENT_UNIT
where CREF_NO = 'PMM-00001-202407'


---OFFER LIST
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_LIST] --RSP_PM_GET_AGREEMENT_LIST-
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CTRANS_CODE = '802043',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

--00 = DRAFT
--10 = OPEN
--30 = APPROVED

RSP_PM_GET_TENANT_DETAIL


--CALL by another program
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_ASSIGN_LOO_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CLOO_TRANS_CODE = '802043',
		@CLOC_TRANS_CODE = '802053',
		@CFROM_DATE = NULL,
		@CLANG_ID = 'en'
SELECT	'Return Value' = @return_value
GO


RSP_PM_GET_TENANT_DETAIL 'rcd', 'ashmd', 'LK002','hmc'



--P_LOO_TRANS_CODE = ‘802043’
--P_LOC_TRANS_CODE = ‘802053’

•	Refresh LOO Grid dengan RSP_PM_GET_ASSIGN_LOO_LIST dengan parameter:
-	VAR_COMPANY_ID
-	Selected Property ID
-	VAR_LOO_TRANS_CODE	
-	VAR_LOC_TRANS_CODE
-	Selected From Offer Date
-	VAR_LANGUAGE_ID

----------CHARGES
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_DT]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'A-001-00',
		@CSEQ_NO = '001',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

---MAINTAIN CHARGES
USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_AGREEMENT_CHARGES]
		@CCOMPANY_ID = NULL,
		@CPROPERTY_ID = NULL,
		@CDEPT_CODE = NULL,
		@CTRANS_CODE = NULL,
		@CREF_NO = NULL,
		@CSEQ_NO = NULL,
		@CCHARGE_MODE = NULL,
		@CBUILDING_ID = NULL,
		@CFLOOR_ID = NULL,
		@CUNIT_ID = NULL,
		@CCHARGES_TYPE = NULL,
		@CCHARGES_ID = NULL,
		@CTAX_ID = NULL,
		@CSTART_DATE = NULL,
		@IYEAR = NULL,
		@IMONTH = NULL,
		@CEND_DATE = NULL,
		@CCURRENCY_CODE = NULL,
		@CBILLING_MODE = NULL,
		@CFEE_METHOD = NULL,
		@NFEE_AMT = NULL,
		@CINVOICE_PERIOD = NULL,
		@NINVOICE_AMT = NULL,
		@CDESCRIPTION = NULL,
		@LCAL_UNIT = NULL,
		@LBASED_OPEN_DATE = NULL,
		@LPRORATE = NULL,
		@CACTION = NULL,
		@CUSER_ID = NULL

SELECT	'Return Value' = @return_value

GO

drop TABLE #CHARGES_ITEMS

BEGIN TRANSACTION

CREATE TABLE #CHARGES_ITEMS (
	ISEQ INT,
    CITEM_NAME VARCHAR(255),
    IQTY INT,
    NUNIT_PRICE NUMERIC(16,2),
    NDISCOUNT NUMERIC(16,2),
    NTOTAL_PRICE NUMERIC(16, 2)
);

INSERT INTO #CHARGES_ITEMS (ISEQ, CITEM_NAME, IQTY,  NUNIT_PRICE, NDISCOUNT, NTOTAL_PRICE)
VALUES (1,'item', 1, 10000, 0, 10000);

EXEC	[RSP_PM_MAINTAIN_AGREEMENT_CHARGES]
@CCOMPANY_ID = 'rcd'
,@CPROPERTY_ID =  'ashmd'
,@CDEPT_CODE = 'ACC'
,@CTRANS_CODE = '802043'
,@CREF_NO = 'A-001-00'
,@CSEQ_NO = '001'
,@CCHARGE_MODE = ''
,@CBUILDING_ID = 'TW-A'
,@CFLOOR_ID = 'L01'
,@CUNIT_ID =  'A0103'
,@CCHARGES_TYPE = '01'
,@CCHARGES_ID = '20240038729910273992'
,@CTAX_ID = ''
,@CSTART_DATE = '20240710'
,@IYEAR = 0
,@CEND_DATE = '20240710'
,@CBILLING_MODE = '01'
,@CFEE_METHOD = '01'
,@NFEE_AMT = 0.00
,@CDESCRIPTION = 'Desc'
,@CINVOICE_PERIOD = '02'
,@NINVOICE_AMT = 100000.0
,@LCAL_UNIT = True
,@CACTION =  'EDIT'
,@CUSER_ID = 'hmc'
,@IMONTH = 0
,@LPRORATE = 0

commit transaction
ROLLBACK

select * from 

USE [BIMASAKTI_11]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_ITEMS]
@CCOMPANY_ID = 'rcd'
,@CPROPERTY_ID =  'ashmd'
,@CDEPT_CODE = 'ACC'
,@CTRANS_CODE = '802043'
,@CREF_NO = 'A-001-00'
,@CCHARGES_SEQ_NO = '001',
@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_DETAIL]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'FIN',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'PMM-00007-202407',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

/*
 PMT01700ParameterFrontChangePageDTO temp = new PMT01700ParameterFrontChangePageDTO
                {
                    
                    CPROPERTY_ID = "ASHMD",
                    CDEPT_CODE = "FIN",
                    CTRANS_CODE = "802043",
                    CREF_NO = "PMM-00004-202407",
                    //For Unit Info, Charges, and Deposit
                    CBUILDING_ID = "TW-A",
                    CBUILDING_NAME = "Tower A",
                    //Updated : For call from Page PMT01200
                    CALLER_ACTION = "ADD",
                    COTHER_UNIT_ID = "",
                    CCHARGE_MODE = "",
                    CFLOOR_ID =""
                    };

*/

--TEST DATA---
	--Agreement List
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_LIST] --RSP_PM_GET_AGREEMENT_LIST-
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CTRANS_CODE = '802043',
		@CUSER_ID = 'hmc'

SELECT	'Return Value' = @return_value
GO


---UTILITIES OLD
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_GS_GET_BUILDING_UTILITIES_LIST]
		@CCOMPANY_ID = NULL,
		@CPROPERTY_ID = NULL,
		@CBUILDING_ID = NULL,
		@CFLOOR_ID = NULL,
		@CUNIT_ID = NULL,
		@CUTILITY_TYPE = NULL,
		@CUSER_ID = NULL
SELECT	'Return Value' = @return_value
GO
---UTILITIES NEW
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_GS_GET_BUILDING_OTHER_UTILITIES_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@COTHER_UNIT_ID = 'A0103',
		@CUTILITY_TYPE = '01',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO


USE [BIMASAKTI_11]
GO

DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_MAINTAIN_AGREEMENT_UTILITIES]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'ACC',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'A-001-00',
		@CUNIT_ID = 'A0103',
		@CFLOOR_ID = 'L01',
		@CBUILDING_ID = 'TW-A',
		@CCHARGES_TYPE = '01',
		@CCHARGES_ID = 'ELC04',
		@CTAX_ID = '',
		@CSEQ_NO = '',
		@CMETER_NO = 'M001',
		@IMETER_START = 0,
		@CSTART_INV_PRD = '',
		@CACTION = 'ADD',
		@CUSER_ID = 'HMC'
SELECT	'Return Value' = @return_value
GO

select * from PMT_AGREEMENT
where CPROPERTY_ID = 'ashmd'
and CTRANS_CODE='802043'

rollback

exec RSP_PM_MAINTAIN_AGREEMENT_UNIT
@CCOMPANY_ID = 'rcd'
,@CPROPERTY_ID = 'JBMPC'
,@CDEPT_CODE = 'HRD'
,@CTRANS_CODE = '802043'
,@CREF_NO = ''
,@CUNIT_ID = 'Stall1'
,@CFLOOR_ID = 'Lt10'
,@CBUILDING_ID = ''
,@NACTUAL_AREA_SIZE = 0
,@NCOMMON_AREA_SIZE = 0
,@CACTION = 'ADD'
,@CUSER_ID = 'hmc'

RSP_PM_MAINTAIN_TENANT   loDb.R_AddCommandParameter(loCommand, "@@CTAX_EMAIL2", DbType.String, 100, "");

RSP_GS_GET_BUILDING_OTHER_UTILITIES_LIST
RSP_GS_GET_BUILDING_UTILITIES_LIST

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CTRANS_CODE = '802043',
		@CFROM_REF_DATE = '20240101',
		@CTRANS_STATUS_LIST = '98',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

/*
00 = draft
10 = OPEN habis submit
30 = Approved
80 = closed

*/

select * from PMT_AGREEMENT
where CBUILDING_ID = 'TWR001'
and CTRANS_CODE = '802030'


Berikukt SP yang saya Jalankan :

BEGIN TRANSACTION

CREATE TABLE #LEASE_AGREEMENT(NO			INT
				,CCOMPANY_ID		VARCHAR(8)							
				,CPROPERTY_ID		VARCHAR(20)		
				,CDEPT_CODE		VARCHAR(20)		
				,CREF_NO			VARCHAR(30)		
				,CREF_DATE		CHAR(8)			
				,CBUILDING_ID		VARCHAR(20)		
				,CDOC_NO			VARCHAR(30)		
				,CDOC_DATE		CHAR(8)			
				,CSTART_DATE		VARCHAR(8)		
				,CEND_DATE		VARCHAR(8)		
				,CMONTH			VARCHAR(2)		
				,CYEAR			VARCHAR(4)		
				,CDAY			VARCHAR(4)		
				,CSALESMAN_ID		VARCHAR(8)		
				,CTENANT_ID		VARCHAR(20)		
				,CUNIT_DESCRIPTION		NVARCHAR(255)	
				,CNOTES			NVARCHAR(MAX)	
				,CCURRENCY_CODE		VARCHAR(3)		
				,CLEASE_MODE		CHAR(2)			
				,CCHARGE_MODE		CHAR(2)			
				)


INSERT INTO #LEASE_AGREEMENT VALUES (
    1,               -- NO (Nomor urutan)
    'RCD',           -- CCOMPANY_ID (ID perusahaan)
    'ASHMD',         -- CPROPERTY_ID (ID properti)
    'ACC',           -- CDEPT_CODE (Kode departemen)
    '',              -- CREF_NO (Referensi nomor kosong)
    '20240910',      -- CREF_DATE (Tanggal referensi 10 September 2024)
    'TWR001',        -- CBUILDING_ID (ID bangunan)
    'DOC-NO-002',    -- CDOC_NO (Nomor dokumen)
    '20240910',      -- CDOC_DATE (Tanggal dokumen 10 September 2024)
    '20240910',      -- CSTART_DATE (Tanggal mulai 10 September 2024)
    '20250910',      -- CEND_DATE (Tanggal selesai 10 September 2025)
    '',              -- CMONTH (Bulan kosong)
    '',              -- CYEAR (Tahun kosong)
    '1',              -- CDAY (Hari kosong)
    'S0001',         -- CSALESMAN_ID (ID salesman)
    'T003',          -- CTENANT_ID (ID tenant)
    'Test Upload',   -- CUNIT_DESCRIPTION (Deskripsi unit: "Test Upload")
    '',              -- CNOTES (Catatan kosong)
    'IDR',           -- CCURRENCY_CODE (Kode mata uang: IDR)
    '01',            -- CLEASE_MODE (Mode leasing)
    '01'             -- CCHARGE_MODE (Mode charge)
);

CREATE TABLE #LEASE_AGREEMENT_UNIT(NO INT
			,CDOC_NO			VARCHAR(30)		
			,CUNIT_ID			VARCHAR(20)		
			,CFLOOR_ID			VARCHAR(20) 
			,CBUILDING_ID		VARCHAR(20)
									 )
INSERT INTO #LEASE_AGREEMENT_UNIT VALUES(1, 'DOC-NO-002', 'U-C-F4-03', 'FLR04','TWR001')

CREATE TABLE #LEASE_AGREEMENT_UTILITY (NO		INT
					,CDOC_NO		VARCHAR(30)								
					,CUTILITY_TYPE	VARCHAR(10)		
					,CUNIT_ID		VARCHAR(20)	
					,CMETER_NO 	VARCHAR(30)
					,CCHARGES_ID 	VARCHAR(30)
					,CTAX_ID 		VARCHAR(30)		
					)	

INSERT INTO #LEASE_AGREEMENT_UTILITY VALUES (1, 'DOC-NO-002', '01', 'U-C-F4-03', 'ELE-U-C-04-03', 'EC01', 'PPN10')

CREATE TABLE #LEASE_AGREEMENT_CHARGES (NO		INT
					,CDOC_NO		VARCHAR(30)								
					,CCHARGES_ID	VARCHAR(30)
					,CTAX_ID 		VARCHAR(30)
					,IYEARS 		INT									
					,IMONTHS 		INT
					,IDAYS 		INT
					,LBASED_OPEN_DATE	BIT
					,CSTART_DATE	VARCHAR(8)
					,CEND_DATE 	VARCHAR(8)
					,CBILLING_MODE 	VARCHAR(30)
					,CCURENCY_CODE 	VARCHAR(30)
					,CFEE_METHOD 	VARCHAR(30)
					,NFEE_AMT 	NUMERIC(18,2)
					,CPERIOD_MODE 	VARCHAR(30)
					,LPRORATE		BIT
					,CDESCRIPTION	NVARCHAR(MAX)	
					)

INSERT INTO #LEASE_AGREEMENT_CHARGES VALUES (1, 'DOC-NO-002', 'CH01', 'PPN10',
0, 0, 1, 0, '20240910', 
'20250910', '02', 'IDR', '01', 
50000, '01', 0, 'Upload Charges Test')

CREATE TABLE #LEASE_AGREEMENT_DEPOSIT (NO		INT
					,CDOC_NO		VARCHAR(30)
					,LCONTRACTOR 	BIT
					,CCONTRACTOR_ID 	VARCHAR(30)
					,CDEPOSIT_ID 	VARCHAR(30)
					,CDEPOSIT_DATE 	VARCHAR(30)
					,CCURRENCY_CODE 	VARCHAR(3)
					,NDEPOSIT_AMT 	NUMERIC(18,2)
					,LPAID 		BIT
					,CDESCRIPTION 	VARCHAR(30)	
					)

INSERT INTO #LEASE_AGREEMENT_DEPOSIT VALUES (1, 'DOC-NO-002', 1, 'CC03', 
'BBC', '20240910', 'IDR', 880000, 
0, 'Upload Deposit Test')

EXEC RSP_PM_UPLOAD_LEASE_AGREEMENT 'RCD', 'ASHMD', '802030', 'ERC', 'XXXXXXX'

rollback

SELECT * FROM GST_UPLOAD_ERROR_STATUS WHERE CKEY_GUID= 'XXXXXXX'
	
	SELECT * FROM GST_UPLOAD_PROCESS_STATUS WHERE CKEY_GUID = 'XXXXXXX'


--------CHARGESSSS
USE [BIMASAKTI_11]
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = 'ACC',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'PMM-00001-202408',
		@CCHARGE_MODE = '01',
		@CBUILDING_ID = '',
		@CFLOOR_ID = '',
		@CUNIT_ID = '',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_DT]
@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID ='ashmd',
		@CDEPT_CODE = 'acc',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'PMM-00001-202408',
		@CSEQ_NO = '001',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO 
('BIMASAKTI', 'BSI', '_BS_INVOICE_PERIOD', '', 'en');

select * from GST_EMAIL_OUTBOX

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_DEPOSIT_LIST]
		@CCOMPANY_ID = 'BSI',
		@CPROPERTY_ID = 'ASHMD',
		@CDEPT_CODE = '00',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'LOC-E-00-2024100002',
		@CCHARGE_MODE = '', --''
		@CBUILDING_ID = '', 
		@CFLOOR_ID = '', --''
		@CUNIT_ID = '', --''
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO


exec RSP_PM_MAINTAIN_AGREEMENT
@CACTION = 'ADD', 
@CBILLING_RULE_CODE= '', 
@CBUILDING_ID= 'TW-1', 
@CCHARGE_MODE= '', 
@CCOMPANY_ID= 'BSI', 
@CCURRENCY_CODE= 'IDR', 
@CDEPT_CODE= '00', 
@CDOC_DATE= '', 
@CDOC_NO= '', 
@CEND_DATE= '20241025', 
@CEXPIRED_DATE= '', 
@CFOLLOW_UP_DATE= '20241024', 
@CLEASE_MODE= '', 
@CLINK_REF_NO= 'LOO-E-00-2024100011', 
@CLINK_TRANS_CODE= '802043', 
@CNOTES= 'note', 
@CORIGINAL_REF_NO= '', 
@CPROPERTY_ID= 'ASHMD',
@CREF_DATE= '20241024', 
@CREF_NO= '', 
@CSALESMAN_ID= 'SLM006',
@CSTART_DATE= '20241024', 
@CTC_CODE= '',
@CTENANT_ID= 'TNT12',
@CTRANS_CODE= '802053', 
@CUNIT_DESCRIPTION= 'Bazar 24 Okt', 
@CUSER_ID= 'HMC', 
@IDAYS= 1, @IHOURS= 0, @IMONTHS= 0, @IYEARS= 0, @LWITH_FO= false, 
@NBOOKING_FEE= 0


exec RSP_PM_MAINTAIN_AGREEMENT
@CACTION= 'ADD', 
@CBILLING_RULE_CODE= '', 
@CBILLING_RULE_TYPE= '', 
@CBUILDING_ID= 'TW-1', 
@CCHARGE_MODE= '01', 
@CCOMPANY_ID= 'BSI', 
@CCURRENCY_CODE= 'IDR', 
@CDEPT_CODE= '00', 
@CDOC_DATE= '', 
@CDOC_NO= '', 
@CEND_DATE= '20241025', 
@CEXPIRED_DATE= '20241024', 
@CFOLLOW_UP_DATE= '20241024',
@CLEASE_MODE= '01', 
@CLINK_REF_NO= '', 
@CLINK_TRANS_CODE= '', 
@CNOTES= 'note', 
@CORIGINAL_REF_NO= null, 
@CPROPERTY_ID= 'ASHMD', 
@CREF_DATE= '20241024', 
@CREF_NO= '', 
@CSALESMAN_ID= 'SLM006', 
@CSTART_DATE= '20241024',
@CTC_CODE= '', 
@CTENANT_ID= 'TNT12', 
@CTRANS_CODE= '802043', 
@CUNIT_DESCRIPTION= 'Bazar 24 Okt',
@CUSER_ID= 'HMC', @IDAYS= 1, 
@IHOURS= 0, @IMONTHS= 0, @IYEARS= 0, @LWITH_FO= false, @NBOOKING_FEE= 0



--------CHARGESSSS
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_CHARGES_LIST]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = '00',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'LOO-E-00-2024100007',
		@CCHARGE_MODE = '01',
		@CBUILDING_ID = '',
		@CFLOOR_ID = '',
		@CUNIT_ID = '',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO


--//
RSP_PM_GET_AGREEMENT_DETAIL
Parameter
{'@CCOMPANY_ID': 'BSI', '@CDEPT_CODE': '00', '@CPROPERTY_ID': 'ASHMD', '@CREF_NO': 'LOO-E-00-2024100007', '@CTRANS_CODE': '802043', '@CUSER_ID': 'HMC'}
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AGREEMENT_DETAIL]
		@CCOMPANY_ID = 'BSI',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = '00',
		@CTRANS_CODE = '802043',
		@CREF_NO = 'LOO-E-00-2024100007',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO



exec RSP_PM_MAINTAIN_AGREEMENT 
@CACTION= 'ADD', 
@CBILLING_RULE_CODE= '', 
@CBILLING_RULE_TYPE= '', 
@CBUILDING_ID= 'TW-1', 
@CCHARGE_MODE= '01', 
@CCOMPANY_ID= 'BSI', 
@CCURRENCY_CODE= 'IDR', 
@CDEPT_CODE= '00', 
@CDOC_DATE= '', 
@CDOC_NO= '', 
@CEND_DATE= '20241025', 
@CEXPIRED_DATE= '20241024', 
@CFOLLOW_UP_DATE= '20241024',
@CLEASE_MODE= '01', 
@CLINK_REF_NO= '', 
@CLINK_TRANS_CODE= '', 
@CNOTES= 'note', 
@CORIGINAL_REF_NO= null, 
@CPROPERTY_ID= 'ASHMD', 
@CREF_DATE= '20241024', 
@CREF_NO= '', 
@CSALESMAN_ID= 'SLM006', 
@CSTART_DATE= '20241024',
@CTC_CODE= '', 
@CTENANT_ID= 'TNT12', 
@CTRANS_CODE= '802043', 
@CUNIT_DESCRIPTION= 'Bazar 24 Okt',
@CUSER_ID= 'HMC', @IDAYS= 1, 
@IHOURS= 0, @IMONTHS= 0, @IYEARS= 0, @LWITH_FO= false, @NBOOKING_FEE= 0


begin transaction

EXEC [RSP_PM_UPDATE_AGREEMENT_TRANS_STS]
		@CCOMPANY_ID = 'BSI',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = '00',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'LOC-E-00-2024110001',
		@CUSER_ID = 'hmc',
		@CNEW_STATUS = '10'
EXEC [RSP_PM_UPDATE_AGREEMENT_TRANS_STS]
		@CCOMPANY_ID = 'BSI',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = '00',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'LOC-E-00-2024100005',
		@CUSER_ID = 'hmc',
		@CNEW_STATUS = '10'


EXEC [RSP_PM_UPDATE_AGREEMENT_TRANS_STS]
		@CCOMPANY_ID = 'BSI',
		@CPROPERTY_ID = 'ashmd',
		@CDEPT_CODE = '00',
		@CTRANS_CODE = '802053',
		@CREF_NO = 'LOC-E-00-2024100007',
		@CUSER_ID = 'hmc',
		@CNEW_STATUS = '10'
rollback

exec RSP_PM_GET_AGREEMENT_UNIT_INFO_LIST 
@CCOMPANY_ID = 'BSI',
@CPROPERTY_ID ='ASHMD',
@CDEPT_CODE  = '00',
@CTRANS_CODE ='802043',
@CREF_NO = 'LOO-E-00-2025020001',
@CUSER_ID ='HMC'

@CUSER_ID
['BSI', 'ASHMD', '00', '802020', 'OAGR-202412-005', 'VFM']

exec RSP_PM_GET_AGREEMENT_UTILITIES_LIST
@CCOMPANY_ID = 'BSI',
@CPROPERTY_ID ='ASHMD',
@CDEPT_CODE  = '00',
@CTRANS_CODE ='802043',
@CREF_NO = 'LOO-E-00-2025020001',
@CUNIT_ID = 'BZ015',
@CFLOOR_ID = 'GF',
@CBUILDING_ID ='TW-1',
@CUSER_ID ='HMC'

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

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_CHARGES_UTILITY_LIST]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CCHARGE_TYPE_ID = '01',
		@CUSER_ID = 'hmc',
		@CTAXABLE_TYPE = '',
		@CACTIVE_TYPE = '',
		@CTAX_DATE = ''
SELECT	'Return Value' = @return_value
GO


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
@CCHARGES_ID ='EC04',
@CSEQ_NO ='004',
@CUSER_ID ='hmc'
LADMIN_FEE_TAX
0

RSP_PM_GET_AGREEMENT_DOC_DT

RSP_PM_MAINTAIN_AGREEMENT_DOC


RSP_GET_REPORT_TEMPLATE_LIST
@CCOMPANY_ID = 'BSI',
@CPROPERTY_ID = 'ASHMD',
@CPROGRAM_ID = 'PMT01700',
@CTEMPLATE_ID = 'Event'

delete from SAT_
CSIGN_STORAGE_ID01
cd69984796994dca9e2853b9fba36dc9