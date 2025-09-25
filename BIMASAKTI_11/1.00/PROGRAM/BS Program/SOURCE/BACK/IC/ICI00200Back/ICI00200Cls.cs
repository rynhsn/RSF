using System.Data;
using System.Data.Common;
using System.Diagnostics;
using ICI00200Common;
using ICI00200Common.DTOs;
using R_BackEnd;
using R_Common;
using R_Storage;
using R_StorageCommon;

namespace ICI00200Back;

public class ICI00200Cls
{
    private LoggerICI00200 _logger;
    private readonly ActivitySource _activitySource;

    public ICI00200Cls()
    {
        _logger = LoggerICI00200.R_GetInstanceLogger();
        _activitySource = ICI00200Activity.R_GetInstanceActivitySource();
    }

    public List<ICI00200ProductDTO> GetProductList(ICI00200ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetProductList));
        R_Exception loEx = new();
        List<ICI00200ProductDTO> loRtn = null;

        try
        {
            var loDataTable = RSP_IC_GET_PROD_INQ_LIST(poParameter);
            loRtn = R_Utility.R_ConvertTo<ICI00200ProductDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public List<ICI00200DeptDTO> GetDeptList(ICI00200ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetProductList));
        R_Exception loEx = new();
        List<ICI00200DeptDTO> loRtn = null;

        try
        {
            var loDataTable = RSP_IC_GET_PROD_INQ_LIST(poParameter);
            loRtn = R_Utility.R_ConvertTo<ICI00200DeptDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public List<ICI00200WarehouseDTO> GetWarehouseList(ICI00200ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetProductList));
        R_Exception loEx = new();
        List<ICI00200WarehouseDTO> loRtn = null;

        try
        {
            var loDataTable = RSP_IC_GET_PROD_INQ_LIST(poParameter);
            loRtn = R_Utility.R_ConvertTo<ICI00200WarehouseDTO>(loDataTable).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    private ICI00200ProductDetailDTO GetProductDetail(ICI00200ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetProductDetail));
        R_Exception loEx = new();
        ICI00200ProductDetailDTO loRtn = null;

        try
        {
            
            var loDataTable = RSP_IC_GET_PROD_INQ_DETAIL(poParameter);
            loRtn = R_Utility.R_ConvertTo<ICI00200ProductDetailDTO>(loDataTable).First();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public ICI00200ProductDetailDTO GetUtilityUsageDetailWithImage(ICI00200ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetUtilityUsageDetailWithImage));
        var loEx = new R_Exception();
        ICI00200ProductDetailDTO loResult = null;
        R_Db loDb;
        DbConnection loConn;
        R_ReadParameter loReadParameter;
        R_ReadResult loReadResult = null;
        try
        {
            loResult = GetProductDetail(poParameter);
    
            loDb = new R_Db();
            loConn = loDb.GetConnection();

            if (!string.IsNullOrEmpty(loResult.CSTORAGE_ID))
            {
                loReadParameter = new R_ReadParameter()
                {
                    StorageId = loResult.CSTORAGE_ID
                };

                loReadResult = R_StorageUtility.ReadFile(loReadParameter, loConn);
                loResult.OIMAGE = loReadResult.Data;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        loEx.ThrowExceptionIfErrors();

        return loResult;


    }

    public ICI00200LastInfoDetailDTO GetLastInfoDetail(ICI00200ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetLastInfoDetail));
        R_Exception loEx = new();
        ICI00200LastInfoDetailDTO loRtn = null;

        try
        {
            var loDataTable = RSP_IC_GET_PROD_INQ_DETAIL(poParameter);
            loRtn = R_Utility.R_ConvertTo<ICI00200LastInfoDetailDTO>(loDataTable).First();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    
    public ICI00200DeptWareDetailDTO GetDeptWareDetail(ICI00200ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetDeptWareDetail));
        R_Exception loEx = new();
        ICI00200DeptWareDetailDTO loRtn = null;

        try
        {
            var loDataTable = RSP_IC_GET_PROD_INQ_DETAIL(poParameter);
            loRtn = R_Utility.R_ConvertTo<ICI00200DeptWareDetailDTO>(loDataTable).First();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }

    private DataTable? RSP_IC_GET_PROD_INQ_LIST(ICI00200ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetProductList));
        R_Exception loEx = new();
        DataTable? loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_IC_GET_PROD_INQ_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CCATEGORY_ID", DbType.String, 20, poParameter.CCATEGORY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPRODUCT_ID", DbType.String, 20, poParameter.CPRODUCT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CLIST_TYPE", DbType.String, 20, poParameter.CLIST_TYPE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CCATEGORY_ID" or
                        "@CPRODUCT_ID" or
                        "@CLIST_TYPE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            loRtn = loDb.SqlExecQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
    private DataTable? RSP_IC_GET_PROD_INQ_DETAIL(ICI00200ParameterDb poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetLastInfoDetail));
        R_Exception loEx = new();
        DataTable? loRtn = null;
        R_Db loDb;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;

        try
        {
            loDb = new R_Db();
            loConn = loDb.GetConnection();
            loCmd = loDb.GetCommand();

            lcQuery = "RSP_IC_GET_PROD_INQ_DETAIL";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDb.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 8, poParameter.CCOMPANY_ID);
            loDb.R_AddCommandParameter(loCmd, "@CPRODUCT_ID", DbType.String, 20, poParameter.CPRODUCT_ID);
            loDb.R_AddCommandParameter(loCmd, "@CFILTER_ID", DbType.String, 20, poParameter.CFILTER_ID);
            loDb.R_AddCommandParameter(loCmd, "@CTYPE", DbType.String, 20, poParameter.CTYPE);

            var loDbParam = loCmd.Parameters.Cast<DbParameter>()
                .Where(x =>
                    x.ParameterName is
                        "@CCOMPANY_ID" or
                        "@CPRODUCT_ID" or
                        "@CFILTER_ID" or
                        "@CTYPE"
                )
                .Select(x => x.Value);

            _logger.LogDebug("EXEC {pcQuery} {@poParam}", lcQuery, loDbParam);

            loRtn = loDb.SqlExecQuery(loConn, loCmd, true);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();

        return loRtn;
    }
}