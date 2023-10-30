using Lookup_GSCOMMON.DTOs;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_GSLBACK
{
    public class PublicLookupCls
    {
        public List<GSL00100DTO> GetALLSalesTax(GSL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00100DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_SALES_TAX_LIST  " +
                    $"@CCOMPANY_ID, " +
                    $"@CUSER_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00100DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00200DTO> GetALLWithholdingTax(GSL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00200DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_WITHHOLDING_LOOKUP_LIST @CCOMPANY_ID, @CPROPERTY_ID, @CTAX_TYPE_LIST";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTAX_TYPE_LIST", DbType.String, 50, poEntity.CTAX_TYPE_LIST);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00200DTO>(loDataTable).ToList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00300DTO> GetALLCurrency(GSL00300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00300DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCURRENCY_CODE, CCURRENCY_NAME, CCURRENCY_SYMBOL FROM GSM_CURRENCY (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CCOMPANY_ID ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00300DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00400DTO> GetALLJournalGroup(GSL00400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00400DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CJRNGRP_CODE, CJRNGRP_NAME, CJRNGRP_TYPE, LACCRUAL FROM GSM_JRNGRP (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    $"AND CPROPERTY_ID = @CPROPERTY_ID " +
                    $"AND CJRNGRP_TYPE = @CJRNGRP_TYPE";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_TYPE", DbType.String, 50, poEntity.CJRNGRP_TYPE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00400DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00500DTO> GetALLGLAccount(GSL00500ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00500DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_GL_ACCOUNT_LIST  " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CPROGRAM_CODE, " +
                    $"@CBSIS, " +
                    $"@CDBCR, " +
                    $"@LCENTER_RESTR, " +
                    $"@LUSER_RESTR, " +
                    $"@CUSER_ID, " +
                    $"@CCENTER_CODE, " +
                    $"@CUSER_LANGUAGE ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROGRAM_CODE", DbType.String, 50, poEntity.CPROGRAM_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CBSIS", DbType.String, 50, poEntity.CBSIS);
                loDb.R_AddCommandParameter(loCmd, "@CDBCR", DbType.String, 50, poEntity.CDBCR);
                loDb.R_AddCommandParameter(loCmd, "@LCENTER_RESTR", DbType.Boolean, 50, poEntity.LCENTER_RESTR);
                loDb.R_AddCommandParameter(loCmd, "@LUSER_RESTR", DbType.Boolean, 50, poEntity.LUSER_RESTR);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCENTER_CODE", DbType.String, 50, poEntity.CCENTER_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_LANGUAGE", DbType.String, 50, poEntity.CUSER_LANGUAGE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00500DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00510DTO> GetALLCOA(GSL00510ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00510DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_COA_LOOKUP_LIST " +
                    $"@CCOMPANY_ID, " +
                    $"@CGL_ACCOUNT_TYPE, " +
                    $"@CUSER_ID ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CGL_ACCOUNT_TYPE", DbType.String, 50, poEntity.CGLACCOUNT_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00510DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00520DTO> GetALLGOACOA(GSL00520ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00520DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_GOA_COA_LIST " +
                    $"@CCOMPANY_ID, " +
                    $"@CGOA_CODE";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 50, poEntity.CGOA_CODE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00520DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00550DTO> GetALLGOA(GSL00550ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00550DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_GOA_LIST  " +
                    $"@CCOMPANY_ID ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00550DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00600DTO> GetALLUnitTypeCategory(GSL00600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00600DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_UNIT_TYPE_CTG_LIST  " +
                    $"@CCOMPANY_ID, " +
                    $"@CPROPERTY_ID, " +
                    $"@CUSER_ID ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00600DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00700DTO> GetALLDepartment(GSL00700ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00700DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_DEPT_LOOKUP_LIST  " +
                    $"@CCOMPANY_ID, " +
                    $"@CUSER_ID  ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00700DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00800DTO> GetALLCurrencyRateType(GSL00800ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00800DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_CURRENCY_TYPE_LIST " +
                    $"@CCOMPANY_ID, " +
                    $"@CUSER_ID ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00800DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL00900DTO> GetALLCenter(GSL00900ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL00900DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_CENTER_LIST " +
                    $"@CCOMPANY_ID, " +
                    $"@CUSER_ID ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL00900DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01000DTO> GetALLUser()
        {
            var loEx = new R_Exception();
            List<GSL01000DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");

                var lcQuery = $"SELECT CUSER_ID, CUSER_NAME FROM SAM_USER (NOLOCK) ";

                loResult = loDb.SqlExecObjectQuery<GSL01000DTO>(lcQuery, loConn, true);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01100DTO> GetALLUserApproval(GSL01100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01100DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT A.CUSER_ID, A.CUSER_NAME FROM SAM_USER A (NOLOCK) " +
                    $"WHERE NOT EXISTS (SELECT TOP 1 1 FROM GSM_TRANSACTION_APPROVAL C (NOLOCK) " +
                    $"WHERE C.CCOMPANY_ID = @CCOMPANY_ID " +
                    $"AND A.CUSER_ID = C.CUSER_ID " +
                    $"AND C.CTRANSACTION_CODE = @CTRANSACTION_CODE) ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CTRANSACTION_CODE", DbType.String, 50, poEntity.CTRANSACTION_CODE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01100DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01200DTO> GetALLBank(GSL01200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01200DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCB_CODE, CCB_NAME FROM GSM_CB (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    $"AND CCB_TYPE = @CCB_TYPE ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCB_TYPE", DbType.String, 50, poEntity.CCB_TYPE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01200DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01300DTO> GetALLBankAccount(GSL01300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01300DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CCB_ACCOUNT_NO, CCB_ACCOUNT_NAME FROM GSM_CB_ACCOUNT (NOLOCK) " +
                    $"WHERE CCOMPANY_ID = @CCOMPANY_ID " +
                    $"AND CBANK_TYPE = @CBANK_TYPE " +
                    $"AND CCB_CODE   = @CCB_CODE " +
                    $"AND CDEPT_CODE = @CDEPT_CODE  ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CBANK_TYPE", DbType.String, 50, poEntity.CBANK_TYPE);
                loDb.R_AddCommandParameter(loCmd, "@CCB_CODE", DbType.String, 50, poEntity.CCB_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 50, poEntity.CDEPT_CODE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01300DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01400DTO> GetALLOtherCharges(GSL01400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01400DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_OTHER_CHARGES_LIST " +
                    $"@CCOMPANY_ID = @CCOMPANY_ID, " +
                    $"@CPROPERTY_ID = @CPROPERTY_ID, " +
                    $"@CCHARGES_TYPE  = @CCHARGES_TYPE, " +
                    $"@CUSER_ID = @CUSER_ID  ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCHARGES_TYPE", DbType.String, 50, poEntity.CCHARGES_TYPE_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_LOGIN_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01400DTO>(loDataTable).ToList();
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
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_CASHFLOW_GRP_LIST  " +
                    $"@CCOMPANY_ID = @CCOMPANY_ID, " +
                    $"@CUSER_ID = @CUSER_ID ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01500ResultGroupDTO>(loDataTable).ToList();
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
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_CASHFLOW_LIST  " +
                    $"@CCOMPANY_ID = @CCOMPANY_ID, " +
                    $"@CCASH_FLOW_GROUP_CODE = @CCASH_FLOW_GROUP_CODE, " +
                    $"@CUSER_ID = @CUSER_ID ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCASH_FLOW_GROUP_CODE", DbType.String, 50, poEntity.CCASH_FLOW_GROUP_CODE);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01500ResultDetailDTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01600DTO> GetALLCashFlowGruopType(GSL01600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01600DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_CASHFLOW_GRP_LIST  " +
                    $"@CCOMPANY_ID = @CCOMPANY_ID, " +
                    $"@CUSER_ID = @CUSER_ID ";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01600DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01700DTO> GetALLCurrencyRate(GSL01700DTOParameter poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01700DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"EXEC RSP_GS_GET_CURRENCY_RATE_LIST " +
                    $"@CCOMPANY_ID = '{poEntity.CCOMPANY_ID}', " +
                    $"@CUSER_ID = '{poEntity.CUSER_ID}', " +
                    $"@CRATETYPE_CODE = '{poEntity.CRATETYPE_CODE}', " +
                    $"@CRATE_DATE = '{poEntity.CRATE_DATE}'";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01700DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01701DTO> GetALLRateType(GSL01700DTOParameter poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01701DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CRATETYPE_CODE, CRATETYPE_DESCRIPTION FROM GSM_RATETYPE (NOLOCK) WHERE CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01701DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01702DTO> GetALLLocalAndBaseCurrency(GSL01700DTOParameter poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01702DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT A.CLOCAL_CURRENCY, B.CCURRENCY_NAME AS CLOCAL_CURRENCY_NAME,  A.CBASE_CURRENCY, C.CCURRENCY_NAME AS CBASE_CURRENCY_NAME " +
                    $"FROM SAM_COMPANIES A (NOLOCK) LEFT JOIN GSM_CURRENCY B (NOLOCK) " +
                    $"ON A.CLOCAL_CURRENCY = B.CCURRENCY_CODE LEFT JOIN GSM_CURRENCY C (NOLOCK) " +
                    $"ON A.CBASE_CURRENCY = C.CCURRENCY_CODE " +
                    $"WHERE A.CCOMPANY_ID = @CCOMPANY_ID";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01702DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public List<GSL01800DTO> GetALLCategory(GSL01800DTOParameter poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01800DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"RSP_GS_GET_CATEGORY_LIST";
                loCmd.CommandText = lcQuery;
                loCmd.CommandType = CommandType.StoredProcedure;

                loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 50, poEntity.CCOMPANY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 50, poEntity.CPROPERTY_ID);
                loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 50, poEntity.CUSER_ID);
                loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_TYPE", DbType.String, 50, poEntity.CCATEGORY_TYPE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01800DTO>(loDataTable).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public List<GSL01900DTO> GetALLLOB(GSL01900DTOParameter poEntity)
        {
            var loEx = new R_Exception();
            List<GSL01900DTO> loResult = null;

            try
            {
                var loDb = new R_Db();
                var loConn = loDb.GetConnection("R_DefaultConnectionString");
                var loCmd = loDb.GetCommand();

                var lcQuery = $"SELECT CLOB_CODE, CLOB_NAME FROM SAM_LOB (NOLOCK) WHERE LACTIVE_FLAG = 1 OR CLOB_CODE = @CLOB_CODE";
                loCmd.CommandText = lcQuery;

                loDb.R_AddCommandParameter(loCmd, "@CLOB_CODE", DbType.String, 50, poEntity.CLOB_CODE);

                var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

                loResult = R_Utility.R_ConvertTo<GSL01900DTO>(loDataTable).ToList();
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
