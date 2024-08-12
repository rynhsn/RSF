using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_PMModel.ViewModel.LML00300
{
    public class LookupLML00300ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML00300DTO> SupervisorList = new ObservableCollection<LML00300DTO>();
        
        public async Task GetSupervisorList(LML00300ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);

                var loResult = await _model.LML00300SupervisorListAsync();
                SupervisorList = new ObservableCollection<LML00300DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<LML00300DTO> GetSupervisor(LML00300ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML00300DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.LML00300SupervisorAsync(poParam);
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