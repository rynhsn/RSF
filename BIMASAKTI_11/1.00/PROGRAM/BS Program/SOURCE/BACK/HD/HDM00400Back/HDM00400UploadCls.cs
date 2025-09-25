using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using HDM00400Common;
using HDM00400Common.DTOs.Upload;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace HDM00400Back;

public class HDM00400UploadCls : R_IBatchProcess
{
    RSP_HD_UPLOAD_PUBLIC_LOCResources.Resources_Dummy_Class _resources = new();

    private readonly ActivitySource _activitySource;
    private LoggerHDM00400 _logger;

    public HDM00400UploadCls()
    {
        _logger = LoggerHDM00400.R_GetInstanceLogger();
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

            _logger.LogInfo("Start Batch Process");
            var loTask = Task.Run(() => { _batchProcess(poBatchProcessPar); });

            // while (!loTask.IsCompleted)
            // {
            //     Thread.Sleep(100);
            // }

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
            _logger.LogError(loException);
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


        try
        {
            await Task.Delay(1000);
            var loTempObject =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<HDM00400UploadForSystemDTO>>(poBatchProcessPar
                    .BigObject);

            _logger.LogInfo("Get User Parameters");
            var loProperty = poBatchProcessPar.UserParameters
                .Where((x) => x.Key.Equals(HDM00400ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;

            var lcProperty = ((System.Text.Json.JsonElement)loProperty).GetString();

            var loObject = R_Utility
                .R_ConvertCollectionToCollection<HDM00400UploadForSystemDTO, HDM00400UploadRequestDTO>(loTempObject);

            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            _logger.LogInfo("Start Create Temporary Table and Bulk Insert Data");
            lcQuery += "CREATE TABLE #PUBLIC_LOC( " +
                       "NO              int, " +
                       "PublicLocId     varchar(20), " +
                       "PublicLocName   varchar(100), " +
                       "BuildingId      varchar(100), " +
                       "FloorId         varchar(100), " +
                       "Active          bit, " +
                       "NonActiveDate   varchar(30), " +
                       "Description     varchar(20)" +
                       ")";

            loDb.SqlExecNonQuery(lcQuery, loConn, false);

            _logger.LogInfo("Bulk Insert to Temp Table for public location Upload");
            loDb.R_BulkInsert((SqlConnection)loConn, $"#PUBLIC_LOC", loObject);

            _logger.LogInfo("End Create Temporary Table and Bulk Insert Data");
            _logger.LogDebug(lcQuery);

            _logger.LogInfo("Start Exec Upload Query");
            lcQuery = "RSP_HD_UPLOAD_PUBLIC_LOC";

            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, lcProperty);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poBatchProcessPar.Key.USER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 100, poBatchProcessPar.Key.KEY_GUID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CUSER_ID" or
                        "@CKEY_GUID"
                )
                .Select(x => x.Value);
            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            _logger.LogInfo("End Process");

            loDb.SqlExecNonQuery(loConn, loCmd, false);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
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
            _logger.LogError("Exception Error", loException);
            lcQuery = $"EXEC RSP_WriteUploadProcessStatus '{poBatchProcessPar.Key.COMPANY_ID}', " +
                      $"'{poBatchProcessPar.Key.USER_ID}', " +
                      $"'{poBatchProcessPar.Key.KEY_GUID}', " +
                      $"100, '{loException.ErrorList[0].ErrDescp}', 9";

            loDb.SqlExecNonQuery(lcQuery);
        }
    }
}