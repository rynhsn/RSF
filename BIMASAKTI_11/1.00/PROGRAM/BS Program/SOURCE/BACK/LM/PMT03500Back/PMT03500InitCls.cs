using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT03500Common;
using PMT03500Common.DTOs;
using R_BackEnd;
using R_Common;

namespace PMT03500Back;

public class PMT03500InitCls
{
    private LoggerPMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public PMT03500InitCls()
    {
        _logger = LoggerPMT03500.R_GetInstanceLogger();
        _activitySource = PMT03500Activity.R_GetInstanceActivitySource();
    }

    public List<PMT03500PropertyDTO> GetPropertyList(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        List<PMT03500PropertyDTO> loRtn = null;
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

            loRtn = R_Utility.R_ConvertTo<PMT03500PropertyDTO>(loDataTable).ToList();
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

    public List<PMT03500TransCodeDTO> GetTransCodeList(PMT03500ParameterDb poParam)
    {
        R_Exception loEx = new();
        List<PMT03500TransCodeDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"RSP_GS_GET_TRANS_CODE_INFO";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poParam.CTRANS_CODE);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CTRANS_CODE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT03500TransCodeDTO>(loDataTable).ToList();
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
}