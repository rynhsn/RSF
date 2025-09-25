using Lookup_PMCOMMON.DTOs.LML01400;
using Lookup_PMCOMMON.DTOs.LML01500;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs;

namespace Lookup_PMModel.ViewModel.LML01500
{
    public class LookupLML01500ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01500DTO> GetList = new ObservableCollection<LML01500DTO>();

        public async Task GetSLACategoryList(LML01500ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCATEGORY_ID, poParam.CCATEGORY_ID ?? "");
                var loResult = await _model.LML01500SLACategoryListAsync();

                GetList = new ObservableCollection<LML01500DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML01500DTO> GetSLACategory(LML01500ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01500DTO loRtn = null;
            try
            {
                LML01500DTO loResult = await _modelGetRecord.LML01500SLACategoryAsync(poParam);
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
