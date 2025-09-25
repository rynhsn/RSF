using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL03300ViewModel : R_ViewModel<GSL03300DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL03300DTO> TaxChargesGrid = new ObservableCollection<GSL03300DTO>();
        public GSL03300ParameterDTO TaxChargesParameter = new GSL03300ParameterDTO();
        public async Task GetTaxChargesList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL03300GetTaxChargesListAsync(TaxChargesParameter);
                TaxChargesGrid = new ObservableCollection<GSL03300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GSL03300DTO> GetTaxCharges(GSL03300ParameterDTO poParameter)
        {
            var loEx = new R_Exception();
            GSL03300DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL03300GetTaxChargesAsync(poParameter);
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
