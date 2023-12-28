using System.Data;
using System.Data.Common;
using LMM01500Common;
using LMM01500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM01500Back;

public class LMM01500InvGrpCls : R_BusinessObject<LMM01500InvGrpDTO>
{
    private LoggerLMM01500 _logger;

    public LMM01500InvGrpCls()
    {
        _logger = LoggerLMM01500.R_GetInstanceLogger();
    }

    protected override LMM01500InvGrpDTO R_Display(LMM01500InvGrpDTO poEntity)
    {
        R_Exception loEx = new();
        LMM01500InvGrpDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_GET_INVOICE_GROUP";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 20, poEntity.CINVGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CINVGRP_CODE" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMM01500InvGrpDTO>(loDataTable).FirstOrDefault();
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

    protected override void R_Saving(LMM01500InvGrpDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            lcAction = poCRUDMode switch
            {
                eCRUDMode.AddMode => "ADD",
                eCRUDMode.EditMode => "EDIT",
                _ => lcAction
            };

            lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 8, poNewEntity.CINVGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 100, poNewEntity.CINVGRP_NAME);
            loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 6, poNewEntity.CSEQUENCE);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poNewEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 2, poNewEntity.CINVOICE_DUE_MODE);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 2, poNewEntity.CINVOICE_GROUP_MODE);
            loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, Int32.MaxValue, poNewEntity.IDUE_DAYS);
            loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, Int32.MaxValue, poNewEntity.IFIXED_DUE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, Int32.MaxValue, poNewEntity.ILIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, Int32.MaxValue, poNewEntity.IBEFORE_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, Int32.MaxValue, poNewEntity.IAFTER_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 1, poNewEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 1, poNewEntity.LDUE_DATE_TOLERANCE_SATURDAY);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 1, poNewEntity.LDUE_DATE_TOLERANCE_SUNDAY);
            loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 1, poNewEntity.LUSE_STAMP);
            loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 20, poNewEntity.CSTAMP_ADD_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 255, poNewEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 1, poNewEntity.LBY_DEPARTMENT);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 100, poNewEntity.CINVOICE_TEMPLATE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 8, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 8, poNewEntity.CBANK_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 20, poNewEntity.CBANK_ACCOUNT);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CINVGRP_CODE" or
                        "@CINVGRP_NAME" or
                        "@CSEQUENCE" or
                        "@LACTIVE" or
                        "@CINVOICE_DUE_MODE" or
                        "@CINVOICE_GROUP_MODE" or
                        "@IDUE_DAYS" or
                        "@IFIXED_DUE_DATE" or
                        "@ILIMIT_INVOICE_DATE" or
                        "@IBEFORE_LIMIT_INVOICE_DATE" or
                        "@IAFTER_LIMIT_INVOICE_DATE" or
                        "@LDUE_DATE_TOLERANCE_HOLIDAY" or
                        "@LDUE_DATE_TOLERANCE_SATURDAY" or
                        "@LDUE_DATE_TOLERANCE_SUNDAY" or
                        "@LUSE_STAMP" or
                        "@CSTAMP_ADD_ID" or
                        "@CDESCRIPTION" or
                        "@LBY_DEPARTMENT" or
                        "@CINVOICE_TEMPLATE" or
                        "@CDEPT_CODE" or
                        "@CBANK_CODE" or
                        "@CBANK_ACCOUNT" or
                        "@CACTION" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                loDb.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        finally
        {
            if (loConn != null)
            {
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }

                loConn.Dispose();
            }
        }

        EndBlock:
        loEx.ThrowExceptionIfErrors();
    }

    protected override void R_Deleting(LMM01500InvGrpDTO poEntity)
    {
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        string lcAction = "";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_LM_MAINTAIN_INVOICE_GRP";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_CODE", DbType.String, 8, poEntity.CINVGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CINVGRP_NAME", DbType.String, 100, poEntity.CINVGRP_NAME);
            loDb.R_AddCommandParameter(loCmd, "@CSEQUENCE", DbType.String, 6, poEntity.CSEQUENCE);
            loDb.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, 1, poEntity.LACTIVE);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_DUE_MODE", DbType.String, 2, poEntity.CINVOICE_DUE_MODE);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_GROUP_MODE", DbType.String, 2, poEntity.CINVOICE_GROUP_MODE);
            loDb.R_AddCommandParameter(loCmd, "@IDUE_DAYS", DbType.Int32, Int32.MaxValue, poEntity.IDUE_DAYS);
            loDb.R_AddCommandParameter(loCmd, "@IFIXED_DUE_DATE", DbType.Int32, Int32.MaxValue, poEntity.IFIXED_DUE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@ILIMIT_INVOICE_DATE", DbType.Int32, Int32.MaxValue, poEntity.ILIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@IBEFORE_LIMIT_INVOICE_DATE", DbType.Int32, Int32.MaxValue, poEntity.IBEFORE_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@IAFTER_LIMIT_INVOICE_DATE", DbType.Int32, Int32.MaxValue, poEntity.IAFTER_LIMIT_INVOICE_DATE);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_HOLIDAY", DbType.Boolean, 1, poEntity.LDUE_DATE_TOLERANCE_HOLIDAY);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SATURDAY", DbType.Boolean, 1, poEntity.LDUE_DATE_TOLERANCE_SATURDAY);
            loDb.R_AddCommandParameter(loCmd, "@LDUE_DATE_TOLERANCE_SUNDAY", DbType.Boolean, 1, poEntity.LDUE_DATE_TOLERANCE_SUNDAY);
            loDb.R_AddCommandParameter(loCmd, "@LUSE_STAMP", DbType.Boolean, 1, poEntity.LUSE_STAMP);
            loDb.R_AddCommandParameter(loCmd, "@CSTAMP_ADD_ID", DbType.String, 20, poEntity.CSTAMP_ADD_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 255, poEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCmd, "@LBY_DEPARTMENT", DbType.Boolean, 1, poEntity.LBY_DEPARTMENT);
            loDb.R_AddCommandParameter(loCmd, "@CINVOICE_TEMPLATE", DbType.String, 100, poEntity.CINVOICE_TEMPLATE);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 8, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBANK_CODE", DbType.String, 8, poEntity.CBANK_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CBANK_ACCOUNT", DbType.String, 20, poEntity.CBANK_ACCOUNT);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, "DELETE");
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CINVGRP_CODE" or
                        "@CINVGRP_NAME" or
                        "@CSEQUENCE" or
                        "@LACTIVE" or
                        "@CINVOICE_DUE_MODE" or
                        "@CINVOICE_GROUP_MODE" or
                        "@IDUE_DAYS" or
                        "@IFIXED_DUE_DATE" or
                        "@ILIMIT_INVOICE_DATE" or
                        "@IBEFORE_LIMIT_INVOICE_DATE" or
                        "@IAFTER_LIMIT_INVOICE_DATE" or
                        "@LDUE_DATE_TOLERANCE_HOLIDAY" or
                        "@LDUE_DATE_TOLERANCE_SATURDAY" or
                        "@LDUE_DATE_TOLERANCE_SUNDAY" or
                        "@LUSE_STAMP" or
                        "@CSTAMP_ADD_ID" or
                        "@CDESCRIPTION" or
                        "@LBY_DEPARTMENT" or
                        "@CINVOICE_TEMPLATE" or
                        "@CDEPT_CODE" or
                        "@CBANK_CODE" or
                        "@CBANK_ACCOUNT" or
                        "@CACTION" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);


            try
            {
                loDb.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _logger.LogError(loEx);
            }

            loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        finally
        {
            if (loConn != null)
            {
                if (loConn.State != ConnectionState.Closed)
                {
                    loConn.Close();
                }

                loConn.Dispose();
            }
        }

        loEx.ThrowExceptionIfErrors();
    }

    public List<LMM01500InvGrpGridDTO> GetInvoiceGroupList(LMM01500ParameterDb poParameter)
    {
        R_Exception loEx = new();
        List<LMM01500InvGrpGridDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_GET_INVOICE_GROUP_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);


            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMM01500InvGrpGridDTO>(loDataTable).ToList();
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