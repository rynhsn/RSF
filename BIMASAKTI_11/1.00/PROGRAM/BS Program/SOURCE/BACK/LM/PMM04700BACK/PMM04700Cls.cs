using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Transactions;
using PMM04700Back;
using PMM04700Common;
using PMM04700Common.DTOs;
using R_BackEnd;
using R_Common;

namespace PMM04700BACK;

public class PMM04700Cls
{
    RSP_PM_MAINTAIN_PRICINGResources.Resources_Dummy_Class _rspMaintainPricing = new();

    RSP_PM_MAINTAIN_PRICING_RATEResources.Resources_Dummy_Class _rspMaintainPricingRate = new();

    RSP_PM_ACTIVE_INACTIVE_UNIT_PRICEResources.ResourceS_Dummy_Class _rspActiveInactivePricing = new();
    
    private LoggerPMM04700 _logger;
    private readonly ActivitySource _activitySource;
    
    public PMM04700Cls()
    {
        _logger = LoggerPMM04700.R_GetInstanceLogger();
        _activitySource = PMM04700Activity.R_GetInstanceActivitySource();
    }

    #region Init
    public List<PropertyDTO> GetPropertyList(PropertyDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new R_Exception();
            List<PropertyDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "RSP_GS_GET_PROPERTY_LIST";
                loCmd.CommandType = CommandType.StoredProcedure;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, 20, poEntity.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, 20, poEntity.CUSER_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<PropertyDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public List<TypeDTO> GetChargesPriceTypeList(TypeParam poParam)
        {
            using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
            R_Exception loEx = new();
            List<TypeDTO> loRtn = null;
            R_Db loDB;
            DbConnection loConn;
            DbCommand loCmd;
            string lcQuery;
            try
            {
                loDB = new R_Db();
                loConn = loDB.GetConnection();
                loCmd = loDB.GetCommand();

                lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO(@CLASS_APPLICATION, @CCOMPANY_ID, @CLASS_ID, @REC_ID_LIST, @LANG_ID)";
                loCmd.CommandType = CommandType.Text;
                loCmd.CommandText = lcQuery;

                loDB.R_AddCommandParameter(loCmd, "@CLASS_APPLICATION", DbType.String, int.MaxValue, poParam.CLASS_APPLICATION);
                loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
                loDB.R_AddCommandParameter(loCmd, "@CLASS_ID", DbType.String, int.MaxValue, poParam.CLASS_ID);
                loDB.R_AddCommandParameter(loCmd, "@REC_ID_LIST", DbType.String, int.MaxValue, poParam.REC_ID_LIST);
                loDB.R_AddCommandParameter(loCmd, "@LANG_ID", DbType.String, int.MaxValue, poParam.LANG_ID);

                ShowLogDebug(lcQuery, loCmd.Parameters);
                var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
                loRtn = R_Utility.R_ConvertTo<TypeDTO>(loRtnTemp).ToList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    #endregion
    
    public List<OtherUnitDTO> GetOtherUnitList(OtherUnitParamDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
        R_Exception loEx = new();
        List<OtherUnitDTO> loRtn = null;
        R_Db loDB;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDB = new R_Db();
            loConn = loDB.GetConnection();
            loCmd = loDB.GetCommand();

            lcQuery = "RSP_GS_GET_OTHER_UNIT_TYPE";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
            loDB.R_AddCommandParameter(loCmd, "@COTHER_UNIT_TYPE_ID", DbType.String, int.MaxValue, "");
            loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);
            loDB.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, true);
            ShowLogDebug(lcQuery, loCmd.Parameters);

            var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<OtherUnitDTO>(loRtnTemp).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            ShowLogError(loEx);
        }
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public List<PricingDTO> GetPricingList(PricingParamDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
        R_Exception loEx = new();
        List<PricingDTO> loRtn = null;
        R_Db loDB;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDB = new R_Db();
            loConn = loDB.GetConnection();
            loCmd = loDB.GetCommand();

            lcQuery = "RSP_PM_GET_PRICING_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CUNIT_CATEGORY_ID", DbType.String, int.MaxValue, poEntity.COTHER_UNIT_TYPE_ID);
            loDB.R_AddCommandParameter(loCmd, "@CPRICE_TYPE", DbType.String, int.MaxValue, poEntity.CPRICE_TYPE);
            loDB.R_AddCommandParameter(loCmd, "@LACTIVE_ONLY", DbType.Boolean, int.MaxValue, poEntity.LACTIVE);
            loDB.R_AddCommandParameter(loCmd, "@CTYPE", DbType.String, int.MaxValue, poEntity.CTYPE);
            loDB.R_AddCommandParameter(loCmd, "@CVALID_DATE", DbType.String, int.MaxValue, poEntity.CVALID_DATE);
            loDB.R_AddCommandParameter(loCmd, "@CVALID_ID", DbType.String, int.MaxValue, poEntity.CVALID_INTERNAL_ID);
            loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);

            ShowLogDebug(lcQuery, loCmd.Parameters);
            var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<PricingDTO>(loRtnTemp).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            ShowLogError(loEx);
        }
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    
    public List<TypeDTO> GetRftGSBCodeInfoList(TypeParam poParam)
    {
        using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
        R_Exception loEx = new();
        List<TypeDTO> loRtn = null;
        R_Db loDB;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDB = new R_Db();
            loConn = loDB.GetConnection();
            loCmd = loDB.GetCommand();

            lcQuery = "SELECT CCODE, CDESCRIPTION FROM RFT_GET_GSB_CODE_INFO(@CLASS_APPLICATION, @CCOMPANY_ID, @CLASS_ID, @REC_ID_LIST, @LANG_ID)";
            loCmd.CommandType = CommandType.Text;
            loCmd.CommandText = lcQuery;

            loDB.R_AddCommandParameter(loCmd, "@CLASS_APPLICATION", DbType.String, int.MaxValue, poParam.CLASS_APPLICATION);
            loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CLASS_ID", DbType.String, int.MaxValue, poParam.CLASS_ID);
            loDB.R_AddCommandParameter(loCmd, "@REC_ID_LIST", DbType.String, int.MaxValue, poParam.REC_ID_LIST);
            loDB.R_AddCommandParameter(loCmd, "@LANG_ID", DbType.String, int.MaxValue, poParam.LANG_ID);

            ShowLogDebug(lcQuery, loCmd.Parameters);
            var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<TypeDTO>(loRtnTemp).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            ShowLogError(loEx);
        }
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public List<PricingDTO> GetPricingDateList(PricingParamDTO poEntity)
    {
        using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
        R_Exception loEx = new();
        List<PricingDTO> loRtn = null;
        R_Db loDB;
        DbConnection loConn;
        DbCommand loCmd;
        string lcQuery;
        try
        {
            loDB = new R_Db();
            loConn = loDB.GetConnection();
            loCmd = loDB.GetCommand();

            lcQuery = "RSP_PM_GET_PRICING_DATE_LIST";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poEntity.CCOMPANY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poEntity.CPROPERTY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CUNIT_CATEGORY_ID", DbType.String, int.MaxValue, poEntity.CUNIT_TYPE_CATEGORY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CPRICE_TYPE", DbType.String, int.MaxValue, poEntity.CPRICE_TYPE);
            loDB.R_AddCommandParameter(loCmd, "@CTYPE", DbType.String, int.MaxValue, poEntity.CTYPE);
            loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poEntity.CUSER_ID);
            ShowLogDebug(lcQuery, loCmd.Parameters);

            var loRtnTemp = loDB.SqlExecQuery(loConn, loCmd, true);
            loRtn = R_Utility.R_ConvertTo<PricingDTO>(loRtnTemp).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            ShowLogError(loEx);
        }
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public void SavePricing(PricingSaveParamDTO poParam)
    {
        var loEx = new R_Exception();
        try
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
            {
                SavePricingSP(poParam);

                transactionScope.Complete();
            }

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    public void SavePricingSP(PricingSaveParamDTO poParam)
    {
        using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
        R_Exception loEx = new R_Exception();
        R_Db loDB = new R_Db();
        DbConnection loConn = null;
        DbCommand loCmd = null;
        string lcQuery = "";
        try
        {


            loCmd = loDB.GetCommand();
            loConn = loDB.GetConnection();
            R_ExternalException.R_SP_Init_Exception(loConn);

            // creating temptable
            lcQuery = @"CREATE TABLE #PRICING 
                          ( ISEQ INT
	                    , CVALID_INTERNAL_ID	VARCHAR(36)
	                    , CCHARGES_TYPE			VARCHAR(2)
	                    , CCHARGES_ID			VARCHAR(20)
	                    , CPRICE_MODE			VARCHAR(2)
	                    , NNORMAL_PRICE			NUMERIC(18,2)
	                    , NBOTTOM_PRICE			NUMERIC(18,2)
	                    , LOVERWRITE			BIT
                        , CINVOICE_PERIOD		VARCHAR(2)
                          ) ";
            //exec temptable
            loDB.SqlExecNonQuery(lcQuery, loConn, false);

            //convert data
            var loConvertedData = ConvertListToBulkDTO(poParam.PRICING_LIST);

            //savebulk
            _logger.LogDebug($"INSERT INTO #PRICING {loConvertedData}");//log insert
            loDB.R_BulkInsert<PricingDBSaveBulkDTO>((SqlConnection)loConn, "#PRICING", loConvertedData);

            //exec rsp
            lcQuery = "RSP_PM_MAINTAIN_PRICING";
            loCmd.CommandText = lcQuery;
            loCmd.CommandType = CommandType.StoredProcedure;
            loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CPRICE_TYPE", DbType.String, int.MaxValue, poParam.CPRICE_TYPE);
            loDB.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CTG_ID", DbType.String, int.MaxValue, poParam.CUNIT_TYPE_CATEGORY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CVALID_FROM_DATE", DbType.String, int.MaxValue, poParam.CVALID_FROM_DATE);
            loDB.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, poParam.LACTIVE);
            loDB.R_AddCommandParameter(loCmd, "@CACTION", DbType.String, int.MaxValue, poParam.CACTION);
            loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);
            try
            {
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loDB.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));

        }
        catch (Exception ex)
        {
            loEx.Add(ex);
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
            if (loCmd != null)
            {
                loCmd.Dispose();
                loCmd = null;
            }
        }
        loEx.ThrowExceptionIfErrors();
    }

    public void ActiveInactivePricing(PricingParamDTO poParam)
    {
        using Activity activity = _activitySource.StartActivity(MethodBase.GetCurrentMethod().Name);
        R_Exception loEx = new();
        R_Db loDB;
        DbConnection loConn = null;
        DbCommand loCmd = null;
        string lcQuery;
        try
        {
            loDB = new R_Db();
            loConn = loDB.GetConnection();
            loCmd = loDB.GetCommand();
            R_ExternalException.R_SP_Init_Exception(loConn);

            lcQuery = "RSP_PM_ACTIVE_INACTIVE_PRICING";
            loCmd.CommandType = CommandType.StoredProcedure;
            loCmd.CommandText = lcQuery;

            loDB.R_AddCommandParameter(loCmd, "@CCOMPANY_ID", DbType.String, int.MaxValue, poParam.CCOMPANY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CPROPERTY_ID", DbType.String, int.MaxValue, poParam.CPROPERTY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CPRICE_TYPE", DbType.String, int.MaxValue, poParam.CPRICE_TYPE);
            loDB.R_AddCommandParameter(loCmd, "@CUNIT_TYPE_CATEGORY_ID", DbType.String, int.MaxValue, poParam.CUNIT_TYPE_CATEGORY_ID);
            loDB.R_AddCommandParameter(loCmd, "@CVALID_INTERNAL_ID", DbType.String, int.MaxValue, poParam.CVALID_INTERNAL_ID);
            loDB.R_AddCommandParameter(loCmd, "@CVALID_DATE", DbType.String, int.MaxValue, poParam.CVALID_DATE);
            loDB.R_AddCommandParameter(loCmd, "@LACTIVE", DbType.Boolean, int.MaxValue, poParam.LACTIVE);
            loDB.R_AddCommandParameter(loCmd, "@CUSER_ID", DbType.String, int.MaxValue, poParam.CUSER_ID);
            try
            {
                ShowLogDebug(lcQuery, loCmd.Parameters);
                loDB.SqlExecNonQuery(loConn, loCmd, false);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.Add(R_ExternalException.R_SP_Get_Exception(loConn));
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
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
            if (loCmd != null)
            {
                loCmd.Dispose();
                loCmd = null;
            }
        }
        loEx.ThrowExceptionIfErrors();
    }

    private List<PricingDBSaveBulkDTO> ConvertListToBulkDTO(List<PricingBulkSaveDTO> poParam)
    {
        var loRtn = new List<PricingDBSaveBulkDTO>();
        R_Exception loEx = new R_Exception();
        try
        {
            // separate new & old data (base on CVALID_INTERNAL_ID)
            var loNewData = poParam.Where(item => string.IsNullOrEmpty(item.CVALID_INTERNAL_ID)).ToList();
            var loOldData = poParam.Where(item => !string.IsNullOrEmpty(item.CVALID_INTERNAL_ID)).ToList();

            // concatenate new data first and then old data
            var loSortedData = loNewData.Concat(loOldData).ToList();

            // give seq
            loRtn = loSortedData.Select((loTemp, i) => new PricingDBSaveBulkDTO
            {
                ISEQ = i + 1,//add sequence
                CVALID_INTERNAL_ID = loTemp.CVALID_INTERNAL_ID,
                CCHARGES_TYPE = loTemp.CCHARGES_TYPE,
                CCHARGES_ID = loTemp.CCHARGES_ID,
                CPRICE_MODE = loTemp.CPRICE_MODE,
                NNORMAL_PRICE = loTemp.NNORMAL_PRICE,
                NBOTTOM_PRICE = loTemp.NBOTTOM_PRICE,
                LOVERWRITE = loTemp.LOVERWRITE,
                CINVOICE_PERIOD = loTemp.CINVOICE_PERIOD,
            }).ToList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }
    
    #region log method helper
    private void ShowLogDebug(string pcQuery, DbParameterCollection poParameters)
    {
        var paramValues = string.Join(", ", poParameters.Cast<DbParameter>().Select(p => $"{p.ParameterName} '{p.Value}'"));
        _logger.LogDebug($"EXEC {pcQuery} {paramValues}");
    }

    private void ShowLogError(Exception ex)
    {
        _logger.LogError(ex);
    }

    #endregion
}