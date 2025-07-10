using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100MODEL.View_Model;
using PMM00100FrontResources;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00400;

namespace PMM00100FRONT
{
    public partial class PMM00101 : R_Page
    {
        //variables
        private PMM00101ViewModel _viewModel = new();
        private R_Conductor _conRef;
        private R_Grid<HoUtilBuildingMappingDTO> _gridRef;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private IClientHelper _clientHelper { get; set; }
        private bool _enableGrid = true;
        private const string CTAXABLE_TYPE = "0";
        private const string CACTIVE_TYPE = "1";
        private int _pageSize = 25;

        //overrides
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel.SetCurrentProeprty(poParameter as SystemParamDetailDTO);
                if (!string.IsNullOrWhiteSpace(_viewModel.SystemParamDetails.CPROPERTY_ID))
                {
                    await _gridRef.R_RefreshGrid(null);
                }
                if (_viewModel.SystemParamDetails.LALL_BUILDING)
                {
                    await _conRef.R_GetEntity(new HoUtilBuildingMappingDTO() { CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID });
                }
                _enableGrid = !_viewModel.SystemParamDetails.LALL_BUILDING;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;

            try
            {
                var loData = (HoUtilBuildingMappingDTO)eventArgs.Data;
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
                        Table_Name = PMM00100ContextConstant.TABLE_NAME_SYSPARAM_UTIL,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CBUILDING_ID)
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
                        Table_Name = PMM00100ContextConstant.TABLE_NAME_SYSPARAM_UTIL,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CBUILDING_ID)
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
        private async Task HoUtilBuildingMappingGrid_GetListAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.GetBuildingListAsync();
                eventArgs.ListEntityResult = _viewModel.HoUtilBuildingMappings;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task HoUtilBuildingMappingGrid_DisplayAsync(R_DisplayEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!_viewModel.SystemParamDetails.LALL_BUILDING)
                {
                    var loCurrentRow = R_FrontUtility.ConvertObjectToObject<HoUtilBuildingMappingDTO>(eventArgs.Data);
                    _viewModel.CurrentBuildingId = loCurrentRow.CBUILDING_ID;
                    await _conRef.R_GetEntity(loCurrentRow);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //form event
        private async Task HoUtilBuildingMapping_GetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<HoUtilBuildingMappingDTO>(eventArgs.Data);
                await _viewModel.GetUtillMapping_BuildingAsync(loData);
                eventArgs.Result = _viewModel.HoUtilBuildingMapping;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void HoUtilBuildingMapping_CheckAdd(R_CheckAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Allow = _viewModel.SystemParamDetails.LALL_BUILDING ? !_viewModel.IsBuildingMappingFound : string.IsNullOrWhiteSpace(_viewModel.HoUtilBuildingMapping.CBUILDING_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void HoUtilBuildingMapping_CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                eventArgs.Allow = _viewModel.IsBuildingMappingFound;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task HoUtilBuildingMapping_SetOtherAsync(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Task.Delay(1);
                await InvokeTabEventCallbackAsync(eventArgs.Enable);
                _enableGrid = eventArgs.Enable;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void HoUtilBuildingMapping_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<HoUtilBuildingMappingDTO>(eventArgs.Data);

                if (_viewModel.HoUtilBuildingMapping.LELECTRICITY && string.IsNullOrWhiteSpace(loData.CELECTRICITY_CHARGES_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val11"));
                }
                if (_viewModel.HoUtilBuildingMapping.LELECTRICITY && loData.LELECTRICITY_CHARGES_TAXABLE && string.IsNullOrWhiteSpace(loData.CELECTRICITY_TAX_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val12"));
                }
                if (_viewModel.HoUtilBuildingMapping.LCHILLER && string.IsNullOrWhiteSpace(loData.CCHILLER_CHARGES_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val24"));
                }
                if (_viewModel.HoUtilBuildingMapping.LCHILLER && loData.LCHILLER_CHARGES_TAXABLE && string.IsNullOrWhiteSpace(loData.CCHILLER_TAX_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val25"));
                }
                if (_viewModel.HoUtilBuildingMapping.LGAS && string.IsNullOrWhiteSpace(loData.CGAS_CHARGES_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val13"));
                }
                if (_viewModel.HoUtilBuildingMapping.LGAS && _viewModel.HoUtilBuildingMapping.LGAS_CHARGES_TAXABLE && string.IsNullOrWhiteSpace(loData.CGAS_TAX_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val14"));
                }
                if (_viewModel.HoUtilBuildingMapping.LWATER && string.IsNullOrWhiteSpace(loData.CWATER_CHARGES_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val15"));
                }
                if (_viewModel.HoUtilBuildingMapping.LWATER && _viewModel.HoUtilBuildingMapping.LWATER_CHARGES_TAXABLE && string.IsNullOrWhiteSpace(loData.CWATER_TAX_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val26"));
                }

                eventArgs.Cancel = loEx.HasError;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void HoUtilBuildingMapping_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = (HoUtilBuildingMappingDTO)eventArgs.Data;
                loData.CBUILDING_ID = _viewModel.CurrentBuildingId ?? "";
                loData.CCOMPANY_ID = _clientHelper.CompanyId;
                loData.CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID;
                loData.LALL_BUILDING = _viewModel.SystemParamDetails.LALL_BUILDING;
                loData.LELECTRICITY = _viewModel.HoUtilBuildingMapping.LELECTRICITY;
                loData.LGAS = _viewModel.HoUtilBuildingMapping.LGAS;
                loData.LWATER = _viewModel.HoUtilBuildingMapping.LWATER;
                loData.LELECTRICITY_CHARGES_TAXABLE = _viewModel.HoUtilBuildingMapping.LELECTRICITY_CHARGES_TAXABLE;
                loData.LGAS_CHARGES_TAXABLE = _viewModel.HoUtilBuildingMapping.LGAS_CHARGES_TAXABLE;
                loData.LWATER_CHARGES_TAXABLE = _viewModel.HoUtilBuildingMapping.LWATER_CHARGES_TAXABLE;
                loData.CELECTRICITY_CHARGES_ID = loData.CELECTRICITY_CHARGES_ID ?? "";
                loData.CGAS_CHARGES_ID = loData.CGAS_CHARGES_ID ?? "";
                loData.CWATER_CHARGES_ID = loData.CWATER_CHARGES_ID ?? "";
                loData.CELECTRICITY_TAX_ID = loData.CELECTRICITY_TAX_ID ?? "";
                loData.CGAS_TAX_ID = loData.CGAS_TAX_ID ?? "";
                loData.CWATER_TAX_ID = loData.CWATER_TAX_ID ?? "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task HoUtilBuildingMapping_SaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = eventArgs.Data as HoUtilBuildingMappingDTO;
                await _viewModel.SaveHoUtilBuildingAsync(loData, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.HoUtilBuildingMapping;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void HoUtilBuildingMapping_AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _enableGrid = !_viewModel.SystemParamDetails.LALL_BUILDING;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //onchange utilities
        private void OnChanged_CheckUtilityEnabled(object poValue)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (!_viewModel.HoUtilBuildingMapping.LELECTRICITY)
                {
                    _viewModel.Data.CELECTRICITY_CHARGES_ID = "";
                    _viewModel.Data.CELECTRICITY_CHARGES_NAME = "";
                    _viewModel.Data.CELECTRICITY_TAX_ID = "";
                    _viewModel.Data.CELECTRICITY_TAX_NAME = "";
                    _viewModel.HoUtilBuildingMapping.LELECTRICITY_CHARGES_TAXABLE = false;
                }
                if (!_viewModel.HoUtilBuildingMapping.LCHILLER)
                {
                    _viewModel.Data.CCHILLER_CHARGES_ID = "";
                    _viewModel.Data.CCHILLER_CHARGES_NAME = "";
                    _viewModel.Data.CCHILLER_TAX_ID = "";
                    _viewModel.Data.CCHILLER_TAX_NAME = "";
                    _viewModel.HoUtilBuildingMapping.LCHILLER_CHARGES_TAXABLE = false;
                }
                if (!_viewModel.HoUtilBuildingMapping.LGAS)
                {
                    _viewModel.Data.CGAS_CHARGES_ID = "";
                    _viewModel.Data.CGAS_CHARGES_NAME = "";
                    _viewModel.Data.CGAS_TAX_ID = "";
                    _viewModel.Data.CGAS_TAX_NAME = "";
                    _viewModel.HoUtilBuildingMapping.LGAS_CHARGES_TAXABLE = false;
                }
                if (!_viewModel.HoUtilBuildingMapping.LWATER)
                {
                    _viewModel.Data.CWATER_CHARGES_ID = "";
                    _viewModel.Data.CWATER_CHARGES_NAME = "";
                    _viewModel.Data.CWATER_TAX_ID = "";
                    _viewModel.Data.CWATER_TAX_NAME = "";
                    _viewModel.HoUtilBuildingMapping.LWATER = false;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        //lookup electricity
        private async Task BeforeOpen_ElecChargesAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new LML00400ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = "",
                    CUSER_ID = _clientHelper.UserId,
                    CTAXABLE_TYPE = CTAXABLE_TYPE,
                    CACTIVE_TYPE = CACTIVE_TYPE,
                };
                eventArgs.TargetPageType = typeof(LML00400);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task AfterOpen_ElecChargesAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (LML00400DTO)eventArgs.Result;
                    _viewModel.Data.CELECTRICITY_CHARGES_ID = loTempResult.CCHARGES_ID ?? "";
                    _viewModel.Data.CELECTRICITY_CHARGES_NAME = loTempResult.CCHARGES_NAME ?? "";
                    _viewModel.HoUtilBuildingMapping.LELECTRICITY_CHARGES_TAXABLE = loTempResult.LTAXABLE;
                    _viewModel.Data.CELECTRICITY_TAX_ID = "";
                    _viewModel.Data.CELECTRICITY_TAX_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task OnLostFocus_ElecChargesAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CELECTRICITY_CHARGES_ID))
                {

                    var loLookupViewModel = new LookupLML00400ViewModel(); //use GSL's model
                    var loResult = await loLookupViewModel.GetUtitlityCharges(new LML00400ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID,
                        //CCHARGE_TYPE_ID = _viewModel.Data.CELECTRICITY_CHARGES_ID,
                        CUSER_ID = _clientHelper.UserId,
                        CTAXABLE_TYPE = CTAXABLE_TYPE,
                        CACTIVE_TYPE = CACTIVE_TYPE,
                        CSEARCH_TEXT = _viewModel.Data.CELECTRICITY_CHARGES_ID
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto Endblock;
                    }
                    _viewModel.Data.CELECTRICITY_CHARGES_ID = loResult.CCHARGES_ID ?? "";
                    _viewModel.Data.CELECTRICITY_CHARGES_NAME = loResult.CCHARGES_NAME ?? "";
                    _viewModel.HoUtilBuildingMapping.LELECTRICITY_CHARGES_TAXABLE = loResult.LTAXABLE;
                    _viewModel.Data.CELECTRICITY_TAX_ID = "";
                    _viewModel.Data.CELECTRICITY_TAX_NAME = "";

                }
                else
                {
                    _viewModel.Data.CELECTRICITY_CHARGES_ID = "";
                    _viewModel.Data.CELECTRICITY_CHARGES_NAME = "";
                    _viewModel.HoUtilBuildingMapping.LELECTRICITY_CHARGES_TAXABLE = false;
                    _viewModel.Data.CELECTRICITY_TAX_ID = "";
                    _viewModel.Data.CELECTRICITY_TAX_NAME = "";
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }
        private async Task BeforeOpen_ElecChargesTaxAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00100ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                };
                eventArgs.TargetPageType = typeof(GSL00100);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task AfterOpen_ElecChargesTaxAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (GSL00100DTO)eventArgs.Result;
                    _viewModel.Data.CELECTRICITY_TAX_ID = loTempResult.CTAX_ID ?? "";
                    _viewModel.Data.CELECTRICITY_TAX_NAME = loTempResult.CTAX_NAME ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task OnLostFocus_ElecChargesTaxAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CELECTRICITY_TAX_ID))
                {

                    var loLookupViewModel = new LookupGSL00100ViewModel();
                    var loResult = await loLookupViewModel.GetSalesTax(new GSL00100ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.Data.CELECTRICITY_TAX_ID
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto Endblock;
                    }
                    _viewModel.Data.CELECTRICITY_TAX_ID = loResult.CTAX_ID ?? "";
                    _viewModel.Data.CELECTRICITY_TAX_NAME = loResult.CTAX_NAME ?? "";
                }
                else
                {
                    _viewModel.Data.CELECTRICITY_TAX_ID = "";
                    _viewModel.Data.CELECTRICITY_TAX_NAME = "";
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        //lookup chiller
        private async Task BeforeOpen_ChillerChargesAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new LML00400ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = "",
                    CUSER_ID = _clientHelper.UserId,
                    CTAXABLE_TYPE = CTAXABLE_TYPE,
                    CACTIVE_TYPE = CACTIVE_TYPE,
                };
                eventArgs.TargetPageType = typeof(LML00400);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task AfterOpen_ChillerChargesAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (LML00400DTO)eventArgs.Result;
                    _viewModel.Data.CCHILLER_CHARGES_ID = loTempResult.CCHARGES_ID ?? "";
                    _viewModel.Data.CCHILLER_CHARGES_NAME = loTempResult.CCHARGES_NAME ?? "";
                    _viewModel.HoUtilBuildingMapping.LCHILLER_CHARGES_TAXABLE = loTempResult.LTAXABLE;
                    _viewModel.Data.CCHILLER_TAX_ID = "";
                    _viewModel.Data.CCHILLER_TAX_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task OnLostFocus_ChillerChargesAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CCHILLER_CHARGES_ID))
                {

                    var loLookupViewModel = new LookupLML00400ViewModel(); //use GSL's model
                    var loResult = await loLookupViewModel.GetUtitlityCharges(new LML00400ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID,
                        //CCHARGE_TYPE_ID = _viewModel.Data.CCHILLER_CHARGES_ID,
                        CUSER_ID = _clientHelper.UserId,
                        CTAXABLE_TYPE = CTAXABLE_TYPE,
                        CACTIVE_TYPE = CACTIVE_TYPE,
                        CSEARCH_TEXT = _viewModel.Data.CCHILLER_CHARGES_ID
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto Endblock;
                    }
                    _viewModel.Data.CCHILLER_CHARGES_ID = loResult.CCHARGES_ID ?? "";
                    _viewModel.Data.CCHILLER_CHARGES_NAME = loResult.CCHARGES_NAME ?? "";
                    _viewModel.HoUtilBuildingMapping.LCHILLER_CHARGES_TAXABLE = loResult.LTAXABLE;
                    _viewModel.Data.CCHILLER_TAX_ID = "";
                    _viewModel.Data.CCHILLER_TAX_NAME = "";

                }
                else
                {
                    _viewModel.Data.CCHILLER_CHARGES_ID = "";
                    _viewModel.Data.CCHILLER_CHARGES_NAME = "";
                    _viewModel.HoUtilBuildingMapping.LCHILLER_CHARGES_TAXABLE = false;
                    _viewModel.Data.CCHILLER_TAX_ID = "";
                    _viewModel.Data.CCHILLER_TAX_NAME = "";
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }
        private async Task BeforeOpen_ChillerChargesTaxAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00100ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                };
                eventArgs.TargetPageType = typeof(GSL00100);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task AfterOpen_ChillerChargesTaxAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (GSL00100DTO)eventArgs.Result;
                    _viewModel.Data.CCHILLER_TAX_ID = loTempResult.CTAX_ID ?? "";
                    _viewModel.Data.CCHILLER_TAX_NAME = loTempResult.CTAX_NAME ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task OnLostFocus_ChillerChargesTaxAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CCHILLER_TAX_ID))
                {

                    var loLookupViewModel = new LookupGSL00100ViewModel();
                    var loResult = await loLookupViewModel.GetSalesTax(new GSL00100ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.Data.CCHILLER_TAX_ID
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto Endblock;
                    }
                    _viewModel.Data.CCHILLER_TAX_ID = loResult.CTAX_ID ?? "";
                    _viewModel.Data.CCHILLER_TAX_NAME = loResult.CTAX_NAME ?? "";
                }
                else
                {
                    _viewModel.Data.CCHILLER_TAX_ID = "";
                    _viewModel.Data.CCHILLER_TAX_NAME = "";
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        //lookup gas
        private async Task BeforeOpen_GasChargesAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new LML00400ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = "",
                    CUSER_ID = _clientHelper.UserId,
                    CTAXABLE_TYPE = CTAXABLE_TYPE,
                    CACTIVE_TYPE = CACTIVE_TYPE,
                };
                eventArgs.TargetPageType = typeof(LML00400);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task AfterOpen_GasChargesAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (LML00400DTO)eventArgs.Result;
                    _viewModel.Data.CGAS_CHARGES_ID = loTempResult.CCHARGES_ID ?? "";
                    _viewModel.Data.CGAS_CHARGES_NAME = loTempResult.CCHARGES_NAME ?? "";
                    _viewModel.HoUtilBuildingMapping.LGAS_CHARGES_TAXABLE = loTempResult.LTAXABLE;
                    _viewModel.Data.CGAS_TAX_ID = "";
                    _viewModel.Data.CGAS_TAX_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        private async Task OnLostFocus_GasChargesAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CGAS_CHARGES_ID))
                {

                    var loLookupViewModel = new LookupLML00400ViewModel(); //use GSL's model
                    var loResult = await loLookupViewModel.GetUtitlityCharges(new LML00400ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID,
                        //CCHARGE_TYPE_ID = _viewModel.Data.CGAS_CHARGES_ID,
                        CUSER_ID = _clientHelper.UserId,
                        CTAXABLE_TYPE = CTAXABLE_TYPE,
                        CACTIVE_TYPE = CACTIVE_TYPE,
                        CSEARCH_TEXT = _viewModel.Data.CGAS_CHARGES_ID
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto Endblock;
                    }
                    _viewModel.Data.CGAS_CHARGES_ID = loResult.CCHARGES_ID ?? "";
                    _viewModel.Data.CGAS_CHARGES_NAME = loResult.CCHARGES_NAME ?? "";
                    _viewModel.HoUtilBuildingMapping.LGAS_CHARGES_TAXABLE = loResult.LTAXABLE;
                    _viewModel.Data.CGAS_TAX_ID = "";
                    _viewModel.Data.CGAS_TAX_NAME = "";
                }
                else
                {
                    _viewModel.Data.CGAS_CHARGES_ID = "";
                    _viewModel.Data.CGAS_CHARGES_NAME = "";
                    _viewModel.HoUtilBuildingMapping.LGAS_CHARGES_TAXABLE = false;

                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }
        private async Task BeforeOpen_GasChargesTaxAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00100ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                };
                eventArgs.TargetPageType = typeof(GSL00100);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task AfterOpen_GasChargesTaxAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (GSL00100DTO)eventArgs.Result;
                    _viewModel.Data.CGAS_TAX_ID = loTempResult.CTAX_ID ?? "";
                    _viewModel.Data.CGAS_TAX_NAME = loTempResult.CTAX_NAME ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task OnLostFocus_GasChargesTaxAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CGAS_TAX_ID))
                {

                    var loLookupViewModel = new LookupGSL00100ViewModel();
                    var loResult = await loLookupViewModel.GetSalesTax(new GSL00100ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.Data.CGAS_TAX_ID
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto Endblock;
                    }
                    _viewModel.Data.CGAS_TAX_ID = loResult.CTAX_ID ?? "";
                    _viewModel.Data.CGAS_TAX_NAME = loResult.CTAX_NAME ?? "";
                }
                else
                {
                    _viewModel.Data.CGAS_TAX_ID = "";
                    _viewModel.Data.CGAS_TAX_NAME = "";
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        //lookup water
        private async Task BeforeOpen_WaterChargesAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new LML00400ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID,
                    CCHARGE_TYPE_ID = "",
                    CUSER_ID = _clientHelper.UserId,
                    CTAXABLE_TYPE = CTAXABLE_TYPE,
                    CACTIVE_TYPE = CACTIVE_TYPE,
                };
                eventArgs.TargetPageType = typeof(LML00400);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task AfterOpen_WaterChargesAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (LML00400DTO)eventArgs.Result;
                    _viewModel.Data.CWATER_CHARGES_ID = loTempResult.CCHARGES_ID ?? "";
                    _viewModel.Data.CWATER_CHARGES_NAME = loTempResult.CCHARGES_NAME ?? "";
                    _viewModel.HoUtilBuildingMapping.LWATER_CHARGES_TAXABLE = loTempResult.LTAXABLE;
                    _viewModel.Data.CWATER_TAX_ID = "";
                    _viewModel.Data.CWATER_TAX_NAME = "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task OnLostFocus_WaterChargesAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CWATER_CHARGES_ID))
                {

                    var loLookupViewModel = new LookupLML00400ViewModel(); //use GSL's model
                    var loResult = await loLookupViewModel.GetUtitlityCharges(new LML00400ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CPROPERTY_ID = _viewModel.SystemParamDetails.CPROPERTY_ID,
                        //CCHARGE_TYPE_ID = _viewModel.Data.CWATER_CHARGES_ID,
                        CUSER_ID = _clientHelper.UserId,
                        CTAXABLE_TYPE = CTAXABLE_TYPE,
                        CACTIVE_TYPE = CACTIVE_TYPE,
                        CSEARCH_TEXT = _viewModel.Data.CWATER_CHARGES_ID
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto Endblock;
                    }
                    _viewModel.Data.CWATER_CHARGES_ID = loResult.CCHARGES_ID ?? "";
                    _viewModel.Data.CWATER_CHARGES_NAME = loResult.CCHARGES_NAME ?? "";
                    _viewModel.HoUtilBuildingMapping.LWATER_CHARGES_TAXABLE = loResult.LTAXABLE;
                    _viewModel.Data.CWATER_TAX_ID = "";
                    _viewModel.Data.CWATER_TAX_NAME = "";
                }
                else
                {
                    _viewModel.Data.CWATER_CHARGES_ID = "";
                    _viewModel.Data.CWATER_CHARGES_NAME = "";
                    _viewModel.HoUtilBuildingMapping.LWATER_CHARGES_TAXABLE = false;
                    _viewModel.Data.CWATER_TAX_ID = "";
                    _viewModel.Data.CWATER_TAX_NAME = "";
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }
        private async Task BeforeOpen_WaterChargesTaxAsync(R_BeforeOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                eventArgs.Parameter = new GSL00100ParameterDTO()
                {
                    CCOMPANY_ID = _clientHelper.CompanyId,
                    CUSER_ID = _clientHelper.UserId,
                };
                eventArgs.TargetPageType = typeof(GSL00100);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task AfterOpen_WaterChargesTaxAsync(R_AfterOpenLookupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Result != null)
                {
                    var loTempResult = (GSL00100DTO)eventArgs.Result;
                    _viewModel.Data.CWATER_TAX_ID = loTempResult.CTAX_ID ?? "";
                    _viewModel.Data.CWATER_TAX_NAME = loTempResult.CTAX_NAME ?? "";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;

        }
        private async Task OnLostFocus_WaterChargesTaxAsync()
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrWhiteSpace(_viewModel.Data.CWATER_TAX_ID))
                {

                    var loLookupViewModel = new LookupGSL00100ViewModel();
                    var loResult = await loLookupViewModel.GetSalesTax(new GSL00100ParameterDTO()
                    {
                        CCOMPANY_ID = _clientHelper.CompanyId,
                        CUSER_ID = _clientHelper.UserId,
                        CSEARCH_TEXT = _viewModel.Data.CWATER_TAX_ID
                    });

                    if (loResult == null)
                    {
                        loEx.Add(R_FrontUtility.R_GetError(
                                typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                                "_ErrLookup01"));
                        goto Endblock;
                    }
                    _viewModel.Data.CWATER_TAX_ID = loResult.CTAX_ID ?? "";
                    _viewModel.Data.CWATER_TAX_NAME = loResult.CTAX_NAME ?? "";
                }
                else
                {
                    _viewModel.Data.CWATER_TAX_ID = "";
                    _viewModel.Data.CWATER_TAX_NAME = "";
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        Endblock:
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }
    }
}
