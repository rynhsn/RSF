using Lookup_PMCOMMON.DTOs.LML01000;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs.LML01100;
using Lookup_PMCOMMON.DTOs;

namespace Lookup_PMModel.ViewModel.LML01100
{
    public class LookupLML01100ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01100DTO> TermNConditionList = new ObservableCollection<LML01100DTO>();

        public async Task GetTermNConditionList(LML01100ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);

                var loResult = await _model.LML01100GetTermNConditionListAsync();
                TermNConditionList = new ObservableCollection<LML01100DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML01100DTO> GetTermNCondition(LML01100ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01100DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.LML01100GetTermNConditionAsync(poParam);
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
