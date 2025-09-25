using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMModel.ViewModel.LML00800
{
    public class LookupLML00800ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();

        public ObservableCollection<LML00800DTO> AgreementList = new ObservableCollection<LML00800DTO>();
        public async Task GetAgreementList(LML00800ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CAGGR_STTS, poParam.CAGGR_STTS);
                //CR26/06/2024
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_CODE, poParam.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CREF_NO, poParam.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTENANT_ID, poParam.CTENANT_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CBUILDING_ID, poParam.CBUILDING_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_STATUS, poParam.CTRANS_STATUS);

                var loResult = await _model.LML00800GetAgreementListAsync();
                AgreementList = new ObservableCollection<LML00800DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML00800DTO> GetAgreement(LML00800ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML00800DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.LML00800GetAgreementAsync(poParam);
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
