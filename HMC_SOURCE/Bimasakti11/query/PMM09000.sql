

delete from SAT_LOCKING where CUSER_ID='hmc'
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_GS_GET_BUILDING_LIST]
		@CCOMPANY_ID = 'rcd',
		@CPROPERTY_ID = 'ashmd',
		@CUSER_ID = 'hmc'

SELECT	'Return Value' = @return_value
GO

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_LIST] -- saat generat mode == a, just view mode
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO


select * from PMM_AMORTIZATION_HD

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_DT]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'LOI-U-2024100037',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

select * from GST_EMAIL_OUTBOX

--seq no masih belum ada


USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_SCH]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'PFI-2024100051',--'LOI-U-2024100027-001', --'LOI-U-2024100027-001', --\tLOI-U-2024100027-001  //RSP_PM_GET_AMORTIZATION_CHARGES
		@CUSER_ID = 'HMC'
SELECT	'Return Value' = @return_value
GO


EXEC RSP_TX_GET_PERIOD_DT_LIST 'rcd', '2024', 's'
EXEC RSP_GS_GET_TAX_NEXT_PERIOD  'rcd', 's'


RSP_PM_GET_AMORTIZATION_DT

RSP_PM_GET_USER_PARAM_DETAIL 'bsi','aoc','152'

RSP_PM_GET_AMORTIZATION_DT
RSP_PM_UPDATE_AMORTIZATION_STATUS
RSP_PM_UPDATE_AMORTIZATION_STATUS

SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', 'rcd', '_BS_JOURNAL_GRP_TYPE', '', 'en')
 SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', 'rcd', '_BS_UNIT_CHARGES_TYPE', '01,02,04,05,06,07,08', 'en')

RSP_PM_MAINTAIN_AMORTIZATION 

EXEC RSP_GS_GET_TAX_NEXT_PERIOD 'rcd', 's'
EXEC RSP_TX_GET_PERIOD_DT_LIST 'rcd', '2024', 's'

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_LIST] -- saat generat mode == a, just view mode
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

-- minta buidling id, building name, cdept_name
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_SCH]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'PFI-2024100036',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

select    * from PMM_AMORTIZATION_DT 
order by DCREATE_DATE desc
where CREF_NO = 'LOI-U-2024100037'


select   * from PMM_AMORTIZATION_HD 
order by DCREATE_DATE desc

--CD1FCC8B-1C75-4FE8-8EE4-6EE7E460CE4D

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_LIST] -- saat generat mode == a, just view mode
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'O',
		@CBUILDING_ID = 'TW-1',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO



begin transaction

CREATE TABLE #AMOR_SCH(
							CSEQ_NO VARCHAR(3)
                            ,CCHARGES_TYPE_ID VARCHAR(2)
                            ,CCHARGES_ID VARCHAR(20)
                            ,CSTART_DATE VARCHAR(8)
                            ,CEND_DATE VARCHAR(8)
                            ,NAMOUNT NUMERIC(8, 2)
							)
	
	
INSERT INTO #AMOR_SCH VALUES 
('001','02','RENTAL', '20241024', '20251023', 100000);

--select * from #AMOR_SCH

EXEC	 [dbo].[RSP_PM_MAINTAIN_AMORTIZATION]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'u',
		@CBUILDING_ID = 'TW-1',
		@CTENANT_ID = 'TNT08',
		@CDEPT_CODE = '00',
		@CAGREEMENT_NO = 'LOI-U-2024100078',
		@CDESCRIPTION = 'description',
		@CTRANS_DEPT_CODE = '00',
		@CAMORTIZATION_NO = 'amortiz',
		@LCUT_OF_PRD = false,
		@CCUT_OF_PRD = '',
		@CSTART_DATE = '20241024',
		@CEND_DATE = '20241124',
		@CCHARGE_ACCRUAL = 'Rental2',
		@CACTION = 'ADD',
		@CUSER_ID = 'HMC'


USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_LIST] -- saat generat mode == a, just view mode
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'O',
		@CBUILDING_ID = 'TW-1',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_DT]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'amor-013',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO
rollback

delete from SAT_LOCKING where CUSER_ID='hmc'

RSP_PM_GET_AMORTIZATION_DT
RSP_PM_UPDATE_AMORTIZATION_STATUS

RSP_PM_UPDATE_AMORTIZATION_STATUS 

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_CHARGES]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'amortiy990',--'LOI-U-2024100027-001', --'LOI-U-2024100027-001', --\tLOI-U-2024100027-001  //RSP_PM_GET_AMORTIZATION_CHARGES
		@CUSER_ID = 'HMC'
SELECT	'Return Value' = @return_value
GO



--MENAMBAHKAN DATA BARU
begin transaction

CREATE TABLE #AMOR_SCH(
							CSEQ_NO VARCHAR(3)
                            ,CCHARGES_TYPE_ID VARCHAR(2)
                            ,CCHARGES_ID VARCHAR(20)
                            ,CSTART_DATE VARCHAR(8)
                            ,CEND_DATE VARCHAR(8)
                            ,NAMOUNT NUMERIC(18, 2)
							)
	
	
INSERT INTO #AMOR_SCH VALUES 
('001','02','DP Rent', '20241025', '20251024', 10000002)

EXEC	 [dbo].[RSP_PM_MAINTAIN_AMORTIZATION]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'u',
		@CBUILDING_ID = 'TW-1',
		@CTENANT_ID = 'TNT22',
		@CDEPT_CODE = '00',
		@CAGREEMENT_NO = 'LOI-U-2024100084',
		@CDESCRIPTION = 'dessc 6 Nov',
		@CTRANS_DEPT_CODE = '00',
		@CAMORTIZATION_NO = 'amr-07-1124',
		@LCUT_OF_PRD = false,
		@CCUT_OF_PRD = '',
		@CSTART_DATE = '20241025',
		@CEND_DATE = '20251025',
		@CCHARGE_ACCRUAL = 'SC',
		@CACTION = 'NEW',
		@CUSER_ID = 'HMC'


USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_DT]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'amr-07-1124',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO 

--AMOUNT PADA TAB ENTRY = 10000002
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_CHARGES]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'amr-07-1124',
		@CUSER_ID = 'HMC'
SELECT	'Return Value' = @return_value
GO

----NILAI NAMOUNT PADA TAB 1
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_SCH]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'amr-07-1124',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

-- EDIT NILAI AMOUNT PADA TAB 1 
drop table #AMOR_SCH;
CREATE TABLE #AMOR_SCH(
							CSEQ_NO VARCHAR(3)
                            ,CCHARGES_TYPE_ID VARCHAR(2)
                            ,CCHARGES_ID VARCHAR(20)
                            ,CSTART_DATE VARCHAR(8)
                            ,CEND_DATE VARCHAR(8)
                            ,NAMOUNT NUMERIC(18, 2)
							)	
INSERT INTO #AMOR_SCH VALUES 
('001','02','DP Rent', '20241025', '20251024', 222000)
select * from #AMOR_SCH


EXEC	 [dbo].[RSP_PM_MAINTAIN_AMORTIZATION]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'u',
		@CBUILDING_ID = 'TW-1',
		@CTENANT_ID = 'TNT22',
		@CDEPT_CODE = '00',
		@CAGREEMENT_NO = 'LOI-U-2024100084',
		@CDESCRIPTION = 'dessc 7 Nov',
		@CTRANS_DEPT_CODE = '00',
		@CAMORTIZATION_NO = 'amr-07-1124',
		@LCUT_OF_PRD = false,
		@CCUT_OF_PRD = '',
		@CSTART_DATE = '20241025',
		@CEND_DATE = '20251025',
		@CCHARGE_ACCRUAL = 'SC',
		@CACTION = 'EDIT',
		@CUSER_ID = 'HMC'



--NILAI AMOUNT PADA TAB ENTRY SUDAH BERUBAH MENJADI 222.0000

USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_CHARGES]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'amr-07-1124', 
		@CUSER_ID = 'HMC'
SELECT	'Return Value' = @return_value
GO

--KETIKA DI CEK DI GRID BAWAH TAB 1, AMOUNT TIDAK TERPENGARUH
USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_SCH]
		@CCOMPANY_ID = 'bsi',RSP_PM_UPDATE_AMORTIZATION_STATUS
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'amr-07-1124',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO

rollback

begin transaction


USE [BIMASAKTI_11]
GO
DECLARE	@return_value int
EXEC	@return_value = [dbo].[RSP_PM_GET_AMORTIZATION_LIST] -- saat generat mode == a, just view mode
		@CCOMPANY_ID = 'BSI',
		@CPROPERTY_ID = 'ASHMD',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CUSER_ID = 'hmc'
SELECT	'Return Value' = @return_value
GO


EXEC	[RSP_PM_UPDATE_AMORTIZATION_STATUS]
		@CCOMPANY_ID = 'bsi',
		@CPROPERTY_ID = 'ashmd',
		@CUNIT_OPTION = 'U',
		@CBUILDING_ID = 'TW-1',
		@CTRANS_TYPE = 'LOI',
		@CREF_NO = 'PFI-2025080001',
		@CACTION = 'Close',
		@CUSER_ID = 'hmc'

rollback
