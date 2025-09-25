using GLT00200COMMON.DTOs.GLT00200;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using R_CommonFrontBackAPI;
using System.Data.Common;
using GLT00200COMMON;

namespace GLT00200BACK
{
    public class GLT00200ValidateUploadCls : R_IBatchProcess
    {
        public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
        {
            R_Exception loException = new R_Exception();
            string lcCmd;
            string lcQuery = "";
            var loDb = new R_Db();
            DbConnection loConn = loDb.GetConnection();
            var loCmd = loDb.GetCommand();
            Dictionary<string, string> loMapping = new Dictionary<string, string>();
            List<GetImportJournalResult> loResult = null;
            int count = 1;

            try
            {
                var loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<ImportJournalParameterDTO>(poBatchProcessPar.BigObject);

                loResult = new List<GetImportJournalResult>();

                List<BulkInsertParameterDTO> loParam = loTempObject.DetailData.Select(x => new BulkInsertParameterDTO
                {
                    CPROCESS_ID = poBatchProcessPar.Key.KEY_GUID,
                    CCOMPANY_ID = poBatchProcessPar.Key.COMPANY_ID,
                    CDEPT_CODE = loTempObject.HeaderData.CDEPT_CODE,
                    CTRANS_CODE = "000000",
                    CREF_NO = loTempObject.HeaderData.CREF_NO,
                    CREF_DATE = (Convert.ToDateTime(loTempObject.HeaderData.CREF_DATE)).ToString("yyyyMMdd"),
                    CDOC_NO = loTempObject.HeaderData.CDOC_NO,
                    CDOC_DATE = (Convert.ToDateTime(loTempObject.HeaderData.CDOC_DATE)).ToString("yyyyMMdd"),
                    CTRANS_DESC = loTempObject.HeaderData.CTRANS_DESC,
                    CCURRENCY_CODE = loTempObject.HeaderData.CCURRENCY_CODE,
                    NLBASE_RATE = loTempObject.HeaderData.NLBASE_RATE,
                    NLCURRENCY_RATE = loTempObject.HeaderData.NLCURRENCY_RATE,
                    NBBASE_RATE = loTempObject.HeaderData.NBBASE_RATE,
                    NBCURRENCY_RATE = loTempObject.HeaderData.NBCURRENCY_RATE,
                    CGLACCOUNT_NO = x.CGLACCOUNT_NO,
                    CGLACCOUNT_NAME = x.CGLACCOUNT_NAME,
                    CCENTER_CODE = x.CCENTER_CODE,
                    CDBCR = x.CDBCR,
                    NTRANS_AMOUNT = x.NTRANS_AMOUNT,
                    NLTRANS_AMOUNT = 0,
                    NBTRANS_AMOUNT = 0,
                    CDETAIL_DESC = x.CDETAIL_DESC,
                    CDOCUMENT_NO = x.CDOCUMENT_NO,
                    CDOCUMENT_DATE = x.CDOCUMENT_DATE.ToString("yyyyMMdd"),
                }).ToList();

                using (var TransScope = new TransactionScope(TransactionScopeOption.Required))
                {
                    lcQuery = $"CREATE TABLE #GLT00200_JOURNAL " +
                        $"(CPROCESS_ID VARCHAR(50) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CPROCESS_ID DEFAULT(''), " +
                        $"CCOMPANY_ID VARCHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCOMPANY_ID DEFAULT(''), " +
                        $"CDEPT_CODE VARCHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDEPT_CODE DEFAULT(''), " +
                        $"CTRANS_CODE VARCHAR(10) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CTRANS_CODE DEFAULT(''), " +
                        $"CREF_NO VARCHAR(30) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CREF_NO DEFAULT(''), " +
                        $"CREF_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CREF_DATE DEFAULT(''), " +
                        $"CDOC_NO VARCHAR(30) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOC_NO DEFAULT(''), " +
                        $"CDOC_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOC_DATE DEFAULT(''), " +
                        $"CTRANS_DESC NVARCHAR(200) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CTRANS_DESC DEFAULT(''), " +
                        $"CCURRENCY_CODE CHAR(3) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCURRENCY_CODE DEFAULT(''), " +
                        $"NLBASE_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLBASE_RATE DEFAULT(1), " +
                        $"NLCURRENCY_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLCURRENCY_RATE DEFAULT(1), " +
                        $"NBBASE_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBBASE_RATE DEFAULT(1), " +
                        $"NBCURRENCY_RATE NUMERIC(20, 6) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBCURRENCY_RATE DEFAULT(1), " +
                        $"CGLACCOUNT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CGLACCOUNT_NO DEFAULT(''), " +
                        $"CGLACCOUNT_NAME NVARCHAR(60) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CGLACCOUNT_NAME DEFAULT(''), " +
                        $"CCENTER_CODE VARCHAR(10) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CCENTER_CODE DEFAULT(''), " +
                        $"CDBCR CHAR(1) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDBCR DEFAULT(''), " +
                        $"NTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NTRANS_AMOUNT DEFAULT(0), " +
                        $"NLTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NLTRANS_AMOUNT DEFAULT(0), " +
                        $"NBTRANS_AMOUNT NUMERIC(19, 2) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_NBTRANS_AMOUNT DEFAULT(0), " +
                        $"CDETAIL_DESC NVARCHAR(200) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDETAIL_DESC DEFAULT(''), " +
                        $"CDOCUMENT_NO VARCHAR(20) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOCUMENT_NO DEFAULT(''), " +
                        $"CDOCUMENT_DATE CHAR(8) NOT NULL CONSTRAINT DF_GLT00200_JOURNAL_CDOCUMENT_DATE DEFAULT('')) ";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<BulkInsertParameterDTO>((SqlConnection)loConn, "#GLT00200_JOURNAL", loParam);

                    //Get Calculated Journal Detail List
                    lcQuery = $"EXEC RSP_GL_GET_IMPORT_JOURNAL_DETAIL_LIST " +
                        $"@CPROCESS_ID, " +
                        $"@CLANGUAGE_ID";

                    loDb.R_AddCommandParameter(loCmd, "@CPROCESS_ID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);
                    loDb.R_AddCommandParameter(loCmd, "@CLANGUAGE_ID", DbType.String, 50, loTempObject.CLANGUAGE_ID);

                    loCmd.CommandText = lcQuery;

                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);

                    loResult = R_Utility.R_ConvertTo<GetImportJournalResult>(loDataTable).ToList();

                    lcQuery = $"CREATE TABLE #JOURNAL_RESULT (CGLACCOUNT_NO VARCHAR(20) NOT NULL DEFAULT(''), " +
                        $"CGLACCOUNT_NAME NVARCHAR(60) NOT NULL DEFAULT(''), " +
                        $"CCENTER_CODE VARCHAR(10) NOT NULL DEFAULT(''), " +
                        $"CDBCR CHAR(1) NOT NULL DEFAULT(''), " +
                        $"NTRANS_AMOUNT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                        $"NDEBIT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                        $"NCREDIT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                        $"CDETAIL_DESC NVARCHAR(200) NOT NULL DEFAULT(''), " +
                        $"NLDEBIT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                        $"NLCREDIT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                        $"NBDEBIT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                        $"NBCREDIT NUMERIC(19, 2) NOT NULL DEFAULT(0), " +
                        $"CDOCUMENT_NO VARCHAR(20) NOT NULL DEFAULT(''), " +
                        $"CDOCUMENT_DATE CHAR(8) NOT NULL DEFAULT(''));";

                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    loDb.R_BulkInsert<GetImportJournalResult>((SqlConnection)loConn, "#JOURNAL_RESULT", loResult);

                    lcQuery = $"EXEC RSP_ConvertTableToXML " +
                        $"@CCOMPANY_ID, " +
                        $"@CUSER_ID, " +
                        $"@KEY_GUID, " +
                        $"'#JOURNAL_RESULT', " +
                        $"1";

                    loCmd.CommandText= lcQuery;

                    loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poBatchProcessPar.Key.COMPANY_ID);
                    loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poBatchProcessPar.Key.USER_ID);
                    loDb.R_AddCommandParameter(loCmd, "@KEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

                    loDb.SqlExecNonQuery(loConn, loCmd, false);

                    TransScope.Complete();
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != ConnectionState.Closed)
                        loConn.Close();

                    loConn.Dispose();
                    loConn = null;
                }
            }

            loException.ThrowExceptionIfErrors();
        }
        
        public List<GetImportJournalResult> GetSuccessProcess(string pcCompanyId, string pcUserId, string pcKeyGuid)
        {
            var loEx = new R_Exception();
            var lcQuery = "";
            var loDb = new R_Db();
            List<GetImportJournalResult> loResult = null;

            try
            {
                var loConn = loDb.GetConnection();
                var loCmd = loDb.GetCommand();

                lcQuery = "EXECUTE RSP_ConvertXMLToTable @CCOMPANY_ID, @CUSER_ID, @CKEY_GUID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, pcCompanyId);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, pcUserId);
                loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, pcKeyGuid);

                var loDataTableResult = loDb.SqlExecQuery(loConn, loCmd, false);

                loResult = R_Utility.R_ConvertTo<GetImportJournalResult>(loDataTableResult).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
