using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00550ViewModel : R_ViewModel<GSL00550DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00550DTO> GOAGrid = new ObservableCollection<GSL00550DTO>();

        public async Task GetGOAList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00550GetGOAListAsync();

                GOAGrid = new ObservableCollection<GSL00550DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL00550DTO> GetGOA(GSL00550ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00550DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00550GetGOAAsync(poEntity);
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
