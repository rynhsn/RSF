SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_PM_SAVE_STAMP_RATE')

EXEC sp_help 'PMM_STAMP_RATE';
delete from sat_locking where CUSER_ID= 'ghc' 

EXEC RSP_GS_GET_CURRENCY_LIST 'rcd','ghc'
EXEC RSP_GS_GET_PROPERTY_LIST 'rcd','ghc'

EXEC RSP_PM_GET_STAMP_RATE_LIST 'bsi','ASHMD','en'
EXEC RSP_PM_GET_STAMP_RATE_DATE_LIST 'bsi','ASHMD','4D951A7F-661C-4F67-B1FE-636E7AF1F62B','en'
EXEC RSP_PM_GET_STAMP_RATE_AMOUNT_LIST 'bsi','ASHMD','4D951A7F-661C-4F67-B1FE-636E7AF1F62B','2DAB9787-7510-4958-97C9-F2F4720C2BCB','en'

EXEC RSP_PM_GET_STAMP_RATE 'rcd','','','A77CAEAD-F140-4A23-86E5-169AA94476E8','en'
EXEC RSP_PM_GET_STAMP_RATE_DATE 'rcd','A51D3789-D9DC-430A-85C9-D4ADA07408E2','en'
EXEC RSP_PM_GET_STAMP_RATE_AMOUNT 'rcd','832977E7-334C-4F77-A05C-6D3A276EEA3B','en'


SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_PM_SAVE_STAMP_RATE')

EXEC RSP_PM_DELETE_STAMP_RATE 'rcd','ASHMD','stampcode','(stampcode)crecid'
EXEC RSP_PM_DELETE_STAMP_RATE_DATE '(stampcode)crecid'
EXEC RSP_PM_DELETE_STAMP_RATE_AMOUNT '(stampamount)crecid'

EXEC RSP_PM_SAVE_STAMP_RATE 'rcd','ASHMD','ghc','ADD','','','desc','curcode'
EXEC RSP_PM_SAVE_STAMP_RATE_DATE 'rcd','ASHMD','ghc','ADD','stampcode','(stampcode)crecid','stampdateid','yyyymmdd'
EXEC RSP_PM_SAVE_STAMP_RATE_AMOUNT 'rcd','ASHMD','ghc','ADD','cstampcode','(stampcode)crecid','(stampdate)crecid','(stampamount)crecid','yyyymmdd',2000,2000


 