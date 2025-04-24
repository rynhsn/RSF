using Microsoft.AspNetCore.Components;
using APR00100MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using APR00100FrontResources;
using System;
using System.Collections.Generic;
using System.Linq;
using Exception = System.Exception;
using Task = System.Threading.Tasks.Task;
using System.Text;
using System.Threading.Tasks;
using APR00100FrontResources;
using APR00100MODEL;
using APR00100MODEL.View_Models;

namespace APR00100FRONT
{
    public partial class PopUpSaveAs : R_Page
    {
        private APR00100ViewModel _viewModel = new();
        
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.SaveAsParam.CREPORT_FILENAME = "Supplier Activity Report";
                _viewModel.SaveAsParam.LIS_PRINT = false;
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
                await Close(true, _viewModel.SaveAsParam);
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