SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_PM_ACTIVE_INACTIVE_PRICING')

delete from sat_locking where cuser_id = 'ghc'
delete from SAM_USER_LOCKING where CUSER_ID='VFM'

SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', 'rcd', '_BS_INVOICE_PERIOD', '', 'en')

--get property 
EXEC RSP_GS_GET_PROPERTY_LIST 'rcd','ghc'

--get unit type ctg
EXEC RSP_GS_GET_UNIT_TYPE_CTG_LIST  'rcd','ASHMD','ghc'

--get all pricing list
EXEC RSP_PM_GET_PRICING_LIST 'rcd', 'ASHMD', 'STUDIO', '02', 0, '01', '', '', 'ghc'

--get pricing date list
EXEC RSP_PM_GET_PRICING_DATE_LIST 'RCD','ASHMD','1ROOM','02','02','ghc'

--get next pricing list
EXEC RSP_PM_GET_PRICING_LIST 'rcd', 'ASHMD', '1ROOM', '02', 0, '03', '20240713','20240711001', 'ghc'

select * from PMM_PRICING

-- save pricing 
BEGIN TRANSACTION
CREATE TABLE #PRICING(
	  ISEQ INT
	, CVALID_INTERNAL_ID	VARCHAR(36)
	, CCHARGES_TYPE			VARCHAR(2)
	, CCHARGES_ID			VARCHAR(20)
	, CPRICE_MODE			VARCHAR(2)
	, NNORMAL_PRICE			NUMERIC(18,2)
	, NBOTTOM_PRICE			NUMERIC(18,2)
	, LOVERWRITE			BIT	
	)
INSERT INTO #PRICING
VALUES 
	(1, '', '02', 'c000', '01', 100000, 100000, 0)
EXEC RSP_PM_MAINTAIN_PRICING 'rcd', 'ASHMD',  '02', 'ST', '20240726', true, 'EDIT', 'ghc'

ROLLBACK


EXEC RSP_PM_GET_PRICING_DATE_LIST 'RCD','ASHMD','1ROOM','02','02','ghc'

IF (OBJECT_ID('tempdb..#__SP_ERR_Table') is null) BEGIN
	select SP_Name=cast('' as varchar(50)), Err_Code=cast('' as varchar(20)), Err_Detail=cast('' as nvarchar(max)) into #__SP_ERR_Table where 0=1
end else begin
	truncate table #__SP_ERR_TABLE
end
begin try
	-- Jalankan script SP disini
EXEC RSP_PM_ACTIVE_INACTIVE_PRICING 'rcd', 'ASHMD', '02','1ROOM', '20240712001' ,'20240710',true, 'ghc'

end try
begin catch
	select * from #__SP_ERR_TABLE --untuk tahu error code yg di raise
end catch

EXEC RSP_PM_GET_PRICING_DATE_LIST 'RCD','ASHMD','1ROOM','02','02','ghc'


--get unit type ctg
EXEC RSP_GS_GET_UNIT_TYPE_CTG_LIST 'rcd','ASHMD','ghc'

--using list category type above
EXEC RSP_PM_GET_PRICING_DATE_LIST 'rcd','ASHMD','1ROOM','02','03','ghc'

--get history pricing list
EXEC RSP_PM_GET_PRICING_LIST 'rcd', 'ASHMD', '1ROOM', '02', 0, '03', '20241020', '20241020001', 'ghc'

--get pricing rate date list
EXEC RSP_PM_GET_PRICNG_RATE_DATE_LIST 'BSI','ASHMD','02','ghc'

--get pricing rate list 
EXEC RSP_PM_GET_PRICING_RATE_LIST 'BSI','ASHMD','02','02','','ghc'
--save pricing rate (using existing data)

CREATE TABLE #LEASE_PRICING_RATE(CCURRENCY_CODE	CHAR(3)
				, NBASE_RATE_AMOUNT	NUMERIC(18,2)
				, NCURRENCY_RATE_AMOUNT	NUMERIC(18,2)
				)
INSERT INTO #LEASE_PRICING_RATE 
VALUES 
('BND',10000,10000),
('TWD',3324.09,3324.09)
EXEC RSP_PM_MAINTAIN_PRICING_RATE 'BSI','ASHMD','02','20241105','ADD','GHC'

EXEC RSP_PM_GET_PRICING_RATE_LIST 'BSI', 'ASHMD','02', '02', '20241105', 'GHC'

SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', 'RCD', '_BS_UNIT_CHARGES_TYPE', '02, 05', 'en')

SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO('BIMASAKTI', 'RCD', '_BS_PRICE_MODE', '', 'EN')
select * from SAM_USER_LOCKING


EXEC RSP_PM_GET_AGREEMENT_CHARGES_DISC_LIST 
'BSI' --company 
, 'ASHMD' --property
, '02' --charge type
, 'RENTAL' --unit charges
, 'DC_DP_00' --discount
, '01' --Selected "Discount Type'
, '202410'
, 1--all building
, ''--building
, 'A'--agreement type
, 'vfm'--userid