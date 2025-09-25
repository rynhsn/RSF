using System.Data;
using System.Data.Common;
using LMM02500Common;
using LMM02500Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500Back;

public class LMM02500ListCls
{
    private LoggerLMM02500 _logger;

    public LMM02500ListCls()
    {
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }
    
    public List<LMM02500TenantGroupDTO> GetTenantGroupList(LMM02500ParameterDb poParameter)
    {
        R_Exception loEx = new();
        List<LMM02500TenantGroupDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_GET_TENANT_GROUP_LIST";
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

            loRtn = R_Utility.R_ConvertTo<LMM02500TenantGroupDTO>(loDataTable).ToList();
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

public class LMM02500DetailCls : R_BusinessObject<LMM02500TenantGroupDetailDTO>
{
    private LoggerLMM02500 _logger;

    public LMM02500DetailCls()
    {
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }
    
    protected override LMM02500TenantGroupDetailDTO R_Display(LMM02500TenantGroupDetailDTO poEntity)
    {
        throw new NotImplementedException();
    }

    protected override void R_Saving(LMM02500TenantGroupDetailDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        throw new NotImplementedException();
    }

    protected override void R_Deleting(LMM02500TenantGroupDetailDTO poEntity)
    {
        throw new NotImplementedException();
    }
}

public class LMM02500TaxInfoCls : R_BusinessObject<LMM02500TaxInfoDTO>
{
    private LoggerLMM02500 _logger;

    public LMM02500TaxInfoCls()
    {
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }
    
    protected override LMM02500TaxInfoDTO R_Display(LMM02500TaxInfoDTO poEntity)
    {
        throw new NotImplementedException();
    }

    protected override void R_Saving(LMM02500TaxInfoDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        throw new NotImplementedException();
    }

    protected override void R_Deleting(LMM02500TaxInfoDTO poEntity)
    {
        throw new NotImplementedException();
    }
}

public class LMM02500TenantListCls : R_BusinessObject<LMM02500TenantListGroupDTO>
{
    private LoggerLMM02500 _logger;

    public LMM02500TenantListCls()
    {
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }

    protected override LMM02500TenantListGroupDTO R_Display(LMM02500TenantListGroupDTO poEntity)
    {
        throw new NotImplementedException();
    }

    protected override void R_Saving(LMM02500TenantListGroupDTO poNewEntity, eCRUDMode poCRUDMode)
    {
        throw new NotImplementedException();
    }

    protected override void R_Deleting(LMM02500TenantListGroupDTO poEntity)
    {
        throw new NotImplementedException();
    }
    
    public List<LMM02500TenantListGroupDTO> GetTenantGroupList(LMM02500ParameterDb poParameter)
    {
        R_Exception loEx = new();
        List<LMM02500TenantListGroupDTO> loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_LM_GET_TENANT_LIST_GROUP";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, 20, poParameter.CPROPERTY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTENANT_GROUP_ID", DbType.String, 20, poParameter.CTENANT_GROUP_ID);
            loDb.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 8, poParameter.CUSER_ID);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPROPERTY_ID" or
                        "@CTENANT_GROUP_ID" or
                        "@CUSER_ID"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);
            
            var loDataTable = loDb.SqlExecQuery(loConn, loCmd, true);

            loRtn = R_Utility.R_ConvertTo<LMM02500TenantListGroupDTO>(loDataTable).ToList();
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