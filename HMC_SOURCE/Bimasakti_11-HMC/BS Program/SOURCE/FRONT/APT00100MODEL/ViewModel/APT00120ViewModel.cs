using APT00100COMMON.DTOs.APT00120;
using APT00100COMMON.DTOs.APT00121;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APT00100MODEL.ViewModel
{
    public class APT00120ViewModel
    {
        public APT00120Model loModel = new APT00120Model();


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
