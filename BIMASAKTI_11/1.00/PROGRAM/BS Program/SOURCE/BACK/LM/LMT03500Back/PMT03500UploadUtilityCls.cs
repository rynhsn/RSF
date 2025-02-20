using System.Collections;
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

public class PMT03500UploadUtilityCls : R_IBatchProcess
{
    RSP_PM_UPLOAD_UTILITY_USAGE_ECResources.Resources_Dummy_Class _rscUploadEC = new();
    RSP_PM_UPLOAD_UTILITY_USAGE_WGResources.Resources_Dummy_Class _rscUploadWG = new();

    private readonly ActivitySource _activitySource;
    private LoggerPMT03500 _logger;

    public PMT03500UploadUtilityCls()
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
        List<PMT03500UploadUtilityErrorValidateDTO> loTempObject = new();
        IList<PMT03500UploadUtilityRequestECDTO> loObjectEC = new List<PMT03500UploadUtilityRequestECDTO>();
        IList<PMT03500UploadUtilityRequestWGDTO> loObjectWG = new List<PMT03500UploadUtilityRequestWGDTO>();
        //object loTempVar = "";
        var LcGroupCode = "";


        try
        {
            await Task.Delay(1000);

            loTempObject =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<PMT03500UploadUtilityErrorValidateDTO>>(
                    poBatchProcessPar.BigObject);

            _logger.LogInfo("Get User Parameters");
            var loUtilityType = poBatchProcessPar.UserParameters
                .Where((x) => x.Key.Equals(PMT03500ContextConstant.CUTILITY_TYPE)).FirstOrDefault().Value;

            var loProperty = poBatchProcessPar.UserParameters
                .Where((x) => x.Key.Equals(PMT03500ContextConstant.CPROPERTY_ID)).FirstOrDefault().Value;

            var lcProperty = ((System.Text.Json.JsonElement)loProperty).GetString();
            var lcUtility = ((System.Text.Json.JsonElement)loUtilityType).GetString();

            if (lcUtility == "EC")
            {
                loObjectEC = R_Utility
                    .R_ConvertCollectionToCollection<PMT03500UploadUtilityErrorValidateDTO,
                        PMT03500UploadUtilityRequestECDTO>(loTempObject);
                foreach (var item in loObjectEC)
                {
                    item.CPHOTO_REC_ID_1 = "";
                    item.CPHOTO_REC_ID_2 = "";
                    item.CPHOTO_REC_ID_3 = "";
                }
            }
            else if (lcUtility == "WG")
            {
                loObjectWG = R_Utility
                    .R_ConvertCollectionToCollection<PMT03500UploadUtilityErrorValidateDTO,
                        PMT03500UploadUtilityRequestWGDTO>(loTempObject);
                foreach (var item in loObjectWG)
                {
                    item.CPHOTO_REC_ID_1 = "";
                    item.CPHOTO_REC_ID_2 = "";
                    item.CPHOTO_REC_ID_3 = "";
                }
            }

            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            _logger.LogInfo("Start Create Temporary Table and Bulk Insert Data");
            lcQuery += $"CREATE TABLE #UTILITY_USAGE_{lcUtility}( " +
                       "NO             int, " +
                       "CCOMPANY_ID    varchar(8), " +
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
                       "CPHOTO_REC_ID_1 varchar(50), " +
                       "CPHOTO_REC_ID_2 varchar(50), " +
                       "CPHOTO_REC_ID_3 varchar(50), ";

            switch (lcUtility)
            {
                case "EC":
                    lcQuery += "NBLOCK1_START   numeric(16,2), " +
                               "NBLOCK2_START   numeric(16,2), " +
                               "NBLOCK1_END 	numeric(16,2), " +
                               "NBLOCK2_END 	numeric(16,2), " +
                               "NBEBAN_BERSAMA 	numeric(16,2));";


                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    for (var i = 0; i < loObjectEC.Count; i++)
                    {
                        _logger.LogDebug($"INSERT INTO #UTILITY_USAGE_EC " +
                                         $"VALUES (" +
                                         $"{loObjectEC[i].NO}, " +
                                         $"'{loObjectEC[i].CCOMPANY_ID}', " +
                                         $"'{loObjectEC[i].CPROPERTY_ID}', " +
                                         $"'{loObjectEC[i].CDEPT_CODE}', " +
                                         $"'{loObjectEC[i].CTRANS_CODE}', " +
                                         $"'{loObjectEC[i].CREF_NO}', " +
                                         $"'{loObjectEC[i].CUTILITY_TYPE}', " +
                                         $"'{loObjectEC[i].CUNIT_ID}', " +
                                         $"'{loObjectEC[i].CFLOOR_ID}', " +
                                         $"'{loObjectEC[i].CBUILDING_ID}', " +
                                         $"'{loObjectEC[i].CCHARGES_TYPE}', " +
                                         $"'{loObjectEC[i].CCHARGES_ID}', " +
                                         $"'{loObjectEC[i].CSEQ_NO}', " +
                                         $"'{loObjectEC[i].CINV_PRD}', " +
                                         $"'{loObjectEC[i].CUTILITY_PRD}', " +
                                         $"'{loObjectEC[i].CSTART_DATE}', " +
                                         $"'{loObjectEC[i].CEND_DATE}', " +
                                         $"'{loObjectEC[i].CMETER_NO}', " +
                                         $"'{loObjectEC[i].CPHOTO_REC_ID_1}', " +
                                         $"'{loObjectEC[i].CPHOTO_REC_ID_2}', " +
                                         $"'{loObjectEC[i].CPHOTO_REC_ID_3}', " +
                                         $"{loObjectEC[i].NBLOCK1_START}, " +
                                         $"{loObjectEC[i].NBLOCK2_START}, " +
                                         $"{loObjectEC[i].NBLOCK1_END}, " +
                                         $"{loObjectEC[i].NBLOCK2_END}, " +
                                         $"{loObjectEC[i].NBEBAN_BERSAMA})");
                    }

                    loDb.R_BulkInsert((SqlConnection)loConn, $"#UTILITY_USAGE_EC", loObjectEC);
                    break;
                case "WG":
                    lcQuery += "NMETER_START 	numeric(16,2), " +
                               "NMETER_END 	    numeric(16,2));";
                    loDb.SqlExecNonQuery(lcQuery, loConn, false);

                    // for (var i = 0; i < loObjectWG.Count; i++)
                    // {
                    //     _logger.LogDebug($"INSERT INTO #UTILITY_USAGE_WG " +
                    //                      $"VALUES (" +
                    //                      $"{loObjectWG[i].NO}, " +
                    //                      $"'{loObjectWG[i].CCOMPANY_ID}', " +
                    //                      $"'{loObjectWG[i].CPROPERTY_ID}', " +
                    //                      $"'{loObjectWG[i].CDEPT_CODE}', " +
                    //                      $"'{loObjectWG[i].CTRANS_CODE}', " +
                    //                      $"'{loObjectWG[i].CREF_NO}', " +
                    //                      $"'{loObjectWG[i].CUTILITY_TYPE}', " +
                    //                      $"'{loObjectWG[i].CUNIT_ID}', " +
                    //                      $"'{loObjectWG[i].CFLOOR_ID}', " +
                    //                      $"'{loObjectWG[i].CBUILDING_ID}', " +
                    //                      $"'{loObjectWG[i].CCHARGES_TYPE}', " +
                    //                      $"'{loObjectWG[i].CCHARGES_ID}', " +
                    //                      $"'{loObjectWG[i].CSEQ_NO}', " +
                    //                      $"'{loObjectWG[i].CINV_PRD}', " +
                    //                      $"'{loObjectWG[i].CUTILITY_PRD}', " +
                    //                      $"'{loObjectWG[i].CSTART_DATE}', " +
                    //                      $"'{loObjectWG[i].CEND_DATE}', " +
                    //                      $"'{loObjectWG[i].CMETER_NO}', " +
                    //                      $"'{loObjectEC[i].CPHOTO_REC_ID_1}', " +
                    //                      $"'{loObjectEC[i].CPHOTO_REC_ID_2}', " +
                    //                      $"'{loObjectEC[i].CPHOTO_REC_ID_3}', " +
                    //                      $"{loObjectWG[i].NMETER_START}, " +
                    //                      $"{loObjectWG[i].NMETER_END})");
                    // }

                    loDb.R_BulkInsert((SqlConnection)loConn, $"#UTILITY_USAGE_WG", loObjectWG);
                    break;
            }

            _logger.LogInfo("End Create Temporary Table and Bulk Insert Data");
            _logger.LogDebug(lcQuery);
            _logger.LogInfo("Start Exec Upload Query");

            lcQuery = $"RSP_PM_UPLOAD_UTILITY_USAGE_{lcUtility}";

            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, lcProperty);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, "");
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CTRANS_CODE" or
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