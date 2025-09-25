using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PMT04200Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace PMT04200MODEL;

public class PMT04210ViewModel : R_ViewModel<PMT04200DTO>
{
    #region Model
    private PMT04200InitModel _PMT04200InitModel = new PMT04200InitModel();
    private PMT04200Model _PMT04200Model = new PMT04200Model();
    #endregion

    #region Initial Data
    public PMT04200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; } = new PMT04200CBSystemParamDTO();
    public PMT04200TodayDateDTO VAR_TODAY { get; set; } = new PMT04200TodayDateDTO();
    public PMT04200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; } = new PMT04200GSCompanyInfoDTO();
    public PMT04200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new PMT04200GSTransInfoDTO();
    public PMT04200PMSystemParamDTO VAR_PM_SYSTEM_PARAM { get; set; } = new PMT04200PMSystemParamDTO();
    public PMT04200GSPeriodDTInfoDTO VAR_SOFT_PERIOD_START_DATE { get; set; } = new PMT04200GSPeriodDTInfoDTO();
    public List<PropertyListDTO> VAR_PROPERTY_LIST { get; set; } = new List<PropertyListDTO>();
    public List<PMT04200LastCurrencyRateDTO> VAR_LAST_CURRENCY_RATE_LIST { get; set; } = new List<PMT04200LastCurrencyRateDTO>();
    #endregion

    #region Public Property ViewModel
    public DateTime? RefDate { get; set; }
    public DateTime? DocDate { get; set; }
    public PMT04200DTO Journal { get; set; } = new PMT04200DTO();
    public bool FlagIsCopy { get; set; } = false;
    #endregion

    #region Property
    public string PropertyDefault = "";
    public async Task GetPropertyList()
    {
        var loEx = new R_Exception();

        try
        {
            var loResult = await _PMT04200InitModel.GetProperyListAsync();
            VAR_PROPERTY_LIST = loResult.Data;
            Data.CPROPERTY_ID = VAR_PROPERTY_LIST.FirstOrDefault().CPROPERTY_ID.ToString();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
        

    #endregion
    public async Task GetAllUniversalData()
    {
        var loEx = new R_Exception();

        try
        {
            // Get Universal Data
            var loResult = await _PMT04200InitModel.GetTabJournalListInitVarAsync();

            //Set Universal Data
            VAR_CB_SYSTEM_PARAM = loResult.VAR_CB_SYSTEM_PARAM;
            VAR_GSM_TRANSACTION_CODE = loResult.VAR_GSM_TRANSACTION_CODE;
            VAR_TODAY = loResult.VAR_TODAY;
            VAR_GSM_COMPANY = loResult.VAR_GSM_COMPANY;
            VAR_PM_SYSTEM_PARAM = loResult.VAR_PM_SYSTEM_PARAM;
            VAR_SOFT_PERIOD_START_DATE = loResult.VAR_SOFT_PERIOD_START_DATE;
            
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task GetJournal(PMT04200DTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            var loResult = await _PMT04200Model.GetJournalRecordAsync(poEntity);
            if (DateTime.TryParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
            {
                loResult.DREF_DATE = ldRefDate;
            }
            else
            {
                loResult.DREF_DATE = null;
            }
            if (DateTime.TryParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
            {
                loResult.DDOC_DATE = ldDocDate;
            }
            else
            {
                loResult.DDOC_DATE = null;
            }

            Journal = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task SaveJournal(PMT04200DTO poEntity, eCRUDMode poCRUDMode)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = new PMT04200SaveParamDTO { Data = poEntity, CRUDMode = poCRUDMode  };
            var loResult = await _PMT04200Model.SaveJournalRecordAsync(loParam);

            Journal = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task<PMT04200LastCurrencyRateDTO> GetLastCurrencyRate()
    {
        var loEx = new R_Exception();
        PMT04200LastCurrencyRateDTO loRtn = null;
        try
        {
            var loData = (PMT04200DTO)R_GetCurrentData();
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT04200LastCurrencyRateDTO>(loData);
            loParam.CRATETYPE_CODE = VAR_PM_SYSTEM_PARAM.CCUR_RATETYPE_CODE;
            loParam.CRATE_DATE = RefDate.Value.ToString("yyyyMMdd");
            var loResult = await _PMT04200InitModel.GetLastCurrencyRateAsync(loParam);

            loRtn = loResult;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        return loRtn;
    }

    public async Task UpdateJournalStatus(PMT04200UpdateStatusDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            await _PMT04200Model.UpdateJournalStatusAsync(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task SubmitCashReceipt(PMT04200UpdateStatusDTO poEntity)
    {
        var loEx = new R_Exception();

        try
        {
            await _PMT04200Model.SubmitCashReceiptAsync(poEntity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}