using APR00600FrontResources;
using APR00600MODEL.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APR00600FRONT
{
    public partial class APR00600PopUpSaveAs : R_Page
    {
        private APR00600ViewModel _viewModel = new();
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.PoReportParam.CREPORT_FILENAME = "AR Customer Analisys";
                _viewModel.PoReportParam.LIS_PRINT = false;
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task OnClickOk()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Close(true, _viewModel.PoReportParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickCancel()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Close(false, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
