using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Microsoft.AspNetCore.Components;
using PMB01600Common.DTOs;
using PMB01600Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMB01600Front
{
    public partial class PMB01600 : R_Page
    {
        private PMB01600ViewModel _viewModel = new();

        private R_ConductorGrid _conductorRefHeader;
        private R_Grid<PMB01600BillingStatementHeaderDTO> _gridRefHeader = new();

        private R_ConductorGrid _conductorRefDetail;
        private R_Grid<PMB01600BillingStatementDetailDTO> _gridRefDetail = new();

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        #region HandleError

        private void DisplayErrorInvoke(R_Exception poException)
        {
            R_DisplayException(poException);
        }

        #endregion

        public async Task ShowSuccessInvoke()
        {
            // _processButton.Enabled = true;
            await R_MessageBox.Show("", _localizer["SuccessDistribute"], R_eMessageBoxButtonType.OK);
        }


        protected override async Task R_Init_From_Master(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.Init();
                await _setDefaultTenant();


                //_viewModelDistribute.StateChangeAction = StateChangeInvoke;
                //_viewModelDistribute.DisplayErrorAction = DisplayErrorInvoke;
                //_viewModelDistribute.ShowSuccessAction = async () => { await ShowSuccessInvoke(); };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private async Task _setDefaultTenant()
        {
            var loEx = new R_Exception();

            try
            {
                if (string.IsNullOrEmpty(_viewModel.Param.CPROPERTY_ID)) return;

                var loLookupViewModel = new LookupLML00600ViewModel();
                var param = new LML00600ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID,
                    CCUSTOMER_TYPE = _viewModel.TenantType
                };
                await loLookupViewModel.GetTenantList(param);
                if (loLookupViewModel.TenantList.Count > 0)
                {
                    _viewModel.Param.CFROM_TENANT_ID = loLookupViewModel.TenantList.FirstOrDefault()?.CTENANT_ID;
                    _viewModel.Param.CFROM_TENANT_NAME = loLookupViewModel.TenantList
                        .Where(x => x.CTENANT_ID == _viewModel.Param.CFROM_TENANT_ID)
                        .Select(x => x.CTENANT_NAME).FirstOrDefault() ?? string.Empty;
                    _viewModel.Param.CTO_TENANT_ID = loLookupViewModel.TenantList.LastOrDefault()?.CTENANT_ID;
                    _viewModel.Param.CTO_TENANT_NAME = loLookupViewModel.TenantList
                        .Where(x => x.CTENANT_ID == _viewModel.Param.CTO_TENANT_ID)
                        .Select(x => x.CTENANT_NAME).FirstOrDefault() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task _valueChangedProperty(string value)
        {
            var loEx = new R_Exception();
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value == _viewModel.Param.CPROPERTY_ID) return;

                    _viewModel.Param.CFROM_TENANT_ID = string.Empty;
                    _viewModel.Param.CFROM_TENANT_NAME = string.Empty;
                    _viewModel.Param.CTO_TENANT_ID = string.Empty;
                    _viewModel.Param.CTO_TENANT_NAME = string.Empty;
                    _viewModel.Param.CPROPERTY_ID = value;
                    _viewModel.Param.CPROPERTY_NAME =
                        _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == value)?.CPROPERTY_NAME ?? string.Empty;

                    await _setDefaultTenant();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region Lookup From Tenant

        private async Task OnLostFocusFromTenant()
        {
            var loEx = new R_Exception();

            var loLookupViewModel = new LookupLML00600ViewModel();
            try
            {
                if (string.IsNullOrEmpty(_viewModel.Param.CFROM_TENANT_ID))
                {
                    _viewModel.Param.CFROM_TENANT_NAME = "";
                    return;
                }

                var param = new LML00600ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID,
                    CCUSTOMER_TYPE = _viewModel.TenantType,
                    CSEARCH_TEXT = _viewModel.Param.CFROM_TENANT_ID
                };

                var loResult = await loLookupViewModel.GetTenant(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                        "_NotFound"));
                    _viewModel.Param.CFROM_TENANT_ID = "";
                    _viewModel.Param.CFROM_TENANT_NAME = "";
                    goto EndBlock;
                }

                _viewModel.Param.CFROM_TENANT_ID = loResult.CTENANT_ID;
                _viewModel.Param.CFROM_TENANT_NAME = loResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            await R_DisplayExceptionAsync(loEx);
        }

        private void BeforeOpenLookupFromTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = new LML00600ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID,
                    CCUSTOMER_TYPE = _viewModel.TenantType
                };

                eventArgs.Parameter = loParameter;
                eventArgs.TargetPageType = typeof(LML00600);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterOpenLookupFromTenant(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                var loTempResult = (LML00600DTO)eventArgs.Result;
                _viewModel.Param.CFROM_TENANT_ID = loTempResult.CTENANT_ID;
                _viewModel.Param.CFROM_TENANT_NAME = loTempResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        #region Lookup To Tenant

        private async Task OnLostFocusToTenant()
        {
            var loEx = new R_Exception();

            var loLookupViewModel = new LookupLML00600ViewModel();
            try
            {
                if (string.IsNullOrEmpty(_viewModel.Param.CTO_TENANT_ID))
                {
                    _viewModel.Param.CTO_TENANT_NAME = "";
                    return;
                }

                var param = new LML00600ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID,
                    CCUSTOMER_TYPE = _viewModel.TenantType,
                    CSEARCH_TEXT = _viewModel.Param.CTO_TENANT_ID
                };

                var loResult = await loLookupViewModel.GetTenant(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                        "_NotFound"));
                    _viewModel.Param.CTO_TENANT_ID = "";
                    _viewModel.Param.CTO_TENANT_NAME = "";
                    goto EndBlock;
                }

                _viewModel.Param.CTO_TENANT_ID = loResult.CTENANT_ID;
                _viewModel.Param.CTO_TENANT_NAME = loResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            await R_DisplayExceptionAsync(loEx);
        }

        private void BeforeOpenLookupToTenant(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParameter = new LML00600ParameterDTO
                {
                    CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID,
                    CCUSTOMER_TYPE = _viewModel.TenantType
                };

                eventArgs.Parameter = loParameter;
                eventArgs.TargetPageType = typeof(LML00600);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterOpenLookupToTenant(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.Result == null) return;

                var loTempResult = (LML00600DTO)eventArgs.Result;
                _viewModel.Param.CTO_TENANT_ID = loTempResult.CTENANT_ID;
                _viewModel.Param.CTO_TENANT_NAME = loTempResult.CTENANT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion


        private void CheckBoxSelectValueChanged(R_CheckBoxSelectValueChangedEventArgs eventArgs)
        {
            eventArgs.Enabled = true;
        }


        private async Task GetHeaderListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetBillingStatementHeaderList();
                eventArgs.ListEntityResult = _viewModel.GridHeaderList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task DisplayHeader(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (PMB01600BillingStatementHeaderDTO)eventArgs.Data;
                _viewModel.EntityHeader = loData;

                await _gridRefDetail.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetDetailListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetBillingStatementDetailList();
                eventArgs.ListEntityResult = _viewModel.GridDetailList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void _valueChangedPeriod(string value)
        {
            _viewModel.Param.CMONTH = string.IsNullOrEmpty(value) ? DateTime.Now.Month.ToString("D2") : value;
        }

        private async Task OnClickRefresh()
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRefHeader.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task SaveBatch(R_ServiceSaveBatchEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempDataList = (List<PMB01600BillingStatementHeaderDTO>)eventArgs.Data;
                //var loDataList =
                //    R_FrontHeader.ConvertCollectionToCollection<PMB01600UploadUtilityErrorValidateDTO>(loTempDataList
                //        .Where(x => x.LSELECTED).ToList());

                //var loUtilityType = loTempDataList.FirstOrDefault().CUTILITY_TYPE;
                //_viewModelSave.CompanyId = ClientHelper.CompanyId;
                //_viewModelSave.UserId = ClientHelper.UserId;
                //_viewModelSave.UploadParam.CPROPERTY_ID = _viewModelUtility.Property.CPROPERTY_ID;
                //_viewModelSave.UploadParam.EUTILITY_TYPE =
                //    loUtilityType is "01" or "02"
                //        ? EPMB01600UtilityUsageType.EC
                //        : EPMB01600UtilityUsageType.WG;

                //_viewModelSave.IsUpload = false;

                //await _viewModelSave.SaveBulkFile(poUploadParam: _viewModelSave.UploadParam,
                //    poDataList: loDataList.ToList());

                //if (_viewModelSave.IsError)
                //{
                //    loEx.Add("Error", "Utility Usage saved is not successfully!");
                //}

                //_enabledBtn = true;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
