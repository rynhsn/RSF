using PMT50600COMMON.DTOs.PMT50620;
using PMT50600COMMON.DTOs.PMT50621;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PMT50600MODEL.ViewModel
{
    public class PMT50620ViewModel
    {
        public PMT50620Model loModel = new PMT50620Model();


        public async Task CloseFormProcessAsync(OnCloseProcessParameterDTO poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loModel.CloseFormProcessAsync(poParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
