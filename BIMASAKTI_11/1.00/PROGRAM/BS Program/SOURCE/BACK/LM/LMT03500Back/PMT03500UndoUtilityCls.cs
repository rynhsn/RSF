using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using PMT03500Common;
using PMT03500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMT03500Back;

public class PMT03500UndoUtilityCls : R_IBatchProcess
{
    RSP_PM_UPLOAD_UTILITY_USAGE_ECResources.Resources_Dummy_Class _rscUpload = new();
    
    private readonly ActivitySource _activitySource;
    private LoggerPMT03500 _logger;

    public PMT03500UndoUtilityCls()
    {
        _logger = LoggerPMT03500.R_GetInstanceLogger();
        _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
    }
    public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
    {
        using var Activity = _activitySource.StartActivity(nameof(R_BatchProcess));
        _logger.LogInfo(string.Format("START process method {0} on Cls", nameof(R_BatchProcess)));
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

            // while (!loTask.IsCompleted)
            // {
            //     Thread.Sleep(100);
            // }
            //
            // if (loTask.IsFaulted)
            // {
            //     loException.Add(loTask.Exception.InnerException != null
            //         ? loTask.Exception.InnerException
            //         : loTask.Exception);
            //
            //     goto EndBlock;
            // }
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
        List<PMT03500UtilityUsageDTO> loTempObject = new();
        // List<PMT03500UndoUtilityDTO> loTempObject = new();

        //object loTempVar = "";
        var LcGroupCode = "";


        try
        {
            await Task.Delay(1000);

            loTempObject =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<PMT03500UtilityUsageDTO>>(
                    poBatchProcessPar.BigObject);

            var loProperty = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(PMT03500ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;
            var loBuilding = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(PMT03500ContextConstant.CBUILDING_ID)).FirstOrDefault().Value;
            var loChargeType = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(PMT03500ContextConstant.CCHARGES_TYPE)).FirstOrDefault().Value;
            var loInvPrd = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(PMT03500ContextConstant.CINVOICE_PRD)).FirstOrDefault().Value;

            var lcProperty = ((System.Text.Json.JsonElement)loProperty).GetString();
            var lcBuilding = ((System.Text.Json.JsonElement)loBuilding).GetString();
            var lcChargeType = ((System.Text.Json.JsonElement)loChargeType).GetString();
            var lcInvPrd = ((System.Text.Json.JsonElement)loInvPrd).GetString();

            // var loObject = new List<PMT03500UploadUtilityRequestECDTO>();
            
            var loObject = R_Utility.R_ConvertCollectionToCollection<PMT03500UtilityUsageDTO, PMT03500UndoUtilityDTO>(loTempObject);
            
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery += $"CREATE TABLE #UNDO_UTILITY_USAGE( " +
                       $"NO				    INT, " +
                       $"CCOMPANY_ID		varchar(8), " +
                       $"CPROPERTY_ID		VARCHAR(20), " +
                       $"CDEPT_CODE		    VARCHAR(20), " +
                       $"CTRANS_CODE		VARCHAR(10), " +
                       $"CREF_NO			VARCHAR(30), " +
                       $"CUNIT_ID			VARCHAR(20), " +
                       $"CFLOOR_ID			VARCHAR(20), " +
                       $"CBUILDING_ID		VARCHAR(20), " +
                       $"CCHARGES_TYPE		VARCHAR(2), " +
                       $"CCHARGES_ID		VARCHAR(20), " +
                       $"CSEQ_NO			VARCHAR(3), " +
                       $"CINV_PRD			VARCHAR(6), " +
                       $"CMETER_NO			VARCHAR(50)" +
                       $");";
            loDb.SqlExecNonQuery(lcQuery, loConn, false);
            loDb.R_BulkInsert((SqlConnection)loConn, $"#UNDO_UTILITY_USAGE", loObject);

            lcQuery = $"RSP_PM_UNDO_UTILITY_USAGE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, lcProperty);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, lcBuilding);
            loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 2, lcChargeType);
            loDb.R_AddCommandParameter(loCmd, "@CINV_PRD", DbType.String, 6, lcInvPrd);
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