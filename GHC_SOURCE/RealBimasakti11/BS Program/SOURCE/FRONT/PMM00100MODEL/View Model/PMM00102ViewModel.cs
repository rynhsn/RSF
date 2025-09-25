using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100COMMON.DTO_s.Helper;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace PMM00100MODEL.View_Model
{
    public class PMM00102ViewModel : R_ViewModel<SystemParamBillingDTO>
    {
        //var
        private PMM00103Model _billingParamModel = new PMM00103Model();
        private PMM00100Model _initModel = new PMM00100Model();
        public ObservableCollection<PropertyDTO> Properties { get; set; } = new ObservableCollection<PropertyDTO>();
        public SystemParamBillingDTO SystemParamBilling { get; set; } = new SystemParamBillingDTO();
        public List<GeneralTypeDTO> PaymenSubmitByList { get; set; } = new List<GeneralTypeDTO>();
        public string _propertyId = "";
        public bool _isRecordFound = false;

        //methods
        public async Task GetList_PropertyAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _initModel.GetPropertyListAsync();
                Properties = new ObservableCollection<PropertyDTO>(loResult) ?? new ObservableCollection<PropertyDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetGSBCodeInfoListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                string lcClassIdCode = "_PM_PAYMENT_SUBMIT_BY";
                R_FrontContext.R_SetStreamingContext(PMM00100ContextConstant.CAPPLICATION, PMM00100ContextConstant.CCONST_APPLICATION);
                R_FrontContext.R_SetStreamingContext(PMM00100ContextConstant.CCLASS_ID, lcClassIdCode);
                R_FrontContext.R_SetStreamingContext(PMM00100ContextConstant.CREC_ID_LIST, PMM00100ContextConstant.CCONST_REC_ID_LIST);
                PaymenSubmitByList = await _initModel.GetGSBCodeInfoListAsync() ?? new List<GeneralTypeDTO>();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetRecord_BillingParamAsync(SystemParamBillingDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CTAB_ID = "02";
                var loResult = await _billingParamModel.R_ServiceGetRecordAsync(poParam);
                _isRecordFound = loResult != null;
                if (loResult != null)
                {
                    if (DateTime.TryParseExact(loResult.COL_PAY_START_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                    {
                        loResult.DOL_PAY_START_DATE = ldRefDate;
                    }
                    else
                    {
                        loResult.DOL_PAY_START_DATE = null;
                    }

                    loResult.IBILLING_STATEMENT_DATE = string.IsNullOrWhiteSpace(loResult.CBILLING_STATEMENT_DATE) ? 0 : int.Parse(loResult.CBILLING_STATEMENT_DATE);
                }
                SystemParamBilling = loResult ?? new SystemParamBillingDTO();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveRecord_BillingParamAsync(SystemParamBillingDTO poParam, eCRUDMode poCRUDMode)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                poParam.CACTION = poCRUDMode switch
                {
                    eCRUDMode.AddMode => "ADD",
                    eCRUDMode.EditMode => "EDIT",
                    _ => "",
                };
                poParam.CTAB_ID = "02";
                poParam.COL_PAY_START_DATE = poParam.DOL_PAY_START_DATE.Value.ToString("yyyyMMdd");
                var loResult = await _billingParamModel.R_ServiceSaveAsync(poParam, poCRUDMode);
                _isRecordFound = loResult != null;
                if (loResult != null)
                {
                    SystemParamBilling = loResult ?? new SystemParamBillingDTO();
                    if (DateTime.TryParseExact(SystemParamBilling.COL_PAY_START_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate)) { SystemParamBilling.DOL_PAY_START_DATE = ldRefDate; } else { SystemParamBilling.DOL_PAY_START_DATE = null; }
                    SystemParamBilling.IBILLING_STATEMENT_DATE = string.IsNullOrWhiteSpace(SystemParamBilling.CBILLING_STATEMENT_DATE) ? 0 : int.Parse(SystemParamBilling.CBILLING_STATEMENT_DATE);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
