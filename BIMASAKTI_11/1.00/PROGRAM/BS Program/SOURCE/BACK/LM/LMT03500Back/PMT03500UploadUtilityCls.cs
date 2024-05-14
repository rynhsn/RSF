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
    private LoggerPMT03500 _loggerPMT03500;

    public PMT03500UploadUtilityCls()
    {
        _loggerPMT03500 = LoggerPMT03500.R_GetInstanceLogger();
        _activitySource = R_OpenTelemetry.R_LibraryActivity.R_GetInstanceActivitySource();
    }

    public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
    {
        using var Activity = _activitySource.StartActivity(nameof(R_BatchProcess));
        _loggerPMT03500.LogInfo(string.Format("START process method {0} on Cls", nameof(R_BatchProcess)));
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
        List<PMT03500UploadUtilityErrorValidateDTO> loTempObject = new();
        IList<PMT03500UploadUtilityRequestECDTO> loObjectEC = new List<PMT03500UploadUtilityRequestECDTO>();
        IList<PMT03500UploadUtilityRequestWGDTO> loObjectWG = new List<PMT03500UploadUtilityRequestWGDTO>();
        //object loTempVar = "";
        var LcGroupCode = "";


        try
        {
            await Task.Delay(1000);

            loTempObject = R_NetCoreUtility.R_DeserializeObjectFromByte<List<PMT03500UploadUtilityErrorValidateDTO>>(poBatchProcessPar.BigObject);

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
            }
            else if (lcUtility == "WG")
            {
                loObjectWG = R_Utility
                    .R_ConvertCollectionToCollection<PMT03500UploadUtilityErrorValidateDTO,
                        PMT03500UploadUtilityRequestWGDTO>(loTempObject);
            }

            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery += $"CREATE TABLE #UTILITY_USAGE_{lcUtility}( " +
                       $"NO             int, " +
                       $"CCOMPANY_ID    varchar(8), " +
                       $"CPROPERTY_ID    varchar(20), " +
                       $"CDEPT_CODE      varchar(20), " +
                       $"CTRANS_CODE     varchar(10), " +
                       $"CREF_NO         varchar(30), " +
                       $"CUTILITY_TYPE   varchar(2),  " +
                       $"CUNIT_ID        varchar(20), " +
                       $"CFLOOR_ID       varchar(20), " +
                       $"CBUILDING_ID    varchar(20), " +
                       $"CCHARGES_TYPE   varchar(2),  " +
                       $"CCHARGES_ID 	varchar(20), " +
                       $"CSEQ_NO 		varchar(3),  " +
                       $"CINV_PRD    	varchar(6),  " +
                       $"CUTILITY_PRD    varchar(6),  " +
                       $"CSTART_DATE 	varchar(8),  " +
                       $"CEND_DATE   	varchar(8),  " +
                       $"CMETER_NO   	varchar(50), ";

            switch (lcUtility)
            {
                case "EC":
                    lcQuery += "IBLOCK1_START   int, " +
                               "IBLOCK2_START   int, " +
                               "IBLOCK1_END 	int, " +
                               "IBLOCK2_END 	int);";
                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    loDb.R_BulkInsert((SqlConnection)loConn, $"#UTILITY_USAGE_EC", loObjectEC);
                    break;
                case "WG":
                    lcQuery += "IMETER_START 	int, " +
                               "IMETER_END 	    int);";
                    loDb.SqlExecNonQuery(lcQuery, loConn, false);
                    loDb.R_BulkInsert((SqlConnection)loConn, $"#UTILITY_USAGE_WG", loObjectWG);
                    break;
            }

            lcQuery = $"RSP_PM_UPLOAD_UTILITY_USAGE_{lcUtility}";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, lcProperty);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 20, "");
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