using CBB00300COMMON.DTOs;
using CBB00300MODEL.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBB00300FRONT
{
    public partial class CBB00300 : R_Page
    {
        #region ViewModel
        private CBB00300ViewModel loViewModel = new CBB00300ViewModel();
        #endregion

        #region Inject
        [Inject] private R_ILocalizer<CBB00300FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        #endregion

        private string lcCurrentPeriodDisplay = "";
        private string lcMinCashflowPeriodDisplay = "";
        private string lcMaxCashflowPeriodDisplay = "";
        private string lcStrip = "";

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loViewModel.GetCashflowInfoAsync();
                lcCurrentPeriodDisplay = loViewModel.loCashFlow.CCURRENT_PERIOD_YY + "-" + loViewModel.loCashFlow.CCURRENT_PERIOD_MM;
                lcMinCashflowPeriodDisplay = loViewModel.loCashFlow.CMIN_PERIOD_YY + "-" + loViewModel.loCashFlow.CMIN_PERIOD_MM;
                lcMaxCashflowPeriodDisplay = loViewModel.loCashFlow.CMAX_PERIOD_YY + "-" + loViewModel.loCashFlow.CMAX_PERIOD_MM;
                if (lcMinCashflowPeriodDisplay=="-" && lcMaxCashflowPeriodDisplay == "-")
                {
                    lcStrip = "";
                    lcMinCashflowPeriodDisplay = "";
                    lcMaxCashflowPeriodDisplay = "";
                }
                else
                {
                    lcStrip = " - ";
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        private async Task ProcessOnClick()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loValidate = await R_MessageBox.Show("", _localizer["M001"], R_eMessageBoxButtonType.YesNo);
                if (loValidate == R_eMessageBoxResult.Yes)
                {
                    await loViewModel.GenerateCashflowProcessAsync();
                    if (!loEx.HasError)
                    {
                        var loValidate1 = await R_MessageBox.Show("", _localizer["M002"], R_eMessageBoxButtonType.OK);
                        if (loValidate1 == R_eMessageBoxResult.OK)
                        {
                            await loViewModel.GetCashflowInfoAsync();
                            lcCurrentPeriodDisplay = loViewModel.loCashFlow.CCURRENT_PERIOD_YY + "-" + loViewModel.loCashFlow.CCURRENT_PERIOD_MM;
                            lcMinCashflowPeriodDisplay = loViewModel.loCashFlow.CMIN_PERIOD_YY + "-" + loViewModel.loCashFlow.CMIN_PERIOD_MM;
                            lcMaxCashflowPeriodDisplay = loViewModel.loCashFlow.CMAX_PERIOD_YY + "-" + loViewModel.loCashFlow.CMAX_PERIOD_MM;
                            if (lcMinCashflowPeriodDisplay == "-" && lcMaxCashflowPeriodDisplay == "-")
                            {
                                lcStrip = "";
                                lcMinCashflowPeriodDisplay = "";
                                lcMaxCashflowPeriodDisplay = "";
                            }
                            else
                            {
                                lcStrip = " - ";
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
    }
}
