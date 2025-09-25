using Lookup_GSCOMMON.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Lookup_GSModel.ViewModel
{
    public class LookupGSL00100ViewModel : R_ViewModel<GSL00100DTO>
    {
        private PublicLookupModel _model = new PublicLookupModel();
        private PublicLookupRecordModel _modelRecord = new PublicLookupRecordModel();

        public ObservableCollection<GSL00100DTO> SalesTaxGrid = new ObservableCollection<GSL00100DTO>();

        public async Task GetSalesTaxList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.GSL00100GetSalesTaxListAsync();

                SalesTaxGrid = new ObservableCollection<GSL00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<GSL00100DTO> GetSalesTax(GSL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            GSL00100DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.GSL00100GetSalesTaxAsync(poEntity);
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
