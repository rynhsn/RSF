using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL02500ViewModel : R_ViewModel<GSL02500DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL02500DTO> CBGrid = new ObservableCollection<GSL02500DTO>();

        public async Task GetCBList(GSL02500ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL02500GetCBListAsync(poParameter);

                CBGrid = new ObservableCollection<GSL02500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL02500DTO> GetCB(GSL02500ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL02500DTO loRtn = null; 
            try
            {
                var loResult = await _modelRecord.GSL02500GetCBAsync(poParameter);
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
