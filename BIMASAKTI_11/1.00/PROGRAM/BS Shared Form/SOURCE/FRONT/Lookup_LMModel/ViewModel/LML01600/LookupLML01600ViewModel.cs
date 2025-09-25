using Lookup_PMCOMMON.DTOs.LML01500;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_PMCOMMON.DTOs.LML01600;
using Lookup_PMCOMMON.DTOs;

namespace Lookup_PMModel.ViewModel.LML01600
{
    public class LookupLML01600ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML01600DTO> GetList = new ObservableCollection<LML01600DTO>();

        public async Task GetSLACallTypeList(LML01600ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCALL_TYPE_ID, poParam.CCALL_TYPE_ID ?? "");
                var loResult = await _model.LML01600SLACallTypeListAsync();

                GetList = new ObservableCollection<LML01600DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML01600DTO> GetSLACallType(LML01600ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML01600DTO loRtn = null;
            try
            {
                LML01600DTO loResult = await _modelGetRecord.LML01600SLACallTypeAsync(poParam);
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
