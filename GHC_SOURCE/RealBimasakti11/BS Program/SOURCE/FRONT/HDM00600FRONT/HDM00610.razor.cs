using BlazorClientHelper;
using HDM00600COMMON.DTO;
using HDM00600MODEL.View_Model_s;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Interfaces;
using HDM00600FrontResources;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Helpers;
using HDM00600COMMON.DTO_s.Helper;
using R_BlazorFrontEnd.Controls.Enums;
using R_CommonFrontBackAPI;
using R_LockingFront;
using HDM00600COMMON;
using Lookup_HDModel.ViewModel.HDL00100;
using Lookup_HDCOMMON.DTOs.HDL00100;
using R_BlazorFrontEnd.Controls.Tab;
using Lookup_HDFRONT;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Controls.MessageBox;

namespace HDM00600FRONT
{
    public partial class HDM00610 : R_Page, R_ITabPage
    {
        private HDM00610ViewModel _viewModel = new();
        private R_Grid<PricelistDTO> _gridPricelist;
        private R_ConductorGrid _conPricelist;
        private int _pageSize_Pricelist = 20;
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_ILocalizer<Lookup_HDFrontResources.Resources_Dummy_Class_LookupHD> _localizerHD { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        //methods - override
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new();
            try
            {
                if (poParameter != null)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PropertyDTO>(poParameter);
                    _viewModel._propertyId = loParam.CPROPERTY_ID as string ?? "";
                    _viewModel._propertyName = loParam.CPROPERTY_NAME as string ?? "";
                    _viewModel._propertyCurr = loParam.CCURRENCY as string ?? "";
                    await _gridPricelist.R_RefreshGrid(null);
                }
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
                var loData = (PricelistDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: PricelistMaster_ContextConstant.DEFAULT_MODULE,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: PricelistMaster_ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PricelistMaster_ContextConstant.PROGRAM_ID,
                        Table_Name = PricelistMaster_ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CCOMPANY_ID, loData.CPROPERTY_ID, loData.CPRICELIST_ID, loData.CVALID_ID)
                    };


                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PricelistMaster_ContextConstant.PROGRAM_ID,
                        Table_Name = PricelistMaster_ContextConstant.TABLE_NAME,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CCOMPANY_ID, loData.CPROPERTY_ID, loData.CPRICELIST_ID, loData.CVALID_ID)
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

        //methods - event
        private async Task Pricelist_GetlistAsync(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel.GetList_Pricelist(HDM00610ViewModel.eListPricingParamType.GetNext);
                eventArgs.ListEntityResult = _viewModel._pricelist_List;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Pricelist_GetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PricelistDTO>(eventArgs.Data);
                await _viewModel.GetRecord_PricelistAsync(loData);
                eventArgs.Result = _viewModel._nextPricelistRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PricelistDTO>(eventArgs.Data);

                if (string.IsNullOrWhiteSpace(loData.CPRICELIST_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add1"));
                }
                if (string.IsNullOrWhiteSpace(loData.CPRICELIST_NAME))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add2"));
                }
                if (string.IsNullOrWhiteSpace(loData.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add8"));
                }
                if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add3"));
                }
                if (string.IsNullOrWhiteSpace(loData.CCURRENCY_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add4"));
                }
                if (string.IsNullOrWhiteSpace(loData.CUNIT))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add5"));
                }
                if (loData.IPRICE < 0)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add6"));
                }
                if (loData.DSTART_DATE == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class), "_val_add7"));
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_BeforeOpenGridLookup(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                switch (eventArgs.ColumnName)
                {
                    case nameof(PricelistDTO.CPRICELIST_ID):
                        eventArgs.Parameter = new HDL00100ParameterDTO()
                        {
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CPROPERTY_ID = _viewModel._propertyId,
                            CUSER_ID = _viewModel._propertyName,
                            CSTATUS = "0",
                            CTAXABLE = "0",
                        };
                        eventArgs.TargetPageType = typeof(HDL00100);
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_Afteradd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if (eventArgs.Data is PricelistDTO loData)
                {
                    loData.CCURRENCY_CODE = _viewModel._propertyCurr;
                    loData.DSTART_DATE = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_AfterOpenGridLookup(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = eventArgs.ColumnData as PricelistDTO;
                if (eventArgs.Result == null)
                {
                    return;
                }
                switch (eventArgs.ColumnName)
                {
                    case nameof(PricelistDTO.CPRICELIST_ID):
                        var loResult = R_FrontUtility.ConvertObjectToObject<PricelistDTO>(eventArgs.Result);
                        loData.CPRICELIST_ID = loResult.CPRICELIST_ID;
                        loData.CPRICELIST_NAME = loResult.CPRICELIST_NAME;
                        loData.CDEPT_CODE = loResult.CDEPT_CODE;
                        loData.CCHARGES_ID = loResult.CCHARGES_ID;
                        loData.CINVGRP_CODE = loResult.CINVGRP_CODE;
                        loData.LTAXABLE = loResult.LTAXABLE;
                        loData.CUNIT = loResult.CUNIT;
                        loData.CDESCRIPTION = loResult.CDESCRIPTION;
                        loData.CACTIVE_START_DATE = loResult.CACTIVE_START_DATE;
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private async Task Pricelist_CellLostFocused(R_CellLostFocusedEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = eventArgs.CurrentRow as PricelistDTO;
                switch (eventArgs.ColumnName)
                {
                    case nameof(PricelistDTO.CPRICELIST_ID):
                        if (!string.IsNullOrWhiteSpace(eventArgs.Value.ToString()))
                        {
                            eventArgs.Value.ToString();
                            LookupHDL00100ViewModel loHDVM = new();
                            var loResult = await loHDVM.GetPriceListRecord(new HDL00100ParameterDTO()
                            {
                                CCOMPANY_ID = _clientHelper.CompanyId,
                                CPROPERTY_ID = _viewModel._propertyId,
                                CUSER_ID = _viewModel._propertyName,
                                CSTATUS = "0",
                                CTAXABLE = "0",
                                CSEARCH_TEXT_ID = eventArgs.Value.ToString(),
                            });

                            if (loResult == null)
                            {
                                R_eMessageBoxResult loMessageResult = await R_MessageBox.Show("", _localizerHD["_ErrLookup01"], R_eMessageBoxButtonType.OK);
                                loData.CPRICELIST_ID = "";
                                loData.CPRICELIST_NAME = "";
                                loData.CDEPT_CODE = "";
                                loData.CCHARGES_ID = "";
                                loData.CINVGRP_CODE = "";
                                loData.LTAXABLE = false;
                                loData.CUNIT = "";
                                loData.CDESCRIPTION = "";
                                loData.CACTIVE_START_DATE = "";
                            }
                            else
                            {
                                loData.CPRICELIST_ID = loResult.CPRICELIST_ID;
                                loData.CPRICELIST_NAME = loResult.CPRICELIST_NAME;
                                loData.CDEPT_CODE = loResult.CDEPT_CODE;
                                loData.CCHARGES_ID = loResult.CCHARGES_ID;
                                loData.CINVGRP_CODE = loResult.CINVGRP_CODE;
                                loData.LTAXABLE = loResult.LTAXABLE;
                                loData.CUNIT = loResult.CUNIT;
                                loData.CDESCRIPTION = loResult.CDESCRIPTION;
                                loData.CACTIVE_START_DATE = loResult.CACTIVE_START_DATE;
                            }
                        }
                        else
                        {
                            loData.CPRICELIST_ID = "";
                            loData.CPRICELIST_NAME = "";
                            loData.CDEPT_CODE = "";
                            loData.CCHARGES_ID = "";
                            loData.CINVGRP_CODE = "";
                            loData.LTAXABLE = false;
                            loData.CUNIT = "";
                            loData.CDESCRIPTION = "";
                            loData.CACTIVE_START_DATE = "";
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }
        private void Pricelist_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (eventArgs.Data is PricelistDTO loData)
                {
                    loData.CSTART_DATE = loData.DSTART_DATE.Value.ToString("yyyyMMdd");
                    loData.CPROPERTY_ID = _viewModel._propertyId;
                    if (_conPricelist.R_ConductorMode==R_eConductorMode.Add)
                    {
                        loData.CVALID_ID = "";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Pricelist_SaveAsync(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PricelistDTO>(eventArgs.Data);
                await _viewModel.SaveRecord_PricelistAsync(loData, (eCRUDMode)_conPricelist.R_ConductorMode);
                eventArgs.Result = _viewModel._nextPricelistRecord;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Pricelist_DeleteAsync(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await _viewModel.DeleteRecord_PricelistAsync(R_FrontUtility.ConvertObjectToObject<PricelistDTO>(eventArgs.Data));
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private async Task Pricelist_SetOtherAsync(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                await InvokeTabEventCallbackAsync(eventArgs.Enable);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void Pricelist_Display(R_DisplayEventArgs eventArgs)
        {
        }

        //methods - tab
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loEx = new();
            try
            {
                if (poParam != null)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<PropertyDTO>(poParam);
                    _viewModel._propertyId = loParam.CPROPERTY_ID as string ?? "";
                    _viewModel._propertyName = loParam.CPROPERTY_NAME as string ?? "";
                    _viewModel._propertyCurr = loParam.CCURRENCY as string ?? "";
                    await _gridPricelist.R_RefreshGrid(null);
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
