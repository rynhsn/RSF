using Lookup_TXCOMMON.DTOs.TXL00200;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lookup_TXModel.ViewModel.TXL00200
{
    public class LookupTXL00200ViewModel
    {
        private PublicLookupTXModel _model = new PublicLookupTXModel();
        private PublicLookupGetRecordTXModel _modelGetRecord = new PublicLookupGetRecordTXModel();
        public ObservableCollection<TXL00200DTO> loList = new ObservableCollection<TXL00200DTO>();
        public bool lHasData = false;
        public async Task TXL00200TaxNoLookUpAsync(TXL00200ParameterDTO poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.TXL00200TaxNoLookUpAsync(poParameter);

                if (loResult.Data.Any())
                {

                    if (!string.IsNullOrWhiteSpace(poParameter.CREMOVE_DATA_BOOKING_TAX_NO_SEPARATOR))
                    {
                        // Gunakan HashSet untuk mempercepat pencarian
                        var itemsToRemove = new HashSet<string>(
                            poParameter.CREMOVE_DATA_BOOKING_TAX_NO_SEPARATOR
                                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => x.Trim())
                                .Where(x => !string.IsNullOrWhiteSpace(x))); // Filter spasi kosong

                        // Hapus item menggunakan HashSet untuk efisiensi
                        loResult.Data!.RemoveAll(item => !string.IsNullOrEmpty(item.CBOOKING_TAX_NO) &&
                                                        itemsToRemove.Contains(item.CBOOKING_TAX_NO));
                    }
                    loList = new ObservableCollection<TXL00200DTO>(loResult.Data);
                }
                else
                {
                    loList = new ObservableCollection<TXL00200DTO>();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<TXL00200DTO> TXL00200TaxNoLookUpAsync(TXL00200ParameterGetRecordDTO poParam)
        {
            var loEx = new R_Exception();
            TXL00200DTO? loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.TXL00200TaxNoLookUpAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }
    }
}
