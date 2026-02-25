using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using FAT01100Common.DTOs;
using FAT01100FrontResources;
using FAT01100Model.VMs;
using System;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Interfaces;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_FACommon.DTOs;
using Lookup_FAFront;
using Lookup_GSModel.ViewModel;
using Lookup_FAModel.ViewModel.FAL00200;

namespace FAT01100Front
{
    public partial class FAT01100 : R_Page
    {
        #region Dependency Injection
        [Inject] private IClientHelper ClientHelper { get; set; } = default!;
        [Inject] private R_ILocalizer<FAT01100FrontResources.Resources_Dummy_Class> Localizer { get; set; } = default!;
        [Inject] private R_MessageBoxService MessageBoxService { get; set; } = default!;
        #endregion

        #region Component References
        private R_TabStrip? _tabStripRef;
        private R_Grid<FAT01100GeTransListResultDTO>? _gridTransListRef;
        private R_TabStripTab _tabTransList = new();
        private R_TabStripTab _tabEntry = new();
        #endregion

        #region ViewModels
        private readonly FAT01100ViewModel _VM = new();
        #endregion

        #region Lifecycle Methods
        protected override async Task R_Init_From_Master(object? poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _VM.ParameterDTO.CCOMPANY_ID = ClientHelper.CompanyId;
                _VM.ParameterDTO.CUSER_ID = ClientHelper.UserId;
                _VM.ParameterDTO.CLANGUAGE_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty;
                await _VM.GetGetSystemParamAsync(ClientHelper.CompanyId, ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty);
                if (_VM.SystemParamData == null)
                {
                    string lcMessage = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "val_systemParam");

                    await R_MessageBox.Show(
                        "",
                        lcMessage,
                        R_eMessageBoxButtonType.OK
                    );
                    await this.CloseProgramAsync();
                    goto EndTry;
                }
                await _VM.GetYearRangeAsync(ClientHelper.CompanyId);
                await _VM.GetDeptLookupListAsync(ClientHelper.CompanyId, ClientHelper.UserId, "");
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndTry:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Event Handlers
        private async Task TabStrip_OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task OnRefreshClick()
        {
            var loEx = new R_Exception();
            try
            {
                await (_gridTransListRef?.R_RefreshGrid(null) ?? Task.CompletedTask);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Grid Event Handlers
        private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_VM.ParameterDTO.CDEPT_CODE))
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Lookup_GSFrontResources.Resources_Dummy_Class), "PS001"));
                    goto EndTry;
                }
                _VM.ParameterDTO.CCOMPANY_ID = ClientHelper.CompanyId;
                _VM.ParameterDTO.CUSER_ID = ClientHelper.UserId;
                _VM.ParameterDTO.CLANGUAGE_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty;
                await _VM.FAT01100GeTransListAsync();
                eventArgs.ListEntityResult = _VM.TransList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndTry:
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Conductor_R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loSelectedRow = R_FrontUtility.ConvertObjectToObject<FAT01100GeTransListResultDTO>(eventArgs.Data);
                eventArgs.Result = loSelectedRow ?? new FAT01100GeTransListResultDTO();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Department Lookup Handlers
        private async Task txtDeptCode_OnLostFocused()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_VM.ParameterDTO.CDEPT_CODE))
                {
                    _VM.ParameterDTO.CDEPT_CODE = "";
                    _VM.DeptName = "";
                    return;
                }

                LookupGSL00700ViewModel loLookupViewModel = new();
                var param = new GSL00700ParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CUSER_ID = ClientHelper.UserId,
                    CSEARCH_TEXT = _VM.ParameterDTO.CDEPT_CODE
                };
                var loResult = await loLookupViewModel.GetDepartment(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Lookup_GSFrontResources.Resources_Dummy_Class), "_ErrLookup01"));
                    _VM.ParameterDTO.CDEPT_CODE = "";
                    _VM.DeptName = "";
                }
                else
                {
                    _VM.ParameterDTO.CDEPT_CODE = loResult.CDEPT_CODE;
                    _VM.DeptName = loResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void btnDeptLook_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            eventArgs.Parameter = new GSL00700ParameterDTO();
            eventArgs.TargetPageType = typeof(GSL00700);
        }

        private async Task btnDeptLook_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loTempResult = (GSL00700DTO)eventArgs.Result;
                if (loTempResult != null)
                {
                    _VM.ParameterDTO.CDEPT_CODE = loTempResult.CDEPT_CODE;
                    _VM.DeptName = loTempResult.CDEPT_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Asset Lookup Handlers
        private async Task txtAssetCode_OnLostFocused()
        {
            var loEx = new R_Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(_VM.ParameterDTO.CASSET_CODE))
                {
                    _VM.ParameterDTO.CASSET_CODE = "";
                    _VM.AssetName = "";
                    return;
                }

                LookupFAL00300ViewModel loLookupViewModel = new();
                string cassetCode = _VM.ParameterDTO.CASSET_CODE ?? "";
                var param = new FAL00300ParameterDTO
                {
                    CCOMPANY_ID = ClientHelper.CompanyId,
                    CTRANS_CODE = string.Empty,
                    CASSET_CODE = cassetCode,
                    CLANGUAGE_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty
                };
                var loResult = await loLookupViewModel.GetTaxCategory(param);

                if (loResult == null)
                {
                    loEx.Add(R_FrontUtility.R_GetError(typeof(Lookup_GSFrontResources.Resources_Dummy_Class), "_ErrLookup01"));
                    _VM.ParameterDTO.CASSET_CODE = "";
                    _VM.AssetName = "";
                }
                else
                {
                    _VM.ParameterDTO.CASSET_CODE = loResult.CASSET_CODE;
                    _VM.AssetName = loResult.CASSET_NAME;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private void btnAssetLook_R_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            string cassetCode = _VM.ParameterDTO.CASSET_CODE ?? "";
            eventArgs.Parameter = new FAL00300ParameterDTO
            {
                CCOMPANY_ID = ClientHelper.CompanyId,
                CTRANS_CODE = string.Empty,
                CASSET_CODE = cassetCode,
                CLANGUAGE_ID = ClientHelper.CultureUI?.TwoLetterISOLanguageName ?? string.Empty
            };
            eventArgs.TargetPageType = typeof(FAL00300);
        }

        private async Task btnAssetLook_R_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData = (FAL00300DTO)eventArgs.Result;
                if (loData == null)
                    return;

                _VM.ParameterDTO.CASSET_CODE = loData.CASSET_CODE;
                _VM.AssetName = loData.CASSET_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region Entry Tab
        private void BeforeOpenEntry(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            
            var loEx = new R_Exception();
            try
            {
                var loSelectedData = _gridTransListRef?.GetCurrentData();
                var lopar = R_FrontUtility.ConvertObjectToObject<FAT01100DTO>(loSelectedData);
                if (loSelectedData != null)
                {
                    eventArgs.Parameter = lopar;
                }
                else
                {
                    eventArgs.Parameter = new FAT01100DTO();
                }
                eventArgs.TargetPageType = typeof(FAT01100Entry);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void AfterOpenEntry(R_AfterOpenTabPageEventArgs eventArgs)
        {
        }

        private void EntryTabEventCallBack(object? poParam)
        {
        }
        #endregion
    }
}
