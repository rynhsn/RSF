using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs.GET_USER_PARAM_DETAIL;

namespace Lookup_PMModel.ViewModel.GetUserParamDetail
{
    public class GetUserParamDetailViewModel
    {
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();

        public async Task<GET_USER_PARAM_DETAILDTO> GetUnitCharges(GET_USER_PARAM_DETAILParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GET_USER_PARAM_DETAILDTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.GetUserParamDetailAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }
    }
}
