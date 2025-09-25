using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Lookup_ICCOMMON.DTOs.ICL00200;
using Lookup_ICCOMMON.DTOs.ICL00300;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_ICModel.ViewModel.ICL00200
{
    public class LookupICL00200ViewModel
    {
        private PublicICLookupModel _model = new PublicICLookupModel();
        private PublicICLookupRecordModel _modelRecord = new PublicICLookupRecordModel();
        public ObservableCollection<ICL00200DTO> RequestNoLookup = new ObservableCollection<ICL00200DTO>();
        public ICL00200DTO RequestNoLookupRecord { get; set; }
        public ICL00200ParameterDTO ParameterLookup { get; set; } = new ICL00200ParameterDTO();
  
        
        public async Task GetRequestNoList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.ICL00200RequestNoLookupAsync(ParameterLookup);
                RequestNoLookup = new ObservableCollection<ICL00200DTO>(loResult);
                foreach (var list in RequestNoLookup)
                {
                    if (string.IsNullOrWhiteSpace(list.CREF_DATE))
                    {
                        list.DREF_DATE = null;
                    }
                    else
                    {
                        list.DREF_DATE = DateTime.ParseExact(list.CREF_DATE.Trim(), "yyyyMMdd",
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

        public async Task<ICL00200DTO> GetRequestNoRecord(ICL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            ICL00200DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.ICL00200GetRecordAsync(poEntity);
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