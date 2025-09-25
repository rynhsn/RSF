using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Lookup_HDCOMMON.DTOs.HDL00100;
using Lookup_HDCOMMON.DTOs.HDL00200;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_HDModel.ViewModel.HDL00200
{
    public class LookupHDL00200ViewModel : R_ViewModel<HDL00200DTO>
    {
        private PublicHDLookupModel _model = new PublicHDLookupModel();
        private PublicHDLookupRecordModel _modelRecord = new PublicHDLookupRecordModel();
        public ObservableCollection<HDL00200DTO> PriceListLookup = new ObservableCollection<HDL00200DTO>();
        public HDL00200DTO PriceListLookupRecord { get; set; }

        public HDL00200ParameterDTO ParameterLookup { get; set; }

        public async Task GetPriceListHeader()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.HDL00200PriceListItemLookupAsync(ParameterLookup);
                PriceListLookup = new ObservableCollection<HDL00200DTO>(loResult);
                foreach (var list in PriceListLookup)
                {
                    if (string.IsNullOrWhiteSpace(list.CSTART_DATE))
                    {
                        list.DSTART_DATE = null;
                    }
                    else
                    {
                        list.DSTART_DATE = DateTime.ParseExact(list.CSTART_DATE.Trim(), "yyyyMMdd",
                            CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<HDL00200DTO> GetPriceListRecord(HDL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            HDL00200DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.HDL00200GetRecordAsync(poEntity);
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