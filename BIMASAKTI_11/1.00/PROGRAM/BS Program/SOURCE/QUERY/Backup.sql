delete
from SAT_LOCKING
where cuser_id = 'rhc'

begin
    transaction
IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null)
    BEGIN
        select SP_Name    = cast('' as varchar(50)),
               Err_Code   = cast('' as varchar(20)),
               Err_Detail = cast('' as nvarchar(max))
        into #__SP_ERR_Table
        where 0 = 1
    end
else
    begin
        truncate table #__SP_ERR_TABLE
    end

begin try
    Begin
        Transaction
        exec RSP_GS_MAINTAIN_APPROVAL 'BSI', '120010', '00', 'AP-202509000005-RTN', '', '', 'RAM', '01'
    rollback
end try
begin catch
    select *
    from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

rollback

exec RSP_GS_GET_PROPERTY_DETAIL 'bsi', 'bsi'

EXEC RSP_PM_LOOKUP_INVOICE_GROUP
     @CACTIVE_TYPE = 'ACTIVE',
     @CCOMPANY_ID = 'BSI',
     @CINVGRP_CODE = '',
     @CLANGUAGE_ID = 'en',
     @CPROPERTY_ID = 'SKMJ'
select *
from PMM_INVGRP
where CPROPERTY_ID = 'SKMJ'
EXEC RSP_PM_GET_AGGREMENTNO_LIST
     @CAGGR_STTS ='',
     @CBUILDING_ID ='', @CCOMPANY_ID= 'BSI', @CDEPT_CODE= '00',
     @CLANG_ID= 'en', @CPROPERTY_ID= 'ASHMD', @CREF_NO= '', @CTENANT_ID= 'TNT19', @CTRANS_CODE= '910020',
     @CTRANS_STATUS= '', @CUSER_ID='NNM'
exec RSP_GS_GET_APPR_TRX_LIST @CCOMPANY_ID='BSI', @CUSER_LOGIN_ID='ram', @CTRANS_TYPE='I'
EXEC RSP_ICR00100_GET_REPORT 'BSI', 'ASHMD', 'Period', '202507', '', '', 'WR01', '', false, 'PROD', 'BH001', 'BH001',
     '', 'en'

EXEC RSP_ICR00100_GET_REPORT 'BSI', 'ASHMD', 'Period', '202507', '', '', 'WR01', '', false, 'PROD', 'BH001', 'BH001',
     '', 'en'

BEGIN
    TRANSACTION
ROLLBACK

exec RSP_PM_GET_AGREEMENT_LIST
     @CCOMPANY_ID = 'BSI',
     @CPROGRAM_ID = 'PMT05500',
     @CPROPERTY_ID = 'PPFSR',
     @CTRANS_CODE = '802030,802061,802020',
     @CTRANS_STATUS_LIST = '30,80',
     @CUSER_ID = 'RHC';

exec RSP_PM_GET_AGREEMENT_DETAIL
     @CCOMPANY_ID = 'PPFSR',
     @CDEPT_CODE = 'FRO',
     @CPROPERTY_ID = 'PPFSR',
     @CREF_NO = 'OWMG/20241200011',
     @CTRANS_CODE = '802020',
     @CUSER_ID = 'VFM'

exec RSP_PM_GET_DEPOSIT_INFO
     @CCOMPANY_ID = 'BSI',
     @CDEPT_CODE = '01',
     @CPROPERTY_ID = 'ASHMD',
     @CREF_NO = 'OAGR-202507-004',
     @CSEQ_NO = '001',
     @CTRANS_CODE = '802020',
     @CUSER_ID = 'VFM'

exec RSP_PM_GET_DEPOSIT_LIST
     @CCHARGE_MODE = '01',
     @CCOMPANY_ID = 'BSI',
     @CDEPT_CODE = '00',
     @CPROPERTY_ID = 'ASHMD',
     @CREF_NO = 'AGRU-2024090007-AU',
     @CTRANS_CODE = '802030',
     @CUSER_ID = 'RHC',
     @LINVOICED = true

exec RSP_PM_GET_DEPOSIT_LIST
     @CCHARGE_MODE = '02',
     @CCOMPANY_ID = 'PPFSR',
     @CDEPT_CODE = 'FRO',
     @CPROPERTY_ID = 'PPFSR',
     @CREF_NO = 'OWMG/20241200011',
     @CTRANS_CODE = '802020',
     @CUSER_ID = 'VFM',
     @LINVOICED = true


exec RSP_PM_GET_AGREEMENT_LIST
     @CCOMPANY_ID = 'BSI',
     @CPROGRAM_ID = 'PMT05500',
     @CPROPERTY_ID = 'ASHMD',
     @CTRANS_CODE = '802030,802061,802020',
     @CTRANS_STATUS_LIST = '80,30',
     @CUSER_ID = 'RHC'

exec RSP_PM_GET_AGREEMENT_LIST
     @CCOMPANY_ID = 'BSI',
     @CPROGRAM_ID = 'PMT05500',
     @CPROPERTY_ID = 'ASHMD',
     @CTRANS_CODE = '802030,802061',
     @CTRANS_STATUS_LIST = '80',
     @CUSER_ID = 'RHC';
select *
from SAM_COMPANIES
where CCOMPANY_ID = 'BSI'

SELECT dbo.RFN_GET_COMPANY_LOGO('BSI') as CLOGO exec RSP_GS_GET_APPR_TRX_LIST 'BSI', 'NNM', 'I'
exec RSP_GS_GET_APPR_TRX_LIST 'BSI', 'RAM', 'I'
select *
from GSM_TRANSACTION_CODE
where CTRANS_CODE = '802061'
select *
from GST_APPROVAL_I
EXEC RSP_PMR02000_GET_REPORT
     'BSI',
     'ASHMD',
     'RAM',
     'S',
     'L',
     'C',
     'TNT47',
     'TNT80',
     '202410',
     'C',
     '20241031',
     '',
     '',
     '',
     '',
     'C',
     'en'

exec RSP_PMR02000_GET_REPORT
     'BSI',
     'ASHMD',
     'RHC',
     'S',
     'B',
     'C',
     'TNT47',
     'TNT50',
     '202410',
     'C',
     '20241031',
     '',
     '',
     '',
     '',
     'C',
     'en'

BEGIN
    TRANSACTION
CREATE TABLE #PRICING
(
    SEQ                INT,
    CVALID_INTERNAL_ID VARCHAR(36),
    CCHARGES_TYPE      VARCHAR(2),
    CCHARGES_ID        VARCHAR(20),
    CPRICE_MODE        VARCHAR(2),
    NNORMAL_PRICE      NUMERIC(18, 2),
    NBOTTOM_PRICE      NUMERIC(18, 2),
    LOVERWRITE         BIT,
    CINVOICE_PERIOD    VARCHAR(2)
)
INSERT INTO #PRICING
VALUES (1, '20280611001', '02', 'RENTAL', '01', 7000, 5500, 1, '01'),
       (2, '20280611001', '05', 'SC', '02', 70000, 100000, 1, '03')
EXEC RSP_PM_MAINTAIN_PRICING 'BSI', 'ASHMD', '02', 'STD', '20280611', true, 'EDIT', 'ghc'

drop table #PRICING

SELECT *
FROM GST_UPLOAD_ERROR_STATUS

SELECT *
FROM GST_UPLOAD_PROCESS_STATUS
ROLLBACK

EXEC RSP_PM_GET_UTILITY_CUTOFF_LIST_EC 'BSI', 'ASHMD', 'TW-1', '01', 'FLR01', '202408', false, 'RHC'


EXEC RSP_GL_REP_ACTIVITY_BY_TRANS_CODE 'BSI', 'RHC', 'S', '000030', '02', '02', 'P', '20250101', '20250601', 'R', 'L',
     'en'

EXEC RSP_GL_REP_ACTIVITY_BY_TRANS_CODE 'BSI', 'RHC', 'D', '000030', '02', '02', 'P', '20250101', '20250601', 'R', 'L',
     'en'

exec RSP_PM_PRINT_AGREEMENT 'BSI', 'ASHMD', '00', '802061', 'LOI-U-2024090007', 'RHC'

EXEC RSP_GL_GET_RECURRING_JRN 'BSI', 'RHC', '6904D65A-B473-4D0B-B6CC-D41FEA41DBBD', 'en'
EXEC RSP_GS_GET_PERIOD_YEAR_RANGE 'BSI', '', ''
EXEC RSP_GS_GET_PROPERTY_LIST 'BSI', 'RHC'

exec RSP_ICR00100_GET_REPORT '', '', '', '', '', '', '', '', '', '', '', '', '', ''

EXEC RSP_PM_GET_UTILITY_CUTOFF_LIST_EC 'BSI', 'ASHMD', 'TW-2', '01', 'FLR04', '202505', false, 'VFM';

select *
from PMT_BILLING_STATEMENT
EXEC RSP_GS_GET_PROPERTY_LIST
     'BSI',
     'RHC',
     '0'

EXEC RSP_GS_GET_PERIOD_DT_LIST 'BSI', '2025'
EXEC RSP_GET_REPORT_TEMPLATE_LIST 'BSI', 'ashmd', 'PMT01300', ''


EXEC RSP_GS_GET_BUILDING_FLOOR_LIST 'BSI', 'ASHMD', 'OFT', 'RAM', 0, 'PMT06000', '', ''

SELECT TOP 1 1 as LIS_PRIMARY
FROM GSM_COMPANY (NOLOCK)
WHERE CCOMPANY_ID <> 'RCD'
  AND LPRIMARY_ACCOUNT = 1
SELECT TOP 1 1 as LIS_PRIMARY
FROM GSM_COMPANY (NOLOCK)
WHERE CCOMPANY_ID <> 'RCD'
  AND LPRIMARY_ACCOUNT = 1

select *
from GSM_COMPANY
where CCOMPANY_ID = 'BSI'

SELECT TOP 1 1 as LIS_PRIMARY
FROM GSM_COMPANY (NOLOCK)
WHERE CCOMPANY_ID <> 'BSI'
  AND LPRIMARY_ACCOUNT = 1
EXEC RSP_GS_SEARCH_SUPPLIER_LOOKUP_LIST 'BSI', '', 'en', '', ''

EXEC RSP_PM_GET_SYSTEM_PARAMETER_DETAIL 'BSI', 'skmj', 'RHC'

exec RSP_GET_REPORT_TEMPLATE_LIST 'BSI', 'ashmd', 'PMT01300', ''

select *
from SAM_COMPANIES
where CCOMPANY_ID = 'BSI'


-- save pricing 
BEGIN
    TRANSACTION


ROLLBACK
-- CREATE TABLE #PRICING
-- (
--     SEQ                INT,
--     CVALID_INTERNAL_ID VARCHAR(36),
--     CCHARGES_TYPE      VARCHAR(2),
--     CCHARGES_ID        VARCHAR(20),
--     CPRICE_MODE        VARCHAR(2),
--     NNORMAL_PRICE      NUMERIC(18, 2),
--     NBOTTOM_PRICE      NUMERIC(18, 2),
--     LOVERWRITE         BIT,
--     CINVOICE_PERIOD    VARCHAR(2)
-- )
-- INSERT INTO #PRICING
-- VALUES (1, '20280611001', '02', 'RENTAL', '01', 7000, 5500, 1, '01'),
--        (2, '20280611001', '05', 'SC', '02', 70000, 100000, 1, '03')
-- EXEC RSP_PM_MAINTAIN_PRICING 'BSI', 'ASHMD', '02', 'STD', '20280611', true, 'EDIT', 'ghc'
-- 
-- 
-- SELECT *
-- FROM GST_UPLOAD_ERROR_STATUS
-- where CUSER_ID = 'ram'
-- 
-- SELECT *
-- FROM GST_UPLOAD_PROCESS_STATUS
-- where CUSER_ID = 'ram'
ROLLBACK

EXEC RSP_PM_MAINTAIN_SYSTEM_PARAMETER 'BSI',
     'ASHMD',
     '202401',
     '202312',
     '202409',
     'True',
     'False',
     'I',
     'R',
     'MR',
     'TR',
     'ADD',
     '5.00',
     'DED',
     '5.00',
     '202408',
     '202408',
     '202408',
     'True',
     'True',
     'True',
     '',
     '',
     '',
     'True',
     '5',
     '5',
     'CARE-GHC',
     'False',
     'True',
     'True',
     'EDIT',
     'RHC'

EXEC RSP_GS_GET_BUILDING_UNIT_LIST 'BSI', 'ASHMD', 'Mall', 'F01', 'RHC', false, '', '01,02', '', '802061',
     'LOI-U-2025060003', '02,03'

exec RSP_IC_VALIDATE_SOFTCLOSE_PRD 'bsi', '2024', '01', 'rhc'
exec RSP_IC_GET_SYSTEM_PARAM 'BSI', 'ASHMD', 'en'
update ICM_SYSTEM_PARAM
set LSOFT_CLOSING_FLAG = 0
where LSOFT_CLOSING_FLAG = 1
exec RSP_PM_GET_TENANT_VA_LIST 'bsi', 'ashmd', 'TNT80', 'en'

exec RSP_PM_GET_OVT_SUMINV_LIST 'BSI', 'ASHMD', '202409', 'LOI-U-2024090009', '00', '802061', 'OVT-2024090016INVOICE',
     '00', 'EDIT'
exec RSP_PM_GET_OVT_INVOICE_DETAIL 'BSI', '00', '802410', 'OVT-2024090016INVOICE',
     '35FE3811-E45F-4386-80ED-8E9247499757', 'en'

exec RSP_PM_GET_BILLING_STATEMENT 'bsi', 'ashmd', 'TNT05', 'TNT05', '202410'

exec RSP_IC_SOFT_CLOSE_PERIOD
     'bsi',
     'ashmd',
     '2024',
     '01',
     'rhc'

EXEC RSP_ICR00100_GET_REPORT 'BSI', 'en', '', '', '', '', '', '', '', '', ''

EXEC RSP_APR00100_GET_REPORT 'BSI', 'ASHMD', 'SP-AP', 'SP02', '', '', '1', '20250618', '', '2', '1', '', '', '',
     'False', '', '', 'en'


IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null)
    BEGIN
        select SP_Name    = cast('' as varchar(50)),
               Err_Code   = cast('' as varchar(20)),
               Err_Detail = cast('' as nvarchar(max))
        into #__SP_ERR_Table
        where 0 = 1
    end
else
    begin
        truncate table #__SP_ERR_TABLE
    end

begin try
    --     Begin Transaction

    CREATE TABLE #PRICING
    (
        SEQ                INT,
        CVALID_INTERNAL_ID VARCHAR(36),
        CCHARGES_TYPE      VARCHAR(2),
        CCHARGES_ID        VARCHAR(20),
        CPRICE_MODE        VARCHAR(2),
        NNORMAL_PRICE      NUMERIC(18, 2),
        NBOTTOM_PRICE      NUMERIC(18, 2),
        LOVERWRITE         BIT,
        CINVOICE_PERIOD    VARCHAR(2)
    )
    INSERT INTO #PRICING
    VALUES (1, '20280611001', '02', 'RENTAL', '01', 7000, 5500, 1, '01'),
           (2, '20280611001', '05', 'SC', '01', 70000, 100000, 1, '03')
    EXEC RSP_PM_MAINTAIN_PRICING 'BSI', 'ASHMD', '02', 'STD', '20280611', true, 'EDIT', 'ghc'

    EXEC RSP_PM_GET_PRICING_LIST 'BSI', 'ASHMD', 'STD', '02', 'True', '02', '20280611', '20280611001', 'ghc'

    drop table #PRICING
--     rollback
end try
begin catch
    select *
    from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

---

Begin
    Transaction

EXEC RSP_GS_VALIDATE_COMP_PARAM 'bsi', 'RH'

CREATE TABLE #PRICING
(
    SEQ                INT,
    CVALID_INTERNAL_ID VARCHAR(36),
    CCHARGES_TYPE      VARCHAR(2),
    CCHARGES_ID        VARCHAR(20),
    CPRICE_MODE        VARCHAR(2),
    NNORMAL_PRICE      NUMERIC(18, 2),
    NBOTTOM_PRICE      NUMERIC(18, 2),
    LOVERWRITE         BIT,
    CINVOICE_PERIOD    VARCHAR(2)
)
INSERT INTO #PRICING
VALUES (1, '20280611001', '02', 'RENTAL', '01', 7000, 5500, 1, '01'),
       (2, '20280611001', '05', 'SC', '02', 70000, 100000, 1, '03')
EXEC RSP_PM_MAINTAIN_PRICING 'BSI', 'ASHMD', '02', 'STD', '20280611', true, 'EDIT', 'ghc'
DROP TABLE #PRICING

SELECT *
FROM PMM_PRICING
WHERE CVALID_INTERNAL_ID = '20280611001'
SELECT *
FROM dbo.PMT_AGREEMENT_CHARGE
WHERE CPRICE_VALID_INT_ID = '20280611001'
SELECT *
FROM PMM_PRICING_DT
WHERE CVALID_INTERNAL_ID = '20280611001'
rollback

exec RSP_PM_GET_BILLING_STATEMENT 'BSI', 'ASHMD', 'TNT05', 'TNT05', '202410'

exec RSP_IC_GET_ADJUSTMENT_LIST 'BSI', 'ASHMD', 'VFM'

EXEC RSP_IC_GET_RECALCULATE_PRODUCT_LIST 'BSI', 'ASHMD', 'F', 'VFM'
EXEC RSP_GS_GET_UNIT_TYPE_CTG_LIST 'BSI', 'ashmd', 'VFM'
exec RSP_ICR00100_GET_REPORT
     @CCOMPANY_ID = 'BSI',
     @CPROPERTY_ID = 'ASHMD',
     @CDATE_FILTER = 'PERIOD/DATE',
     @CPERIOD = '202410',
     @CFROM_DATE = '20241001',
     @CTO_DATE = '20241031',
     @CWAREHOUSE_CODE = 'TNT47',
     @CDEPT_CODE = 'TNT80',
     @LINC_FUTURE_TRANSACTION = true,
     @CFILTER_BY = 'PROD/CATEGORY/JOURNAL',
     @CFROM_PROD_ID = 'TNT47',
     @CTO_PROD_ID = 'TNT80',
     @CFILTER_DATA = 'SELECTED CATEGORY/JOURNAL',
     @CLANG_ID = 'EN'


EXEC RSP_ICR00100_GET_REPORT
     'BSI',
     'ASHMD',
     'Period',
     '202507',
     '',
     '',
     '',
     '',
     false,
     'PROD',
     'BH001',
     'OLL01',
     '',
     'en'

EXEC RSP_IC_GET_STOCK_TAKE_ACTIVITY_REPORT 'BSI', 'APART_A', '', '', 'QTY', '', '', 'en'

EXEC RSP_IC_GET_STOCK_TAKE_ACTIVITY_REPORT 'BSI', 'ASHMD', '00', '', 'QTY', '', '', 'en'

EXEC RSP_IC_GET_STOCK_TAKE_ACTIVITY_REPORT 'BSI', 'APART_A', '', '202501', 'QTY', '', '', 'en'

EXEC RSP_PM_GET_AGREEMENT_BY_UNIT 'BSI', 'ASHMD', 'TW-1', 'U-GF-22', 'GF', false, '', 'RHC'

EXEC RSP_PM_GET_AGREEMENT_BY_UNIT
     @CCOMPANY_ID ='BSI',
     @CPROPERTY_ID = 'ASHMD',
     @CBUILDING_ID = 'TW-1',
     @CUNIT_ID = 'U-GF-22',
     @CFLOOR_ID = 'GF',
     @LOTHER_UNIT = false,
     @CUSER_ID = 'RHC'

EXEC RSP_IC_GET_SYSTEM_PARAM 'BSI', 'ASHMD', 'en'
select *
from ICM_SYSTEM_PARAM
where LSOFT_CLOSING_FLAG = 1
set LSOFT_CLOSING_FLAG = 0


begin
    transaction
exec RSP_IC_VALIDATE_SOFTCLOSE_PRD
EXEC RSP_IC_SOFT_CLOSE_PERIOD 'bsi', 'ashmd', '2025', '03', 'rhc'
rollback

EXEC RSP_IC_GET_RECALCULATE_PRODUCT_LIST 'BSI', 'ASHMD', 'F', 'RHC'

EXEC RSP_ICR00100_GET_REPORT 'BSI', 'ASHMD', 'Period', '202507', '', '', 'WR01', '', false, 'PROD', 'BH001', 'BH001',
     '', 'en'
exec RSP_GS_GET_APPR_TRX_LIST
     @CCOMPANY_ID='BSI',
     @CTRANS_TYPE='I',
     @CUSER_LOGIN_ID='NNM'

EXEC RSP_IC_GET_INVENTORY_REQ_DETAIL
     @CCOMPANY_ID='BSI',
     @CPROPERTY_ID='',
     @CDEPT_CODE='00',
     @CALLOC_ID='',
     @CREF_NO='IR-2025070008-00',
     @CUSER_ID='NNM',
     @CTRANS_CODE='500010'

exec RSP_PM_GET_DEPOSIT_LIST
     @CBUILDING_ID = 'TW-1',
     @CCHARGE_MODE = '01',
     @CCOMPANY_ID = 'BSI',
     @CDEPT_CODE = '01',
     @CFLOOR_ID = 'GF',
     @CPROPERTY_ID = 'ASHMD',
     @CREF_NO = 'OAGR-202507-004',
     @CTRANS_CODE = '802020',
     @CUNIT_ID = 'U-GF-26',
     @CUSER_ID = 'VFM',
     @LINVOICED = true
EXEC RSP_PM_GET_UTILITY_USAGE_LIST_WG
     @CCOMPANY_ID = 'BSI',
     @CPROPERTY_ID = 'ASHMD',
     @CPERIOD = '202501',
     @CFROM_TENANT = 'BLG01',
     @CTO_TENANT = 'TntRPT01',
     @CREPORT_TYPE = 'D',
     @CSERVICE_TYPE = 'UN',
     @LPRINT = true,
     @CUSER_ID = 'RHC'

EXEC RSP_PM_GET_UTILITY_USAGE_LIST_EC 'BSI', 'ASHMD', 'TW-1', '01', '', '', '202508', false, '202407', '20250801',
     '20250831', false, 'RHC'

begin
    transaction
CREATE TABLE #UTILITY_USAGE_EC
(
    NO              int,
    CCOMPANY_ID     varchar(8),
    CPROPERTY_ID    varchar(20),
    CDEPT_CODE      varchar(20),
    CTRANS_CODE     varchar(10),
    CREF_NO         varchar(30),
    CUTILITY_TYPE   varchar(2),
    CUNIT_ID        varchar(20),
    CFLOOR_ID       varchar(20),
    CBUILDING_ID    varchar(20),
    CCHARGES_TYPE   varchar(2),
    CCHARGES_ID     varchar(20),
    CSEQ_NO         varchar(3),
    CINV_PRD        varchar(6),
    CUTILITY_PRD    varchar(6),
    CSTART_DATE     varchar(8),
    CEND_DATE       varchar(8),
    CMETER_NO       varchar(50),
    CPHOTO_REC_ID_1 varchar(50),
    CPHOTO_REC_ID_2 varchar(50),
    CPHOTO_REC_ID_3 varchar(50),
    NBLOCK1_START   numeric(16, 2),
    NBLOCK2_START   numeric(16, 2),
    NBLOCK1_END     numeric(16, 2),
    NBLOCK2_END     numeric(16, 2),
    NBEBAN_BERSAMA  numeric(16, 2)
);

INSERT INTO #UTILITY_USAGE_EC
VALUES (2,
        'BSI',
        'ASHMD',
        '01',
        '802020',
        'OAGR-202505-001',
        '01',
        'U-F6-03',
        'FLR06',
        'TW-1',
        '01',
        'EC05',
        '001',
        '202506',
        '202505',
        '20250501',
        '20250624',
        'ELE-F6-03',
        '',
        '',
        '',
        1.00,
        0.00,
        4.0,
        0.00,
        0.00);

EXEC RSP_PM_UPLOAD_UTILITY_USAGE_EC 'BSI', 'ASHMD', '', 'RHC', 'RHC';
select *
from SAM_COMPANIES
where CCOMPANY_ID = 'BSI';
DROP TABLE #UTILITY_USAGE_EC

SELECT *
FROM GST_UPLOAD_ERROR_STATUS
WHERE CKEY_GUID = 'RHC'

SELECT *
FROM GST_UPLOAD_PROCESS_STATUS
WHERE CKEY_GUID = 'RHC'
ROLLBACK

begin
    transaction
EXEC RSP_PM_PMA00300 'BSI', 'ASHMD', '202508', 'TNT14', 'TNT14', 'S', '', true, 'RHC'
EXEC RSP_PM_PMA00300 'BSI', 'ASHMD', '202508', 'TNT14', 'TNT14', 'D', 'UN', true, 'RHC'
EXEC RSP_PM_PMA00300 'BSI', 'ASHMD', '202508', 'TNT14', 'TNT14', 'D', 'UT', true, 'RHC'
rollback
EXEC RSP_PM_SAVE_BILLING_STATEMENT @CCOMPANY_ID = 'BSI', @CPROPERTY_ID = 'ASHMD', @CTENANT_ID = 'TNT14',
     @CLOI_AGRMT_REC_ID = '', @CREF_PRD = '202508', @CREF_DATE = '20250806', @CDUE_DATE = '20250814',
     @CSTORAGE_ID = '75f1f6be45d743e98d4fd7457ad406cc', @CUSER_ID = 'RHC'

EXEC RSP_PM_GET_UTILITY_INFO 'BSI', 'ASHMD', '01', '802020', 'OAGR-202507-010', 'U-GF-31', 'GF', 'TW-1', '04', 'GAS',
     '004', '202508', 'VFM'
EXEC RSP_GS_GET_MESSAGE_LIST @CCOMPANY_ID = 'BSI', @CMESSAGE_TYPE = '03', @CUSER_ID = 'RHC'


exec RSP_PM_GET_TENANT_VA_LIST
exec RSP_GS_GET_EMAIL_TEMPLATE 'BSI', 'BILLING_STATEMENT_DISTRIBUTE'

begin
    transaction
create table #UTILITY_USAGE_WG
(
    NO              int,
    CCOMPANY_ID     varchar(8),
    CPROPERTY_ID    varchar(20),
    CDEPT_CODE      varchar(20),
    CTRANS_CODE     varchar(10),
    CREF_NO         varchar(30),
    CUTILITY_TYPE   varchar(2),
    CUNIT_ID        varchar(20),
    CFLOOR_ID       varchar(20),
    CBUILDING_ID    varchar(20),
    CCHARGES_TYPE   varchar(2),
    CCHARGES_ID     varchar(20),
    CSEQ_NO         varchar(3),
    CINV_PRD        varchar(6),
    CUTILITY_PRD    varchar(6),
    CSTART_DATE     varchar(8),
    CEND_DATE       varchar(8),
    CMETER_NO       varchar(50),
    CPHOTO_REC_ID_1 varchar(50),
    CPHOTO_REC_ID_2 varchar(50),
    CPHOTO_REC_ID_3 varchar(50),
    NMETER_START    numeric(16, 2),
    NMETER_END      numeric(16, 2)
);

insert #UTILITY_USAGE_WG
values (1,
        'BSI',
        'ASHMD',
        '01',
        '',
        'OAGR-202505-002',
        '03',
        'U-F6-04',
        'Flr06',
        'TW-1',
        '03',
        'WTR01',
        '001',
        '202506',
        '202505',
        '20250526',
        '20250624',
        'WTR-F6-04',
        '',
        '',
        '',
        2.00,
        5.00);

EXEC RSP_PM_UPLOAD_UTILITY_USAGE_WG 'BSI', 'ASHMD', '', 'RHC', 'RHC';
EXEC RSP_GET_REPORT_TEMPLATE_LIST @CCOMPANY_ID = 'BSI', @CPROGRAM_ID = null, @CPROPERTY_ID = 'ASHMD',
     @CTEMPLATE_ID = null
select *
from GST_UPLOAD_ERROR_STATUS
where CKEY_GUID = 'RHC'
select *
from GST_UPLOAD_PROCESS_STATUS
where CKEY_GUID = 'RHC'

drop table #UTILITY_USAGE_WG
ROLLBACK
EXEC RSP_PM_GET_UTILITY_INFO 'BSI', 'ASHMD', '01', '802020', 'LOI-U-2025070015', 'U-GF-29', 'GF', 'TW-1', '03', 'WTR01',
     '002', '202508', 'VFM'
EXEC RSP_PM_GET_UTILITY_INFO_RATE_WG 'BSI', 'ASHMD', '04', 'GAS', 'RHC', '20250725'
EXEC RSP_PM_GET_RATE_WG_LIST 'BSI', 'ASHMD', '03', 'WTR01', 'VFM', '20250625'
EXEC RSP_PM_GET_UTILITY_INFO_RATE_WG 'BSI', 'ASHMD', '03', 'WTR01', 'VFM', '20250625'
exec RSP_PM_CLOSE_UTILITY_METER_NO


begin
    transaction
CREATE TABLE #UNDO_UTILITY_USAGE
(
    NO            INT,
    CCOMPANY_ID   varchar(8),
    CPROPERTY_ID  VARCHAR(20),
    CDEPT_CODE    VARCHAR(20),
    CTRANS_CODE   VARCHAR(10),
    CREF_NO       VARCHAR(30),
    CUNIT_ID      VARCHAR(20),
    CFLOOR_ID     VARCHAR(20),
    CBUILDING_ID  VARCHAR(20),
    CCHARGES_TYPE VARCHAR(2),
    CCHARGES_ID   VARCHAR(20),
    CSEQ_NO       VARCHAR(3),
    CINV_PRD      VARCHAR(6),
    CMETER_NO     VARCHAR(50)
);
INSERT INTO #UNDO_UTILITY_USAGE
VALUES (1, 'BSI', 'ASHMD', '01', '802020', 'OAGR-202507-008', 'OFR-GR-02', 'GF', 'OFT', '01', 'EC01', '001', '202508',
        'ELE-OFR-GF-02');

EXEC RSP_PM_UNDO_UTILITY_USAGE 'BSI', 'ASHMD', 'OFT', '01', '202508', 'APM', 'APM';

select *
from GST_UPLOAD_ERROR_STATUS
where CKEY_GUID = 'APM'

select *
from GST_UPLOAD_PROCESS_STATUS
where CKEY_GUID = 'APM'

drop table #UTILITY_USAGE_WG
ROLLBACK
EXEC RSP_GS_GET_PERIOD_DT_LIST 'BSI', '2025'
exec RSP_PM_LOOKUP_CANCEL_CUST_RECEIPT @CCOMPANY_ID = 'BSI', @CCUSTOMER_ID = 'TNT05', @CDEPT_CODE = '00',
     @CLANGUAGE_ID = 'en', @CLOI_AGRMT_ID = '', @CPERIOD = '202507', @CPROPERTY_ID = 'ASHMD', @CUSER_ID = 'FMC'
exec RSP_GS_GET_APPR_TRX_LIST @CCOMPANY_ID = 'BSI', @CUSER_LOGIN_ID = 'NNM', @CTRANS_TYPE = 'I'
exec RSP_PM_GET_SYSTEM_PARAM
exec RSP_PM_VALIDATE_SOFT_CLOSE
EXEC RSP_PM_GET_UTILITY_INFO 'PPFSR', 'PPFSR', 'FRO', '802020', 'OWMG/20241200022', 'SP06C', 'L06', 'T1-SP', '03',
     'W0001', '002', '202507', 'VFM'
exec RSP_GS_GET_APPR_TRX_LIST 'BSI', 'NNM', 'I'

begin
    transaction
EXEC RSP_PM_PMA00300 'BSI', 'ASHMD', '202508', 'TNT14', 'TNT14', 'S', '', true, 'RHC'
EXEC RSP_PM_PMA00300 'BSI', 'ASHMD', '202508', 'TNT14', 'TNT14', 'D', 'UN', true, 'RHC'
EXEC RSP_PM_PMA00300 'BSI', 'ASHMD', '202508', 'TNT14', 'TNT14', 'D', 'UT', true, 'RHC'
rollback

begin
    transaction
IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null)
    BEGIN
        select SP_Name    = cast('' as varchar(50)),
               Err_Code   = cast('' as varchar(20)),
               Err_Detail = cast('' as nvarchar(max))
        into #__SP_ERR_Table
        where 0 = 1
    end
else
    begin
        truncate table #__SP_ERR_TABLE
    end

begin try
    Begin
        Transaction
        EXEC RSP_PM_PMA00300 'BSI', 'ASHMD', '202505', 'TNT14', 'tnt14', 'S', '', true, 'VFM'
        EXEC RSP_PM_PMA00300 'BSI', 'ASHMD', '202505', 'TNT14', 'tnt14', 'D', 'UN', true, 'VFM'
        EXEC RSP_PM_PMA00300 'BSI', 'ASHMD', '202505', 'TNT14', 'tnt14', 'D', 'UT', true, 'VFM'
    rollback
end try
begin catch
    select *
    from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

rollback

EXEC RSP_PMR02400_GET_REPORT @CCOMPANY_ID = 'BSI', @CPROPERTY_ID = 'ASHMD', @CREPORT_OPTION = 'I', @CREPORT_TYPE = 'S',
     @CCUT_OFF_DATE = '20250822', @CFR_PERIOD = '202508', @CTO_PERIOD = '202508', @CCURRENCY_TYPE = 'T',
     @CFR_CUSTOMER = 'BLG01', @CTO_CUSTOMER = 'TNT02', @CLANGUAGE_ID = 'en'

exec RSP_PM_GET_UTILITY_INFO_RATE_WG 'BSI', 'ASHMD', '03', 'WTR01', 'RHC', '20250825'
exec RSP_IC_GET_PHYSICAL_INV_COUNT_SHEET @CCOMPANY_ID = 'BSI', @CDEPT_CODE = '00', @CFILTER_BY = 'PROD',
     @CFILTER_DATA = '', @CFROM_PROD_ID = 'AIB001', @CPROPERTY_ID = 'ASHMD', @CTO_PROD_ID = 'WH001',
     @CWAREHOUSE_ID = 'GD', @LPROD_BALANCE = false
EXEC RSP_ICR00100_GET_REPORT 'BSI', 'ASHMD', 'Period', '202506', '', '', 'WR01', '', false, 'PROD', 'AIB001', 'WH001',
     '', 'en', 'QTY'
exec RSP_PM_GET_BILLING_STATEMENT 'BSI', 'ASHMD', 'TNT14', 'TNT14', '202508'

EXEC RSP_AP_SEARCH_SUPPLIER_LIST 'BSI', 'ASHMD', '', '', 'en'
EXEC RSP_GS_MAINTAIN_DEPARTMENT @CCOMPANY_ID = 'PPFSR', @CDEPT_CODE = 'FIN', @CDEPT_NAME = 'Finance Accounting',
     @CCENTER_CODE = 'A&G', @CMANAGER_NAME = '', @CBRANCH_CODE = 'PPFSR', @LEVERYONE = 'False', @LACTIVE = 'True',
     @CEMAIL1 = 'ranzstok62@gmail.com', @CEMAIL2 = '', @CACTION = 'EDIT', @CUSER_ID = 'Admin'
EXEC RSP_APR00200_GET_REPORT @CBASED_ON = 'S', @CCOMPANY_ID = 'BSI', @CCURRENCY_TYPE = 'T', @CCUT_OFF_DATE = '20250819',
     @CFR_BASED_ON = 'SP-01', @CLANGUAGE_ID = 'en', @CPROPERTY_ID = 'ASHMD', @CREPORT_TYPE = 'S',
     @CTO_BASED_ON = 'SP-01'
exec RSP_PMR02100_GET_REPORT
     @CCOMPANY_ID = 'PPFSR',
     @CPROPERTY_ID = 'PPFSR',
     @CREPORT_TYPE = 'D',
     @CCUT_OFF_DATE = '20250131',
     @CCURRENCY_CODE = '',
     @CCURRENCY_TYPE = 'TNT05',
     @CBASED_ON = 'S',
     @CFR_BASED_ON = 'L',
     @CTO_BASED_ON = 'C',
     @CINVGRP_CODE = 'C',
     @CLANGUAGE_ID = 'en'

EXEC RSP_PMR00200_GET_REPORT
     @CCOMPANY_ID = 'PPFSR',
     @CPROPERTY_ID = 'PPFSR',
     @CFROM_DEPT_CODE = 'FRO',
     @CTO_DEPT_CODE = 'FRO',
     @CFROM_SALESMAN_ID = 'SLM001',
     @CTO_SALESMAN_ID = 'SLM001',
     @CFROM_PERIOD = '202506',
     @CTO_PERIOD = '202507',
     @CREPORT_TYPE = '2',
     @LOUTSTANDING_AGREEMENT = 'False',
     @CLANG_ID = 'en'

EXEC RSP_PMR02000_GET_REPORT
     @CCOMPANY_ID = 'BSI',
     @CPROPERTY_ID = 'ASHMD',
     @CUSER_ID = 'RHC',
     @CREPORT_TYPE = 'S',
     @CCURRENCY_TYPE = 'T',
     @CBASED_ON = 'C',
     @CFR_BASED_ON = 'BLG01',
     @CTO_BASED_ON = 'TntRPT01',
     @CPERIOD = '202507',
     @CREMAINING_BASED_ON = 'C',
     @CCUT_OFF_DATE = '20250730',
     @CFR_DEPT_CODE = '',
     @CTO_DEPT_CODE = '',
     @CTRANS_CODE = '',
     @CTENANT_CATEGORY_ID = '',
     @CSORT_BY = 'C',
     @CLANGUAGE_ID = 'en'
EXEC RSP_GS_GET_DEPT_LOOKUP_LIST 'BSI', 'VFM', ''
EXEC RSP_PMR02000_GET_REPORT
     @CCOMPANY_ID = 'PPFSR',
     @CPROPERTY_ID = 'PPFSR',
     @CUSER_ID = 'Admin',
     @CREPORT_TYPE = 'S',
     @CCURRENCY_TYPE = 'T',
     @CBASED_ON = 'C',
     @CFR_BASED_ON = 'COMB1000',
     @CTO_BASED_ON = 'U33CO001',
     @CPERIOD = '202507',
     @CREMAINING_BASED_ON = 'C',
     @CCUT_OFF_DATE = '20250730',
     @CFR_DEPT_CODE = '',
     @CTO_DEPT_CODE = '',
     @CTRANS_CODE = '',
     @CTENANT_CATEGORY_ID = '',
     @CSORT_BY = 'C',
     @CLANGUAGE_ID = 'en'

exec RSP_PM_GET_TENANT_LIST @CCOMPANY_ID = 'BSI', @CCUSTOMER_TYPE = '01', @CPROPERTY_ID = 'APT001', @CUSER_ID = 'RHC'
EXEC RSP_PM_GET_UTILITY_INFO_RATE_WG 'BSI', 'ASHMD', '03', 'WTR01', 'VFM', '20250825'
EXEC RSP_PM_GET_UTILITY_USAGE_LIST_EC 'PPFSR', 'PPFSR', 'T1-SP', '01', 'L13', '', '202508', false, '202506', '20250528',
     '20250827', false, 'VFM'
DECLARE
    @CPROPERTY_ID VARCHAR(20)
SET @CPROPERTY_ID = (SELECT TOP 1 CPROPERTY_ID
                     FROM GSM_PROPERTY (NOLOCK)
                     WHERE CCOMPANY_ID = 'bsi'
                       AND LACTIVE = 1)
select @CPROPERTY_ID

SELECT CCODE, CDESCRIPTION

FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', @CPROPERTY_ID, '_BS_MESSAGE_TYPE', '', 'en')
EXEC RSP_GS_GET_MESSAGE_LIST 'bsi', '03', 'rhc'

exec RSP_GS_GET_MESSAGE_DETAIL 'BSI', '03', 'BILLING01', 'RHC'

EXEC RSP_AP_CLOSE_PROCESS @CCOMPANY_ID = 'BSI', @CPERIOD_YEAR = '2024', @CPERIOD_MONTH = '10', @CUSER_ID = 'RHC'
EXEC RSP_AP_CLOSE_PRD_TODO_LIST @CCOMPANY_ID = 'BSI', @CPERIOD_YEAR = '2024', @CPERIOD_MONTH = '10', @CUSER_ID = 'RHC'
EXEC RSP_GS_GET_MESSAGE_LIST 'BSI', '', 'RHC'

EXEC RSP_PM_GET_UTILITY_INFO 'BSI', 'ASHMD', '01', '802020', 'OAGR-202505-002', 'U-F6-04', 'FLR06', 'TW-1', '03',
     'WTR01', '003', '202509', 'RHC'
EXEC RSP_PM_GET_UTILITY_INFO 'BSI', 'ASHMD', '01', '802020', 'OAGR-202505-002', 'U-F6-04', 'FLR06', 'TW-1', '03',
     'WTR01', '003', '202509', 'VFM'

EXEC RSP_PM_GET_SYSTEM_PARAM 'BSI', '', 'skmj'
EXEC RSP_GS_GET_PERIOD_DT_INFO 'BSI', '2025', '02'

select *
from RFT_GET_PERIOD_UTILITY('BSI', 'SKMJ', '01', '2025', '03')
EXEC RSP_PM_GET_UTILITY_USAGE_LIST_EC 'BSI', 'SKMJ', 'B001', '01', '', '', '202502', false, '202412', '20241201',
     '20250228', false, 'RHC'

EXEC RSP_IC_GET_SYSTEM_PARAM 'BSI', 'ASHMD', 'en'
update ICM_SYSTEM_PARAM
set LSOFT_CLOSING_FLAG = 0
where CPROPERTY_ID = 'ASHMD'
  and CCOMPANY_ID = 'BSI'
EXEC RSP_IC_GET_SYSTEM_PARAM 'BSI', 'ASHMD', 'en';

IF
    (OBJECT_ID('tempdb..#__SP_ERR_Table') is null)
    BEGIN
        select SP_Name    = cast('' as varchar(50)),
               Err_Code   = cast('' as varchar(20)),
               Err_Detail = cast('' as nvarchar(max))
        into #__SP_ERR_Table
        where 0 = 1
    end
else
    begin
        truncate table #__SP_ERR_TABLE
    end

begin try
    --     Begin Transaction


    EXEC RSP_IC_VALIDATE_SOFTCLOSE_PRD 'BSI', 'ASHMD', '2024', '09', 'RHC';
--     rollback
end try
begin catch
    select *
    from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

---
EXEC RSP_IC_SOFT_CLOSE_PERIOD 'BSI', 'ASHMD', '2024', '09', 'RHC';

EXEC RSP_IC_GET_RECALCULATE_PRODUCT_LIST 'BSI', 'ASHMD', 'N', 'RHC'

EXEC RSP_IC_GET_PROD_INQ_DETAIL 'BSI', 'BAUT001', '', 'LASTINFO'

EXEC RSP_ICR00100_GET_REPORT 'BSI', 'ASHMD', 'Period', '202507', '', '', 'WRB', '', false, 'PROD', 'AIB001', 'WH001',
     '', 'en', 'QTY'

EXEC RSP_APR00500_GET_REPORT 'BSI', 'ASHMD', '20250813', '', '202505', '202508', '', '', '', '', '', '', '', '', 0.0,
     0.0, 0.0, 0.0, 0, 0, 'en'