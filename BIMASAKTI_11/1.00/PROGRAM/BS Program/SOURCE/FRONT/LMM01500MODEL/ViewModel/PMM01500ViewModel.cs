using PMM01500COMMON;
using PMM01500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PMM01500MODEL
{
    public class PMM01500ViewModel : R_ViewModel<PMM01500DTO>
    {
        private PMM01500Model _PMM01500Model = new PMM01500Model();
        private PMM01510Model _PMM01510Model = new PMM01510Model();

        public ObservableCollection<PMM01501DTO> InvoinceGroupGrid { get; set; } = new ObservableCollection<PMM01501DTO>();
        public List<PMM01500DTOPropety> PropertyList { get; set; } = new List<PMM01500DTOPropety>();
        public LinkedList<PMM01500StampRateDTO> StampRateList { get; set; } = new LinkedList<PMM01500StampRateDTO>();
        public LinkedList<PMM01500DTOInvTemplate> InvoiceTemplateList { get; set; } = new LinkedList<PMM01500DTOInvTemplate>();
        public List<PMM01500UniversalDTO> TaxCodeList { get; set; } = new List<PMM01500UniversalDTO>();
        public LinkedList<PMM01500UniversalDTO> AdditinalDescList { get; set; } = new LinkedList<PMM01500UniversalDTO>();

        public PMM01500DTO InvoiceGroup = new PMM01500DTO();

        public string PropertyValueContext = "";
        public bool StatusChange;
        public bool TabDept = false;
        public bool OldByDeptValue { get; set; }

        public async Task GetUniversalList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01500Model.GetPropertyAsync();
                PropertyList = loResult;

                TaxCodeList = await _PMM01500Model.GetAllUniversalListAsync("_TAX_CODE");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetAdditionalDescList(string pcParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01500Model.GetAllUniversalListAsync(pcParameter);

                AdditinalDescList = new LinkedList<PMM01500UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetInvoiceGroupList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01500Model.GetInvoiceGrpListAsync(PropertyValueContext);
                InvoinceGroupGrid = new ObservableCollection<PMM01501DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetStampRateList(string pcPropertyId)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01500Model.GetStampRateListAsync(pcPropertyId);
                StampRateList = new LinkedList<PMM01500StampRateDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetInvoiceTemplate(string pcPropertyId)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMM01500Model.GetInvoiceTemplateAsync(pcPropertyId);
                InvoiceTemplateList = new LinkedList<PMM01500DTOInvTemplate>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoiceGroup(PMM01500DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _PMM01500Model.R_ServiceGetRecordAsync(poParam);

                loResult.CSEQUENCEInt = int.Parse(loResult.CSEQUENCE);
                TabDept = loResult.LBY_DEPARTMENT;
                OldByDeptValue = loResult.LBY_DEPARTMENT;

                InvoiceGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveInvoiceGroup(PMM01500DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CPROPERTY_ID = PropertyValueContext;
                    poNewEntity.LACTIVE = true;
                }
                poNewEntity.CSEQUENCE = string.Format("{0}", poNewEntity.CSEQUENCEInt);

                var loResult = await _PMM01500Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                InvoiceGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationInvoiceGrp(PMM01500DTO poParam)
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
                        "V02"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CINVGRP_CODE);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1501"));
                }

                lCancel = string.IsNullOrEmpty(poParam.CINVGRP_NAME);

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1502"));

                }

                lCancel = poParam.IBEFORE_LIMIT_INVOICE_DATE < poParam.ILIMIT_INVOICE_DATE;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1503"));
                }

                lCancel = poParam.IAFTER_LIMIT_INVOICE_DATE > poParam.ILIMIT_INVOICE_DATE;

                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1504"));
                }

                if (poParam.LUSE_STAMP)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CSTAMP_CODE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1519"));
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CSTAMP_ADD_ID);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1505"));
                    }
                }

                if (poParam.LTAX_EXEMPTION)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CTAX_EXEMPTION_CODE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1518"));
                    }
                }

                if (!poParam.LBY_DEPARTMENT)
                {
                    lCancel = string.IsNullOrEmpty(poParam.CINV_DEPT_CODE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1521"));
                    }

                    // lCancel = string.IsNullOrEmpty(poParam.FileNameExtension);
                    //
                    // if (lCancel)
                    // {
                    //     loEx.Add(R_FrontUtility.R_GetError(
                    //     typeof(Resources_Dummy_Class),
                    //     "1506"));
                    // }
                    
                    lCancel = string.IsNullOrEmpty(poParam.CINVOICE_TEMPLATE);
                    
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1506"));
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CDEPT_CODE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1507"));
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CBANK_CODE);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1508"));
                    }

                    lCancel = string.IsNullOrEmpty(poParam.CBANK_ACCOUNT);
                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1509"));
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

        public async Task DeleteInvoiceGroup(PMM01500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _PMM01500Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<PMM01500DTO> ActiveInactiveProcessAsync(PMM01500DTO poParameter)
        {
            R_Exception loException = new R_Exception();
            PMM01500DTO loRtn = null;

            try
            {
                poParameter.CPROPERTY_ID = PropertyValueContext;
                poParameter.LACTIVE = StatusChange;

                await _PMM01500Model.PMM01500ActiveInactiveAsync(poParameter);
                var loResult = await _PMM01500Model.R_ServiceGetRecordAsync(poParameter);
                loResult.CSEQUENCEInt = int.Parse(loResult.CSEQUENCE);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return loRtn;
        }
        public async Task<bool> CheckDataTab2(PMM01500DTO poParameter)
        {
            R_Exception loException = new R_Exception();
            bool llRtn = false;

            try
            {
                llRtn =  await _PMM01500Model.CheckDataTabTemplateBankAsync(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

            return llRtn;
        }

        public List<RadioButton> RadioInvDMList { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Tenant")},
            new RadioButton { Id = "02", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_InvGrp") },
        };

        public List<RadioButton> RadioInvGrpMode { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text =  R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_DueDays")},
            new RadioButton { Id = "02", Text =  R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_FixedDueDate")},
            new RadioButton { Id = "03", Text =  R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_RangeFixDueDate")},
        };

    }

    public class RadioButton
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class RadioButtonInt
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
