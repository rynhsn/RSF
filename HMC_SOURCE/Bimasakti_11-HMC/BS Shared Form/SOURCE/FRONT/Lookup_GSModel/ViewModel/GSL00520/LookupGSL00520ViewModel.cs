using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00520ViewModel : R_ViewModel<GSL00520DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00520DTO> GOACOAGrid = new ObservableCollection<GSL00520DTO>();

        public async Task GetGOACOAList(GSL00520ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00520GetGOACOAListAsync(poParam);

                GOACOAGrid = new ObservableCollection<GSL00520DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL00520DTO> GetGOACOA(GSL00520ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GSL00520DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00520GetGOACOAAsync(poParam);
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
