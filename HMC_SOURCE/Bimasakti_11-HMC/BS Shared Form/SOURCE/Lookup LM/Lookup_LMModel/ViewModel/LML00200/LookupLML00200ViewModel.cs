using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_PMModel.ViewModel.LML00200
{
    public class LookupLML00200ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML00200DTO> UnitChargesList = new ObservableCollection<LML00200DTO>();

        public async Task GetUnitChargesList(LML00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCHARGE_TYPE_ID, poParam.CCHARGE_TYPE_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAXABLE_TYPE, poParam.CTAXABLE_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CACTIVE_TYPE, poParam.CACTIVE_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTAX_DATE, poParam.CTAX_DATE);

                var loResult = await _model.LML00200GetUnitChargesListAsync();
                UnitChargesList = new ObservableCollection<LML00200DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML00200DTO> GetUnitCharges(LML00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML00200DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.LML00200GetUnitChargesAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
