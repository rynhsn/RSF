using System.Data;
using System.Data.Common;
using LMM02500Common;
using LMM02500Common.DTOs;
using R_BackEnd;
using R_Common;

namespace LMM02500Back;

public class LMM02500InitCls
{
    private LoggerLMM02500 _logger;

    public LMM02500InitCls()
    {
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }

    public List<LMM02500PropertyDTO> GetPropertyList(LMM02500ParameterDb poParameter)
    {
        R_Exception loEx = new();
        List<LMM02500PropertyDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"EXEC RSP_GS_GET_PROPERTY_LIST '{poParameter.CCOMPANY_ID}', '{poParameter.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMM02500PropertyDTO>(loDataTable).ToList();
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