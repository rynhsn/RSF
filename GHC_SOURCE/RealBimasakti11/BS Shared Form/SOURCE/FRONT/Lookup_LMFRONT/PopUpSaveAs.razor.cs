using Lookup_PMModel.ViewModel.PopUpSaveAs;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMFRONT
{
    public partial class PopUpSaveAs : R_Page
    {
        private PoUpSaveAsViewModel _viewModel = new();
        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.SaveAsParam.CREPORT_FILETYPE = _viewModel.FileType.FirstOrDefault()!;
                _viewModel.SaveAsParam.CREPORT_FILENAME = "";
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
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
