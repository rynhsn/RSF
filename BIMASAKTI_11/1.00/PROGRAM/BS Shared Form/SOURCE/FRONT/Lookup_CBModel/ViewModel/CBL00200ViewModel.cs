using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Lookup_CBCOMMON.DTOs.CBL00100;
using Lookup_CBCOMMON.DTOs.CBL00200;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_CBModel.ViewModel
{
    public class CBL00200ViewModel
    {
        private PublicCBLookupModel _model = new PublicCBLookupModel();
        private PublicCBLookupRecordModel _modelRecord = new PublicCBLookupRecordModel();

        public ObservableCollection<CBL00200DTO> CBJournalLookupGrid = new ObservableCollection<CBL00200DTO>();

        public CBL00200DTO CBJournalLookupRecord { get; set; }
        public CBL00200DTO CBJournalLookupEntity = new CBL00200DTO();

        public CBL00200ParameterDTO ParameterLookup { get; set; } = new CBL00200ParameterDTO();
        public CBL00100DTO PeriodLookup = new CBL00100DTO();

        public List<CBL00200DTO> RadioButton = new List<CBL00200DTO>()
        {
            new CBL00200DTO() { Code = "A", Desc = "All" },
            new CBL00200DTO() { Code = "P", Desc = "For Period" }
        };

        public List<CBL00200DTO> Month = new List<CBL00200DTO>()
        {
            new CBL00200DTO() { Code = "01", Desc = "01" },
            new CBL00200DTO() { Code = "02", Desc = "02" },
            new CBL00200DTO() { Code = "03", Desc = "03" },
            new CBL00200DTO() { Code = "04", Desc = "04" },
            new CBL00200DTO() { Code = "05", Desc = "05" },
            new CBL00200DTO() { Code = "06", Desc = "06" },
            new CBL00200DTO() { Code = "07", Desc = "07" },
            new CBL00200DTO() { Code = "08", Desc = "08" },
            new CBL00200DTO() { Code = "09", Desc = "09" },
            new CBL00200DTO() { Code = "10", Desc = "10" },
            new CBL00200DTO() { Code = "11", Desc = "11" },
            new CBL00200DTO() { Code = "12", Desc = "12" }
        };

        public async Task GetCBJournalList()
        {
            var loEx = new R_Exception();

            try
            {
                if (CBJournalLookupEntity.RadioButton == "A")
                {
                    ParameterLookup.CPERIOD = "";
                }
                else
                {
                    ParameterLookup.CPERIOD = CBJournalLookupEntity.VAR_GSM_PERIOD +
                                              CBJournalLookupEntity.Month;
                }

                var loResult = await _model.CBL00200JournalLookupAsync(ParameterLookup);
                CBJournalLookupGrid = new ObservableCollection<CBL00200DTO>(loResult);
                foreach (var list in CBJournalLookupGrid)
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

        public async Task<CBL00200DTO> GetCBJournal(CBL00200ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            CBL00200DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.CBL00200GetRecordAsync(poEntity);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task GetInitialProcess()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.CBL00100InitialProcessLookupAsync();
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