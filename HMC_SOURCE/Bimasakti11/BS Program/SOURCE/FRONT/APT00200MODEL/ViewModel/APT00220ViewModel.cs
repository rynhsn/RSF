using APT00200COMMON.DTOs.APT00220;
using APT00200COMMON.DTOs.APT00221;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APT00200MODEL.ViewModel
{
    public class APT00220ViewModel
    {
        public APT00220Model loModel = new APT00220Model();


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
