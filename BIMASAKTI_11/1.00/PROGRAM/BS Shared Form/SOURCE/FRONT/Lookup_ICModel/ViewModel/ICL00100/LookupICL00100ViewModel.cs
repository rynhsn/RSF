using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Lookup_ICCOMMON.DTOs.ICL00100;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_ICModel.ViewModel.ICL00100
{
    public class LookupICL00100ViewModel
    {
        private PublicICLookupModel _model = new PublicICLookupModel();
        private PublicICLookupRecordModel _modelRecord = new PublicICLookupRecordModel();
        public ObservableCollection<ICL00100DTO> RequestLookup = new ObservableCollection<ICL00100DTO>();
        public ICL00100DTO RequestLookupRecord { get; set; }
        public ICL00100ParameterDTO ParameterLookup { get; set; } = new ICL00100ParameterDTO();
        
        
        public async Task GetRequestList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.ICL00100RequestLookupAsync(ParameterLookup);
                RequestLookup = new ObservableCollection<ICL00100DTO>(loResult);
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<ICL00100DTO> GetRequestRecord(ICL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            ICL00100DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.ICL00100GetRecordAsync(poEntity);
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