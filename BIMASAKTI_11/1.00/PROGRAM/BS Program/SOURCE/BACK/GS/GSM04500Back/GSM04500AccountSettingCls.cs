using System.Data;
using System.Data.Common;
using GSM04500Common;
using GSM04500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM04500Back;

public class GSM04500AccountSettingCls : R_BusinessObject<GSM04500AccountSettingDTO>
{
    private LoggerGSM04500 _logger;

    public GSM04500AccountSettingCls()
    {
        _logger = LoggerGSM04500.R_GetInstanceLogger();
    }

    protected override GSM04500AccountSettingDTO R_Display(GSM04500AccountSettingDTO poEntity)
    {
        R_Exception loEx = new();
        GSM04500AccountSettingDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = @"RSP_GS_GET_JOURNAL_GRP_GOA_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poEntity.CJRNGRP_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_CODE", DbType.String, 30, poEntity.CJRNGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 20, poEntity.CGOA_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 8, poEntity.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CJOURNAL_GROUP_TYPE" or
                        "@CJOURNAL_GROUP_CODE" or
                        "@CGOA_CODE" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<GSM04500AccountSettingDTO>(loDataTable).FirstOrDefault();
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

    protected override void R_Saving(GSM04500AccountSettingDTO poNewEntity, eCRUDMode poCRUDMode)
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

            lcQuery = "RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_TYPE", DbType.String, 2, poNewEntity.CJRNGRP_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 8, poNewEntity.CJRNGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 8, poNewEntity.CGOA_CODE);
            loDb.R_AddCommandParameter(loCmd, "@LLDEPARTMENT_MODE", DbType.Boolean, 1, poNewEntity.LDEPARTMENT_MODE);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poNewEntity.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, lcAction);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CJRNGRP_TYPE" or
                        "@CJRNGRP_CODE" or
                        "@CGOA_CODE" or
                        "@LLDEPARTMENT_MODE" or
                        "@CGLACCOUNT_NO" or
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

    protected override void R_Deleting(GSM04500AccountSettingDTO poEntity)
    {
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_GS_MAINTAIN_JOURNAL_GROUP_ACCOUNT";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_TYPE", DbType.String, 2, poEntity.CJRNGRP_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CJRNGRP_CODE", DbType.String, 8, poEntity.CJRNGRP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CGOA_CODE", DbType.String, 8, poEntity.CGOA_CODE);
            loDb.R_AddCommandParameter(loCmd, "@LLDEPARTMENT_MODE", DbType.Boolean, 1, poEntity.LDEPARTMENT_MODE);
            loDb.R_AddCommandParameter(loCmd, "@CGLACCOUNT_NO", DbType.String, 20, poEntity.CGLACCOUNT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, "DELETE");
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poEntity.CUSER_ID);


            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CJRNGRP_TYPE" or
                        "@CJRNGRP_CODE" or
                        "@CGOA_CODE" or
                        "@LLDEPARTMENT_MODE" or
                        "@CGLACCOUNT_NO" or
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

    public List<GSM04500AccountSettingDTO> GetAccountSettingList(GSM04500ParameterDb poParameter)
    {
        R_Exception loEx = new();
        List<GSM04500AccountSettingDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_GS_GET_JOURNAL_GRP_GOA_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 100, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_TYPE", DbType.String, 2, poParameter.CJOURNAL_GROUP_TYPE);
            loDb.R_AddCommandParameter(loCmd, "@CJOURNAL_GROUP_CODE", DbType.String, 30, poParameter.CJOURNAL_GROUP_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_LOGIN_ID", DbType.String, 8, poParameter.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CJOURNAL_GROUP_TYPE" or
                        "@CJOURNAL_GROUP_CODE" or
                        "@CUSER_LOGIN_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);


            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<GSM04500AccountSettingDTO>(loDataTable).ToList();
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