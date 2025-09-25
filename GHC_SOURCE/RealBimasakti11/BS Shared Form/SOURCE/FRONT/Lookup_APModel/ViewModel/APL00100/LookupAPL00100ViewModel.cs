using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00100;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_APModel.ViewModel.APL00100
{
    public class LookupAPL00100ViewModel : R_ViewModel<APL00100DTO>
    {
        private PublicAPLookupModel _model = new PublicAPLookupModel();
        private PublicAPLookupRecordModel _modelRecord = new PublicAPLookupRecordModel();
        public ObservableCollection<APL00100DTO> SupplierGrid = new ObservableCollection<APL00100DTO>();
        public string SearchText { get; set; } = "";
        public APL00100ParameterDTO ParameterLookup { get; set; }
        public async Task GetSupplierList()
        {
            var loEx = new R_Exception();

            try
            {
                
                ParameterLookup.CSEARCH_TEXT = SearchText;
                var loResult = await _model.APL00100SupplierLookUpAsync(ParameterLookup);

                SupplierGrid = new ObservableCollection<APL00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<APL00100DTO> GetSuplierInfo(APL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00100DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.APL00100GetRecordAsync(poEntity);
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
