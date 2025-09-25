using BlazorClientHelper;
using BMM00500COMMON.DTO;
using BMM00500MODEL.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMM00500FRONT
{
    public partial class BMM00500 : R_Page
    {
        [Inject] private R_ILocalizer<BMM00500FrontResources.Resources_Dummy_Class> _localizer { get; set; } = null!;
        [Inject] private IClientHelper _clientHelper { get; set; } = null!;

        private BMM00500ViewModel _viewModel = new BMM00500ViewModel();
        private R_ConductorGrid _conductorGridRef = null!;
        private R_Grid<BMM00500DTO> _gridRef = null!;

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region LOCKING
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlBM";
        private const string DEFAULT_MODULE_NAME = "BM";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (BMM00500DTO)eventArgs.Data;

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
                        Program_Id = "BMM00500",
                        Table_Name = "BMM_MOBILE_PROGRAM",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROGRAM_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = "BMM00500",
                        Table_Name = "BMM_MOBILE_PROGRAM",
                        Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROGRAM_ID)
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

        #region GRID METHODS
        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.GetList();
                if (_viewModel.loMobileProgramList.Count() == 0)
                {
                    _viewModel.loSelectedMobileProgram = new BMM00500DTO();
                    await R_MessageBox.Show("", "No data found!", R_eMessageBoxButtonType.OK);
                }
                eventArgs.ListEntityResult = _viewModel.loMobileProgramList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.ServiceGetRecord((BMM00500DTO)eventArgs.Data);
                eventArgs.Result = _viewModel.loSelectedMobileProgram;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.ServiceSave((BMM00500DTO)eventArgs.Data, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.loSelectedMobileProgram;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task R_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                await _viewModel.ServiceDelete((BMM00500DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #endregion
    }
}
