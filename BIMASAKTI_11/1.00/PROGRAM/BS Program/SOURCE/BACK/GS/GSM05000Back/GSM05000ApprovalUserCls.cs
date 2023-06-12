using System.Data;
using System.Data.Common;
using GSM05000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Back;

public class GSM05000ApprovalUserCls: R_BusinessObject<GSM05000ApprovalUserDTO>
{
    protected override GSM05000ApprovalUserDTO R_Display(GSM05000ApprovalUserDTO poEntity)
    {
        var loEx = new R_Exception();
        GSM05000ApprovalUserDTO loRtn = null;
        try
        {
            loRtn = poEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    protected override void R_Saving(GSM05000ApprovalUserDTO poNewEntity, eCRUDMode poCRUDMode)
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

                lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_APPROVER";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poNewEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 255, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 10, poNewEntity.CSEQUENCE);
            loDb.R_AddCommandParameter(loCmd, "@LREPLACEMENT", DbType.Boolean, 1, poNewEntity.LREPLACEMENT);
            loDb.R_AddCommandParameter(loCmd, "@NLIMIT_AMOUNT", DbType.Int32, 20, poNewEntity.NLIMIT_AMOUNT);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 10, poNewEntity.CUSER_LOGIN_ID);
            loDb.SqlExecNonQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    protected override void R_Deleting(GSM05000ApprovalUserDTO poEntity)
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
            
            lcQuery = "RSP_GS_MAINTAIN_TRANS_CODE_APPROVER";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 255, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 10, poEntity.CSEQUENCE);
            loDb.R_AddCommandParameter(loCmd, "@LREPLACEMENT", DbType.Boolean, 1, poEntity.LREPLACEMENT);
            loDb.R_AddCommandParameter(loCmd, "@NLIMIT_AMOUNT", DbType.Int32, 20, poEntity.NLIMIT_AMOUNT);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 10, poEntity.CUSER_LOGIN_ID);
            loDb.SqlExecNonQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }
    
    public List<GSM05000ApprovalUserDTO> GSM05000GetApprovalUser(GSM05000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        List<GSM05000ApprovalUserDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_TRANS_CODE_APPROVER_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;
            
            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poParameter.CUSER_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalUserDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public List<GSM05000ApprovalDepartmentDTO> GSM05000GetApprovalDepartment(GSM05000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        List<GSM05000ApprovalDepartmentDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"SELECT CDEPT_CODE, CDEPT_NAME FROM GSM_DEPARTMENT (NOLOCK) WHERE CCOMPANY_ID = '{poParameter.CCOMPANY_ID}' AND LACTIVE = '1'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalDepartmentDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    
    public GSM05000ApprovalHeaderDTO GSM05000GetApprovalHeader (GSM05000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        GSM05000ApprovalHeaderDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"SELECT CTRANSACTION_CODE
                        , CTRANSACTION_NAME
                        , LDEPT_MODE
                        , CPERIOD_MODE
                        , CAPPROVAL_MODE
                        , ISNULL((CASE WHEN CAPPROVAL_MODE = '1' THEN 'Hierarchy'
                                        WHEN CAPPROVAL_MODE = '2' THEN 'Flat And'
                                        WHEN CAPPROVAL_MODE = '3' THEN 'Flat Or' END),'') AS CAPPROVAL_MODE_DESCR
                        FROM GSM_TRANSACTION_CODE (NOLOCK)
                        WHERE CCOMPANY_ID = '{poParameter.CCOMPANY_ID}'
                        AND CTRANSACTION_CODE = '{poParameter.CTRANSACTION_CODE}'";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM05000ApprovalHeaderDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    
    public string GSM05000ValidationForAction (GSM05000ParameterDb poParameter)
    {
        R_Exception loEx = new R_Exception();
        string lcRtn = "";
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"SELECT TOP 1 1 FROM GSM_TRANS_APV_REPLACEMENT (NOLOCK)
			                WHERE CCOMPANY_ID = @CCOMPANY_ID
			                AND CTRANSACTION_CODE = @CTRANSACTION_CODE
			                AND CDEPT_CODE = @CDEPT_CODE
			                AND CUSER_ID = @CUSER_ID";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poParameter.CTRANSACTION_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poParameter.CUSER_ID);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            lcRtn = R_Utility.R_ConvertTo<string>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return lcRtn;
    }
}