using System.Data;
using System.Data.Common;
using GSM05000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Back;

public class GSM05000Cls : R_BusinessObject<GSM05000DTO>
{
    protected override GSM05000DTO R_Display(GSM05000DTO poEntity)
    {
        R_Exception loEx = new R_Exception();
        GSM05000DTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"Select A.* From GSM_TRANSACTION_CODE A (Nolock) 
                        Where A.CCOMPANY_ID = @CCOMPANY_ID
                        AND A.CTRANSACTION_CODE = @CTRANSACTION_CODE";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 30, poEntity.CTRANSACTION_CODE);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000DTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    protected override void R_Deleting(GSM05000DTO poEntity)
    {
        throw new NotImplementedException();
    }

    protected override void R_Saving(GSM05000DTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loEx = new R_Exception();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            if (poCRUDMode == eCRUDMode.EditMode)
            {
                lcQuery = @"UPDATE GSM_TRANSACTION_CODE SET 
                        LINCREMENT_FLAG = @LINCREMENT_FLAG 
                      , LDEPT_MODE = @LDEPT_MODE 
                      , CDEPT_DELIMITER = @CDEPT_DELIMITER 
                      , LTRANSACTION_MODE = @LTRANSACTION_MODE 
                      , CTRANSACTION_DELIMITER = @CTRANSACTION_DELIMITER 
                      , CPERIOD_MODE = @CPERIOD_MODE 
                      , CYEAR_FORMAT = @CYEAR_FORMAT 
                      , CPERIOD_DELIMITER = @CPERIOD_DELIMITER 
                      , INUMBER_LENGTH = @INUMBER_LENGTH 
                      , CNUMBER_DELIMITER = @CNUMBER_DELIMITER 
                      , CPREFIX = @CPREFIX 
                      , CPREFIX_DELIMITER = @CPREFIX_DELIMITER 
                      , CSUFFIX = @CSUFFIX 
                      , CSEQUENCE01 = @CSEQUENCE01 
                      , CSEQUENCE02 = @CSEQUENCE02 
                      , CSEQUENCE03 = @CSEQUENCE03 
                      , CSEQUENCE04 = @CSEQUENCE04 
                      , LAPPROVAL_FLAG = @LAPPROVAL_FLAG 
                      , LUSE_THIRD_PARTY = @LUSE_THIRD_PARTY 
                      , CAPPROVAL_MODE = @CAPPROVAL_MODE 
                      , LAPPROVAL_DEPT = @LAPPROVAL_DEPT 
                      , CUPDATE_BY = @CUPDATE_BY 
                      , DUPDATE_DATE = @DUPDATE_DATE
                        WHERE CCOMPANY_ID = @CCOMPANY_ID AND CTRANSACTION_CODE = @CTRANSACTION_CODE";

                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@LINCREMENT_FLAG", DbType.Boolean, 1, poNewEntity.LINCREMENT_FLAG);
                loDb.R_AddCommandParameter(loCmd, "@LDEPT_MODE", DbType.Boolean, 1, poNewEntity.LDEPT_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_DELIMITER", DbType.String, 1, poNewEntity.CDEPT_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@LTRANSACTION_MODE", DbType.Boolean, 1,
                    poNewEntity.LTRANSACTION_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_DELIMITER", DbType.String, 1,
                    poNewEntity.CTRANSACTION_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_MODE", DbType.String, 1, poNewEntity.CPERIOD_MODE);
                loDb.R_AddCommandParameter(loCmd, "@CYEAR_FORMAT", DbType.String, 4, poNewEntity.CYEAR_FORMAT);
                loDb.R_AddCommandParameter(loCmd, "@CPERIOD_DELIMITER", DbType.String, 1,
                    poNewEntity.CPERIOD_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@INUMBER_LENGTH", DbType.Int32, 4, poNewEntity.INUMBER_LENGTH);
                loDb.R_AddCommandParameter(loCmd, "@CNUMBER_DELIMITER", DbType.String, 1,
                    poNewEntity.CNUMBER_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@CPREFIX", DbType.String, 10, poNewEntity.CPREFIX);
                loDb.R_AddCommandParameter(loCmd, "@CPREFIX_DELIMITER", DbType.String, 1,
                    poNewEntity.CPREFIX_DELIMITER);
                loDb.R_AddCommandParameter(loCmd, "@CSUFFIX", DbType.String, 10, poNewEntity.CSUFFIX);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE01", DbType.String, 10, poNewEntity.CSEQUENCE01);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE02", DbType.String, 10, poNewEntity.CSEQUENCE02);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE03", DbType.String, 10, poNewEntity.CSEQUENCE03);
                loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE04", DbType.String, 10, poNewEntity.CSEQUENCE04);
                loDb.R_AddCommandParameter(loCmd, "@LAPPROVAL_FLAG", DbType.Boolean, 1, poNewEntity.LAPPROVAL_FLAG);
                loDb.R_AddCommandParameter(loCmd, "@LUSE_THIRD_PARTY", DbType.Boolean, 1, poNewEntity.LUSE_THIRD_PARTY);
                loDb.R_AddCommandParameter(loCmd, "@CAPPROVAL_MODE", DbType.String, 1, poNewEntity.CAPPROVAL_MODE);
                loDb.R_AddCommandParameter(loCmd, "@LAPPROVAL_DEPT", DbType.Boolean, 1, poNewEntity.LAPPROVAL_DEPT);
                loDb.R_AddCommandParameter(loCmd, "@CUPDATE_BY", DbType.String, 10, poNewEntity.CUPDATE_BY);
                loDb.R_AddCommandParameter(loCmd, "@DUPDATE_DATE", DbType.DateTime, 8, poNewEntity.DUPDATE_DATE);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poNewEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 10,
                    poNewEntity.CTRANSACTION_CODE);

                loDb.SqlExecNonQuery(loConn, loCmd, true);
                
            }
            else
            {
                loEx.Add("403", "Mode is not allowed");
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    //method get semua data
    public List<GSM05000GridDTO> GetTransactionCodeListDb(GSM05000ParameterDb poParameterDb)
    {
        R_Exception loEx = new R_Exception();
        List<GSM05000GridDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @$"Select A.*, CMODULE_NAME = Isnull((Select Top 1 RTRIM(CMODULE_NAME)
                      From SAM_PROGRAM (Nolock) Where CMODULE_ID = A.CMODULE_ID), '***Not found***') 
                      From GSM_TRANSACTION_CODE A (Nolock) 
                      WHERE A.CCOMPANY_ID = '{poParameterDb.CCOMPANY_ID}' 
                      ORDER BY A.CTRANSACTION_CODE, A.CMODULE_ID ASC";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000GridDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    
    public GSM05000ExistDTO GetValidateUpdateDb(GSM05000ParameterDb poEntity)
    {
        R_Exception loEx = new R_Exception();
        GSM05000ExistDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"select top 1 1 as EXIST 
                          from GSM_TRANSACTION_NUMBER (nolock) 
                         where CCOMPANY_ID = @CCOMPANY_ID 
                           and CTRANSACTION_CODE = @CTRANSACTION_CODE";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 10, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 30, poEntity.CTRANSACTION_CODE);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000ExistDTO>(loDataTable).FirstOrDefault();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public List<GSM05000DelimiterDTO> GetDelimiterListDb(GSM05000ParameterDb poParameterDb)
    {
        R_Exception loEx = new R_Exception();
        List<GSM05000DelimiterDTO> loRtn = null;
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
                @$"SELECT * FROM RFT_GET_GSB_CODE_INFO ('SIAPP', '{poParameterDb.CCOMPANY_ID}', '_GS_REFNO_DELIMITER', '', '{poParameterDb.CLANGUAGE_ID}')";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM05000DelimiterDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}