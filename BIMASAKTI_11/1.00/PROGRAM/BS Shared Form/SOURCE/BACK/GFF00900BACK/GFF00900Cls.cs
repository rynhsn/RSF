using GFF00900COMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFF00900BACK
{
    public class GFF00900Cls
    {
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
