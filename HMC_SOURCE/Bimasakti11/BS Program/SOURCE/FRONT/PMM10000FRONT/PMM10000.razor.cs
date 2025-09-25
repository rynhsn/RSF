using BlazorClientHelper;
using Global_PMCOMMON.DTOs.Response.Property;
using Microsoft.AspNetCore.Components;
using PMM10000COMMON.SLA_Call_Type;
using PMM10000COMMON.SLA_Category;
using PMM10000COMMON.UtilityDTO;
using PMM10000FrontResources;
using PMM10000MODEL.DTO;
using PMM10000MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Menu.Tab;
using R_BlazorFrontEnd.Controls.MessageBox;
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
using System.Transactions;

namespace PMM10000FRONT
{
    public partial class PMM10000 : R_Page
    {
        private PMM10000CategoryViewModel _viewModel = new();
        private R_TabStrip? _tabCategory;
        private R_TabPage? _tabPageCallType;
        private R_TabPage? _tabPagePricelist;
        private R_TreeView<PMM10000TreeDTO>? _treeRef;
        private R_Conductor? _conductorRef;
        [Inject] IClientHelper? clientHelper { get; set; }
        private bool EnableDelete = true;
        private bool _pageOnCRUDmode = false;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();
            try
            {
                await PropertyListRecord(null);
                await _treeRef.R_RefreshTree(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region PropertyId
        private async Task PropertyListRecord(R_ServiceGetListRecordEventArgs? eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetPropertyList();
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
                PropertyDTO PropertyTemp = _viewModel._PropertyList
                    .FirstOrDefault(data => data.CPROPERTY_ID == lsProperty)!;
                _viewModel._PropertyValue = PropertyTemp;

                await _treeRef!.R_RefreshTree(null);

                if (_tabCategory!.ActiveTab.Id == "TabCallType")
                {
                    await _tabPageCallType!.InvokeRefreshTabPageAsync(_viewModel._PropertyValue);
                }  //await _gridSLACallType.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }
        #endregion

        #region Category CRUD
        private async Task ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                await _viewModel.GetCategoryList();
                eventArgs.ListEntityResult = _viewModel._CategoryGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task ServiceGetRecordAsync(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<PMM10000CategoryDTO>(eventArgs.Data);
                PMM10000CategoryDTO loParam = new PMM10000CategoryDTO()
                {
                    CPROPERTY_ID = loData.CPROPERTY_ID!,
                    CCATEGORY_ID = loData.CCATEGORY_ID
                };
                await _viewModel.GetEntity(loParam);
                eventArgs.Result = _viewModel._CategoryData;
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
                var loParam = (PMM10000CategoryDTO)eventArgs.Data;
                await _viewModel.ServiceDelete(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task R_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (PMM10000CategoryDTO)eventArgs.Data;
                if (loData != null)
                    EnableDelete = loData.ILEVEL != 0 && eventArgs.ConductorMode == R_eConductorMode.Normal;
                if (eventArgs.ConductorMode == R_eConductorMode.Edit)
                {
                    await CatName_textBox.FocusAsync();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (PMM10000CategoryDTO)eventArgs.Data;
                await _viewModel.ServiceSave(loParam, eventArgs.ConductorMode);
                eventArgs.Result = _viewModel._CategoryData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion
        #region Utility
        private R_TextBox? CatName_textBox;
        private R_TextBox? CatId_textBox;
        private bool _EnablehasData = true;
        private void ConvertToGridEntity(R_ConvertToGridEntityEventArgs eventArgs)
        {
            var loConductorData = (PMM10000CategoryDTO)eventArgs.Data;
            // loConductorData.CCATEGORY_ID_NAME_DISPLAY = string.Format("[{0}] {1} - {2}", loConductorData.ILEVEL, loConductorData.CCATEGORY_ID, loConductorData.CCATEGORY_NAME);

            var loData = R_FrontUtility.ConvertObjectToObject<PMM10000TreeDTO>(loConductorData);
            loData.Id = loConductorData.CCATEGORY_ID;
            loData.ParentId = loConductorData.CPARENT_CATEGORY_ID;
            loData.Description = string.Format("[{0}] {1} - {2}", loConductorData.ILEVEL, loConductorData.CCATEGORY_ID, loConductorData.CCATEGORY_NAME);
            loData.CNOTES = loConductorData.CNOTES;
            loData.Level = loConductorData.ILEVEL;

            eventArgs.GridData = loData;
        }
        public async Task ServiceAfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
     
        private void CheckEdit(R_CheckEditEventArgs eventArgs)
        {
            var loData = (PMM10000CategoryDTO)_conductorRef.R_GetCurrentData();
            eventArgs.Allow = loData.ILEVEL != 0;
        }
        private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loCurrentSelectDataList = (PMM10000TreeDTO)_treeRef.CurrentSelectedData;
                var loData = (PMM10000CategoryDTO)eventArgs.Data;

                // loData.CPARENT = loCurrentSelectDataList.Id;
                loData.CPROPERTY_ID = _viewModel._PropertyValue.CPROPERTY_ID!;
                loData.CPARENT_CATEGORY_ID = loCurrentSelectDataList.Id;
                loData.CPARENT_NAME = loCurrentSelectDataList.Name;
                loData.ILEVEL = loCurrentSelectDataList.Level + 1;
                loData.DCREATE_DATE = DateTime.Now;
                loData.DUPDATE_DATE = DateTime.Now;

                await CatId_textBox.FocusAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);

        }
        public async Task AfterDelete()
        {
            await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
        }
        private void SetHasData(R_SetEventArgs eventArgs)
        {
            _EnablehasData = eventArgs.Enable;
        }
        private void Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                _viewModel.ValidationFieldEmpty((PMM10000CategoryDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task BeforeCancel(R_BeforeCancelEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loValidate = await R_MessageBox.Show("", R_FrontUtility.R_GetMessage(typeof(Resources_PMM10000_Class), "ValidationBeforeCancel"), R_eMessageBoxButtonType.YesNo);

                eventArgs.Cancel = loValidate != R_eMessageBoxResult.Yes;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void SetOther(R_SetEventArgs eventArgs)
        {
            _pageOnCRUDmode = !eventArgs.Enable;
        }
        #endregion

        private void TabChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
        {
            _viewModel._dropdownProperty = true;
            eventArgs.Cancel = _pageOnCRUDmode;
            //if (eventArgs.TabStripTab.Id != "TabCategory")
            //{
            //    _viewModel._dropdownProperty = false;
            //}
        }

        private void Before_Open_CallType(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(PMM10000CallType);

            if (_viewModel._PropertyList.Any())
            {
                PMM10000DbParameterDTO poParam = new PMM10000DbParameterDTO
                {
                    CPROPERTY_ID = _viewModel._PropertyValue.CPROPERTY_ID!,
                    CPROPERTY_NAME = _viewModel._PropertyValue.CPROPERTY_NAME!,
                };
                eventArgs.Parameter = poParam;
            }
            else
            {
                eventArgs.Parameter = null;
            }
        }
        private void Before_Open_Pricelist(R_BeforeOpenTabPageEventArgs eventArgs)
        {
            eventArgs.TargetPageType = typeof(PMM10000Pricelist);

            if (_viewModel._PropertyList.Any())
            {
                PMM10000DbParameterDTO poParam = new PMM10000DbParameterDTO
                {
                    CPROPERTY_ID = _viewModel._PropertyValue.CPROPERTY_ID!,
                    CPROPERTY_NAME = _viewModel._PropertyValue.CPROPERTY_NAME!,
                };
                eventArgs.Parameter = poParam;
            }
            else
            {
                eventArgs.Parameter = null;
            }
        }

        private void R_TabEventCallback(object poValue)
        {
            var loEx = new R_Exception();

            try
            {
                _pageOnCRUDmode = (bool)poValue;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();

        }

        #region UserLocking
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
        private const string DEFAULT_MODULE_NAME = "PM";
        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult? loLockResult = null;

            try
            {
                var loData = (PMM10000CategoryDTO)eventArgs.Data;

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
                        Program_Id = "PMM10000",
                        Table_Name = "PMM_SLA_CATEGORY",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCATEGORY_ID)

                    };

                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = clientHelper.CompanyId,
                        User_Id = clientHelper.UserId,
                        Program_Id = "PMM10000",
                        Table_Name = "PMM_SLA_CATEGORY",
                        Key_Value = string.Join("|", clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CCATEGORY_ID)
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
