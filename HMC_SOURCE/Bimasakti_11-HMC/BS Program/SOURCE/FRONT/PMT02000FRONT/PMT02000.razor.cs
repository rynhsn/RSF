using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMT02000COMMON.LOI_List;
using PMT02000COMMON.Upload;
using PMT02000COMMON.Utility;
using PMT02000MODEL;
using PMT02000MODEL.ViewModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02000FRONT
{
    public partial class PMT02000 : R_Page
    {
        private PMT02000ViewModel _viewModel = new();
        private PMT02000ViewModel_Upload _viewModelTemplate = new();
        private R_Grid<PMT02000LOIDTO>? _gridLOIref;
        private R_ConductorGrid? _conGridLOI;

        private R_TabStrip? _tabLOI;
        private R_TabPage? _tabHO;
        private int _pageSize = 15;
        [Inject] IClientHelper clientHelper { get; set; }
        [Inject] public R_IExcel? ExcelInject { get; set; }
        [Inject] IJSRuntime? JSRuntime { get; set; }
        private void StateChangeInvoke()
        {
            StateHasChanged();
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await PropertyListRecord(null);
                _viewModelTemplate.StateChangeAction = StateChangeInvoke;
                _viewModelTemplate.ActionDataSetExcel = ActionFuncDataSetExcel;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region PropertyID
        private async Task PropertyListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetPropertyList();
                if (_viewModel.lPropertyExist)
                {
                    await _gridLOIref!.R_RefreshGrid(null);

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task PropertyDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsProperty = (string)poParam;
            try
            {
                if (_viewModel.lPropertyExist)
                {
                    var loPropertyTemp = _viewModel.PropertyList
                                  .Where(item => item.CPROPERTY_ID == lsProperty)
                                  .FirstOrDefault()!;   

                    _viewModel._PropertyWithName = loPropertyTemp;

                    if (_tabLOI!.ActiveTab.Id == "TabLOI")
                    {
                        await _gridLOIref!.R_RefreshGrid(null);
                    }
                    if (_tabLOI!.ActiveTab.Id == "Tab_HandOver")
                    {
                        await _tabHO!.InvokeRefreshTabPageAsync(_viewModel._PropertyWithName);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            await R_DisplayExceptionAsync(loEx);
        }
        private async Task OnActiveTabIndexChanged(R_TabStripTab eventArgs)
        {
            switch (eventArgs.Id)
            {
                case "TabLOI":
                    if (_viewModel.lPropertyExist)
                    {
                        await _gridLOIref!.R_RefreshGrid(null);
                    }
                    break;
            }

        }
        #endregion
        #region LOIList
        private async Task R_ServiceLOIListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetAllList("LOI");
                eventArgs.ListEntityResult = _viewModel.LOIList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                PMT02000LOIDTO loData = (PMT02000LOIDTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    _viewModel._CurrentLOI = loData;
                }

                switch (loData.CTRANS_STATUS)
                {
                    case "00":

                        _viewModel.lControlButtonRedraft = false;
                        _viewModel.lControlButtonSubmit = true;
                        _viewModel.lControlBtnEditDelete = true;
                        break;
                    case "10":
                        _viewModel.lControlButtonSubmit = false;
                        _viewModel.lControlBtnEditDelete = false;
                        _viewModel.lControlButtonRedraft = true;
                        break;
                    default:
                        _viewModel.lControlBtnEditDelete =
                        _viewModel.lControlButtonRedraft =
                        _viewModel.lControlButtonSubmit = false;
                        break;
                }

            }

            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion

        private void Btn_HandOver(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader>(_viewModel._CurrentLOI);
                loParam.CSAVEMODE = "NEW";
                loParam.CPROPERTY_NAME = _viewModel._PropertyWithName.CPROPERTY_NAME;

                eventArgs.TargetPageType = typeof(PopUpHandOver);
                eventArgs.Parameter = loParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task After_HandOver(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridLOIref!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Before_Open_HandOver(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                PMT02000PropertyDTO poPropertyParam = _viewModel._PropertyWithName;
                eventArgs.Parameter = poPropertyParam;
                eventArgs.TargetPageType = typeof(PMT02000HO);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        #region Template
        private async Task TemplateBtn_OnClick()
        {
            var loEx = new R_Exception();
            string loCompanyName = clientHelper.CompanyId.ToUpper();
            try
            {
                var loValidate = await R_MessageBox.Show("", "Are you sure download this template?", R_eMessageBoxButtonType.YesNo);

                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    PMT02000DBParameter loParam = new PMT02000DBParameter
                    {
                        CPROPERTY_ID = _viewModel._PropertyWithName.CPROPERTY_ID!,
                    };
                    await _viewModelTemplate.DownloadTemplate(loParam);
                    await ActionFuncDataSetExcel();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task ActionFuncDataSetExcel()
        {
            byte[] loByte = ExcelInject.R_WriteToExcel(_viewModelTemplate.ExcelDataSetTemplate);
            var lcName = $"HandOver" + ".xlsx";

            await JSRuntime.downloadFileFromStreamHandler(lcName, loByte);
        }
        #endregion
        #region Upload

        private void Before_Open_Upload(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loTemp = _viewModel._PropertyWithName;
            PMT02000ParameterUploadDTO loParam =  R_FrontUtility.ConvertObjectToObject<PMT02000ParameterUploadDTO>(loTemp);
            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(PMT02000Upload);
        }

        private async Task After_Open_UploadAsync(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {

                if (eventArgs.Success == false)
                {
                    return;
                }
                if ((bool)eventArgs.Result == true)
                {
                    await _gridLOIref!.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion
        #region Locking
        [Inject] IClientHelper? _clientHelper { get; set; }
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (PMT02000LOIDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT02000",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "PMT02000",
                        Table_Name = "PMT_AGREEMENT",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CDEPT_CODE, loData.CTRANS_CODE, loData.CREF_NO)
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


    }
}
