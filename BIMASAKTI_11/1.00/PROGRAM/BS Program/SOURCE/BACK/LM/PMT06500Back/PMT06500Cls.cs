using System.Data;
using System.Data.Common;
using System.Diagnostics;
using PMT06500Common;
using PMT06500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using RSP_PM_MAINTAIN_OVT_INVOICEResources;

namespace PMT06500Back;

public class PMT06500Cls : R_BusinessObject<PMT06500InvoiceDTO>
{
    Resources_Dummy_Class _resources = new();
    
    private LoggerPMT06500 _logger;
    private readonly ActivitySource _activitySource;

    public PMT06500Cls()
    {
        _logger = LoggerPMT06500.R_GetInstanceLogger();
        _activitySource = PMT06500Activity.R_GetInstanceActivitySource();
    }

    protected override PMT06500InvoiceDTO R_Display(PMT06500InvoiceDTO poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_Display));
        R_Exception loEx = new();
        PMT06500InvoiceDTO loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_OVT_INVOICE_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 10, poEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poEntity.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 10, poEntity.CLANG_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CDEPT_CODE" or
                        "@CTRANS_CODE" or
                        "@CREF_NO" or
                        "@CREC_ID" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT06500InvoiceDTO>(loDataTable).FirstOrDefault();
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

    //Gak dipake
    protected override void R_Saving(PMT06500InvoiceDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_Saving));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        PMT06500InvoiceDTO loResult = null;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            poNewEntity.CACTION = poCRUDMode == eCRUDMode.AddMode ? "NEW" : "EDIT";

            lcQuery = "RSP_PM_MAINTAIN_OVT_INVOICE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 6, poNewEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poNewEntity.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CAGREEMENT_NO", DbType.String, 30, poNewEntity.CAGREEMENT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 255, poNewEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poNewEntity.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
            loDb.R_AddCommandParameter(loCmd, "@CLINK_DEPT_CODE", DbType.String, 20, poNewEntity.CLINK_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CLINK_TRANS_CODE", DbType.String, 10, poNewEntity.CLINK_TRANS_CODE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CTRANS_CODE" or
                        "@CREF_NO" or
                        "@CREF_DATE" or
                        "@CTENANT_ID" or
                        "@CBUILDING_ID" or
                        "@CAGREEMENT_NO" or
                        "@CDESCRIPTION" or
                        "@CPERIOD" or
                        "@CUSER_ID" or
                        "@CREC_ID" or
                        "@CACTION" or 
                        "@CLINK_DEPT_CODE" or
                        "@CLINK_TRANS_CODE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                var loReturn = loDb.SqlExecQuery(loConn, loCmd, false);
                loResult = R_Utility.R_ConvertTo<PMT06500InvoiceDTO>(loReturn).FirstOrDefault();
                poNewEntity.CREC_ID = loResult?.CREC_ID;
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
    
    protected override void R_Deleting(PMT06500InvoiceDTO poEntity)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_Deleting));
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

            lcQuery = "RSP_PM_DELETE_OVT_INVOICE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poEntity.CREC_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is 
                        "@CCOMPANY_ID" or 
                        "@CUSER_ID" or
                        "@CREC_ID"
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

    public List<PMT06500AgreementDTO> GetAgreementList(PMT06500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetAgreementList));
        R_Exception loEx = new();
        List<PMT06500AgreementDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_OVT_AGGRENO_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 6, poParam.COVT_TRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParam.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_STATUS", DbType.String, 200, poParam.CTRANS_STATUS);
            loDb.R_AddCommandParameter(loCmd, "@COVERTIME_STATUS", DbType.String, 200, poParam.COVERTIME_STATUS);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CTRANS_CODE" or
                        "@CPERIOD" or
                        "@CTRANS_STATUS" or
                        "@COVERTIME_STATUS"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT06500AgreementDTO>(loDataTable).ToList();
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

    public List<PMT06500OvtDTO> GetOvertimeList(PMT06500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetOvertimeList));
        R_Exception loEx = new();
        List<PMT06500OvtDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        var OVT_TRANS_CODE = "802400";
        var TRANS_CODE = "802410";

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_OVT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 6, poParam.COVT_TRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParam.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 3, poParam.CLANG_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_STATUS", DbType.String, 200, poParam.CTRANS_STATUS);
            loDb.R_AddCommandParameter(loCmd, "@COVERTIME_STATUS", DbType.String, 200, poParam.COVERTIME_STATUS);
            loDb.R_AddCommandParameter(loCmd, "@CAGREEMENT_NO", DbType.String, 30, poParam.CAGREEMENT_NO);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CTRANS_CODE" or
                        "@CPERIOD" or
                        "@CLANG_ID" or
                        "@CTRANS_STATUS" or
                        "@COVERTIME_STATUS" or
                        "@CAGREEMENT_NO"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT06500OvtDTO>(loDataTable).ToList();
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

    public List<PMT06500ServiceDTO> GetServiceList(PMT06500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetServiceList));
        R_Exception loEx = new();
        List<PMT06500ServiceDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_OVT_SERVICE_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CPARENT_ID", DbType.String, 50, poParam.CPARENT_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CPARENT_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT06500ServiceDTO>(loDataTable).ToList();
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

    public List<PMT06500UnitDTO> GetUnitList(PMT06500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetUnitList));
        R_Exception loEx = new();
        List<PMT06500UnitDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_OVT_UNIT_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CPARENT_ID", DbType.String, 50, poParam.CPARENT_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CPARENT_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT06500UnitDTO>(loDataTable).ToList();
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

    public List<PMT06500InvoiceDTO> GetInvoiceList(PMT06500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetInvoiceList));
        R_Exception loEx = new();
        List<PMT06500InvoiceDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_OVT_INVOICE_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 6, poParam.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_STATUS", DbType.String, 200, poParam.CTRANS_STATUS);
            loDb.R_AddCommandParameter(loCmd, "@CLANG_ID", DbType.String, 3, poParam.CLANG_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CTRANS_CODE" or
                        "@CTRANS_STATUS" or
                        "@CLANG_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT06500InvoiceDTO>(loDataTable).ToList();
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

    public List<PMT06500SummaryDTO> GetSummaryList(PMT06500ParameterDb poParam)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GetSummaryList));
        R_Exception loEx = new();
        List<PMT06500SummaryDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_PM_GET_OVT_SUMINV_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poParam.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CAGREEMENT_NO", DbType.String, 30, poParam.CAGREEMENT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CLINK_DEPT_CODE", DbType.String, 30, poParam.CLINK_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CLINK_TRANS_CODE", DbType.String, 20, poParam.CLINK_TRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poParam.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poParam.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poParam.CACTION);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CPERIOD" or
                        "@CAGREEMENT_NO" or
                        "@CLINK_DEPT_CODE" or
                        "@CLINK_TRANS_CODE" or
                        "@CREF_NO" or
                        "@CDEPT_CODE" or
                        "@CACTION"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<PMT06500SummaryDTO>(loDataTable).ToList();
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
    
    public void ProcessSubmit(PMT06500ParameterDb poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(ProcessSubmit));
        R_Exception loEx = new();
        R_Db loDb;
        DbConnection loConn = null;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();
            
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_PM_UPDATE_OVT_INVOICE_STATUS";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParam.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParam.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poParam.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poParam.CREC_ID);
            loDb.R_AddCommandParameter(loCmd, "@CNEW_STATUS", DbType.String, 2, poParam.CNEW_STATUS);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CUSER_ID" or
                        "@CREC_ID" or
                        "@CNEW_STATUS"
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

    public void SavingInvoice(PMT06500InvoiceDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        using var loActivity = _activitySource.StartActivity(nameof(SavingInvoice));
        R_Exception loEx = new();
        string lcQuery;
        R_Db loDb;
        DbCommand loCmd;
        DbConnection loConn = null;
        PMT06500InvoiceDTO loResult = null;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            R_ExternalException.R_SP_Init_Exception(loConn);

            poNewEntity.CACTION = poCRUDMode == eCRUDMode.AddMode ? "NEW" : "EDIT";

            lcQuery = "RSP_PM_MAINTAIN_OVT_INVOICE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poNewEntity.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poNewEntity.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CDEPT_CODE", DbType.String, 20, poNewEntity.CDEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CTRANS_CODE", DbType.String, 6, poNewEntity.CTRANS_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CREF_NO", DbType.String, 30, poNewEntity.CREF_NO);
            loDb.R_AddCommandParameter(loCmd, "@CREF_DATE", DbType.String, 8, poNewEntity.CREF_DATE);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_ID", DbType.String, 20, poNewEntity.CTENANT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CBUILDING_ID", DbType.String, 20, poNewEntity.CBUILDING_ID);
            loDb.R_AddCommandParameter(loCmd, "@CAGREEMENT_NO", DbType.String, 30, poNewEntity.CAGREEMENT_NO);
            loDb.R_AddCommandParameter(loCmd, "@CDESCRIPTION", DbType.String, 255, poNewEntity.CDESCRIPTION);
            loDb.R_AddCommandParameter(loCmd, "@CPERIOD", DbType.String, 6, poNewEntity.CPERIOD);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poNewEntity.CUSER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CREC_ID", DbType.String, 50, poNewEntity.CREC_ID);
            loDb.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, 10, poNewEntity.CACTION);
            loDb.R_AddCommandParameter(loCmd, "@CLINK_DEPT_CODE", DbType.String, 20, poNewEntity.CLINK_DEPT_CODE);
            loDb.R_AddCommandParameter(loCmd, "@CLINK_TRANS_CODE", DbType.String, 10, poNewEntity.CLINK_TRANS_CODE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CDEPT_CODE" or
                        "@CTRANS_CODE" or
                        "@CREF_NO" or
                        "@CREF_DATE" or
                        "@CTENANT_ID" or
                        "@CBUILDING_ID" or
                        "@CAGREEMENT_NO" or
                        "@CDESCRIPTION" or
                        "@CPERIOD" or
                        "@CUSER_ID" or
                        "@CREC_ID" or
                        "@CACTION" or 
                        "@CLINK_DEPT_CODE" or
                        "@CLINK_TRANS_CODE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            try
            {
                loDb.SqlExecNonQuery(loConn, loCmd, false);
                // loResult = R_Utility.R_ConvertTo<PMT06500InvoiceDTO>(loReturn).FirstOrDefault();
                // poNewEntity.CREC_ID = loResult?.CREC_ID;
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
}