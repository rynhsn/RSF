using System.Data;
using System.Data.Common;
using GSM05000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Back;

public class GSM05000NumberingCls : R_BusinessObject<GSM05000NumberingGridDTO>
{
    protected override GSM05000NumberingGridDTO R_Display(GSM05000NumberingGridDTO poEntity)
    {
        R_Exception loEx = new R_Exception();
        GSM05000NumberingGridDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_NUMBER_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_ID);
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000NumberingGridDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    protected override void R_Saving(GSM05000NumberingGridDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loEx = new R_Exception();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn;
        string lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            if (poCRUDMode == eCRUDMode.AddMode)
            {
                lcAction = "ADD";
            }
            else if (poCRUDMode == eCRUDMode.EditMode)
            {
                lcAction = "EDIT";
            }

            lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_NUMBERING";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CCYEAR", DbType.String, 4, poNewEntity.CCYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poNewEntity.CPERIOD_NO);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@ISTART_NUMBER", DbType.Int32, 8, poNewEntity.ISTART_NUMBER);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poNewEntity.CUSER_ID);
            loDb.SqlExecNonQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    protected override void R_Deleting(GSM05000NumberingGridDTO poEntity)
    {
        R_Exception loEx = new R_Exception();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn;
        string lcAction = "DELETE";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_NUMBERING";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CCYEAR", DbType.String, 50, poEntity.CCYEAR);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD_NO", DbType.String, 50, poEntity.CPERIOD_NO);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@ISTART_NUMBER", DbType.Int32, 8, poEntity.ISTART_NUMBER);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 10, poEntity.CUSER_ID);
            loDb.SqlExecNonQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    public List<GSM05000NumberingGridDTO> GetNumberingListDb(GSM05000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        List<GSM05000NumberingGridDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_NUMBER_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poParameter.CUSER_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000NumberingGridDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    public GSM05000NumberingHeaderDTO GetNumberingHeaderDb(GSM05000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        GSM05000NumberingHeaderDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"SELECT CTRANSACTION_CODE, 
                        CTRANSACTION_NAME, 
                        LDEPT_MODE, 
                        CPERIOD_MODE, 
                        (CASE WHEN CPERIOD_MODE = 'N' THEN 'None'
                              WHEN CPERIOD_MODE = 'P' THEN 'Periodically'
                              WHEN CPERIOD_MODE = 'Y' THEN 'Yearly' END) AS CPERIOD_MODE_DESCR
                        FROM GSM_TRANSACTION_CODE (NOLOCK)
                        WHERE CCOMPANY_ID = @CCOMPANY_ID
                        AND CTRANSACTION_CODE = @CTRANSACTION_CODE";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
            
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000NumberingHeaderDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    } 
}