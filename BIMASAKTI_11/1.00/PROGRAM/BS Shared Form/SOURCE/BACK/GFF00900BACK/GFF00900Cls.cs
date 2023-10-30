using GFF00900COMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GFF00900BACK
{
    public class GFF00900Cls
    {
        public List<RSP_ACTIVITY_VALIDITYDataDTO> RSP_ACTIVITY_VALIDITYMethod(RSP_ACTIVITY_VALIDITYParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            List<RSP_ACTIVITY_VALIDITYDataDTO> loResult = null;
            DbConnection loConn = loDb.GetConnection();
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                lcQuery = $"EXEC RSP_ACTIVITY_VALIDITY " +
                    $"@COMPANY_ID, " +
                    $"@ACTIVITY_CODE";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@COMPANY_ID", DbType.String, 50, poEntity.COMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@ACTIVITY_CODE", DbType.String, 50, poEntity.ACTIVITY_CODE);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    var loDataTable = loDb.SqlExecQuery(loConn, loCmd, false);
                    loResult = R_Utility.R_ConvertTo<RSP_ACTIVITY_VALIDITYDataDTO>(loDataTable).ToList();
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
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
            loException.ThrowExceptionIfErrors();
            return loResult;
        }

        public void RSP_CREATE_ACTIVITY_APPROVAL_LOGMethod(GFF00900DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                loConn = loDb.GetConnection();
                lcQuery = $"EXEC RSP_CREATE_ACTIVITY_APPROVAL_LOG " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CACTION_CODE, " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CCOMPANY_ID_DETAIL_ACTION, " +
                                 $"@CURRENT_DATETIME, " +
                                 $"@CUSER_ID, " +
                                 $"@CUSER_LOGIN_ID, " +
                                 $"'', " +
                                 $"@DETAIL_ACTION";
/*
                string lcQuery = $"EXEC RSP_CREATE_ACTIVITY_APPROVAL_LOG " +
                    $"'{poEntity.CCOMPANY_ID}', " +
                    $"'{poEntity.CACTION_CODE}', " +
                    $"'{poEntity.CCOMPANY_ID}', " +
                    $"'{poEntity.CCOMPANY_ID + "|" + poEntity.DETAIL_ACTION}', " +
                    $"'{DateTime.Now}', " +
                    $"'{poEntity.CUSER_ID}', " +
                    $"'{poEntity.CUSER_LOGIN_ID}', " +
                    $"'', " +
                    $"'{poEntity.DETAIL_ACTION}'";
*/

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CACTION_CODE", DbType.String, 50, poEntity.CACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID_DETAIL_ACTION", DbType.String, 100, poEntity.CCOMPANY_ID + "|" + poEntity.DETAIL_ACTION);
                loDb.R_AddCommandParameter(loCmd, "@CURRENT_DATETIME", DbType.DateTime, 8, DateTime.Now);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);
                loDb.R_AddCommandParameter(loCmd, "@DETAIL_ACTION", DbType.String, 100, poEntity.DETAIL_ACTION);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
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
            loException.ThrowExceptionIfErrors();
            /*
            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"EXEC RSP_CREATE_ACTIVITY_APPROVAL_LOG " +
                    $"'{poEntity.P_CCOMPANY_ID}', " +
                    $"'{poEntity.P_CAPPROVAL_CODE}', " +
                    $"'{poEntity.P_CREFERENCE_NO}', " +
                    $"'{poEntity.P_CREFERENCE_INFO}', " +
                    $"'{poEntity.P_DAPPROVAL_DATE}', " +
                    $"'{poEntity.P_CAPPROVAL_USER_ID}', " +
                    $"'{poEntity.P_CACTIVITY_USER_ID}', " +
                    $"'{poEntity.P_CREASON_CODE}', " +
                    $"'{poEntity.P_CAPPROVAL_NOTE}'";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.SqlExecNonQuery(loConn, loCmd, true);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();*/
        }

        public void UsernameAndPasswordValidationMethod(GFF00900DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = null;
            DbCommand loCmd = null;
            string lcQuery;

            try
            {
                /*
                string lcQuery = $"EXEC RSP_GS_VALIDATE_USER_ACT_APPR " +
                    $"'{poEntity.CCOMPANY_ID}', " +
                    $"'{poEntity.CUSER_ID}', " +
                    $"'{poEntity.CPASSWORD}', " +
                    $"'{poEntity.CACTION_CODE}', " +
                    $"'{poEntity.CUSER_LOGIN_ID}'";
                */
                loConn = loDb.GetConnection();

                lcQuery = $"EXEC RSP_GS_VALIDATE_USER_ACT_APPR " +
                                 $"@CCOMPANY_ID, " +
                                 $"@CUSER_ID, " +
                                 $"@CPASSWORD, " +
                                 $"@CACTION_CODE, " +
                                 $"@CUSER_LOGIN_ID";

                loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPASSWORD", DbType.String, 50, poEntity.CPASSWORD);
                loDb.R_AddCommandParameter(loCmd, "@CACTION_CODE", DbType.String, 50, poEntity.CACTION_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);

                R_ExternalException.R_SP_Init_Exception(loConn);

                try
                {
                    loDb.SqlExecNonQuery(loConn, loCmd, false);
                }
                catch (Exception ex)
                {
                    loException.Add(ex);
                }

                loException.Add(R_ExternalException.R_SP_Get_Exception(loConn));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            finally
            {
                if (loConn != null)
                {
                    if (loConn.State != System.Data.ConnectionState.Closed)
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
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
    }
}
