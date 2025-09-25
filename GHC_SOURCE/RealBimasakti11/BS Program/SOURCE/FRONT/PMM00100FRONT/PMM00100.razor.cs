using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML01600;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100MODEL.View_Model;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace PMM00100FRONT
{
    public partial class PMM00100 : R_Page
    {
        //var
        private PMM00100ViewModel _viewModel = new();

        private R_Conductor _conRef;
        private R_Grid<SystemParamDTO> _gridRef;
        private R_TabPage _tabPage_HoUtillBuildingMapping;
        private R_TabPage _tabPage_BillingParam;
        private R_TabStrip _tabStrip; //ref Tabstrip
        [Inject] private R_ILocalizer<PMM00100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private IClientHelper _clientHelper { get; set; }
        private bool _enableHasNormalMode = true;
        private bool _isOnCRUDmode_tabHoUtillBuildingMapping = false;
        private bool _isOnCRUDmode_tabBilling = false;
        private int _pageSize = 25;

        //abstract
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetInitData(_localizer);
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (SystemParamDetailDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: PMM00100ContextConstant.DEFAULT_MODULE,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: PMM00100ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PMM00100ContextConstant.PROGRAM_ID,
                        Table_Name = PMM00100ContextConstant.TABLE_NAME_SYSPARAM,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PMM00100ContextConstant.PROGRAM_ID,
                        Table_Name = PMM00100ContextConstant.TABLE_NAME_SYSPARAM,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID)
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

        //grid events
        private async Task SysParam_GetListAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetSystemParamListAsync();
                eventArgs.ListEntityResult = _viewModel.SystemParams;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async void SysParam_Display(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loCurrentProperty = R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(eventArgs.Data);
                _viewModel.CurrentPropertyId = loCurrentProperty.CPROPERTY_ID ?? "";
                await _conRef.R_GetEntity(loCurrentProperty);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task SysParam_GetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(eventArgs.Data);
                await _viewModel.GetSystemParamDetailAsync(loData);
                eventArgs.Result = _viewModel.SystemParamDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void SysParam_GridToEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.GridData = R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void SysParam_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (SystemParamDetailDTO)eventArgs.Data;
                if (loData != null)
                {
                    _viewModel.SelectedCurrentPeriodMonth = DateTime.Now.Month.ToString("D2");
                    _viewModel.SelectedCurrentPeriodYear = DateTime.Now.Year;
                    _viewModel.SelectedSoftPeriodMonth = DateTime.Now.Month.ToString("D2");
                    _viewModel.SelectedSoftPeriodYear = DateTime.Now.Year;
                    loData.CWHT_MODE = "I";
                    loData.CINVOICE_MODE = "I";
                    loData.LELECTRIC_END_MONTH = false;
                    loData.LWATER_END_MONTH = false;
                    loData.LGAS_END_MONTH = false;
                    loData.LGLLINK = false;
                    loData.LINV_PROCESS_FLAG = false;
                    _viewModel.SelectedElectricPeriodMonth = DateTime.Now.Month.ToString("D2");
                    _viewModel.SelectedWaterPeriodMonth = DateTime.Now.Month.ToString("D2");
                    _viewModel.SelectedGasPeriodMonth = DateTime.Now.Month.ToString("D2");
                    _viewModel.SelectedElectricPeriodYear = DateTime.Now.Year;
                    _viewModel.SelectedWaterPeriodYear = DateTime.Now.Year;
                    _viewModel.SelectedGasPeriodYear = DateTime.Now.Year;
                    loData.LALL_BUILDING = true;
                    loData.LPRIORITY = false;
                    loData.LDISTRIBUTE_PDF = false;
                    loData.LINCLUDE_IMAGE = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void SysParam_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.CurrentPropertyId))
                {
                    eventArgs.Allow = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public void SysParam_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_viewModel.CurrentPropertyId))
                {
                    eventArgs.Allow = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void SysParam_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(eventArgs.Data);

                if (string.IsNullOrWhiteSpace(loData.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val1"));
                }
                if (_viewModel.SelectedSoftPeriodYear == null || string.IsNullOrWhiteSpace(_viewModel.SelectedSoftPeriodMonth))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val2"));
                }

                if (_viewModel.SelectedCurrentPeriodYear == null || string.IsNullOrWhiteSpace(_viewModel.SelectedCurrentPeriodMonth))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val3"));
                }

                if (_viewModel.SelectedInvPeriodYear == null || string.IsNullOrWhiteSpace(_viewModel.SelectedInvPeriodMonth))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val4"));
                }

                if (_viewModel.SelectedElectricPeriodYear == null || string.IsNullOrWhiteSpace(_viewModel.SelectedElectricPeriodMonth))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val5"));
                }

                if (_viewModel.SelectedWaterPeriodYear == null || string.IsNullOrWhiteSpace(_viewModel.SelectedWaterPeriodMonth))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val6"));
                }

                if (_viewModel.SelectedGasPeriodYear == null || string.IsNullOrWhiteSpace(_viewModel.SelectedGasPeriodMonth))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val7"));
                }

                if (!loData.LELECTRIC_END_MONTH && _viewModel.SelectedElectricCutOffDate == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val8"));
                }

                if (!loData.LWATER_END_MONTH && _viewModel.SelectedWaterCutOffDate == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val9"));
                }

                if (!loData.LGAS_END_MONTH && _viewModel.SelectedGasCutOffDate == 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val10"));
                }

                if (loData.IMAX_DAYS <= 0 || loData.IMAX_DAYS == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val16"));
                }
                if (loData.IMAX_DAYS <= 0 || loData.IMAX_ATTEMPTS == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val17"));
                }
                if (string.IsNullOrWhiteSpace(loData.CCALL_TYPE_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val18"));
                }
                eventArgs.Cancel = loEx.HasError;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void SysParam_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = eventArgs.Data as SystemParamDetailDTO;
                loData.CPROPERTY_ID = _viewModel.CurrentPropertyId;
                loData.CCURRENT_PERIOD = _viewModel.SelectedCurrentPeriodYear.ToString() + _viewModel.SelectedCurrentPeriodMonth;
                loData.CSOFT_PERIOD = _viewModel.SelectedSoftPeriodYear.ToString() + _viewModel.SelectedSoftPeriodMonth;
                loData.CINV_PRD = _viewModel.SelectedInvPeriodYear.ToString() + _viewModel.SelectedInvPeriodMonth;
                loData.CELECTRIC_PERIOD = _viewModel.SelectedElectricPeriodYear.ToString() + _viewModel.SelectedElectricPeriodMonth;
                loData.CWATER_PERIOD = _viewModel.SelectedWaterPeriodYear.ToString() + _viewModel.SelectedWaterPeriodMonth;
                loData.CGAS_PERIOD = _viewModel.SelectedGasPeriodYear.ToString() + _viewModel.SelectedGasPeriodMonth;
                loData.CELECTRIC_DATE = !loData.LELECTRIC_END_MONTH ? _viewModel.SelectedElectricCutOffDate.ToString("D2") : "";
                loData.CWATER_DATE = !loData.LWATER_END_MONTH ? _viewModel.SelectedWaterCutOffDate.ToString("D2") : "";
                loData.CGAS_DATE = !loData.LGAS_END_MONTH ? _viewModel.SelectedGasCutOffDate.ToString("D2") : "";
                loData.LINCLUDE_IMAGE = loData.LDISTRIBUTE_PDF && loData.LINCLUDE_IMAGE;
                loData.LALL_BUILDING = true; //CR06
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task SysParam_SaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loParam = eventArgs.Data as SystemParamDetailDTO;
                await _viewModel.SaveSytemParamDetailAsync(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.SystemParamDetail;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void SysParam_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //soft,inv,curr period
                _viewModel.SelectedSoftPeriodMonth = !string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CSOFT_PERIOD) && _viewModel.SystemParamDetail.CSOFT_PERIOD.Length >= 6
                    ? _viewModel.SystemParamDetail.CSOFT_PERIOD.Substring(4, 2)
                    : "";
                _viewModel.SelectedCurrentPeriodMonth = !string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CCURRENT_PERIOD) && _viewModel.SystemParamDetail.CCURRENT_PERIOD.Length >= 6
                    ? _viewModel.SystemParamDetail.CCURRENT_PERIOD.Substring(4, 2)
                    : "";
                _viewModel.SelectedInvPeriodMonth = !string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CINV_PRD) && _viewModel.SystemParamDetail.CINV_PRD.Length >= 6
                    ? _viewModel.SystemParamDetail.CINV_PRD.Substring(4, 2)
                    : "";
                if (!string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CCURRENT_PERIOD))
                {
                    _viewModel.SelectedCurrentPeriodYear = int.Parse(_viewModel.SystemParamDetail.CCURRENT_PERIOD.Substring(0, 4));
                }
                else
                {
                    _viewModel.SelectedCurrentPeriodYear = 0;
                }

                if (!string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CSOFT_PERIOD))
                {
                    _viewModel.SelectedSoftPeriodYear = int.Parse(_viewModel.SystemParamDetail.CSOFT_PERIOD.Substring(0, 4));
                }
                else
                {
                    _viewModel.SelectedSoftPeriodYear = 0;
                }

                if (!string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CINV_PRD))
                {
                    _viewModel.SelectedInvPeriodYear = int.Parse(_viewModel.SystemParamDetail.CINV_PRD.Substring(0, 4));
                }
                else
                {
                    _viewModel.SelectedInvPeriodYear = 0;
                }

                //utility
                _viewModel.SelectedElectricPeriodMonth = !string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CELECTRIC_PERIOD) && _viewModel.SystemParamDetail.CELECTRIC_PERIOD.Length >= 6
                       ? _viewModel.SystemParamDetail.CELECTRIC_PERIOD.Substring(4, 2)
                       : "";
                _viewModel.SelectedWaterPeriodMonth = !string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CWATER_PERIOD) && _viewModel.SystemParamDetail.CWATER_PERIOD.Length >= 6
                    ? _viewModel.SystemParamDetail.CWATER_PERIOD.Substring(4, 2)
                    : "";
                _viewModel.SelectedGasPeriodMonth = !string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CGAS_PERIOD) && _viewModel.SystemParamDetail.CGAS_PERIOD.Length >= 6
                    ? _viewModel.SystemParamDetail.CGAS_PERIOD.Substring(4, 2)
                    : "";
                if (!string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CELECTRIC_PERIOD))
                {
                    _viewModel.SelectedElectricPeriodYear = int.Parse(_viewModel.SystemParamDetail.CELECTRIC_PERIOD.Substring(0, 4));
                }
                else
                {
                    _viewModel.SelectedElectricPeriodYear = 0;
                }

                if (!string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CWATER_PERIOD))
                {
                    _viewModel.SelectedWaterPeriodYear = int.Parse(_viewModel.SystemParamDetail.CWATER_PERIOD.Substring(0, 4));
                }
                else
                {
                    _viewModel.SelectedWaterPeriodYear = 0;
                }

                if (!string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CGAS_PERIOD))
                {
                    _viewModel.SelectedGasPeriodYear = int.Parse(_viewModel.SystemParamDetail.CGAS_PERIOD.Substring(0, 4));
                }
                else
                {
                    _viewModel.SelectedGasPeriodYear = 0;
                }

                //cutoffDate for utility
                _viewModel.SelectedElectricCutOffDate = !string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CELECTRIC_DATE) && int.TryParse(_viewModel.SystemParamDetail.CELECTRIC_DATE, out var lnDate) ? lnDate : 0;
                _viewModel.SelectedWaterCutOffDate = !string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CWATER_DATE) && int.TryParse(_viewModel.SystemParamDetail.CWATER_DATE, out lnDate) ? lnDate : 0;
                _viewModel.SelectedGasCutOffDate = !string.IsNullOrWhiteSpace(_viewModel.SystemParamDetail.CGAS_DATE) && int.TryParse(_viewModel.SystemParamDetail.CGAS_DATE, out lnDate) ? lnDate : 0;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void SysParam_SetOther(R_SetEventArgs eventArgs)
        {
            _enableHasNormalMode = eventArgs.Enable;
        }

        //tabstrip
        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Cancel = string.IsNullOrWhiteSpace(_viewModel.Data.CPROPERTY_ID) || _isOnCRUDmode_tabHoUtillBuildingMapping || _isOnCRUDmode_tabHoUtillBuildingMapping || _conRef.R_ConductorMode != R_eConductorMode.Normal;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //tabpage - HO utill
        private void BeforeOpenTabPage_HoUtillBuildingMapping(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CPROPERTY_ID))
                {
                    eventArgs.Parameter = new SystemParamDetailDTO()
                    {
                        CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                        CPROPERTY_NAME = _viewModel.Data.CPROPERTY_NAME,
                        LALL_BUILDING = _viewModel.Data.LALL_BUILDING
                    };
                    eventArgs.TargetPageType = typeof(PMM00101);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void TabEventCallback_HOUtilBuildMapping(object poValue)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _isOnCRUDmode_tabHoUtillBuildingMapping = !(bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //tabpage - Billing
        private void BeforeOpenTabPage_BillingSystemParam(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CPROPERTY_ID))
                {
                    eventArgs.Parameter = new SystemParamDetailDTO()
                    {
                        CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                        CPROPERTY_NAME = _viewModel.Data.CPROPERTY_NAME,
                    };
                    eventArgs.TargetPageType = typeof(PMM00102);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void TabEventCallback_BillingSystemParam(object poValue)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _isOnCRUDmode_tabBilling = !(bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //valuechanged
        public async Task ComboboxProperty_ValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.Data.CPROPERTY_ID = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;

                //reset field that use property as param
                _viewModel.Data.COVER_RECEIPT = "";
                _viewModel.Data.COVER_RECEIPT_DESC = "";
                _viewModel.Data.CLESS_RECEIPT = "";
                _viewModel.Data.CLESS_RECEIPT_DESC = "";
                _viewModel.Data.CCALL_TYPE_ID = "";
                _viewModel.Data.CCALL_TYPE_NAME = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        //lookups
        private async Task BeforeOpen_CurrRatetypeAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00800ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId
                };
                eventArgs.TargetPageType = typeof(GSL00800);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task AfterOpen_CurrRatetypeAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (GSL00800DTO)eventArgs.Result;
                    _viewModel.Data.CCUR_RATETYPE_CODE = loTempResult.CRATETYPE_CODE ?? "";
                    _viewModel.Data.CCUR_RATETYPE_DESC = loTempResult.CRATETYPE_DESCRIPTION ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_LookupCurrRatetype()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CCUR_RATETYPE_CODE))
                {
                    LookupGSL00800ViewModel loLookupViewModel = new LookupGSL00800ViewModel(); //use GSL's model

                    var loResult = await loLookupViewModel.GetCurrencyRateType(new GSL00800ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.Data.CCUR_RATETYPE_CODE
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    _viewModel.Data.CCUR_RATETYPE_CODE = loResult?.CRATETYPE_CODE ?? "";
                    _viewModel.Data.CCUR_RATETYPE_DESC = loResult?.CRATETYPE_DESCRIPTION ?? "";
                }
                else
                {
                    _viewModel.Data.CCUR_RATETYPE_CODE = "";
                    _viewModel.Data.CCUR_RATETYPE_DESC = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        private async Task BeforeOpen_TaxRatetypeAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00800ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId
                };
                eventArgs.TargetPageType = typeof(GSL00800);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task AfterOpen_TaxRatetypeAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (GSL00800DTO)eventArgs.Result;
                    _viewModel.Data.CTAX_RATETYPE_CODE = loTempResult.CRATETYPE_CODE ?? "";
                    _viewModel.Data.CTAX_RATETYPE_DESC = loTempResult.CRATETYPE_DESCRIPTION ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_LookupTaxRatetype()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CTAX_RATETYPE_CODE))
                {
                    LookupGSL00800ViewModel loLookupViewModel = new LookupGSL00800ViewModel(); //use GSL's model

                    var loResult = await loLookupViewModel.GetCurrencyRateType(new GSL00800ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.Data.CTAX_RATETYPE_CODE
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                    }
                    _viewModel.Data.CTAX_RATETYPE_CODE = loResult?.CRATETYPE_CODE ?? "";
                    _viewModel.Data.CTAX_RATETYPE_DESC = loResult?.CRATETYPE_DESCRIPTION ?? "";
                }
                else
                {
                    _viewModel.Data.CTAX_RATETYPE_CODE = "";
                    _viewModel.Data.CTAX_RATETYPE_DESC = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        private async Task BeforeOpen_OverReceiptAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(_conRef.R_GetCurrentData()));
                if (string.IsNullOrEmpty(loData.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val1"));
                }
                else
                {
                    eventArgs.Parameter = new GSL01400ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                        CCHARGES_TYPE_ID = "A"
                    };
                    eventArgs.TargetPageType = typeof(GSL01400);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task AfterOpen_OverReceiptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (GSL01400DTO)eventArgs.Result;
                    _viewModel.Data.COVER_RECEIPT = loTempResult.CCHARGES_ID ?? "";
                    _viewModel.Data.COVER_RECEIPT_DESC = loTempResult.CCHARGES_NAME ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_LookupOverReceipt()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(_conRef.R_GetCurrentData()));
                if (string.IsNullOrEmpty(loData.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val1"));
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(_viewModel.Data.COVER_RECEIPT))
                    {
                        LookupGSL01400ViewModel loLookupViewModel = new LookupGSL01400ViewModel(); //use GSL's model

                        var loResult = await loLookupViewModel.GetOtherCharges(new GSL01400ParameterDTO()
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                            CCHARGES_TYPE_ID = "A",
                            CSEARCH_TEXT = _viewModel.Data.COVER_RECEIPT,
                        });

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));
                        }
                        _viewModel.Data.COVER_RECEIPT = loResult?.CCHARGES_ID ?? "";
                        _viewModel.Data.COVER_RECEIPT_DESC = loResult?.CCHARGES_NAME ?? "";
                    }
                    else
                    {
                        _viewModel.Data.COVER_RECEIPT = "";
                        _viewModel.Data.COVER_RECEIPT_DESC = "";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        private async Task BeforeOpen_LessReceiptAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(_conRef.R_GetCurrentData()));
                if (string.IsNullOrEmpty(loData.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val1"));
                }
                else
                {
                    eventArgs.Parameter = new GSL01400ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                        CCHARGES_TYPE_ID = "D"
                    };
                    eventArgs.TargetPageType = typeof(GSL01400);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task AfterOpen_LessReceiptAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (GSL01400DTO)eventArgs.Result;
                    _viewModel.Data.CLESS_RECEIPT = loTempResult.CCHARGES_ID ?? "";
                    _viewModel.Data.CLESS_RECEIPT_DESC = loTempResult.CCHARGES_NAME ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_LookupLessReceipt()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(_conRef.R_GetCurrentData()));
                if (string.IsNullOrEmpty(loData.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val1"));
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(_viewModel.Data.CLESS_RECEIPT))
                    {
                        LookupGSL01400ViewModel loLookupViewModel = new LookupGSL01400ViewModel(); //use GSL's model

                        var loResult = await loLookupViewModel.GetOtherCharges(new GSL01400ParameterDTO()
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                            CCHARGES_TYPE_ID = "D",
                            CSEARCH_TEXT = _viewModel.Data.CLESS_RECEIPT,
                        });

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));
                        }
                        _viewModel.Data.CLESS_RECEIPT = loResult?.CCHARGES_ID ?? "";
                        _viewModel.Data.CLESS_RECEIPT_DESC = loResult?.CCHARGES_NAME ?? "";
                    }
                    else
                    {
                        _viewModel.Data.CLESS_RECEIPT = "";
                        _viewModel.Data.CLESS_RECEIPT_DESC = "";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        private async Task BeforeOpen_SLATypeAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(_conRef.R_GetCurrentData()));
                if (string.IsNullOrEmpty(loData.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val1"));
                }
                else
                {
                    eventArgs.Parameter = new LML01600ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                        CCALL_TYPE_ID = "",
                        CSEARCH_TEXT = ""
                    };
                    eventArgs.TargetPageType = typeof(LML01600);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task AfterOpen_SLATypeAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (LML01600DTO)eventArgs.Result;
                    _viewModel.Data.CCALL_TYPE_ID = loTempResult.CCALL_TYPE_ID ?? "";
                    _viewModel.Data.CCALL_TYPE_NAME = loTempResult.CCALL_TYPE_NAME ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task OnLostFocus_LookupSLAType()
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (R_FrontUtility.ConvertObjectToObject<SystemParamDetailDTO>(_conRef.R_GetCurrentData()));
                if (string.IsNullOrEmpty(loData.CPROPERTY_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(PMM00100FrontResources.Resources_Dummy_Class), "_val1"));
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(_viewModel.Data.CCALL_TYPE_ID))
                    {
                        LookupLML01600ViewModel loLookupViewModel = new(); //use GSL's model

                        var loResult = await loLookupViewModel.GetSLACallType(new LML01600ParameterDTO()
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CPROPERTY_ID = _viewModel.Data.CPROPERTY_ID,
                            CCALL_TYPE_ID = "",
                            CSEARCH_TEXT = _viewModel.Data.CCALL_TYPE_ID,
                        });

                        if (loResult == null)
                        {
                            loEx.Add(R_FrontUtility.R_GetError(
                                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                    "_ErrLookup01"));
                            goto EndBlock;
                        }
                        _viewModel.Data.CCALL_TYPE_ID = loResult.CCALL_TYPE_ID ?? "";
                        _viewModel.Data.CCALL_TYPE_NAME = loResult.CCALL_TYPE_NAME ?? "";
                    }
                    else
                    {
                        _viewModel.Data.CCALL_TYPE_ID = "";
                        _viewModel.Data.CCALL_TYPE_NAME = "";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        private void OnChange_DistributePDF()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //turn off linclude image if distribute pdf =false
                if (!_viewModel.Data.LDISTRIBUTE_PDF) _viewModel.Data.LINCLUDE_IMAGE = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}