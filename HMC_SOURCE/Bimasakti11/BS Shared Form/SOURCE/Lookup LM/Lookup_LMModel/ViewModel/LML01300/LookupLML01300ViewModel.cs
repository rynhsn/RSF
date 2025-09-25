using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs.LML01300;

namespace Lookup_PMModel.ViewModel.LML01300
{
    public class LookupLML01300ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01300DTO> GetList = new ObservableCollection<LML01300DTO>();


        public async Task GetLOIAgreementList(LML01300ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, poParam.CDEPT_CODE);

                var loResult = await _model.LML01300LOIAgreementListAsync();
                GetList = new ObservableCollection<LML01300DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML01300DTO> GetLOIAgreement (LML01300ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01300DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.LML01300GetLOIAgreementAsync(poParam);
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
