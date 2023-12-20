using System.Data;
using System.Data.Common;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_BackEnd;
using R_Common;

namespace GSM04500Back;

public class GSM04500InitCls
{
    private LoggerGSM04500 _logger;

    public GSM04500InitCls()
    {
        _logger = LoggerGSM04500.R_GetInstanceLogger();
    }

    public List<GSM04500PropertyDTO> GetPropertyList(GSM04500ParameterDb poParameter)
    {
        R_Exception loEx = new();
        List<GSM04500PropertyDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery =
                @$"EXEC RSP_GS_GET_PROPERTY_LIST '{poParameter.CCOMPANY_ID}', '{poParameter.CUSER_ID}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM04500PropertyDTO>(loDataTable).ToList();
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

    public List<GSM04500FunctionDTO> GetTypeList(GSM04500ParameterDb dbPar)
    {
        R_Exception loEx = new();
        List<GSM04500FunctionDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"SELECT * FROM RFT_GET_GSB_CODE_INFO ('BIMASAKTI', '{dbPar.CCOMPANY_ID}', '_BS_JOURNAL_GRP_TYPE', '', '{dbPar.CLANGUAGE_ID}')";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            _logger.LogDebug("{pcQuery}", lcQuery);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM04500FunctionDTO>(loDataTable).ToList();
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