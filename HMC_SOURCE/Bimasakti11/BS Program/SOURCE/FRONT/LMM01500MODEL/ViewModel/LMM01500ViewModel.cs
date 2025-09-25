using LMM01500COMMON;
using LMM01500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01500ViewModel : R_ViewModel<LMM01500DTO>
    {
        private LMM01500Model _LMM01500Model = new LMM01500Model();

        public ObservableCollection<LMM01501DTO> InvoinceGroupGrid { get; set; } = new ObservableCollection<LMM01501DTO>();
        public List<LMM01500DTOPropety> PropertyList { get; set; } = new List<LMM01500DTOPropety>();

        public LMM01500DTO InvoiceGroup = new LMM01500DTO();

        public string PropertyValueContext = "";
        public bool StatusChange;
        public bool TabDept = false;

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _LMM01500Model.GetPropertyAsync();
                PropertyList = loResult;

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
                var loResult = await _LMM01500Model.GetInvoiceGrpListAsync(PropertyValueContext);
                InvoinceGroupGrid = new ObservableCollection<LMM01501DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoiceGroup(LMM01500DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _LMM01500Model.R_ServiceGetRecordAsync(poParam);

                loResult.CSEQUENCEInt = int.Parse(loResult.CSEQUENCE);
                TabDept = loResult.LBY_DEPARTMENT;

                InvoiceGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveInvoiceGroup(LMM01500DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CPROPERTY_ID = PropertyValueContext;
                    poNewEntity.LACTIVE = StatusChange;
                }
                poNewEntity.CSEQUENCE = string.Format("{0}", poNewEntity.CSEQUENCEInt);

                var loResult = await _LMM01500Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                InvoiceGroup = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidationInvoiceGrp(LMM01500DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

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
                    lCancel = string.IsNullOrEmpty(poParam.CSTAMP_ADD_ID);

                    if (lCancel)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1505"));
                    }
                }

                if (!poParam.LBY_DEPARTMENT)
                {
                    lCancel = string.IsNullOrEmpty(poParam.FileName);

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

        public async Task DeleteInvoiceGroup(LMM01500DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _LMM01500Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<LMM01500DTO> ActiveInactiveProcessAsync(LMM01500DTO poParameter)
        {
            R_Exception loException = new R_Exception();
            LMM01500DTO loRtn = null;

            try
            {
                poParameter.CPROPERTY_ID = PropertyValueContext;
                poParameter.LACTIVE = StatusChange;

                await _LMM01500Model.LMM01500ActiveInactiveAsync(poParameter);
                var loResult = await _LMM01500Model.R_ServiceGetRecordAsync(poParameter);
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
