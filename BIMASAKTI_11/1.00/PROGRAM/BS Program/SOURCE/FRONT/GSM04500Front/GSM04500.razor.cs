using GSM04500Common;
using GSM04500Model;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using Microsoft.JSInterop;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM04500Front
{
    public partial class GSM04500 : R_Page
    {
        private GSM04500ViewModel journalGroupViewModel = new();
        private R_ConductorGrid? _conJournalGroupRef;
        private R_Grid<GSM04500DTO> _gridRef;
        private R_Conductor _conductorRef;

        private R_TabStrip _tabStrip;
        private R_TabPage _tabPageAccountSetting;
        [Inject] IJSRuntime JS { get; set; }
        [Inject] IClientHelper clientHelper { get; set; }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await journalGroupViewModel.GetJournalGroupTypeList();
                if (journalGroupViewModel._lGroupTypeExist)
                {
                    await _gridRef.R_RefreshGrid(null);
                  
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task JournalGroupDropdown_OnChange(object poParam)
        {
            var loEx = new R_Exception();
            string lsJournalGrpType = (string)poParam;
            try
            {
                journalGroupViewModel.JournalGroupTypeValue = lsJournalGrpType;
                journalGroupViewModel.JournalGroupCurrent.CJRNGRP_TYPE = lsJournalGrpType;

                await _gridRef.R_RefreshGrid(poParam);
                journalGroupViewModel.DropdownGroupType = true;

                if (_tabStrip.ActiveTab.Id == "Tab_AccountSetting")
                {
                    journalGroupViewModel.DropdownGroupType = false;
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #region Journal Group
        private async Task R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await journalGroupViewModel.GetAllJournalAsync();
                eventArgs.ListEntityResult = journalGroupViewModel.JournalGroupList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM04500DTO)eventArgs.Data;
                eventArgs.Result = await journalGroupViewModel.GetGroupJournalOneRecord(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04500DTO)eventArgs.Data;
                await journalGroupViewModel.DeleteOneRecordJournalGroup(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            // R_DisplayException(loEx);
            loEx.ThrowExceptionIfErrors();
        }
        public async Task AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        private void AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loException = new R_Exception();
            var loData = (GSM04500DTO)eventArgs.Data;

            try
            {
                loData.CJRNGRP_TYPE = journalGroupViewModel.JournalGroupTypeValue;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            R_DisplayException(loException);
        }
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM04500DTO)eventArgs.Data;
                await journalGroupViewModel.SaveJournalGroup(loParam, eventArgs.ConductorMode);

                eventArgs.Result = journalGroupViewModel.JournalGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM04500DTO)eventArgs.Data;
                journalGroupViewModel.JournalGroupCurrent = loParam;
            }
        }

        public void ServiceValidation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                journalGroupViewModel.ValidationFieldEmpty((GSM04500DTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region CHANGE TAB
        //CHANGE TAB
        private void Before_Open_AccountSetting(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(GSM04500AccountSetting);
            if (journalGroupViewModel.JournalGroupList.Count > 0)
            {
                eventArgs.Parameter = journalGroupViewModel.JournalGroupCurrent;
            }
            else
            {
                eventArgs.Parameter = null;
            }
        }
        private void onTabChange(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            journalGroupViewModel.DropdownGroupType = true;
            if (eventArgs.TabStripTab.Id == "Tab_AccountSetting")
            {
                journalGroupViewModel.DropdownGroupType = false;
            }
        }
        #endregion

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
                    var loByteFile = await journalGroupViewModel.DownloadTemplate();

                    var saveFileName = $"Journal Group - {loCompanyName}.xlsx";

                    await JS.downloadFileFromStreamHandler(saveFileName, loByteFile.FileBytes);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        #endregion

        #region Upload

        private void Before_Open_Upload(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var param = new GSM004500ParamDTO()
            {
                CCOMPANY_ID = journalGroupViewModel.JournalGroupCurrent.CCOMPANY_ID,
                CUSER_ID = clientHelper.UserId,
                CJRNGRP_TYPE = journalGroupViewModel.JournalGroupCurrent.CJRNGRP_TYPE
            };

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(GSM04500Upload);
        }

        private async Task After_Open_Upload(R_AfterOpenPopupEventArgs eventArgs)
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
                    await _gridRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        #endregion


        #region UserLocking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";

        protected override async Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult = null;
            try
            {
                var loData = (GSM04500DTO)eventArgs.Data;
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
                        Program_Id = "GSM04500",
                        Table_Name = "GSM_JRNGRP",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CJRNGRP_TYPE, loData.CJRNGRP_CODE)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "GSM04500",
                        Table_Name = "GSM_JRNGRP",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CJRNGRP_TYPE, loData.CJRNGRP_CODE)
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
