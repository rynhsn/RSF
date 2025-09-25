using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using ICB00300Common;
using ICB00300Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using R_OpenTelemetry;

namespace ICB00300Back;

public class ICB00300ProcessBatchCls : R_IBatchProcess
{
    // RSP_PM_UPLOAD_UTILITY_USAGE_WGResources.Resources_Dummy_Class _rscUploadWG = new();

    private readonly ActivitySource _activitySource;
    private LoggerICB00300 _logger;


    public ICB00300ProcessBatchCls()
    {
        _logger = LoggerICB00300.R_GetInstanceLogger();
        _activitySource = R_LibraryActivity.R_GetInstanceActivitySource();
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
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
    }


    private async Task _batchProcess(R_BatchProcessPar poBatchProcessPar)
    {
        using var Activity = _activitySource.StartActivity(nameof(_batchProcess));
        var loException = new R_Exception();
        var loDb = new R_Db();
        DbCommand loCmd = null;
        DbConnection loConn = null;
        var lcQuery = "";
        List<ICB00300ProductDTO> loTempObject = new(); //
        // IList<PMT03500UploadUtilityRequestECDTO> loObjectEC = new List<PMT03500UploadUtilityRequestECDTO>();
        //object loTempVar = "";
        var LcGroupCode = "";


        try
        {
            await Task.Delay(1000);

            loTempObject =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<ICB00300ProductDTO>>(
                    poBatchProcessPar.BigObject);

            _logger.LogInfo("Get User Parameters");
            var loProperty = poBatchProcessPar.UserParameters
                .FirstOrDefault(x => x.Key.Equals(ICB00300ContextConstant.CPROPERTY_ID))?.Value;
            var loRecalculationType = poBatchProcessPar.UserParameters
                .FirstOrDefault(x => x.Key.Equals(ICB00300ContextConstant.CRECALCULATE_TYPE))?.Value;
            var loUpdateBalance = poBatchProcessPar.UserParameters
                .FirstOrDefault(x => x.Key.Equals(ICB00300ContextConstant.LUPDATE_BALANCE))?.Value;
            var loFailFacility = poBatchProcessPar.UserParameters
                .FirstOrDefault(x => x.Key.Equals(ICB00300ContextConstant.LFAIL_FACILITY))?.Value;

            var lcProperty = ((System.Text.Json.JsonElement)loProperty).GetString();
            var lcRecalculationType = ((System.Text.Json.JsonElement)loRecalculationType).GetString();
            var llUpdateBalance = ((System.Text.Json.JsonElement)loUpdateBalance).GetBoolean();
            var llFailFacility = ((System.Text.Json.JsonElement)loFailFacility).GetBoolean();


            var loObject =
                R_Utility.R_ConvertCollectionToCollection<ICB00300ProductDTO, ICB00300ProductBulkInsertDTO>(
                    loTempObject);

            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            _logger.LogInfo("Start Create Temporary Table and Bulk Insert Data");
            lcQuery += "RECALCULATE_PRODUCTS(" +
                       "NO				INT, " +
                       "CPRODUCT_ID		varchar(20)";

            loDb.SqlExecNonQuery(lcQuery, loConn, false);

            for (var i = 0; i < loObject.Count; i++)
            {
                _logger.LogDebug($"INSERT INTO #RECALCULATE_PRODUCTS " +
                                 $"VALUES (" +
                                 $"{loObject[i].NO}, " +
                                 $"{loObject[i].CPRODUCT_ID})");
            }
            
            loDb.R_BulkInsert((SqlConnection)loConn, $"#RECALCULATE_PRODUCTS", loObject);

            _logger.LogInfo("End Create Temporary Table and Bulk Insert Data");
            _logger.LogDebug(lcQuery);
            _logger.LogInfo("Start Exec Upload Query");

            lcQuery = $"RSP_IC_RECALCULATE_STOCK_PROCESS";

            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, lcProperty);
            loDb.R_AddCommandParameter(loCmd, "@CRECALCULATE_TYPE", DbType.String, 2, lcRecalculationType);
            loDb.R_AddCommandParameter(loCmd, "@LUPDATE_BALANCE", DbType.Boolean, 1, llUpdateBalance);
            loDb.R_AddCommandParameter(loCmd, "@LFAIL_FACILITY", DbType.Boolean, 1, llFailFacility);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poBatchProcessPar.Key.USER_ID);
            // loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CRECALCULATE_TYPE" or
                        "@LUPDATE_BALANCE" or
                        "@LFAIL_FACILITY" or
                        "@CUSER_ID"
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