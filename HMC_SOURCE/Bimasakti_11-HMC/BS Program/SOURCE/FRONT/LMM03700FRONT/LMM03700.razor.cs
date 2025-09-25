using LMM03700Common;
using LMM03700Common.DTO_s;
using LMM03700Model;
using Microsoft.Extensions.Logging;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace LMM03700Front
{
    public partial class LMM03700 : R_Page
    {
        private LMM03700ViewModel _viewTenantClassGrpModel = new();
        private R_ConductorGrid _conTenantClassGroupRef; //ref conductor TenantClassGrp
        private R_Grid<TenantClassificationGroupDTO> _gridTenantClassGroupRef; //ref grid TenantClassGrp
        private R_TabPage _tab2TenantClass; //ref TabPage tab2
        private R_TabStrip _tabStrip; //ref Tabstrip
        public bool _pageTenantClassOnCRUDmode = false; //to disable moving tab while crudmode
        private bool _comboboxPropertyEnabled = true; //to disable combobox while crudmode

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

        #region PropertyDropdown
        public async Task ComboboxPropertyValueChanged(string poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewTenantClassGrpModel._propertyId = poParam;//re assign when property klicked on combobox

                if (_conTenantClassGroupRef.R_ConductorMode == R_eConductorMode.Normal)
                {
                    await _gridTenantClassGroupRef.R_RefreshGrid(null);

                    if (_tabStrip.ActiveTab.Id == "TC")
                    {
                        //sending property ud to tab2 (will be catch at init master tab2)
                        await _tab2TenantClass.InvokeRefreshTabPageAsync(_viewTenantClassGrpModel._propertyId);
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
            eventArgs.TargetPageType = typeof(LMM03700Tab2);
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
            eventArgs.Cancel = _pageTenantClassOnCRUDmode;//prevent move to another tab
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
        private void TenantClassGrp_Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loData= (TenantClassificationGroupDTO)eventArgs.Data;
                loData.CTENANT_CLASSIFICATION_GROUP_ID = string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_GROUP_ID) ? "" : loData.CTENANT_CLASSIFICATION_GROUP_ID;
                loData.CTENANT_CLASSIFICATION_GROUP_NAME = string.IsNullOrWhiteSpace(loData.CTENANT_CLASSIFICATION_GROUP_NAME) ? "" : loData.CTENANT_CLASSIFICATION_GROUP_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
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

        #endregion
    }
}
