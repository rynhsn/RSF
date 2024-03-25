using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using LMT03500Common;
using LMT03500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMT03500Back;

public class LMT03500UploadUtilityCls : R_IBatchProcess
{
    private readonly ActivitySource _activitySource;

    public LMT03500UploadUtilityCls()
    {
        _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
    }


    public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
    {
        using var Activity = _activitySource.StartActivity(nameof(R_BatchProcess));
        R_Exception loException = new R_Exception();
        var loDb = new R_Db();

        try
        {
            if (loDb.R_TestConnection() == false)
            {
                loException.Add("01", "Database Connection Failed");
                goto EndBlock;
            }

            var loTask = Task.Run(() => { _batchProcess(poBatchProcessPar); });

            while (!loTask.IsCompleted)
            {
                Thread.Sleep(100);
            }

            if (loTask.IsFaulted)
            {
                loException.Add(loTask.Exception.InnerException != null
                    ? loTask.Exception.InnerException
                    : loTask.Exception);

                goto EndBlock;
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
    }

    public async Task _batchProcess(R_BatchProcessPar poBatchProcessPar)
    {
        using var Activity = _activitySource.StartActivity(nameof(_batchProcess));
        var loException = new R_Exception();
        var loDb = new R_Db();
        DbCommand loCmd = null;
        DbConnection loConn = null;
        var lcQuery = "";
        List<LMT03500UploadErrorValidateDTO> loTempObject = new();
        IList<LMT03500UploadRequestDTO> loObject = new List<LMT03500UploadRequestDTO>();
        //object loTempVar = "";
        var LcGroupCode = "";


        try
        {
            await Task.Delay(1000);

            loTempObject =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<LMT03500UploadErrorValidateDTO>>(poBatchProcessPar.BigObject);
            loObject = R_Utility
                .R_ConvertCollectionToCollection<LMT03500UploadErrorValidateDTO,
                    LMT03500UploadRequestDTO>(loTempObject);

            var loUtilityType = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(LMT03500ContextConstant.CUTILITY_TYPE)).FirstOrDefault().Value;
            var loProperty = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(LMT03500ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
            
            var lcUtilityType = ((System.Text.Json.JsonElement)loUtilityType).GetString();
            var lcProperty = ((System.Text.Json.JsonElement)loProperty).GetString();

            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery += "CREATE TABLE #UTILITY_USAGE_EC(CCOMPANY_ID varchar(8), " +
                       "CPROPERTY_ID    varchar(20), " +
                       "CDEPT_CODE      varchar(20), " +
                       "CTRANS_CODE     varchar(10), " +
                       "CREF_NO         varchar(30), " +
                       "CUTILITY_TYPE   varchar(2),  " +
                       "CUNIT_ID        varchar(20), " +
                       "CFLOOR_ID       varchar(20), " +
                       "CBUILDING_ID    varchar(20), " +
                       "CCHARGES_TYPE   varchar(2),  " +
                       "CCHARGES_ID 	varchar(20), " +
                       "CSEQ_NO 		varchar(3),  " +
                       "CINV_PRD    	varchar(6),  " +
                       "CUTILITY_PRD    varchar(6),  " +
                       "CSTART_DATE 	varchar(8),  " +
                       "CEND_DATE   	varchar(8),  " +
                       "CMETER_NO   	varchar(50), " +
                       "IBLOCK1_START   int, " +
                       "IBLOCK2_START   int, " +
                       "IBLOCK1_END 	int, " +
                       "IBLOCK2_END 	int) ";

            loDb.SqlExecNonQuery(lcQuery, loConn, false);
            loDb.R_BulkInsert<LMT03500UploadRequestDTO>((SqlConnection)loConn, "#UTILITY_USAGE_EC", loObject);


            lcQuery = "EXECUTE RSP_LM_UPLOAD_UTILITY_USAGE_EC @CCOMPANY_ID, @CPROPERTY_ID, @CTRANS_CODE, @CUSER_ID, @CKEY_GUID";
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, lcProperty);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, "Trans Code");
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

            loDb.SqlExecNonQuery(loConn, loCmd, false);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        finally
        {
            if (loConn != null)
            {
                if (!(loConn.State == ConnectionState.Closed))
                    loConn.Close();
                loConn.Dispose();
                loConn = null;
            }

            if (loCmd != null)
            {
                loCmd.Dispose();
                loCmd = null;
            }
        }

        if (loException.Haserror)
        {
            lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                      $"'{poBatchProcessPar.Key.USER_ID}', " +
                      $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                      $"100, '{loException.ErrorList[0].ErrDescp}', 9";

            loDb.SqlExecNonQuery(lcQuery);
        }
    }
}