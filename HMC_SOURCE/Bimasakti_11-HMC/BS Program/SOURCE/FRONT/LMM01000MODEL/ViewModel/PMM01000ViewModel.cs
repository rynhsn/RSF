using PMM01000COMMON;
using PMM01000FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Threading.Tasks;

namespace PMM01000MODEL
{
    public class PMM01000ViewModel : R_ViewModel<PMM01000DTO>
    {
        private PMM01000Model _PMM01000Model = new PMM01000Model();
        private PMM01000UniversalModel _PMM01000UniversalModel = new PMM01000UniversalModel();

        public ObservableCollection<PMM01000UniversalDTO> ChargesTypeGrid { get; set; } = new ObservableCollection<PMM01000UniversalDTO>();
        public ObservableCollection<PMM01000DTO> ChargesUtilityGrid { get; set; } = new ObservableCollection<PMM01000DTO>();
        public PMM01000DTO UtilityCharges { get; set; } = new PMM01000DTO();

        //List Data
        public List<PMM01000DTOPropety> PropertyList { get; set; } = new List<PMM01000DTOPropety>();
        public List<PMM01000UniversalDTO> TaxExemptionList { get; set; } = new List<PMM01000UniversalDTO>();
        public List<PMM01000UniversalDTO> WithholdingTaxList { get; set; } = new List<PMM01000UniversalDTO>();
        public List<PMM01000UniversalDTO> AccrualMethodList { get; set; } = new List<PMM01000UniversalDTO>();

        public string PropertyValueContext = "";
        public bool StatusChange;
        public bool Accrual { get; set; } = false;
        public bool hiddenDisplay = false;

        public async Task GetAllInitList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetAllInitPMM01000ListAsync();

                PropertyList = loResult.PropertyList;
                TaxExemptionList = loResult.TaxExemptionCodeList;
                WithholdingTaxList = loResult.WithholdingTaxTypeList;
                AccrualMethodList = loResult.AccrualMethodTypeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesTypeList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000UniversalModel.GetChargesTypeListAsync();

                ChargesTypeGrid = new ObservableCollection<PMM01000UniversalDTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesUtilityList(PMM01002DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                poParam.CPROPERTY_ID = PropertyValueContext;
                var loResult = await _PMM01000Model.GetChargesUtilityListAsync(poParam);
                var loData = R_FrontUtility.ConvertCollectionToCollection<PMM01000DTO>(loResult);

                ChargesUtilityGrid = new ObservableCollection<PMM01000DTO>(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetChargesUtility(PMM01000DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _PMM01000Model.R_ServiceGetRecordAsync(poParam);

                Accrual = loResult.LACCRUAL != null ? loResult.LACCRUAL : Accrual;

                UtilityCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationUtility(PMM01000DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(PropertyValueContext);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1000"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CCHARGES_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1004"));
                }
                else
                {
                    lCancel = poParam.CCHARGES_ID.Length > 20;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "10017"));
                    }
                }

                lCancel = string.IsNullOrEmpty(poParam.CCHARGES_NAME);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10018"));

                }
                else
                {
                    lCancel = poParam.CCHARGES_NAME.Length > 100;
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "1004"));
                    }
                }


                lCancel = string.IsNullOrEmpty(poParam.CDESCRIPTION);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1006"));

                }
                if (hiddenDisplay == false)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CUTILITY_JRNGRP_CODE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Resources_Dummy_Class),
                            "1007"));

                    }
                }

                if (poParam.LTAXABLE)
                {
                    if (poParam.LTAX_EXEMPTION)
                    {
                        lCancel = string.IsNullOrEmpty(poParam.CTAX_EXEMPTION_CODE);

                        if (lCancel)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1008"));
                        }

                        lCancel = poParam.NTAX_EXEMPTION_PCT== 0 || poParam.NTAX_EXEMPTION_PCT< 0 || poParam.NTAX_EXEMPTION_PCT> 100;

                        if (lCancel)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1009"));
                        }
                    }
                }

                if (poParam.LOTHER_TAX)
                {
                    lCancel = string.IsNullOrEmpty(poParam.COTHER_TAX_ID);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10010"));
                    }
                }

                if (poParam.LWITHHOLDING_TAX)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CWITHHOLDING_TAX_TYPE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10011"));
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CWITHHOLDING_TAX_ID);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10012"));
                    }
                }

                if (Accrual)
                {

                    lCancel = string.IsNullOrEmpty(poParam.CACCRUAL_METHOD);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "10013"));
                    }
                }

               await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveChargesUtility(PMM01000DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01000Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                UtilityCharges = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteChargesUtility(PMM01000DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMM01000Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMM01000DTO> ActiveInactiveProcessAsync(PMM01000DTO poParameter)
        {
            R_Exception loException = new R_Exception();
            PMM01000DTO loRtn = null;

            try
            {
                poParameter.LACTIVE = StatusChange;

                await _PMM01000Model.PMM01000ActiveInactiveAsync(poParameter);
                var loResult = await _PMM01000Model.R_ServiceGetRecordAsync(poParameter);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }

        public async Task<PMM01000BeforeDeleteDTO> ValidateBeforeDelete(PMM01000DTO poParam)
        {
            R_Exception loException = new R_Exception();
            PMM01000BeforeDeleteDTO loRtn = null;

            try
            {
                var loResult = await _PMM01000Model.ValidateBeforeDeleteAsync(poParam);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
    }

    public class RadioButton
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}
