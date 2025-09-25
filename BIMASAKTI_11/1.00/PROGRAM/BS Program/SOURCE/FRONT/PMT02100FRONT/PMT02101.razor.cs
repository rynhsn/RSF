using BlazorClientHelper;
using PMT02100COMMON.DTOs;
using PMT02100MODEL.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using R_BlazorFrontEnd.Interfaces;
using R_APICommonDTO;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100MODEL.FrontDTOs.PMT02100;

namespace PMT02100FRONT
{
    public partial class PMT02101 : R_Page
    {
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMT02100FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        
        private PMT02101ViewModel loViewModel = new PMT02101ViewModel();

        private R_ConductorGrid _conductorGridRef;

        private R_Grid<PMT02100HandoverDTO> _gridRef;

        public void StateChangeInvoke()
        {
            StateHasChanged();
        }

        public void ShowErrorInvoke(R_APIException poException)
        {
            var loEx = R_FrontUtility.R_ConvertFromAPIException(poException);
            this.R_DisplayException(loEx);
        }

        public async Task ShowSuccessInvoke()
        {
            await this.Close(true, true);
        }
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loScheduleParameter = (ScheduleProcessParameterDTO)poParameter;

                loViewModel.loScheduleHeader.DSCHEDULED_DATE = loViewModel.loScheduleHeader.DSCHEDULED_DATE.Value.Date.AddHours(9);

                loViewModel.SelectedUserId = loClientHelper.UserId;
                loViewModel.SelectedCompanyId = loClientHelper.CompanyId;

                loViewModel.StateChangeAction = StateChangeInvoke;
                loViewModel.ShowErrorAction = ShowErrorInvoke;
                loViewModel.ShowSuccessAction = async () =>
                {
                    await ShowSuccessInvoke();
                };

                await _gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loHandoverList = new ObservableCollection<PMT02100HandoverDTO>(loViewModel.loScheduleParameter.loSelectedHandover);
                eventArgs.ListEntityResult = loViewModel.loHandoverList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnCancel()
        {
            await this.Close(false, false);
        }

        private async Task OnSchedule()
        {
            bool lCancel;
            R_Exception loEx = new R_Exception();
            try
            {
                //loViewModel.loScheduleHeader.CSCHEDULED_TIME_HOURS = loViewModel.loScheduleHeader.ISCHEDULED_TIME_HOURS.ToString("D2");
                //loViewModel.loScheduleHeader.CSCHEDULED_TIME_MINUTES = loViewModel.loScheduleHeader.ISCHEDULED_TIME_MINUTES.ToString("D2");

                lCancel = !loViewModel.loScheduleHeader.DSCHEDULED_DATE.HasValue;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V001"));
                }
                //lCancel = string.IsNullOrWhiteSpace(loViewModel.loScheduleHeader.CSCHEDULED_TIME_HOURS) || string.IsNullOrWhiteSpace(loViewModel.loScheduleHeader.CSCHEDULED_TIME_MINUTES);
                //if (lCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                //        "V002"));
                //}

                if (loEx.HasError)
                {
                    goto EndBlock;
                }

                await loViewModel.ScheduleProcessAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
    }
}