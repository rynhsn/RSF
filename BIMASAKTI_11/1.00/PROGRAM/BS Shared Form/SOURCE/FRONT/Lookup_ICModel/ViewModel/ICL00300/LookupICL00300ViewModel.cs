using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Lookup_ICCOMMON.DTOs.ICL00300;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_ICModel.ViewModel.ICL00300
{
    public class LookupICL00300ViewModel
    {
        private PublicICLookupModel _model = new PublicICLookupModel();
        private PublicICLookupRecordModel _modelRecord = new PublicICLookupRecordModel();
        public ObservableCollection<ICL00300DTO> TransactionLookupGrid = new ObservableCollection<ICL00300DTO>();
        public ICL00300DTO TransactionLookupRecord { get; set; }
        public ICL00300DTO TransactionLookupEntity = new ICL00300DTO();

        public ICL00300ParameterDTO ParameterLookup { get; set; } = new ICL00300ParameterDTO();
        public ICL00300PeriodDTO PeriodLookup = new ICL00300PeriodDTO();
        
        public List<ICL00300DTO> RadioButton = new List<ICL00300DTO>()
        {
            new ICL00300DTO() { Code = "A", Desc = "All" },
            new ICL00300DTO() { Code = "P", Desc = "For Period" }
        };

        public List<ICL00300DTO> Month = new List<ICL00300DTO>()
        {
            new ICL00300DTO() { Code = "01", Desc = "01" },
            new ICL00300DTO() { Code = "02", Desc = "02" },
            new ICL00300DTO() { Code = "03", Desc = "03" },
            new ICL00300DTO() { Code = "04", Desc = "04" },
            new ICL00300DTO() { Code = "05", Desc = "05" },
            new ICL00300DTO() { Code = "06", Desc = "06" },
            new ICL00300DTO() { Code = "07", Desc = "07" },
            new ICL00300DTO() { Code = "08", Desc = "08" },
            new ICL00300DTO() { Code = "09", Desc = "09" },
            new ICL00300DTO() { Code = "10", Desc = "10" },
            new ICL00300DTO() { Code = "11", Desc = "11" },
            new ICL00300DTO() { Code = "12", Desc = "12" }
        };
        public async Task GetTransactionList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _model.ICL00300TransactionLookupAsync(ParameterLookup);
                TransactionLookupGrid = new ObservableCollection<ICL00300DTO>(loResult);
                foreach (var list in TransactionLookupGrid)
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

        public async Task<ICL00300DTO> GetTransactionRecord(ICL00300ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            ICL00300DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.ICL00300GetRecordAsync(poEntity);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public async Task GetInitialTransactionLookupList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.ICLInitiateTransactionLookupAsync();
                PeriodLookup = (loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}