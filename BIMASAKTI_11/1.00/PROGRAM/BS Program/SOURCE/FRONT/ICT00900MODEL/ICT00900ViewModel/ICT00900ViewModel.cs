using ICT00900COMMON;
using ICT00900COMMON.DTO;
using ICT00900COMMON.Param;
using ICT00900COMMON.Utility_DTO;
using ICT00900FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICT00900MODEL.ICT00900ViewModel
{
    public class ICT00900ViewModel : R_ViewModel<ICT00900AjustmentDetailDTO>
    {
        private ICT00900Model _model = new ICT00900Model();

        public List<PropertyDTO> PropertyList = new List<PropertyDTO>();
        public List<CurrencyDTO> CurrencyList = new List<CurrencyDTO>();
        public ObservableCollection<ICT00900AdjustmentDTO> CostAdjustmentList = new ObservableCollection<ICT00900AdjustmentDTO>();

        public ICT00900AjustmentDetailDTO oEntityAdjustmentDetail = new ICT00900AjustmentDetailDTO();
        public VarGsmTransactionCodeDTO VarTransaction = new VarGsmTransactionCodeDTO();
        public VarGsmCompanyInfoDTO VarCompanyInfo = new VarGsmCompanyInfoDTO();

        public PropertyDTO PropertyValue = new PropertyDTO();
        public CurrencyDTO CurrencyValue = new CurrencyDTO();
        public ICT00900ParameterAdjustment ParameterGetList = new ICT00900ParameterAdjustment();
        public ICT00900AjustmentDetailDTO ParameterGetDetail = new ICT00900AjustmentDetailDTO();
        public ICT00900ParameterChangeStatusDTO ParameterChangeStatus = new ICT00900ParameterChangeStatusDTO();
        public List<ComboBoxDTO> AdjustmentMethodList = new List<ComboBoxDTO>
        {
            new ComboBoxDTO{CCODE = "C",  CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), $"_labelUnitCost") },
            new ComboBoxDTO{CCODE = "V",  CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), $"_labelTotalValue") },
            new ComboBoxDTO{CCODE = "A",  CDESCRIPTION = R_FrontUtility.R_GetMessage(typeof(Resources_ICT00900_Class), $"_labelTotalAdjustment") },
        };
        public bool lPropertyExist = true;
        public bool _dropdownProperty = true;
        public bool lControlCRUDMode = true;
        public bool lControlButtonRedraft = true;
        public bool lControlButtonSubmit = true;
        public bool _lDataCREF_NO = false;
        public async Task<VarGsmTransactionCodeDTO> GetVarTransactionCode()
        {
            R_Exception loException = new R_Exception();
            VarGsmTransactionCodeDTO loResult = new VarGsmTransactionCodeDTO();
            try
            {
                loResult = await _model.GetVAR_GSM_TRANSACTION_CODEAsync();
                VarTransaction = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task<VarGsmCompanyInfoDTO> GetVarCompanyInfo()
        {
            R_Exception loException = new R_Exception();
            VarGsmCompanyInfoDTO loResult = new VarGsmCompanyInfoDTO();
            try
            {
                loResult = await _model.GetVAR_GSM_COMPANY_INFOAsync();
                VarCompanyInfo = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task GetPropertyList()
        {
            R_Exception loEx = new R_Exception();
            List<PropertyDTO>? loReturn = new List<PropertyDTO>();
            try
            {
                var loResult = await _model.PropertyListAsync();
                if (loResult.Data.Any())
                {
                    PropertyList = loResult.Data!;
                    PropertyValue = PropertyList[0];
                    ParameterGetList.CPROPERTY_ID = PropertyValue.CPROPERTY_ID!;
                    lPropertyExist = true;
                }
                else
                {
                    lPropertyExist = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetCurrencyList()
        {
            R_Exception loEx = new R_Exception();
            List<CurrencyDTO>? loReturn = new List<CurrencyDTO>();
            try
            {
                var loResult = await _model.CurrencyListAsync();
                if (loResult.Data.Any())
                {
                    CurrencyList = loResult.Data!;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAdjustmentList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(ParameterGetList.CPROPERTY_ID))
                {
                    R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, ParameterGetList.CPROPERTY_ID);
                    var loResult = await _model.GetAdjustmentListAsyncModel();
                    if (loResult.Data.Any())
                    {
                        CostAdjustmentList = new ObservableCollection<ICT00900AdjustmentDTO>(loResult.Data);
                    }
                    else
                    {
                        CostAdjustmentList = new ObservableCollection<ICT00900AdjustmentDTO>();
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        public async Task<ICT00900AjustmentDetailDTO> GetEntity(ICT00900AjustmentDetailDTO poEntity)
        {
            R_Exception loException = new R_Exception();
            ICT00900AjustmentDetailDTO loResult = new ICT00900AjustmentDetailDTO();
            try
            {
                loResult = await _model.R_ServiceGetRecordAsync(poEntity);

                if (!string.IsNullOrEmpty(loResult.CREF_NO))
                {
                    oEntityAdjustmentDetail = loResult;
                    oEntityAdjustmentDetail.CLOCAL_CURRENCY_CODE = VarCompanyInfo.CLOCAL_CURRENCY_CODE;
                    oEntityAdjustmentDetail.CBASE_CURRENCY_CODE = VarCompanyInfo.CBASE_CURRENCY_CODE;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loResult;
        }
        public async Task ServiceSave(ICT00900AjustmentDetailDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CREF_DATE = ConvertDateTimeToStringFormat(poNewEntity.DREF_DATE);
                //poNewEntity.CHO_PLAN_DATE = ConvertDateTimeToStringFormat(poNewEntity.DHO_PLAN_DATE);
                //poNewEntity.CSTART_DATE = ConvertDateTimeToStringFormat(poNewEntity.DHO_PLAN_DATE);
                //poNewEntity.CEND_DATE = "";
                var loResult = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                oEntityAdjustmentDetail = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task ServiceDelete(ICT00900AjustmentDetailDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<ICT00900AdjustmentDTO> ChangeStatusAdjustment(string lcNewStatus)
        {
            R_Exception loEx = new R_Exception();
            ICT00900AdjustmentDTO loReturn = new ICT00900AdjustmentDTO();
            try
            {
                if (!string.IsNullOrEmpty(oEntityAdjustmentDetail.CREF_NO))
                {
                    ICT00900ParameterChangeStatusDTO currentCostAdjustment = R_FrontUtility.ConvertObjectToObject<ICT00900ParameterChangeStatusDTO>(oEntityAdjustmentDetail);
                    currentCostAdjustment.CSTATUS = lcNewStatus;

                    var loResult = await _model.ChangeStatusAdjAsyncModel(currentCostAdjustment);
                    loReturn = loResult;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        #region Validation
        public void ValidationFieldEmpty(ICT00900AjustmentDetailDTO poEntity)
        {
            var loEx = new R_Exception();
            try
            {
                if (_lDataCREF_NO)
                {
                    if (string.IsNullOrWhiteSpace(poEntity.CREF_NO))
                    {
                        var loErr = R_FrontUtility.R_GetError(typeof(Resources_ICT00900_Class), "ValidationRefNo");
                        loEx.Add(loErr);
                    }
                }
                if (string.IsNullOrWhiteSpace(poEntity.CDEPT_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_ICT00900_Class), "ValidationDepartment");
                    loEx.Add(loErr);
                }
                if (poEntity.DREF_DATE == null)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_ICT00900_Class), "ValidationRefDate");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CADJUST_METHOD))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_ICT00900_Class), "ValidationAdjustmentMethod");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CPRODUCT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_ICT00900_Class), "ValidationProductId");
                    loEx.Add(loErr);
                }
                if (poEntity.NADJUST_AMOUNT < 0)
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_ICT00900_Class), "ValidationAdjustmentValue");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CCURRENCY_CODE))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_ICT00900_Class), "ValidationCurrency");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(poEntity.CALLOC_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_ICT00900_Class), "ValidationAllocation");
                    loEx.Add(loErr);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        private string? ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (!ptEntity.HasValue || ptEntity.Value == null)
            {
                // Jika ptEntity adalah null atau DateTime.MinValue, kembalikan null
                return null;
            }
            else
            {
                // Format DateTime ke string "yyyyMMdd"
                return ptEntity.Value.ToString("yyyyMMdd");
            }
        }
    }
}
