using System.Data;
using System.Data.Common;
using LMM01500Common;
using LMM01500Common.DTOs;
using R_BackEnd;
using R_Common;

namespace LMM01500Back;

public class LMM01500InitCls
{
    private LoggerLMM01500 _logger;

    public LMM01500InitCls()
    {
        _logger = LoggerLMM01500.R_GetInstanceLogger();
    }

    public List<LMM01500PropertyDTO> GetPropertyList(LMM01500ParameterDb poParameter)
    {
        R_Exception loEx = new();
        List<LMM01500PropertyDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = $"EXEC RSP_GS_GET_PROPERTY_LIST '{poParameter.CCOMPANY_ID}', '{poParameter.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMM01500PropertyDTO>(loDataTable).ToList();
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