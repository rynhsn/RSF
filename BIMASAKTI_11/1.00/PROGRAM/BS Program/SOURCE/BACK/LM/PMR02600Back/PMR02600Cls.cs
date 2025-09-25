using PMR02600Common;
using PMR02600Common.DTOs;
using PMR02600Common.DTOs.Print;
using PMR02600Common.Print;
using R_BackEnd;
using R_Common;
using R_Storage;
using R_StorageCommon;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Windows.Input;

namespace PMR02600Back;

public class PMR02600Cls
{
    private LoggerPMR02600 _logger;
    private readonly ActivitySource _activitySource;

    public PMR02600Cls()
    {
        _logger = LoggerPMR02600.R_GetInstanceLogger();
        _activitySource = PMR02600Activity.R_GetInstanceActivitySource();
    }

    public List<PMR02600PropertyDTO> GetPropertyList(PMR02600ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetPropertyList));
        R_Exception loEx = new();
        List<PMR02600PropertyDTO> loRtn = null;
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

            loRtn = R_Utility.R_ConvertTo<PMR02600PropertyDTO>(loDataTable).ToList();
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
    
    public List<PMR02600DataResultDTO> GetReportData(PMR02600ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetReportData));
        R_Exception loEx = new();
        List<PMR02600DataResultDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PMR02600_OCCUPANCY_GET_REPORT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParam.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFROM_BUILDING", DbType.String, 20, poParam.CFROM_BUILDING);
            loDb.R_AddCommandParameter(loCmd, "@CTO_BUILDING", DbType.String, 20, poParam.CTO_BUILDING);
            loDb.R_AddCommandParameter(loCmd, "@CCUT_OFF_DATE", DbType.String, 8, poParam.CCUT_OFF_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 3, poParam.CLANG_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CUSER_ID" or
                        "@CPROPERTY_ID" or
                        "@CFROM_BUILDING" or
                        "@CTO_BUILDING" or
                        "@CCUT_OFF_DATE" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMR02600DataResultDTO>(loDataTable).ToList();
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
    
    public PMR02600PrintBaseHeaderLogoDTO GetBaseHeaderLogoCompany(PMR02600ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetBaseHeaderLogoCompany));
        var loEx = new R_Exception();
        PMR02600PrintBaseHeaderLogoDTO loResult = null;
        R_Db loDb = null; // Database object    
        DbConnection loConn = null;
        DbCommand loCmd = null;

    
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection(R_Db.eDbConnectionStringType.ReportConnectionString);
            loCmd = loDb.GetCommand();

            //var lcQuery = $"SELECT dbo.RFN_GET_COMPANY_LOGO('{pcCompanyId}') as BLOGO";
            //loCmd.CommandText = lcQuery;
            //loCmd.CommandType = CommandType.Text;

            //_logger.LogDebug("{pcQuery}", lcQuery);

            //var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            //loResult = R_Utility.R_ConvertTo<PMR02600PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();


            var lcQuery = "RSP_GS_GET_PROPERTY_DETAIL";
            loCmd = loDb.GetCommand();
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poParam.CPROPERTY_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x != null && x.ParameterName.StartsWith("@"))
                .Select(x => x.Value);
            _logger.LogDebug("EXEC {lcQuery} {@Parameters}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            loResult = R_Utility.R_ConvertTo<PMR02600PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

            if (string.IsNullOrEmpty(loResult.CSTORAGE_ID) == false)
            {
                var loReadParameter = new R_ReadParameter()
                {
                    StorageId = loResult.CSTORAGE_ID
                };

                var loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);

                loResult.BLOGO = loReadResult.Data;
            }

            //ambil company name
            lcQuery = $"EXEC RSP_GS_GET_COMPANY_INFO '{poParam.CCOMPANY_ID}'"; // Query to get company name
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.Text;

            //Debug Logs
            _logger.LogDebug(lcQuery);
            loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
            var loCompanyNameResult = R_Utility.R_ConvertTo<PMR02600PrintBaseHeaderLogoDTO>(loDataTable).FirstOrDefault();

            loResult!.CCOMPANY_NAME = loCompanyNameResult?.CCOMPANY_NAME;
            loResult.CDATETIME_NOW = loCompanyNameResult.CDATETIME_NOW;

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
}