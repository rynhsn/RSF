using GFF00900COMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GFF00900BACK
{
    public class GFF00900Cls
    {
        public RSP_ACTIVITY_VALIDITYDataDTO RSP_ACTIVITY_VALIDITYMethod(RSP_ACTIVITY_VALIDITYParameterDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            RSP_ACTIVITY_VALIDITYDataDTO loResult = new RSP_ACTIVITY_VALIDITYDataDTO();

            try
            {
                R_Db loDb = new R_Db();
                DbConnection loConn = loDb.GetConnection("R_DefaultConnectionString");

                string lcQuery = $"EXEC RSP_ACTIVITY_VALIDITY " +
                    $"'{poEntity.COMPANY_ID}', " +
                    $"'{poEntity.ACTIVITY_CODE}'";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

                loResult = loDb.SqlExecObjectQuery<RSP_ACTIVITY_VALIDITYDataDTO>(lcQuery, loConn, true).FirstOrDefault();
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();

            return loResult;
        }

        public void RSP_CREATE_ACTIVITY_APPROVAL_LOGMethod(GFF00900DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            R_Db loDb = new R_Db();
            DbConnection loConn = loDb.GetConnection();

            try
            {
                string lcQuery = $"EXEC RSP_CREATE_ACTIVITY_APPROVAL_LOG " +
                    $"'{poEntity.CCOMPANY_ID}', " +
                    $"'{poEntity.CACTION_CODE}', " +
                    $"'{poEntity.CCOMPANY_ID}', " +
                    $"'{poEntity.CCOMPANY_ID + "|" +poEntity.DETAIL_ACTION}', " +
                    $"'{DateTime.Now}', " +
                    $"'{poEntity.CUSER_ID}', " +
                    $"'{poEntity.CUSER_LOGIN_ID}', " +
                    $"'', " +
                    $"'{poEntity.DETAIL_ACTION}'";

                DbCommand loCmd = loDb.GetCommand();
                loCmd.CommandText = lcQuery;

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
            DbConnection loConn = loDb.GetConnection();

            try
            {
                DbCommand loCmd = loDb.GetCommand();
                string lcQuery = $"EXEC RSP_GS_VALIDATE_USER_ACT_APPR " +
                    $"'{poEntity.CCOMPANY_ID}', " +
                    $"'{poEntity.CUSER_ID}', " +
                    $"'{poEntity.CPASSWORD}', " +
                    $"'{poEntity.CACTION_CODE}', " +
                    $"'{poEntity.CUSER_LOGIN_ID}'";
                loCmd.CommandText = lcQuery;

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
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
        }
    }
}
