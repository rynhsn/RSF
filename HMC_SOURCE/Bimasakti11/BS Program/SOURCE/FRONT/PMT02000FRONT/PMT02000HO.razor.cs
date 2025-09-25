using PMT02000COMMON.LOI_List;
using PMT02000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Helpers;
using PMT02000COMMON.Utility;
using R_BlazorFrontEnd.Controls.MessageBox;
using System.Diagnostics.Tracing;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using System.Xml.Linq;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace PMT02000FRONT
{
    public partial class PMT02000HO : R_Page, R_ITabPage
    {
        private PMT02000ViewModel _viewModel = new();
        private R_Grid<PMT02000LOIDTO>? _gridHOref;
        private R_ConductorGrid? _conGridLOI;
        private int _pageSize = 15;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                _viewModel._PropertyWithName = (PMT02000PropertyDTO)poParameter;
                if (_viewModel._PropertyWithName.CPROPERTY_ID != null)
                {
                    await _gridHOref!.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region HOList
        private async Task R_ServiceHOListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetAllList("HO");
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
                    var loTemp = (PMT02000LOIDTO)eventArgs.Data;

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

        #endregion CHANGE TAB
        //ini buat Dapet fungsi dari Page 1
        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loException = new R_Exception();

            try
            {
                _viewModel._PropertyWithName = R_FrontUtility.ConvertObjectToObject<PMT02000PropertyDTO>(poParam);

                await _gridHOref!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }


            R_DisplayException(loException);

        }

        #region Button
        private void Btn_Edit(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.TargetPageType = typeof(PopUpHandOver);
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader>(_viewModel._CurrentLOI);
                loParam.CSAVEMODE = "EDIT";
                loParam.CPROPERTY_NAME = _viewModel._PropertyWithName.CPROPERTY_NAME;
                eventArgs.Parameter = loParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task After_Edit(R_AfterOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _gridHOref!.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Btn_DeleteAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["Delete_Confirmation"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    var loParamDelete = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader_DetailDTO>(_viewModel._CurrentLOI);
                    await _viewModel.ServiceDelete(loParamDelete);
                    await R_MessageBox.Show("", _localizer["Success_Delete"], R_eMessageBoxButtonType.OK);
                    await _gridHOref!.R_RefreshGrid(null);
                }
                else
                {
                    return;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Btn_SubmitAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["Submit_Confirmation"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SubmitRedraft("Submit");
                    await R_MessageBox.Show("", _localizer["Success_Submit"], R_eMessageBoxButtonType.OK);
                    await _gridHOref!.R_RefreshGrid(null);
                }
                else
                {
                    return;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task Btn_RedraftAsync()
        {
            var loEx = new R_Exception();
            try
            {
                R_eMessageBoxResult loValidate = await R_MessageBox.Show("", _localizer["Redraft_Confirmation"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await _viewModel.SubmitRedraft("Redraft");
                    await R_MessageBox.Show("", _localizer["Success_Redraft"], R_eMessageBoxButtonType.OK);
                    await _gridHOref!.R_RefreshGrid(null);
                }
                else
                {
                    return;

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Btn_View(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                eventArgs.TargetPageType = typeof(PopUpHandOver);
                var loParam = R_FrontUtility.ConvertObjectToObject<PMT02000LOIHeader>(_viewModel._CurrentLOI);
                loParam.CALLER_ACTION = "VIEW";
                loParam.CPROPERTY_NAME = _viewModel._PropertyWithName.CPROPERTY_NAME;
                eventArgs.Parameter = loParam;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
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
