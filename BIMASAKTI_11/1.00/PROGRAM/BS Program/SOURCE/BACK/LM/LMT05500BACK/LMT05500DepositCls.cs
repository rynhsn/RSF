using PMT05500COMMON.DTO;
using PMT05500COMMON.Logs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace PMT05500Back;

public class LMT05500DepositCls : R_BusinessObject<LMT05500DepositInfoDTO>
{
    private LoggerLMT05500 _loggerLMT05500;
    private readonly ActivitySource _activitySource;
    public LMT05500DepositCls()
    {
        _loggerLMT05500 = LoggerLMT05500.R_GetInstanceLogger();
        _activitySource = LMT05500Activity.R_GetInstanceActivitySource();
    }
    protected override LMT05500DepositInfoDTO R_Display(LMT05500DepositInfoDTO poEntity)
    {

        string lcMethodName = nameof(R_Display);
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerLMT05500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

        R_Exception loException = new R_Exception();
        LMT05500DepositInfoDTO? loReturn = null;
        R_Db loDb;
        try
        {
            var lcQuery = "RSP_PM_GET_DEPOSIT_INFO";
            loDb = new R_Db();
            var loCommand = loDb.GetCommand();
            var loConn = loDb.GetConnection();
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 10, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 8, poEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
            loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 30, poEntity.CSEQ_NO);


            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
            _loggerLMT05500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

            var loReturnTemp = loDb.SqlExecQuery(loConn, loCommand, false);
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            loReturn = R_Utility.R_ConvertTo<LMT05500DepositInfoDTO>(loReturnTemp).ToList().FirstOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _loggerLMT05500.LogError(loException);
        }
        loException.ThrowExceptionIfErrors();
        _loggerLMT05500.LogInfo("End process method R_Display on Cls");

        return loReturn;
    }

    protected override void R_Saving(LMT05500DepositInfoDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        string lcMethodName = nameof(R_Saving);
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerLMT05500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

        R_Exception loException = new R_Exception();
        string lcQuery = null;
        R_Db loDb;
        DbCommand loCommand;
        DbConnection loConn = null;
        string lcAction = null;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            R_ExternalException.R_SP_Init_Exception(loConn);
            loCommand = loDb.GetCommand();

            switch (poCRUDMode)
            {
                case eCRUDMode.AddMode:
                    lcAction = "ADD";
                    break;

                case eCRUDMode.EditMode:
                    lcAction = "EDIT";
                    break;
            }
            lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DEPOSIT";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;
            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poNewEntity.CCOMPANY_ID);

            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poNewEntity.CCHARGE_MODE);
            loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poNewEntity.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poNewEntity.CUNIT_ID);

            loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poNewEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);

            //input on field
            loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poNewEntity.CSEQ_NO);
            loDb.R_AddCommandParameter(loCommand, "@LCONTRACTOR", DbType.Boolean, 3, poNewEntity.LCONTRACTOR);
            loDb.R_AddCommandParameter(loCommand, "@CCONTRACTOR_ID", DbType.String, 20, poNewEntity.CCONTRACTOR_ID);
            loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_ID ", DbType.String, 20, poNewEntity.CDEPOSIT_ID);
            loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_DATE ", DbType.String, 8, poNewEntity.CDEPOSIT_DATE);
            loDb.R_AddCommandParameter(loCommand, "@CCURRENCY_CODE", DbType.String, 3, poNewEntity.CLOCAL_CURRENCY);
            loDb.R_AddCommandParameter(loCommand, "@NDEPOSIT_AMT", DbType.Decimal, int.MaxValue, poNewEntity.NDEPOSIT_AMT);
            loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION ", DbType.String, int.MaxValue, poNewEntity.CDESCRIPTION);

            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
            _loggerLMT05500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

            try
            {
                var loDataTable = loDb.SqlExecQuery(loConn, loCommand, false);
                var loEntity = R_Utility.R_ConvertTo<LMT05500DepositInfoDTO>(loDataTable).FirstOrDefault();
                if (loEntity != null && poCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CSEQ_NO = string.IsNullOrEmpty(loEntity.CSEQ_NO) ? "" : loEntity.CSEQ_NO;
                }
                _loggerLMT05500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMT05500.LogError(loException);
            }
            loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));


        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _loggerLMT05500.LogError(loException);
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }
                loConn.Dispose();
            }
        }
        loException.ThrowExceptionIfErrors();
    }

    protected override void R_Deleting(LMT05500DepositInfoDTO poEntity)
    {

        string lcMethodName = nameof(R_Deleting);
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerLMT05500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));

        R_Exception loException = new R_Exception();
        string lcQuery = null;
        R_Db loDb;
        DbCommand loCommand;
        DbConnection loConn = null;
        string lcAction = null;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            R_ExternalException.R_SP_Init_Exception(loConn);
            loCommand = loDb.GetCommand();
            lcAction = "DELETE";

            lcQuery = "RSP_PM_MAINTAIN_AGREEMENT_DEPOSIT";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poEntity.CCHARGE_MODE);
            loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poEntity.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poEntity.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poEntity.CUNIT_ID);

            loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
            loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poEntity.CSEQ_NO);

            loDb.R_AddCommandParameter(loCommand, "@LCONTRACTOR", DbType.Boolean,3, poEntity.LCONTRACTOR);
            loDb.R_AddCommandParameter(loCommand, "@CCONTRACTOR_ID", DbType.String, 20, poEntity.CCONTRACTOR_ID);
            loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_ID ", DbType.String, 20, poEntity.CDEPOSIT_ID);
            loDb.R_AddCommandParameter(loCommand, "@CDEPOSIT_DATE ", DbType.String, 8, poEntity.CDEPOSIT_DATE);

            loDb.R_AddCommandParameter(loCommand, "@NDEPOSIT_AMT", DbType.Decimal, int.MaxValue, poEntity.NDEPOSIT_AMT);
            loDb.R_AddCommandParameter(loCommand, "@CDESCRIPTION ", DbType.String, int.MaxValue, poEntity.CDESCRIPTION);

            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CACTION", DbType.String, 10, lcAction);


            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
            _loggerLMT05500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

            try
            {
                loDb.SqlExecNonQuery(loConn, loCommand, false);
                _loggerLMT05500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _loggerLMT05500.LogError(loException);
            }
            loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _loggerLMT05500.LogError(loException);
        }
        finally
        {
            if (loConn != null)
            {
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }
                loConn.Dispose();
            }
        }
        loException.ThrowExceptionIfErrors();
    }

    public LMT05500DepositHeaderDTO GetDepositHeader(LMT05500DBParameter poParameter)
    {
        string? lcMethodName = nameof(GetDepositHeader);
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerLMT05500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


        R_Exception loException = new R_Exception();
        LMT05500DepositHeaderDTO? loReturn = null;
        string lcQuery;
        DbCommand loCommand;
        R_Db loDb;
        try
        {
            loDb = new();
            DbConnection? loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            lcQuery = "RSP_PM_GET_AGREEMENT_DETAIL";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            //--SUDAH SESUAI SQL
            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

            loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 10, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 8, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
            _loggerLMT05500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<LMT05500DepositHeaderDTO>(loDataTable).FirstOrDefault();

        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _loggerLMT05500.LogError(loException);
        }

        if (loException.Haserror)
            loException.ThrowExceptionIfErrors();

        _loggerLMT05500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));

#pragma warning disable CS8603 // Possible null reference return.
        return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
    }

    public List<LMT05500DepositListDTO> GetDepositList(LMT05500DBParameter poParameter)
    {
        string? lcMethodName = nameof(GetDepositList);
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerLMT05500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


        R_Exception loException = new R_Exception();
        List<LMT05500DepositListDTO>? loReturn = null;
        string lcQuery;
        DbCommand loCommand;
        R_Db loDb;
        try
        {
            loDb = new();
            DbConnection? loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            lcQuery = "RSP_PM_GET_DEPOSIT_LIST";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

            loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 10, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 8, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);

            loDb.R_AddCommandParameter(loCommand, "@CCHARGE_MODE", DbType.String, 2, poParameter.CCHARGE_MODE);
            loDb.R_AddCommandParameter(loCommand, "@CBUILDING_ID", DbType.String, 20, poParameter.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCommand, "@CFLOOR_ID", DbType.String, 20, poParameter.CFLOOR_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUNIT_ID", DbType.String, 20, poParameter.CUNIT_ID);
            //CR 02 Okt 2024
            loDb.R_AddCommandParameter(loCommand, "@LINVOICED", DbType.Boolean, 2, poParameter.LINVOICED);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
            _loggerLMT05500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


            var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<LMT05500DepositListDTO>(loDataTable).ToList();

        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _loggerLMT05500.LogError(loException);
        }

        if (loException.Haserror)
            loException.ThrowExceptionIfErrors();

        _loggerLMT05500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
#pragma warning disable CS8603 // Possible null reference return.
        return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
    }
    public List<LMT05500DepositDetailListDTO> GetDepositDetailList(LMT05500DBParameter poParameter)
    {
        string? lcMethodName = nameof(GetDepositDetailList);
        using Activity activity = _activitySource.StartActivity(lcMethodName);
        _loggerLMT05500.LogInfo(string.Format("START process method {0} on Cls", lcMethodName));


        R_Exception loException = new R_Exception();
        List<LMT05500DepositDetailListDTO>? loReturn = null;
        string lcQuery;
        DbCommand loCommand;
        R_Db loDb;
        try
        {
            loDb = new();
            DbConnection? loConn = loDb.GetConnection();
            loCommand = loDb.GetCommand();
            lcQuery = "RSP_PM_GET_DEPOSIT_DETAIL_LIST";
            loCommand.CommandText = lcQuery;
            loCommand.CommandType = CommandType.StoredProcedure;

            loDb.R_AddCommandParameter(loCommand, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCommand, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);
            loDb.R_AddCommandParameter(loCommand, "@CDEPT_CODE", DbType.String, 10, poParameter.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CTRANS_CODE", DbType.String, 8, poParameter.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCommand, "@CREF_NO", DbType.String, 30, poParameter.CREF_NO);
            loDb.R_AddCommandParameter(loCommand, "@CSEQ_NO", DbType.String, 3, poParameter.CSEQ_NO);

            var loDbParam = loCommand.Parameters.Cast<DbParameter>()
                .Where(x => x != null && x.ParameterName.StartsWith("@"))
                .ToDictionary(x => x.ParameterName, x => x.Value);
            _loggerLMT05500.LogDebug("{@ObjectQuery} {@Parameter}", loCommand.CommandText, loDbParam);


            var loDataTable = loDb.SqlExecQuery(loConn, loCommand, true);
            loReturn = R_Utility.R_ConvertTo<LMT05500DepositDetailListDTO>(loDataTable).ToList();

        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _loggerLMT05500.LogError(loException);
        }

        if (loException.Haserror)
            loException.ThrowExceptionIfErrors();

        _loggerLMT05500.LogInfo(string.Format("END process method {0} on Cls", lcMethodName));
#pragma warning disable CS8603 // Possible null reference return.
        return loReturn;
#pragma warning restore CS8603 // Possible null reference return.
    }

}