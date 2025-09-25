using PMM03700COMMON;
using PMM03700COMMON.DTO_s;
using PMM03700MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Interfaces;
using R_LockingFront;
using BlazorClientHelper;
using R_BlazorFrontEnd.Controls.Enums;

namespace PMM03700FRONT
{
    public partial class PMM03700 : R_Page
    {
        private PMM03700ViewModel _viewTenantClassGrpModel = new();
        private R_ConductorGrid _conTenantClassGroupRef; //ref conductor TenantClassGrp
        private R_Grid<TenantClassificationGroupDTO> _gridTenantClassGroupRef; //ref grid TenantClassGrp
        private R_TabPage _tabTenantClass; //ref TabPage tab2
        private R_TabStrip _tabStrip; //ref Tabstrip
        private bool _pageTenantClassOnCRUDmode = false; //to disable moving tab while tenant class crudmode
        private bool _pageTenantClassGrpOnCRUDmode = false; //to disable moving tab while tenant class grp crudmode
        private bool _comboboxPropertyEnabled = true; //to disable combobox while crudmode
        [Inject] private R_ILocalizer<PMM03700FRONTResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewTenantClassGrpModel.GetPropertyList();
                await Task.Delay(300);
                if (_viewTenantClassGrpModel._PropertyList.Count >= 1)
                {
                    await _gridTenantClassGroupRef.R_RefreshGrid(null);
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
                var loData = R_FrontUtility.ConvertObjectToObject<TenantClassificationDTO>(eventArgs.Data);

                var loCls = new R_LockingServiceClient(pcModuleName: PMM03700ContextConstant.DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: PMM03700ContextConstant.DEFAULT_HTTP_NAME);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PMM03700ContextConstant.PROGRAM_ID,
                        Table_Name = PMM03700ContextConstant.TABLE_NAME_1,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewTenantClassGrpModel._propertyId, loData.CTENANT_CLASSIFICATION_GROUP_ID)
                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = _clientHelper.CompanyId,
                        User_Id = _clientHelper.UserId,
                        Program_Id = PMM03700ContextConstant.PROGRAM_ID,
                        Table_Name = PMM03700ContextConstant.TABLE_NAME_1,
                        Key_Value = string.Join("|", _clientHelper.CompanyId, _viewTenantClassGrpModel._propertyId, loData.CTENANT_CLASSIFICATION_GROUP_ID)
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

        #region PropertyDropdown
        public async Task ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new();
            try
            {
                _viewTenantClassGrpModel._propertyId = string.IsNullOrWhiteSpace(poParam) ? "" : poParam;

                if (_conTenantClassGroupRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    await _gridTenantClassGroupRef.R_RefreshGrid(null);
                    await Task.Delay(200);
                    if (_tabStrip.ActiveTab.Id == nameof(PMM03710))
                    {
                        //sending property ud to tab2 (will be catch at init master tab2)
                        await _tabTenantClass.InvokeRefreshTabPageAsync(_viewTenantClassGrpModel._propertyId);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        #endregion

        #region TabPage
        private void R_Before_Open_TabPage(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(PMM03710);
            eventArgs.Parameter = _viewTenantClassGrpModel._propertyId;
        }
        private void R_TabEventCallback(object poValue)
        {
            _comboboxPropertyEnabled = (bool)poValue;
            _pageTenantClassOnCRUDmode = !(bool)poValue;
        }
        #endregion

        #region TabSet
        private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                if (_pageTenantClassGrpOnCRUDmode || _pageTenantClassOnCRUDmode)
                {
                    eventArgs.Cancel = true;//prevent move to another tab
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region TenantClassGrp
        private async Task TenantClassGrp_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewTenantClassGrpModel.GetTenantClassGroupList();
                eventArgs.ListEntityResult = _viewTenantClassGrpModel._TenantClassificationGroupList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        private async Task TenantClassGrp_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(eventArgs.Data);
                await _viewTenantClassGrpModel.GetTenantClassGroupRecord(loParam);
                eventArgs.Result = _viewTenantClassGrpModel._TenantClassificationGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }
        private async Task TenantClassGrp_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(eventArgs.Data);
                await _viewTenantClassGrpModel.DeleteTenantClassGroup(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void TenantGrp_AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (TenantClassificationGroupDTO)eventArgs.Data;
                loData.DUPDATE_DATE = DateTime.Now;
                loData.DCREATE_DATE = DateTime.Now;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private void TenantClassGrp_Saving(R_SavingEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = (TenantClassificationGroupDTO)eventArgs.Data;
                loData.CTENANT_CLASSIFICATION_GROUP_ID = string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_GROUP_ID) ? "" : loData.CTENANT_CLASSIFICATION_GROUP_ID;
                loData.CTENANT_CLASSIFICATION_GROUP_NAME = string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_GROUP_NAME) ? "" : loData.CTENANT_CLASSIFICATION_GROUP_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void TenantClassGrp_Validation(R_ValidationEventArgs eventArgs)
        {
            R_Exception loEx = new();
            try
            {
                var loData = eventArgs.Data as TenantClassificationGroupDTO;
                if (string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_GROUP_ID))
                {
                    loEx.Add("", _localizer["_val_tcg1"]);
                }
                if (string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_GROUP_NAME))
                {
                    loEx.Add("", _localizer["_val_tcg2"]);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            eventArgs.Cancel = loEx.HasError;
            loEx.ThrowExceptionIfErrors();
        }
        private async Task TenantClassGrp_ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<TenantClassificationGroupDTO>(eventArgs.Data);
                await _viewTenantClassGrpModel.SaveTenantClassGroup(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewTenantClassGrpModel._TenantClassificationGroup;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }
        private void TenantClassGrp_SetOther(R_SetEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _comboboxPropertyEnabled = eventArgs.Enable;
                _pageTenantClassGrpOnCRUDmode = !eventArgs.Enable;
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
