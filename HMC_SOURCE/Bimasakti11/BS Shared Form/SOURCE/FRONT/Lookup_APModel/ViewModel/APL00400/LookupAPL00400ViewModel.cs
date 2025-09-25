using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00110;
using Lookup_APCOMMON.DTOs.APL00400;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
namespace Lookup_APModel.ViewModel.APL00400
{
    public class LookupAPL00400ViewModel : R_ViewModel<APL00400DTO>
    {
        private PublicAPLookupModel _model = new PublicAPLookupModel();
        private PublicAPLookupRecordModel _modelRecord = new PublicAPLookupRecordModel();
        public ObservableCollection<APL00400DTO> ProductAllocationGrid = new ObservableCollection<APL00400DTO>();
        public APL00400ParameterDTO ParameterLookup { get; set; }

        public async Task GetProductAllocationList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.APL00400ProductAllocationLookUpAsync(ParameterLookup);

                ProductAllocationGrid = new ObservableCollection<APL00400DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<APL00400DTO> GetProductAllocation(APL00400ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00400DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.APL00400GetRecordAsync(poEntity);
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