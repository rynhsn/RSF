using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00110ViewModel : R_ViewModel<GSL00110DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00110DTO> TaxByDateGrid = new ObservableCollection<GSL00110DTO>();

        public async Task GetTaxByDateList(GSL00110ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00110GetTaxByDateListAsync(poParam);

                TaxByDateGrid = new ObservableCollection<GSL00110DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSL00110DTO> GetTaxByDate(GSL00110ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GSL00110DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00110GetTaxByDateAsync(poParam);
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
