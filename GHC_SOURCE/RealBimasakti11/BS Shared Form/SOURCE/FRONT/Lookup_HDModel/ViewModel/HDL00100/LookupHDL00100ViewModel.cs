using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Lookup_HDCOMMON.DTOs.HDL00100;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_HDModel.ViewModel.HDL00100
{
    public class LookupHDL00100ViewModel : R_ViewModel<HDL00100DTO>
    {
        private PublicHDLookupModel _model = new PublicHDLookupModel();
        private PublicHDLookupRecordModel _modelRecord = new PublicHDLookupRecordModel();
        public ObservableCollection<HDL00100DTO> PriceListLookupHeader = new ObservableCollection<HDL00100DTO>();
        public ObservableCollection<HDL00100DTO> PriceListLookupDetail = new ObservableCollection<HDL00100DTO>();
        public HDL00100DTO PriceListLookupRecord { get; set; }
        
        public HDL00100ParameterDTO ParameterLookup { get; set; }
        
        public async Task GetPriceListHeader()
        {
            var loEx = new R_Exception();

            try
            {
                
                var loResult = await _model.HDL00100PriceListLookupAsync(ParameterLookup);

                PriceListLookupHeader = new ObservableCollection<HDL00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPriceListDetail()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.HDL00100PriceListDetailLookupAsync(ParameterLookup);
                PriceListLookupDetail = new ObservableCollection<HDL00100DTO>(loResult);
                foreach (var list in PriceListLookupDetail)
                {
                    if (string.IsNullOrWhiteSpace(list.CSTART_DATE))
                    {
                        list.DSTART_DATE = null;
                    }
                    else
                    {
                        list.DSTART_DATE = DateTime.ParseExact(list.CSTART_DATE.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task<HDL00100DTO> GetPriceListRecord(HDL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            HDL00100DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.HDL00100GetRecordAsync(poEntity);
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
