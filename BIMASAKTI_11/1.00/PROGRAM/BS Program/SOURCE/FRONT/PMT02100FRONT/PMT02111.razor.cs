using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PMT02100COMMON.DTOs.PMT02100;
using PMT02100MODEL.FrontDTOs.PMT02100;
using PMT02100MODEL.ViewModel;
using R_APICommonDTO;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMT02100COMMON.DTOs.PMT02110;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Extensions;
using R_BlazorFrontEnd.Controls.MessageBox;
using PMM03500COMMON.DTOs;
using PMM03500FRONT;

namespace PMT02100FRONT
{
    public partial class PMT02111 : R_Page
    {
        [Inject] private IClientHelper loClientHelper { get; set; }
        [Inject] IJSRuntime JS { get; set; }
        [Inject] private R_ILocalizer<PMT02100FrontResources.Resources_Dummy_Class> _localizer { get; set; }

        private PMT02111ViewModel loViewModel = new PMT02111ViewModel();

        private R_Conductor _conductorRef;

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
            var loValidate = await R_MessageBox.Show("", "Confirm Schedule Succeed", R_eMessageBoxButtonType.OK);
            if (loValidate == R_eMessageBoxResult.OK)
            {
                await this.Close(true, true);
            }
            //await _gridRef!.R_RefreshGrid(null);
        }


        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();

            try
            {
                loViewModel.loSelectedHandover = (PMT02100HandoverDTO)poParameter;
                loViewModel.loScheduleHeader.DSCHEDULED_DATE = loViewModel.loSelectedHandover.DSCHEDULED_HO_DATE;
                //if ((!string.IsNullOrWhiteSpace(loViewModel.loSelectedHandover.CSCHEDULED_HO_TIME)) && loViewModel.loScheduleHeader.DSCHEDULED_DATE.HasValue)
                //{
                //    TimeSpan time = TimeSpan.Parse(loViewModel.loSelectedHandover.CSCHEDULED_HO_TIME);
                //    loViewModel.loScheduleHeader.DSCHEDULED_DATE = loViewModel.loScheduleHeader.DSCHEDULED_DATE.Value.Date + time;
                //    //string[] loSplitTime = loViewModel.loSelectedHandover.CSCHEDULED_HO_TIME.Split(':');
                //    //loViewModel.loScheduleHeader.ISCHEDULED_TIME_HOURS = int.Parse(loSplitTime[0]);
                //    //loViewModel.loScheduleHeader.ISCHEDULED_TIME_MINUTES = int.Parse(loSplitTime[1]);
                //}

                loViewModel.lcCompanyId = loClientHelper.CompanyId;
                loViewModel.lcUserId = loClientHelper.UserId;

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

        #region Tenant
        private void Before_Open_Tenant_Popup(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new TabParameterDTO()
                {
                    CSELECTED_PROPERTY_ID = loViewModel.loSelectedHandover.CPROPERTY_ID,
                    CSELECTED_PROPERTY_NAME = loViewModel.loSelectedHandover.CPROPERTY_NAME,
                    CSELECTED_TENANT_ID = loViewModel.loSelectedHandover.CTENANT_ID,
                    LVIEW_ONLY = false,
                    LOPEN_AS_POPUP = true
                };
                eventArgs.PageTitle = "Edit Tenant Info";
                eventArgs.FormAccess = R_eFormAccess.Update.ToDescription();
                eventArgs.TargetPageType = typeof(PMM03502);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task After_Open_Tenant_Popup(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            PMT02110TenantDTO loTenant = null;
            try
            {
                
                loTenant = await loViewModel.GetTenantDetailAsync();
                loViewModel.loSelectedHandover.CTENANT_NAME = loTenant.CTENANT_NAME;
                loViewModel.loSelectedHandover.CTENANT_PHONE_NO = loTenant.CPHONE1;
                loViewModel.loSelectedHandover.CTENANT_EMAIL = loTenant.CEMAIL;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Button

        private async Task OnCancel()
        {
            await this.Close(false, false);
        }

        private async Task OnConfirm()
        {
            bool lCancel;
            R_Exception loEx = new R_Exception();
            try
            {
                lCancel = !loViewModel.loScheduleHeader.DSCHEDULED_DATE.HasValue;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V001"));
                }
                lCancel = loViewModel.loScheduleHeader.DSCHEDULED_DATE.Value < DateTime.Today;
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V004"));
                }
                //lCancel = string.IsNullOrWhiteSpace(loViewModel.loScheduleHeader.CSCHEDULED_TIME_HOURS) || string.IsNullOrWhiteSpace(loViewModel.loScheduleHeader.CSCHEDULED_TIME_MINUTES);
                //if (lCancel)
                //{
                //    loEx.Add(R_FrontUtility.R_GetError(
                //        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                //        "V005"));
                //}
                lCancel = string.IsNullOrWhiteSpace(loViewModel.loSelectedHandover.CTENANT_EMAIL);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(PMT02100FrontResources.Resources_Dummy_Class),
                        "V006"));
                }

                if (loEx.HasError)
                {
                    goto EndBlock;
                }

                await loViewModel.ConfirmScheduleProcessAsync();

                //if (!loEx.HasError)
                //{
                //    await this.Close(true, true);
                //}
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }
}