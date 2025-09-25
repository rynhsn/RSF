using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100COMMON.DTOs.PMT02120;
using PMT02100COMMON.DTOs.PMT02120Print;
using PMT02100MODEL.FrontDTOs.PMT02100;
using PMT02100MODEL.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT02100FRONT
{
    public partial class PMT02120 : R_Page, R_ITabPage
    {
        [Inject] IClientHelper _clientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMT02100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        [Inject] private R_IReport _reportService { get; set; }

        private PMT02120ViewModel loViewModel = new PMT02120ViewModel();

        public GetPropertyListDTO loSelectedProperty = new GetPropertyListDTO();

        private PMT02100TabParameterDTO loTabParameter = new PMT02100TabParameterDTO();

        private R_TabStrip _TabStripRef;

        private R_TabPage _tabOpenRef;

        private R_TabPage _tabConfirmedRef;

        private R_TabPage _tabScheduledRef;

        private R_TabPage _tabHistoryRef;

        private R_ConductorGrid _conductorHandoverBuildingRef;
        private R_Conductor _conductorPrintRef;

        private R_ConductorGrid _conductorHandoverRef;

        private R_ConductorGrid _conductorEmployeeRef;

        private R_ConductorGrid _conductorRescheduleRef;

        private R_Grid<PMT02100HandoverBuildingDTO> _gridHandoverBuildingRef;

        private R_Grid<PMT02100HandoverDTO> _gridHandoverRef;

        private R_Grid<PMT02120EmployeeListDTO> _gridEmployeeRef;

        private R_Grid<PMT02120RescheduleListDTO> _gridRescheduleRef;

        private bool IsTenantListExist = false;

        private bool _comboboxEnabled = true;

        private bool _pageTenantOnCRUDmode = false;

        private void StateChangeInvoke()
        {
            StateHasChanged();
        }

        private void DisplayErrorInvoke(R_Exception poException)
        {
            this.R_DisplayException(poException);
        }

        public async Task ShowSuccessInvoke()
        {
            if (loViewModel.IsReinvite)
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.OK);
                //var loValidate = await R_MessageBox.Show("", "Confirm Schedule Succeed", R_eMessageBoxButtonType.OK);
                if (loValidate == R_eMessageBoxResult.OK)
                {
                    //await this.Close(true, true);
                    //await _gridHandoverRef!.R_RefreshGrid(null);
                    int index = loViewModel.loHandoverList.IndexOf(loViewModel.loHandover);
                    if (index != -1)
                    {
                        loViewModel.loHandoverList[index].IREINVITATION_COUNT++;
                        StateHasChanged();
                    }
                }
                //await _gridRef!.R_RefreshGrid(null);
            }
        }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                loViewModel.lcStatusCode = "03";
                loViewModel.loTabParameter = (PMT02100TabParameterDTO)poParameter;

                await loViewModel.GetPMSystemParamAsync();
                await _gridHandoverBuildingRef.R_RefreshGrid(null);

                loViewModel.lcCompanyId = _clientHelper.CompanyId;
                loViewModel.lcUserId = _clientHelper.UserId;

                loViewModel.StateChangeAction = StateChangeInvoke;
                loViewModel.DisplayErrorAction = DisplayErrorInvoke;
                loViewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            R_Exception loException = new R_Exception();

            try
            {
                loViewModel.loTabParameter = (PMT02100TabParameterDTO)poParam;

                await loViewModel.GetPMSystemParamAsync();
                await _gridHandoverBuildingRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }


            R_DisplayException(loException);
        }
        #region Handover Building

        private async Task Grid_Handover_Building_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetHandoverBuildingListStreamAsync();
                if (loViewModel.loHandoverBuildingList.Count == 0)
                {
                    _gridHandoverRef.DataSource.Clear();
                    _gridEmployeeRef.DataSource.Clear();
                    _gridRescheduleRef.DataSource.Clear();
                    loViewModel.loHandoverBuilding = new PMT02100HandoverBuildingDTO();
                    loViewModel.loHandover = new PMT02100HandoverDTO();
                }
                eventArgs.ListEntityResult = loViewModel.loHandoverBuildingList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Handover_Building_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loViewModel.loHandoverBuilding = (PMT02100HandoverBuildingDTO)eventArgs.Data;
                await _gridHandoverRef.R_RefreshGrid(null);
                //lcSelectedBuilding = loViewModel.loHandoverBuilding.CBUILDING_ID;
            }
        }

        #endregion

        #region Handover

        private async Task Grid_Handover_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetHandoverListStreamAsync();
                if (loViewModel.loHandoverList.Count == 0)
                {
                    _gridEmployeeRef.DataSource.Clear();
                    _gridRescheduleRef.DataSource.Clear();
                    loViewModel.loHandover = new PMT02100HandoverDTO();
                }
                eventArgs.ListEntityResult = loViewModel.loHandoverList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Handover_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loViewModel.loHandover = (PMT02100HandoverDTO)eventArgs.Data;
                //lcSelectedRefNo = loViewModel.loHandover.CREF_NO;
                await _gridEmployeeRef.R_RefreshGrid(null);
                await _gridRescheduleRef.R_RefreshGrid(null);
            }
        }

        #endregion

        #region Employee

        private async Task Grid_Employee_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetEmployeeListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loEmployeeList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Employee_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loViewModel.loEmployee = (PMT02120EmployeeListDTO)eventArgs.Data;
            }
        }

        #endregion

        #region Reschedule

        private async Task Grid_Reschedule_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await loViewModel.GetRescheduleListStreamAsync();
                eventArgs.ListEntityResult = loViewModel.loRescheduleList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Grid_Reschedule_Display(R_DisplayEventArgs eventArgs)
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                loViewModel.loReschedule = (PMT02120RescheduleListDTO)eventArgs.Data;
            }
        }

        #endregion

        #region Button

        private async Task OnClickReinvite()
        {
            R_Exception loEx = new R_Exception();
            string lcSelectedBuilding = "";
            string lcSelectedRefNo = "";

            try
            {
                lcSelectedBuilding = loViewModel.loHandoverBuilding.CBUILDING_ID;
                lcSelectedRefNo = loViewModel.loHandover.CREF_NO;

                loViewModel.IsReinvite = true;

                await loViewModel.ReinviteProcessAsync();

                //await loViewModel.ReinviteProcessAsync(new PMT02120ReinviteProcessParameterDTO()
                //{
                //    CPROPERTY_ID = loViewModel.loHandover.CPROPERTY_ID,
                //    CDEPT_CODE = loViewModel.loHandover.CDEPT_CODE,
                //    CREF_NO = loViewModel.loHandover.CREF_NO,
                //    CTRANS_CODE = loViewModel.loHandover.CTRANS_CODE
                //});

                //if (!loEx.HasError)
                //{

                //await _gridHandoverBuildingRef.R_RefreshGrid(null);
                //if (loViewModel.loHandoverBuildingList.Count > 0)
                //{
                //    PMT02100HandoverBuildingDTO loTemp = loViewModel.loHandoverBuildingList.Where(x => x.CBUILDING_ID == lcSelectedBuilding).FirstOrDefault();
                //    if (loTemp != null)
                //    {
                //        await _gridHandoverBuildingRef.R_SelectCurrentDataAsync(loTemp);
                //    }
                //    if (loViewModel.loHandoverList.Count > 0)
                //    {
                //        PMT02100HandoverDTO loTemp1 = loViewModel.loHandoverList.Where(x => x.CREF_NO == lcSelectedRefNo).FirstOrDefault();
                //        if (loTemp1 != null)
                //        {
                //            await _gridHandoverRef.R_SelectCurrentDataAsync(loTemp1);
                //        }
                //    }
                //}

                //int index = loViewModel.loHandoverList.IndexOf(loViewModel.loHandover);
                //if (index != -1)
                //{
                //    loViewModel.loHandoverList[index].IREINVITATION_COUNT++;
                //    StateHasChanged();
                //}

                //var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.OK);
                //}

                //await _gridHandoverRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            await R_DisplayExceptionAsync(loEx);
        }

        #endregion

        #region Assign_Employee
        private void Before_Open_Assign_Employee_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.loHandover;
            eventArgs.TargetPageType = typeof(PMT02121);
        }

        private async Task After_Open_Assign_Employee_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                if ((bool)eventArgs.Success && (bool)eventArgs.Result)
                {
                    await _gridEmployeeRef.R_RefreshGrid(null);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Print
        private void Before_Open_Print_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            List<PMT02100HandoverDTO> loFilteredHandover = _gridHandoverRef.GetFilteredGridData();

            List<PMT02120FilteredHandoverDTO> loParam = loFilteredHandover.Select((x, i) => new PMT02120FilteredHandoverDTO()
            {
                NO = i + 1,
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = x.CPROPERTY_ID,
                CDEPT_CODE = x.CDEPT_CODE,
                CREF_NO = x.CREF_NO,
                CTRANS_CODE = x.CTRANS_CODE,
            }).ToList();

            eventArgs.Parameter = new PMT02120PrintReportParameterDTO()
            {
                FilteredHandoverData = loParam
            };
            eventArgs.TargetPageType = typeof(PMT02122);
        }

        private async Task After_Open_Print_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //await _gridHandoverBuildingRef.R_RefreshGrid(null);
                //if (loViewModel.loHandoverBuildingList.Count > 0)
                //{
                //    PMT02100HandoverBuildingDTO loTemp = loViewModel.loHandoverBuildingList.Where(x => x.CBUILDING_ID == lcSelectedBuilding).FirstOrDefault();
                //    if (loTemp != null)
                //    {
                //        await _gridHandoverBuildingRef.R_SetCurrentData(loTemp);
                //    }
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Reschedule
        private void Before_Open_Reschedule_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.loHandover;
            eventArgs.TargetPageType = typeof(PMT02124);
        }

        private async Task After_Open_Reschedule_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            PMT02120PrintReportParameterDTO loPrintParameter = new PMT02120PrintReportParameterDTO();
            string lcSelectedBuilding = "";
            string lcSelectedRefNo = "";

            try
            {
                lcSelectedBuilding = loViewModel.loHandoverBuilding.CBUILDING_ID;
                lcSelectedRefNo = loViewModel.loHandover.CREF_NO;
                if ((bool)eventArgs.Success)
                {
                    if ((bool)eventArgs.Result)
                    {
                        loPrintParameter.LASSIGNMENT = true;
                        loPrintParameter.LCHECKLIST = true;
                        loPrintParameter.CCOMPANY_ID = _clientHelper.CompanyId;
                        loPrintParameter.CLANG_ID = _clientHelper.Culture.TwoLetterISOLanguageName;

                        //List<PMT02120FilteredHandoverDTO> loParam = loViewModel.loHandoverList.Select((x, i) => new PMT02120FilteredHandoverDTO()
                        //{
                        //    NO = i + 1,
                        //    CCOMPANY_ID = _clientHelper.CompanyId,
                        //    CPROPERTY_ID = x.CPROPERTY_ID,
                        //    CDEPT_CODE = x.CDEPT_CODE,
                        //    CREF_NO = x.CREF_NO,
                        //    CTRANS_CODE = x.CTRANS_CODE,
                        //}).ToList();

                        List<PMT02120FilteredHandoverDTO> loParam = new List<PMT02120FilteredHandoverDTO>();
                        loParam.Add(new PMT02120FilteredHandoverDTO()
                        {
                            NO = 1,
                            CCOMPANY_ID = _clientHelper.CompanyId,
                            CDEPT_CODE = loViewModel.loHandover.CDEPT_CODE,
                            CPROPERTY_ID = loViewModel.loHandover.CPROPERTY_ID,
                            CREF_NO = loViewModel.loHandover.CREF_NO,
                            CTRANS_CODE = loViewModel.loHandover.CTRANS_CODE
                        });

                        loPrintParameter.FilteredHandoverData = loParam;

                        await _reportService.GetReport(
                            "R_DefaultServiceUrlPM",
                            "PM",
                            "rpt/PMT02120Print/PrintReportPost",
                            "rpt/PMT02120Print/PrintReportGet",
                            loPrintParameter);
                    }

                    loViewModel.IsReinvite = false;
                    await loViewModel.ReinviteProcessAsync();

                    await _gridHandoverBuildingRef.R_RefreshGrid(null);
                    if (loViewModel.loHandoverBuildingList.Count > 0)
                    {
                        PMT02100HandoverBuildingDTO loTemp = loViewModel.loHandoverBuildingList.Where(x => x.CBUILDING_ID == lcSelectedBuilding).FirstOrDefault();
                        if (loTemp != null)
                        {
                            await _gridHandoverBuildingRef.R_SelectCurrentDataAsync(loTemp);
                        }
                        if (loViewModel.loHandoverList.Count > 0)
                        {
                            PMT02100HandoverDTO loTemp1 = loViewModel.loHandoverList.Where(x => x.CREF_NO == lcSelectedRefNo).FirstOrDefault();
                            if (loTemp1 != null)
                            {
                                await _gridHandoverRef.R_SelectCurrentDataAsync(loTemp1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Handover
        private void Before_Open_Handover_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            eventArgs.Parameter = loViewModel.loHandover;
            eventArgs.TargetPageType = typeof(PMT02125);
        }

        private async Task After_Open_Handover_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            string lcSelectedBuilding = "";
            string lcSelectedRefNo = "";

            try
            {
                lcSelectedBuilding = loViewModel.loHandoverBuilding.CBUILDING_ID;
                lcSelectedRefNo = loViewModel.loHandover.CREF_NO;

                if ((bool)eventArgs.Success && (bool)eventArgs.Result)
                {
                    await _gridHandoverBuildingRef.R_RefreshGrid(null);
                    if (loViewModel.loHandoverBuildingList.Count > 0)
                    {
                        PMT02100HandoverBuildingDTO loTemp = loViewModel.loHandoverBuildingList.Where(x => x.CBUILDING_ID == lcSelectedBuilding).FirstOrDefault();
                        if (loTemp != null)
                        {
                            await _gridHandoverBuildingRef.R_SetCurrentData(loTemp);
                        }
                    }
                }
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
