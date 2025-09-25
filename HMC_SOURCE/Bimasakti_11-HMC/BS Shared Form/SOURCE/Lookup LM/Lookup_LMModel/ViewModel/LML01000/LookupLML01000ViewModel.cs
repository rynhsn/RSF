using Lookup_PMCOMMON.DTOs;
using Lookup_PMCOMMON.DTOs.LML01000;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMModel.ViewModel.LML01000
{
    public class LookupLML01000ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01000DTO> BillingRuleList = new ObservableCollection<LML01000DTO>();

        public async Task GetBillingRuleList(LML01000ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBILLING_RULE_TYPE, poParam.CBILLING_RULE_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CUNIT_TYPE_CTG_ID, poParam.CUNIT_TYPE_CTG_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LACTIVE_ONLY, poParam.LACTIVE_ONLY);

                var loResult = await _model.LML01000GetBillingRuleListAsync();
                BillingRuleList = new ObservableCollection<LML01000DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML01000DTO> GetBillingRule(LML01000ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01000DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.LML01000GetBillingRuleAsync(poParam);
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
