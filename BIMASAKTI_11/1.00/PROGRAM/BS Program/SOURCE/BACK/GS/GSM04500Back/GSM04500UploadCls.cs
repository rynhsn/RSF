using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM04500Back;

public class GSM04500UploadCls : R_IBatchProcess
{
    public void R_BatchProcess(R_BatchProcessPar poBatchProcessPar)
    {
        R_Exception loException = new R_Exception();
        var loDb = new R_Db();

        try
        {
            if (loDb.R_TestConnection() == false)
            {
                loException.Add("000", "Database Connection Failed");
                goto EndBlock;
            }

            var loTask = Task.Run(() => { _BatchProcess(poBatchProcessPar); });

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

    public async Task _BatchProcess(R_BatchProcessPar poBatchProcessPar)
    {
        R_Exception loException = new R_Exception();
        string lcQuery = "";
        R_Db loDb = new R_Db();
        DbConnection loConn = null;
        DbCommand loCommand = null;
        try
        {
            await Task.Delay(100);

            loCommand = loDb.GetCommand();
            loConn = loDb.GetConnection();
            //Get data from poBatchPRocessParam
            var loTempObject =
                R_NetCoreUtility.R_DeserializeObjectFromByte<List<GSM04500UploadErrorValidateDTO>>(poBatchProcessPar
                    .BigObject);
            //CONVERT DATA, SO TO BE READY INSERT TO TEMPORARY TABLE 
            var loObject =
                R_Utility
                    .R_ConvertCollectionToCollection<GSM04500UploadErrorValidateDTO, GSM04500FieldTemporaryTableDTO>(
                        loTempObject);

            #region GetParameterPropert

            //get parameter
            var loVar = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CPROPERTY_ID))
                .FirstOrDefault().Value;
            var lcPropertyId = ((System.Text.Json.JsonElement)loVar).GetString();

            var loVar2 = poBatchProcessPar.UserParameters.Where((x) => x.Key.Equals(ContextConstant.CJRNGRP_TYPE))
                .FirstOrDefault().Value;
            var lcJournalGroupType = ((System.Text.Json.JsonElement)loVar2).GetString();

            #endregion

            lcQuery = $"CREATE TABLE #JRNLGROUP " +
                      $"(No INT, " +
                      $"JournalGroup VARCHAR(100), " +
                      $"JournalGroupName VARCHAR(200), " +
                      $"EnableAccrual BIT )";

            loDb.SqlExecNonQuery(lcQuery, loConn, false);

            loDb.R_BulkInsert((SqlConnection)loConn, "#JRNLGROUP", loObject);

            lcQuery = "RSP_GS_UPLOAD_JOURNAL_GROUP";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 8, poBatchProcessPar.Key.COMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 20, poBatchProcessPar.Key.USER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, lcPropertyId);
            loDb.R_AddCommandParameter(loCommand, "@CJOURNAL_GROUP_TYPE", DbType.String, 20, lcJournalGroupType);
            loDb.R_AddCommandParameter(loCommand, "@CKEY_GUID", DbType.String, 50, poBatchProcessPar.Key.KEY_GUID);

            var abc = loDb.SqlExecNonQuery(loConn, loCommand, false);
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

            if (loCommand != null)
            {
                loCommand.Dispose();
                loCommand = null;
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