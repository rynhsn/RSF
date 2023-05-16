using Lookup_GSCOMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSLBACK
{
    public class PublicLookupCls
    {
        public List<GSL00500DTO> GetALLGLAccount(GSL00500ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00500DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_GL_ACCOUNT_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CPROPERTY_ID = '{poEntity.CPROPERTY_ID}', " +
                    $"@CPROGRAM_CODE = '{poEntity.CPROGRAM_CODE}', " +
                    $"@CBSIS = '{poEntity.CBSIS}', " +
                    $"@CDBCR = '{poEntity.CDBCR}', " +
                    $"@LCENTER_RESTR = '{poEntity.LCENTER_RESTR}', " +
                    $"@LUSER_RESTR = '{poEntity.LUSER_RESTR}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}', " +
                    $"@CCENTER_CODE = '{poEntity.CCENTER_CODE}', " +
                    $"@CUSER_LANGUAGE = '{poEntity.CUSER_LANGUAGE}' ";

               loResult = loDb.SqlExecObjectQuery<GSL00500DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01500ResultGroupDTO> GetALLCashFlowGroup(GSL01500ParameterGroupDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01500ResultGroupDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_CASHFLOW_GRP_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CUSER_LOGIN_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL01500ResultGroupDTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01500ResultDetailDTO> GetALLCashFlowDetail(GSL01500ParameterDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01500ResultDetailDTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"EXEC RSP_GS_GET_CASHFLOW_LIST  " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CCASH_FLOW_GROUP_CODE = '{poEntity.CCASH_FLOW_GROUP_CODE}', " +
                    $"@CUSER_LOGIN_ID = '{poEntity.CUSER_ID}' ";

                loResult = loDb.SqlExecObjectQuery<GSL01500ResultDetailDTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
