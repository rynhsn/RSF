using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PMM00500Common;
using PMM00500Common.DTOs;
using PMM00500Model;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Forms;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ContextFrontEnd;
using GFF00900COMMON.DTOs;
using R_BlazorFrontEnd.Controls.Popup;
using System.Diagnostics.Tracing;
using R_LockingFront;
using Lookup_GSModel.ViewModel;
using BlazorClientHelper;

namespace PMM00500Front
{
    public partial class PMM00500 : R_Page
    {
        private PMM00500ViewModel _viewModelType = new();
        private R_Grid<ChargesTypeDTO> _gridTypeRef;
        private R_Conductor _conTypeRef;

        private PMM00510ViewModel _viewModel = new PMM00510ViewModel();
        private R_Conductor _conductorRef;
        private R_Grid<PMM00500GridDTO> _gridRef;

        private string loLabel = "";
        private bool _enableLookup = false;
        private R_TextBox _chargesID;
        private R_TextBox _chargesName;
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";

        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] private R_PopupService PopupService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loLabel = @_localizer["_active"];
                await _viewModel.GetPropertyList();
                await TaxCodeListData();
                await TaxTypeListData();
                await RecurringListData();
                _gridTypeRef.R_RefreshGrid(this);
                if (string.IsNullOrWhiteSpace(_viewModel.PropertyValue) && string.IsNullOrWhiteSpace(_viewModelType.CharTypeId))
                {
                    _gridRef.R_RefreshGrid(this);

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #region Locking
        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = _viewModel.Charge;
                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM00500",
                        Table_Name = "PMM_UNIT_CHARGES",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM00500",
                        Table_Name = "PMM_UNIT_CHARGES",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCHARGES_TYPE, loData.CCHARGES_ID)
                    };

                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (!loLockResult.IsSuccess && loLockResult.Exception != null)
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }
        #endregion

        #region ChargesType (PARENT)
        private async Task GridType_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModelType.GetChargeTypeGridList();
                eventArgs.ListEntityResult = _viewModelType.ChargesTypeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task GridType_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<ChargesTypeDTO>(eventArgs.Data);
                _viewModel.ChargeTypeValue = loParam.CCODE;
                eventArgs.Result = _viewModelType.ChargeType;
                if (!string.IsNullOrWhiteSpace(_viewModel.PropertyValue))
                {
                    _gridRef.R_RefreshGrid(this);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Conductor
        private async Task Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                _viewModel.enableButton = true;
                _enableLookup = false;
                _viewModel.recurringComboEnable = false;
                _viewModel.enableTaxExemption = false;
                _viewModel.enableOtherTax = false;
                _viewModel.enableWitholdingTax = false;
                _viewModel.enableTaxable = false;
                R_LookupBtnOT.Enabled = false;
                R_LookupBtnJGL.Enabled = false;
                R_LookupBtnWT.Enabled = false;

                var loParam = _viewModel.Data;
                loLabel = loParam.LACTIVE ? @_localizer["_inactive"] : @_localizer["_active"];
            }

            if (eventArgs.ConductorMode != R_eConductorMode.Normal)
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await _chargesName.FocusAsync();
                }
                _enableLookup = true;
                R_LookupBtnOT.Enabled = true;
                R_LookupBtnJGL.Enabled = true;
                R_LookupBtnWT.Enabled = true;
                _viewModel.enableButton = false;

                _viewModel.recurringComboEnable = _viewModel.Data.LACCRUAL;
                _viewModel.Data.CACCRUAL_METHOD = _viewModel.Data.LACCRUAL ? _viewModel.Data.CACCRUAL_METHOD : "";

                _viewModel.enableTaxExemption = _viewModel.Data.LTAX_EXEMPTION;
                _viewModel.Data.CTAX_EXEMPTION_CODE = _viewModel.Data.LTAX_EXEMPTION ? _viewModel.Data.CTAX_EXEMPTION_CODE : "";
                _viewModel.Data.NTAX_EXEMPTION_PCT = _viewModel.Data.LTAX_EXEMPTION ? _viewModel.Data.NTAX_EXEMPTION_PCT : 0;

                _viewModel.enableWitholdingTax = _viewModel.Data.LWITHHOLDING_TAX;
                _viewModel.Data.CWITHHOLDING_TAX_TYPE = _viewModel.Data.LWITHHOLDING_TAX ? _viewModel.Data.CWITHHOLDING_TAX_TYPE : "";
                _viewModel.Data.CWITHHOLDING_TAX_ID = _viewModel.Data.LWITHHOLDING_TAX ? _viewModel.Data.CWITHHOLDING_TAX_ID : "";
                _viewModel.Data.CWITHHOLDING_TAX_NAME = _viewModel.Data.LWITHHOLDING_TAX ? _viewModel.Data.CWITHHOLDING_TAX_NAME : "";
                _viewModel.Data.NWITHHOLDING_TAX_PCT = _viewModel.Data.LWITHHOLDING_TAX ? _viewModel.Data.NWITHHOLDING_TAX_PCT : 0;

                _viewModel.enableOtherTax = _viewModel.Data.LOTHER_TAX;
                _viewModel.Data.COTHER_TAX_ID = _viewModel.Data.LOTHER_TAX ? _viewModel.Data.COTHER_TAX_ID : "";
                _viewModel.Data.COTHER_TAX_NAME = _viewModel.Data.LOTHER_TAX ? _viewModel.Data.COTHER_TAX_NAME : "";
                _viewModel.Data.NOTHER_TAX_PCT = _viewModel.Data.LOTHER_TAX ? _viewModel.Data.NOTHER_TAX_PCT : 0;
            }
        }
        private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMM00510DTO>(eventArgs.Data);
                loParam.CCHARGE_TYPE_ID = loParam.CCHARGES_TYPE;
                await _viewModel.GetChargesById(loParam);
                eventArgs.Result = _viewModel.Charge;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetGridList();
                eventArgs.ListEntityResult = _viewModel.ChargesList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Conductor_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.SaveCharge((PMM00510DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.Charge;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task Conductor_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEntity = (PMM00510DTO)eventArgs.Data;
            await _chargesID.FocusAsync();
            loEntity.LACTIVE = true;
        }
        private async Task Connductor_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            PMM00510DTO param = new PMM00510DTO()
            {
                CPROPERTY_ID = _viewModel.PropertyValue,
                CCHARGE_TYPE_ID = _viewModel.ChargeTypeValue,
            };
            await _gridRef.R_RefreshGrid(param);
        }
        private async Task Conductor_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (PMM00510DTO)eventArgs.Data;
                await _viewModel.DeleteCharge(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }


        #endregion

        private async Task TaxTypeListData()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetTaxType();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task RecurringListData()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetAccrual();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TaxCodeListData()
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetTaxCode();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnChanged(object? poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                _viewModel.PropertyValue = lsProperty;
                await _viewModel.GetGridList();
                _gridRef.R_RefreshGrid(this);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnChangedTaxable(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                if (_viewModel.Data.LTAXABLE == false)
                {
                    // Simpan data sebelum direset (jika diperlukan)
                    string previousCOTHER_TAX_ID = _viewModel.Data.COTHER_TAX_ID;
                    string previousCTAX_EXEMPTION_CODE = _viewModel.Data.CTAX_EXEMPTION_CODE;
                    decimal previousNTAX_EXEMPTION_PCT = _viewModel.Data.NTAX_EXEMPTION_PCT;

                    // Reset data
                    _viewModel.Data.LTAX_EXEMPTION = false;
                    _viewModel.Data.CTAX_EXEMPTION_CODE = "";
                    _viewModel.Data.NTAX_EXEMPTION_PCT = 0;
                    _viewModel.enableTaxable = false;
                    _viewModel.enableTaxExemption = false;

                    _viewModel.loTmp = new()
                    {
                        COTHER_TAX_ID = previousCOTHER_TAX_ID,
                        COTHER_TAX_NAME = previousCTAX_EXEMPTION_CODE,
                        NOTHER_TAX_PCT = previousNTAX_EXEMPTION_PCT
                    };
                }
                else
                {
                    _viewModel.Data.CTAX_EXEMPTION_CODE = _viewModel.loTmp.CTAX_EXEMPTION_CODE;
                    _viewModel.Data.NOTHER_TAX_PCT = _viewModel.loTmp.NOTHER_TAX_PCT;
                    _viewModel.enableTaxable = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnChangedOtherTax(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                if (_viewModel.Data.LOTHER_TAX == false)
                {
                    // Simpan data sebelum direset (jika diperlukan)
                    string previousCOTHER_TAX_ID = _viewModel.Data.COTHER_TAX_ID;
                    string previousCTAX_NAMEOT = _viewModel.Data.COTHER_TAX_NAME;
                    decimal previousNOTHER_TAX_PCT = _viewModel.Data.NOTHER_TAX_PCT;

                    // Reset data
                    _viewModel.Data.COTHER_TAX_ID = "";
                    _viewModel.Data.COTHER_TAX_NAME = "";
                    _viewModel.Data.NOTHER_TAX_PCT = 0;
                    _viewModel.enableOtherTax = false;
                    // Lakukan tindakan lain (jika diperlukan) dengan data sebelum direset
                    // Misalnya, simpan data sebelum direset di loTmp
                    _viewModel.loTmp = new()
                    {
                        COTHER_TAX_ID = previousCOTHER_TAX_ID,
                        COTHER_TAX_NAME = previousCTAX_NAMEOT,
                        NOTHER_TAX_PCT = previousNOTHER_TAX_PCT
                    };
                }
                else
                {
                    _viewModel.Data.COTHER_TAX_ID = _viewModel.loTmp.COTHER_TAX_ID;
                    _viewModel.Data.COTHER_TAX_NAME = _viewModel.loTmp.COTHER_TAX_NAME;
                    _viewModel.Data.NOTHER_TAX_PCT = _viewModel.loTmp.NOTHER_TAX_PCT;
                    _viewModel.enableOtherTax = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnChangedTaxExepmtion(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                if (_viewModel.Data.LTAX_EXEMPTION == false)
                {
                    // Simpan data sebelum direset (jika diperlukan)
                    string previousCTAX_EXEMPTION_CODE = _viewModel.Data.CTAX_EXEMPTION_CODE;
                    decimal previousNTAX_EXEMPTION_PCT = _viewModel.Data.NTAX_EXEMPTION_PCT;

                    // Reset data
                    _viewModel.Data.CTAX_EXEMPTION_CODE = "";
                    _viewModel.Data.NTAX_EXEMPTION_PCT = 0;
                    _viewModel.enableTaxExemption = false;
                    // Lakukan tindakan lain (jika diperlukan) dengan data sebelum direset
                    // Misalnya, simpan data sebelum direset di loTmp
                    _viewModel.loTmp = new()
                    {
                        CTAX_EXEMPTION_CODE = previousCTAX_EXEMPTION_CODE,
                        NTAX_EXEMPTION_PCT = previousNTAX_EXEMPTION_PCT
                    };
                }
                else
                {
                    _viewModel.Data.CTAX_EXEMPTION_CODE = _viewModel.loTmp.CTAX_EXEMPTION_CODE;
                    _viewModel.Data.NTAX_EXEMPTION_PCT = _viewModel.loTmp.NTAX_EXEMPTION_PCT;
                    var firstExemptionCode = _viewModel.loChargeTaxCodeList.FirstOrDefault();
                    _viewModel.Data.CTAX_EXEMPTION_CODE = firstExemptionCode.CCODE;
                    _viewModel.enableTaxExemption = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnChangedWitholdingTax(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                if (_viewModel.Data.LWITHHOLDING_TAX == false)
                {
                    _viewModel.enableWitholdingTax = false;
                    // Jika checkbox di-uncheck, simpan data sebelumnya ke _viewModel.loTmp
                    _viewModel.loTmp.CWITHHOLDING_TAX_ID = _viewModel.Data.CWITHHOLDING_TAX_ID;
                    _viewModel.loTmp.CWITHHOLDING_TAX_TYPE = _viewModel.Data.CWITHHOLDING_TAX_TYPE;
                    _viewModel.loTmp.NWITHHOLDING_TAX_PCT = _viewModel.Data.NWITHHOLDING_TAX_PCT;

                    // Reset data
                    _viewModel.Data.CWITHHOLDING_TAX_ID = "";
                    _viewModel.Data.CWITHHOLDING_TAX_TYPE = "";
                    _viewModel.Data.NWITHHOLDING_TAX_PCT = 0;
                }
                else
                {
                    _viewModel.enableWitholdingTax = true;
                    _viewModel.Data.CWITHHOLDING_TAX_ID = _viewModel.loTmp.CWITHHOLDING_TAX_ID;
                    _viewModel.Data.CWITHHOLDING_TAX_TYPE = _viewModel.loTmp.CWITHHOLDING_TAX_TYPE;
                    var firstWT = _viewModel.loChargeTaxTypeList.FirstOrDefault();
                    _viewModel.Data.CWITHHOLDING_TAX_TYPE = firstWT.CCODE;
                    _viewModel.Data.NWITHHOLDING_TAX_PCT = _viewModel.loTmp.NWITHHOLDING_TAX_PCT;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnChangedRecurring(object poParam)
        {
            var loEx = new R_Exception();
            try
            {
                if (_viewModel.Data.LACCRUAL != true)
                {
                    _viewModel.loTmp.CACCRUAL_METHOD = _viewModel.Data.CACCRUAL_METHOD;
                    var result = await R_MessageBox.Show("", @_localizer["_validationDisableAccrual"], R_eMessageBoxButtonType.YesNo);
                    if (result == R_eMessageBoxResult.No)
                    {
                        _viewModel.Data.LACCRUAL = true;
                    }
                }

                if (_viewModel.Data.LACCRUAL == false)
                {
                    _viewModel.loTmp.CACCRUAL_METHOD = _viewModel.Data.CACCRUAL_METHOD;
                    // Reset data jika tidak memenuhi kondisi
                    _viewModel.recurringComboEnable = false;
                    _viewModel.Data.CACCRUAL_METHOD = "";
                }
                else
                {
                    _viewModel.Data.CACCRUAL_METHOD = _viewModel.loTmp.CACCRUAL_METHOD;
                    var firstAccrual = _viewModel.loAccrualList.FirstOrDefault();
                    _viewModel.Data.CACCRUAL_METHOD = firstAccrual.CCODE;
                    _viewModel.recurringComboEnable = true;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private bool _gridEnabled = true;
        private void R_SetOther(R_SetEventArgs eventArgs)
        {
            _gridEnabled = eventArgs.Enable;
        }

        private async Task ValidaitonPMM00500(R_ValidationEventArgs eventArgs)
        {
            var loException = new R_Exception();
            R_PopupResult loResult = null;
            GFF00900ParameterDTO loParam = null;
            try
            {
                var loData = (PMM00510DTO)eventArgs.Data;
                if (string.IsNullOrEmpty(loData.CCHARGES_ID))
                    loException.Add("", @_localizer["_validaitonChargesId"]);

                if (string.IsNullOrEmpty(loData.CCHARGES_NAME))
                    loException.Add("", @_localizer["_validationChargesName"]);

                if (string.IsNullOrEmpty(loData.CDESCRIPTION))
                    loException.Add("", @_localizer["_validaitonDescription"]);

                if (string.IsNullOrEmpty(loData.CSERVICE_JRNGRP_CODE))
                    loException.Add("", @_localizer["_validationServiceJournalGroupId"]);
                if (string.IsNullOrEmpty(loData.CSERVICE_JRNGRP_NAME))
                    loException.Add("", @_localizer["_validationServiceJournalGroupName"]);



                if (loData.LTAX_EXEMPTION)
                {
                    if (string.IsNullOrEmpty(loData.CTAX_EXEMPTION_CODE))
                        loException.Add("", @_localizer["_validationTaxExemptionCode"]);
                    if (loData.NOTHER_TAX_PCT == null)
                        loException.Add("", @_localizer["_validationTaxExemptionPercentage"]);
                }

                if (loData.LWITHHOLDING_TAX)
                {
                    if (string.IsNullOrEmpty(loData.CWITHHOLDING_TAX_TYPE))
                        loException.Add("", @_localizer["_validationWithholdingTaxType"]);
                    if (string.IsNullOrEmpty(loData.CWITHHOLDING_TAX_ID))
                        loException.Add("", @_localizer["_validationWithholdingTaxId"]);
                    if (string.IsNullOrEmpty(loData.CWITHHOLDING_TAX_NAME))
                        loException.Add("", @_localizer["_validationWithholdingTaxName"]);
                    if (loData.NWITHHOLDING_TAX_PCT == null)
                        loException.Add("", @_localizer["_validationNwithholdingTax"]);
                }
                if (loData.LACTIVE && _conductorRef.R_ConductorMode == R_eConductorMode.Add)
                {
                    var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                    loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMM00501";
                    await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                    if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                    {
                        eventArgs.Cancel = false;
                    }
                    else
                    {
                        loParam = new GFF00900ParameterDTO()
                        {
                            Data = loValidateViewModel.loRspActivityValidityList,
                            IAPPROVAL_CODE = "PMM00501"
                        };
                        loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                        if (loResult.Success == false || (bool)loResult.Result == false)
                        {
                            eventArgs.Cancel = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }


        #region Report(Print)
        private void R_Before_Open_Popup_Print(R_BeforeOpenPopupEventArgs eventArgs)
        {
            _viewModel.Charge.CPROPERTY_NAME = _viewModel.PropertyList
                .Where(property => _viewModel.Charge.CPROPERTY_ID == property.CPROPERTY_ID)
                .Select(property => property.CPROPERTY_NAME)
                .FirstOrDefault();
            eventArgs.Parameter = _viewModel.Charge;
            eventArgs.TargetPageType = typeof(PrintPMM00500);
        }
        private async Task R_After_Open_Popup_Print(R_AfterOpenPopupEventArgs eventArgs)
        {
            PMM00510DTO param = new PMM00510DTO()
            {
                CPROPERTY_ID = _viewModel.PropertyValue,
                CCHARGE_TYPE_ID = _viewModel.ChargeTypeValue,
            };
            await _gridRef.R_RefreshGrid(param);
        }
        #endregion
        #region CopyNew
        private void R_Before_Open_Popup_CopyNew(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = _viewModel.Charge;
            eventArgs.TargetPageType = typeof(CopyNewPMM00500);
        }
        private async Task R_After_Open_Popup_CopyNew(R_AfterOpenPopupEventArgs eventArgs)
        {
            PMM00510DTO param = new PMM00510DTO()
            {
                CPROPERTY_ID = _viewModel.PropertyValue,
                CCHARGE_TYPE_ID = _viewModel.ChargeTypeValue,
            };
            await _gridRef.R_RefreshGrid(param);
        }
        #endregion

        #region ActiveInactive
        private async Task R_Before_Open_Popup_ActivateInactive(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = "PMM00501"; //Uabh Approval Code sesuai Spec masing masing
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" && loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    var loLastData = await _viewModel.ActiveInactiveProcessAsync(_viewModel.Charge); //Ganti jadi method ActiveInactive masing masing
                    await _conductorRef.R_SetCurrentData(loLastData);
                    return;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    eventArgs.Parameter = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "PMM00501" //Uabh Approval Code sesuai Spec masing masing
                    };
                    eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();

        }
        private async Task R_After_Open_Popup_ActivateInactive(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            try
            {
                PMM00510DTO param = new PMM00510DTO()
                {
                    CPROPERTY_ID = _viewModel.PropertyValue,
                    CCHARGES_TYPE = _viewModel.ChargeTypeValue,
                    CCHARGES_ID = _viewModel.Charge.CCHARGES_ID,
                    CCHARGE_TYPE_ID = _viewModel.ChargeTypeValue,
                };

                bool result = (bool)eventArgs.Result;
                if (result)
                {
                    var loLastData = await _viewModel.ActiveInactiveProcessAsync(_viewModel.Charge); //Ganti jadi method ActiveInactive masing masing
                    await _conductorRef.R_SetCurrentData(loLastData);
                    return;
                }
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
        }
        #endregion

        #region lookupOT
        private R_Lookup R_LookupBtnOT;
        private R_TextBox R_TextBoxOT;
        private void Before_Open_LookupOT(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00100ParameterDTO
            {
                CUSER_ID = clientHelper.UserId,
                CCOMPANY_ID = clientHelper.CompanyId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00100);
        }
        private void After_Open_LookupOT(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00100DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (PMM00510DTO)_conductorRef.R_GetCurrentData();
            loGetData.COTHER_TAX_ID = loTempResult.CTAX_ID;
            loGetData.COTHER_TAX_NAME = loTempResult.CTAX_NAME;
            loGetData.NOTHER_TAX_PCT = loTempResult.NTAX_PERCENTAGE;
        }
        private async Task OnLostFocusOT()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMM00510DTO loGetData = (PMM00510DTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.COTHER_TAX_ID))
                {
                    loGetData.COTHER_TAX_NAME = "";
                    loGetData.NOTHER_TAX_PCT = 0;
                    return;
                }


                LookupGSL00100ViewModel loLookupViewModel = new LookupGSL00100ViewModel();
                var param = new GSL00100ParameterDTO
                {
                    CUSER_ID = clientHelper.UserId,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CSEARCH_TEXT = loGetData.COTHER_TAX_ID,
                };
                var loResult = await loLookupViewModel.GetSalesTax(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.COTHER_TAX_ID = "";
                    loGetData.COTHER_TAX_NAME = "";
                    loGetData.NOTHER_TAX_PCT = 0;
                }
                else
                {
                    loGetData.COTHER_TAX_ID = loResult.CTAX_ID;
                    loGetData.COTHER_TAX_NAME = loResult.CTAX_NAME;
                    loGetData.NOTHER_TAX_PCT = loResult.NTAX_PERCENTAGE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region lookupWT
        private R_Lookup R_LookupBtnWT;
        private R_TextBox R_TextBoxWT;
        private void Before_Open_LookupWT(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new GSL00200ParameterDTO
            {
                CTAX_TYPE_LIST = _viewModel.Data.CWITHHOLDING_TAX_TYPE,
                CCOMPANY_ID = clientHelper.CompanyId,
                CPROPERTY_ID = _viewModel.PropertyValue
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSL00200);
        }
        private void After_Open_LookupWT(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00200DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            var loGetData = (PMM00510DTO)_conductorRef.R_GetCurrentData();
            loGetData.CWITHHOLDING_TAX_ID = loTempResult.CTAX_ID;
            loGetData.CWITHHOLDING_TAX_NAME = loTempResult.CTAX_NAME;
            loGetData.NWITHHOLDING_TAX_PCT= loTempResult.NTAX_PERCENTAGE;
        }
        private async Task OnLostFocusWT()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMM00510DTO loGetData = (PMM00510DTO)_viewModel.Data;

                if (string.IsNullOrWhiteSpace(loGetData.CWITHHOLDING_TAX_ID))
                {
                    loGetData.CWITHHOLDING_TAX_NAME = "";
                    loGetData.NWITHHOLDING_TAX_PCT = 0;
                    return;
                }


                LookupGSL00200ViewModel loLookupViewModel = new LookupGSL00200ViewModel();
                var param = new GSL00200ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.PropertyValue,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CSEARCH_TEXT = loGetData.CWITHHOLDING_TAX_ID,
                    CTAX_TYPE_LIST = loGetData.CWITHHOLDING_TAX_TYPE
                };
                var loResult = await loLookupViewModel.GetWithholdingTax(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CWITHHOLDING_TAX_ID = "";
                    loGetData.CWITHHOLDING_TAX_NAME = "";
                    loGetData.NWITHHOLDING_TAX_PCT = 0;
                }
                else
                {
                    loGetData.CWITHHOLDING_TAX_ID = loResult.CTAX_ID;
                    loGetData.CWITHHOLDING_TAX_NAME = loResult.CTAX_NAME;
                    loGetData.NWITHHOLDING_TAX_PCT = loResult.NTAX_PERCENTAGE;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion

        #region lookupJGL
        private R_Lookup R_LookupBtnJGL;
        private R_TextBox R_TextBoxJGL;
        private void Before_Open_LookupJGL(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var param = new GSL00400ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.PropertyValue,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CJRNGRP_TYPE = "10"
                };

                eventArgs.Parameter = param;
                eventArgs.TargetPageType = typeof(GSL00400);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void After_Open_LookupJGL(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (GSL00400DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }
            var loGetData = (PMM00510DTO)_conductorRef.R_GetCurrentData();
            loGetData.CSERVICE_JRNGRP_CODE = loTempResult.CJRNGRP_CODE;
            loGetData.CSERVICE_JRNGRP_NAME = loTempResult.CJRNGRP_NAME;
        }
        private async Task OnLostFocusJGL()
        {
            R_Exception loEx = new R_Exception();

            try
            {
                PMM00510DTO loGetData = (PMM00510DTO)_viewModel.Data;
                if (string.IsNullOrWhiteSpace(loGetData.CSERVICE_JRNGRP_CODE))
                {
                    loGetData.CSERVICE_JRNGRP_NAME = "";
                    return;
                }

                var param = new GSL00400ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.PropertyValue,
                    CCOMPANY_ID = clientHelper.CompanyId,
                    CJRNGRP_TYPE = "10",
                    CUSER_LOGIN_ID = clientHelper.UserId,
                    CSEARCH_TEXT = loGetData.CSERVICE_JRNGRP_CODE
                };


                LookupGSL00400ViewModel loLookupViewModel = new LookupGSL00400ViewModel();
                var loResult = await loLookupViewModel.GetJournalGroup(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                            typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                            "_ErrLookup01"));
                    loGetData.CSERVICE_JRNGRP_CODE = "";
                    loGetData.CSERVICE_JRNGRP_NAME = "";
                }
                else
                {
                    loGetData.CSERVICE_JRNGRP_CODE = loResult.CJRNGRP_CODE;
                    loGetData.CSERVICE_JRNGRP_NAME = loResult.CJRNGRP_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        #endregion


    }
}
