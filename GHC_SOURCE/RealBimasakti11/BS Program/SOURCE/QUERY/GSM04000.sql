delete from sat_locking where CUSER_ID= 'ghc'

SELECT p.name AS ParameterName, t.name AS ParameterType, p.max_length AS ParameterLength
FROM sys.parameters AS p
JOIN sys.types AS t ON t.user_type_id = p.user_type_id
WHERE object_id = OBJECT_ID('RSP_GS_MAINTAIN_DEPARTMENT')

SELECT 
    kcu.COLUMN_NAME
FROM 
    INFORMATION_SCHEMA.TABLE_CONSTRAINTS AS tc
INNER JOIN 
    INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS kcu
    ON tc.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
WHERE 
    tc.TABLE_NAME = 'GSM_DEPARTMENT'
    AND tc.CONSTRAINT_TYPE = 'PRIMARY KEY';


CREATE TABLE #DEPARTMENT (
	No INT,
	DepartmentCode VARCHAR(254),
	DepartmentName VARCHAR(254),
	CenterCode VARCHAR(254),
	ManagerName VARCHAR(254),
	BranchCode VARCHAR(100),
	Everyone BIT,
	Active BIT,
	NonActiveDate VARCHAR(8));
INSERT INTO #DEPARTMENT VALUES
(1,'testupload1', 'testupload1', 'erc', 'ghc', 'KPP01', 1, 1,''),
(2,'testupload2', 'testupload2', 'erc', 'ghc', 'KPP01', 1, 1,'')

EXEC RSP_GS_UPLOAD_DEPARTMENT 'RCD','ghc','testupload1'

RSP_GS_MAINTAIN_DEPARTMENT


