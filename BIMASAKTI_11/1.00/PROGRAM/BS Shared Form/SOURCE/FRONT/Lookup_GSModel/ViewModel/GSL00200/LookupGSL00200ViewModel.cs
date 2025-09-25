using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00200ViewModel : R_ViewModel<GSL00200DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00200DTO> WithholdingTaxGrid = new ObservableCollection<GSL00200DTO>();

        public async Task GetWithholdingTaxList(GSL00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {

                var loResult = await _model.GSL00200GetWithholdingTaxListAsync(poParam);

                WithholdingTaxGrid = new ObservableCollection<GSL00200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL00200DTO> GetWithholdingTax(GSL00200ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GSL00200DTO loRtn = null;
            try
            {

                var loResult = await _modelRecord.GSL00200GetWithholdingTaxAsync(poParam);
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
