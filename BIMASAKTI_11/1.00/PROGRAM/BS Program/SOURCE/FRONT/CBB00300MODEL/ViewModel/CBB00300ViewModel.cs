using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBB00300COMMON.DTOs;

namespace CBB00300MODEL.ViewModel
{
    public class CBB00300ViewModel : R_ViewModel<CBB00300DTO>
    {
        private CBB00300Model loModel = new CBB00300Model();
        public CBB00300DTO loCashFlow { get; set; } = new CBB00300DTO();

        public async Task GetCashflowInfoAsync()
        {
            R_Exception loEx = new R_Exception();
            CBB00300ResultDTO loTempResult = null;

            try
            {
                loTempResult = await loModel.GetCashflowInfoAsync();
                loCashFlow = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GenerateCashflowProcessAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await loModel.GenerateCashflowProcessAsync(new GenerateCashflowParameterDTO()
                {
                    CCURRENT_PERIOD = loCashFlow.CCURRENT_PERIOD_YY + loCashFlow.CCURRENT_PERIOD_MM
                });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
