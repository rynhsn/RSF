using System.Data;
using System.Data.Common;
using System.Diagnostics;
using APR00500Common;
using APR00500Common.DTOs;
using APR00500Common.DTOs.Print;
using R_BackEnd;
using R_Common;

namespace APR00500Back;

public class APR00500Cls
{
    private LoggerAPR00500 _logger;
    private readonly ActivitySource _activitySource;

    public APR00500Cls()
    {
        _logger = LoggerAPR00500.R_GetInstanceLogger();
        _activitySource = APR00500Activity.R_GetInstanceActivitySource();
    }

    public List<APR00500PropertyDTO> GetPropertyList(APR00500ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList));
        R_Exception loEx = new();
        List<APR00500PropertyDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"EXEC RSP_GS_GET_PROPERTY_LIST '{poParam.CCOMPANY_ID}', '{poParam.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<APR00500PropertyDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public APR00500PeriodYearRangeDTO GetYearRange(APR00500ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetYearRange));
        R_Exception loEx = new();
        APR00500PeriodYearRangeDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_PERIOD_YEAR_RANGE '{poParams.CCOMPANY_ID}', '', ''";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<APR00500PeriodYearRangeDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public APR00500SystemParamDTO GetSystemParam(APR00500ParameterDb poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetSystemParam));
        R_Exception loEx = new();
        APR00500SystemParamDTO loReturn = new();
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_CB_GET_SYSTEM_PARAM '{poParams.CCOMPANY_ID}', '{poParams.CLANGUAGE_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var DataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loReturn = R_Utility.R_ConvertTo<APR00500SystemParamDTO>(DataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    public APR00500TransCodeInfoDTO GetTransCodeInfo(APR00500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetTransCodeInfo));
        R_Exception loEx = new();
        APR00500TransCodeInfoDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            // var loTransCode = "110010";

            lcQuery = @$"EXEC RSP_GS_GET_TRANS_CODE_INFO '{poParam.CCOMPANY_ID}', '{poParam.CTRANS_CODE}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<APR00500TransCodeInfoDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public List<APR00500PeriodDTO> GetPeriodList(APR00500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetPeriodList));
        R_Exception loEx = new();
        List<APR00500PeriodDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_PERIOD_DT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CYEAR", DbType.String, 4, poParam.CYEAR);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CYEAR"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<APR00500PeriodDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }


    public List<APR00500DataResultDTO> GetReportData(APR00500ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetReportData));
        R_Exception loEx = new();
        List<APR00500DataResultDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_APR00500_GET_REPORT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCUT_OFF_DATE", DbType.String, 8, poParam.CCUT_OFF_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_PERIOD", DbType.String, 6, poParam.CFROM_PERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CTO_PERIOD", DbType.String, 6, poParam.CTO_PERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_REFERENCE_DATE", DbType.String, 8, poParam.CFROM_REFERENCE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_REFERENCE_DATE", DbType.String, 8, poParam.CTO_REFERENCE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_DUE_DATE", DbType.String, 8, poParam.CFROM_DUE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTO_DUE_DATE", DbType.String, 8, poParam.CTO_DUE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CSUPPLIER_ID", DbType.String, 20, poParam.CSUPPLIER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_REFERENCE_NO", DbType.String, 20, poParam.CFROM_REFERENCE_NO);
            loDb.R_AddCommandParameter(loCmd, "@CTO_REFERENCE_NO", DbType.String, 20, poParam.CTO_REFERENCE_NO);
            loDb.R_AddCommandParameter(loCmd, "@CCURRENCY", DbType.String, 3, poParam.CCURRENCY);
            loDb.R_AddCommandParameter(loCmd, "@NFROM_TOTAL_AMOUNT", DbType.Decimal, int.MaxValue, poParam.NFROM_TOTAL_AMOUNT);
            loDb.R_AddCommandParameter(loCmd, "@NTO_TOTAL_AMOUNT", DbType.Decimal, int.MaxValue, poParam.NTO_TOTAL_AMOUNT);
            loDb.R_AddCommandParameter(loCmd, "@NFROM_REMAINING_AMOUNT", DbType.Decimal, int.MaxValue, poParam.NFROM_REMAINING_AMOUNT);
            loDb.R_AddCommandParameter(loCmd, "@NTO_REMAINING_AMOUNT", DbType.Decimal, int.MaxValue, poParam.NTO_REMAINING_AMOUNT);
            loDb.R_AddCommandParameter(loCmd, "@IFROM_DAYS_LATE", DbType.Int32, int.MaxValue, poParam.IFROM_DAYS_LATE);
            loDb.R_AddCommandParameter(loCmd, "@ITO_DAYS_LATE", DbType.Int32, int.MaxValue, poParam.ITO_DAYS_LATE);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 2, poParam.CLANG_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CCUT_OFF_DATE" or
                        "@CDEPT_CODE" or
                        "@CFROM_PERIOD" or
                        "@CTO_PERIOD" or
                        "@CFROM_REFERENCE_DATE" or
                        "@CTO_REFERENCE_DATE" or
                        "@CFROM_DUE_DATE" or
                        "@CTO_DUE_DATE" or
                        "@CSUPPLIER_ID" or
                        "@CFROM_REFERENCE_NO" or
                        "@CTO_REFERENCE_NO" or
                        "@CCURRENCY" or
                        "@NFROM_TOTAL_AMOUNT" or
                        "@NTO_TOTAL_AMOUNT" or
                        "@NFROM_REMAINING_AMOUNT" or
                        "@NTO_REMAINING_AMOUNT" or
                        "@IFROM_DAYS_LATE" or
                        "@ITO_DAYS_LATE" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<APR00500DataResultDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public APR00500PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(string pcCompanyId)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany));
        var loEx = new R_Exception();
        APR00500PrintBaseHeaderLogoDTO loResult = null;
        R_Db loDb = null; // Database object    
        DbConnection loConn = null;
        DbCommand loCmd = null;


        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            var lcQuery = $"SELECT dbo.RFN_GET_COMPANY_LOGO('{pcCompanyId}') as BLOGO";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            loResult = R_Utility.R_ConvertTo<APR00500PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();
            
            //ambil company name
            lcQuery = $"SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = '{pcCompanyId}'"; // Query to get company name
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            //Debug Logs
            _logger.LogDebug(string.Format("SELECT CCOMPANY_NAME FROM SAM_COMPANIES WHERE CCOMPANY_ID = '@CCOMPANY_ID'", pcCompanyId));
            loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            var loCompanyNameResult = R_Utility.R_ConvertTo<APR00500PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

            loResult!.CCOMPANY_NAME = loCompanyNameResult?.CCOMPANY_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex); // Add the exception to the exception object
            _logger.LogError(loEx); // Log the exception
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
            if (loCmd != null)
            {
                loCmd.Dispose();
                loCmd = null;
            }
        }

        loEx.ThrowExceptionIfErrors();

        return loResult;
    }

    #region GetCodeInfoList

    // public List<APR00500FunctDTO> GetCodeInfoList(APR00500ParameterDb poParameter)
    // {
    //     using var loActivity = _activitySource.StartActivity(nameof(GetCodeInfoList));
    //     R_Exception loEx = new();
    //     List<APR00500FunctDTO> loRtn = null;
    //     R_Db loDb;
    //     DbConnection loConn;
    //     DbCommand loCmd;
    //     string lcQuery;
    //
    //     try
    //     {
    //         loDb = new R_Db();
    //         loConn = loDb.GetConnection();
    //         loCmd = loDb.GetCommand();
    //
    //         lcQuery =
    //             $"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '{poParameter.CCOMPANY_ID}' , '_ROUNDING_MODE', '', '{poParameter.CLANGUAGE_ID}')";
    //         loCmd.CommandType = CommandType.Text;
    //         loCmd.CommandText = lcQuery;
    //
    //         _logger.LogDebug("{poQuery}", lcQuery);
    //
    //         var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
    //         loRtn = R_Utility.R_ConvertTo<APR00500FunctDTO>(loDataTable).ToList();
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);            
    //         _logger.LogError(loEx);
    //     }
    //
    //     loEx.ThrowExceptionIfErrors();
    //
    //     return loRtn;
    // }

    #endregion
}

public class APR00500ParameterDb
{
    public string CCOMPANY_ID { get; set; } = "";
    public string CUSER_ID { get; set; } = "";
    public string CLANGUAGE_ID { get; set; } = "";
    public string CTRANS_CODE { get; set; } = "110010";
    public string CYEAR { get; set; } = "";

    public string CPROPERTY_ID { get; set; } = "";
    public string CPROPERTY_NAME { get; set; } = "";
    public string CCUT_OFF_DATE { get; set; } = "";
    public string CDEPT_CODE { get; set; } = "";
    public string CFROM_PERIOD { get; set; } = "";
    public string CTO_PERIOD { get; set; } = "";
    public string CFROM_REFERENCE_DATE { get; set; } = "";
    public string CTO_REFERENCE_DATE { get; set; } = "";
    public string CFROM_DUE_DATE { get; set; } = "";
    public string CTO_DUE_DATE { get; set; } = "";
    public string CSUPPLIER_ID { get; set; } = "";
    public string CFROM_REFERENCE_NO { get; set; } = "";
    public string CTO_REFERENCE_NO { get; set; } = "";
    public string CCURRENCY { get; set; } = "";
    public decimal NFROM_TOTAL_AMOUNT { get; set; }
    public decimal NTO_TOTAL_AMOUNT { get; set; }
    public decimal NFROM_REMAINING_AMOUNT { get; set; }
    public decimal NTO_REMAINING_AMOUNT { get; set; }
    public int IFROM_DAYS_LATE { get; set; }
    public int ITO_DAYS_LATE { get; set; }
    public string CLANG_ID { get; set; } = "";
}