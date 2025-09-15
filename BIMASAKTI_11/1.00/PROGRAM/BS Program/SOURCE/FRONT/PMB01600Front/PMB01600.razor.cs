using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Microsoft.AspNetCore.Components;
using PMB01600Common.DTOs;
using PMB01600Common.Batch;
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
using BlazorClientHelper;
using R_APICommonDTO;

namespace PMB01600Front
{
    public partial class PMB01600 : R_Page
    {
        private PMB01600ViewModel _viewModel = new();
        private PMB01600BatchViewModel _viewModelBatch = new();

        private R_ConductorGrid _conductorRefHeader;
        private R_Grid<PMB01600BillingStatementHeaderDTO> _gridRefHeader = new();

        private R_ConductorGrid _conductorRefDetail;
        private R_Grid<PMB01600BillingStatementDetailDTO> _gridRefDetail = new();

        [Inject] IClientHelper ClientHelper { get; set; }

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        #region HandleError

        private void DisplayErrorInvoke(R_APIException poException)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
            this.R_DisplayException(loEx);
        }
        #endregion

        public async Task ShowSuccessInvoke()
        {
            // _processButton.Enabled = true;
            var loMsg = await R_MessageBox.Show("", _localizer["Undo Billing Statement completed successfully!"], R_eMessageBoxButtonType.OK);

            await _gridRefHeader.R_RefreshGrid(null);
        }


        protected override async Task R_Init_From_Master(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.Init();
                await _setDefaultTenant();


                _viewModelBatch.StateChangeAction = StateChangeInvoke;
                _viewModelBatch.DisplayErrorAction = DisplayErrorInvoke;
                _viewModelBatch.ShowSuccessAction = async () => { await ShowSuccessInvoke(); };
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
                var loDataList = R_FrontUtility.ConvertCollectionToCollection<PMB01600ForBatchDTO>(loTempDataList
                        .Where(x => x.LSELECTED).ToList());

                _viewModelBatch.BatchParam.CCOMPANY_ID = ClientHelper.CompanyId;
                _viewModelBatch.BatchParam.CUSER_ID = ClientHelper.UserId;
                _viewModelBatch.BatchParam.CPROPERTY_ID = _viewModel.Param.CPROPERTY_ID;
                _viewModelBatch.BatchParam.CREF_PRD = _viewModel.Param.IYEAR + _viewModel.Param.CMONTH;


                await _viewModelBatch.SaveBulkFile(poBatchParam: _viewModelBatch.BatchParam,
                    poDataList: loDataList.ToList());

                if (_viewModelBatch.IsError)
                {
                    loEx.Add("Error", _localizer["Undo Billing Statement has been cancelled!"]);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickProcess()
        {
            var loEx = new R_Exception();

            try
            {
                var loConfirm = await R_MessageBox.Show("", _localizer["Are you sure want to process selected Tenant?"], R_eMessageBoxButtonType.YesNo);

                if (loConfirm == R_eMessageBoxResult.Yes)
                {
                    await _gridRefHeader.R_SaveBatch();
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
