using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_PMModel.ViewModel.LML00600
{
    public class LookupLML00600ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML00600DTO> TenantList = new ObservableCollection<LML00600DTO>();
        public async Task GetTenantList(LML00600ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCUSTOMER_TYPE, poParam.CCUSTOMER_TYPE);

                var loResult = await _model.LML00600GetTenantListAsync();
                TenantList = new ObservableCollection<LML00600DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<LML00600DTO> GetTenant(LML00600ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML00600DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.LML00600GetTenantAsync(poParam);
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
