using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Lookup_CBCOMMON.DTOs.CBL00100;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_CBModel.ViewModel
{
    public class CBL00100ViewModel
    {
        private PublicCBLookupModel _model = new PublicCBLookupModel();
        private PublicCBLookupRecordModel _modelRecord = new PublicCBLookupRecordModel();

        public ObservableCollection<CBL00100DTO>
            ReceiptFromCustomerLookupGrid = new ObservableCollection<CBL00100DTO>();

        public CBL00100DTO ReceiptFromCustomerLookupRecord { get; set; }
        public CBL00100DTO ReceiptFromCustomerLookupEntity = new CBL00100DTO();

        public CBL00100ParameterDTO ParameterLookup { get; set; } = new CBL00100ParameterDTO();
        public CBL00100DTO PeriodLookup = new CBL00100DTO();

        public List<CBL00100DTO> RadioButton = new List<CBL00100DTO>()
        {
            new CBL00100DTO() { Code = "A", Desc = "All" },
            new CBL00100DTO() { Code = "P", Desc = "For Period" }
        };

        public List<CBL00100DTO> Month = new List<CBL00100DTO>()
        {
            new CBL00100DTO() { Code = "01", Desc = "01" },
            new CBL00100DTO() { Code = "02", Desc = "02" },
            new CBL00100DTO() { Code = "03", Desc = "03" },
            new CBL00100DTO() { Code = "04", Desc = "04" },
            new CBL00100DTO() { Code = "05", Desc = "05" },
            new CBL00100DTO() { Code = "06", Desc = "06" },
            new CBL00100DTO() { Code = "07", Desc = "07" },
            new CBL00100DTO() { Code = "08", Desc = "08" },
            new CBL00100DTO() { Code = "09", Desc = "09" },
            new CBL00100DTO() { Code = "10", Desc = "10" },
            new CBL00100DTO() { Code = "11", Desc = "11" },
            new CBL00100DTO() { Code = "12", Desc = "12" }
        };

        public async Task GetReceiptFromCustomerList()
        {
            var loEx = new R_Exception();

            try
            {
                if (ReceiptFromCustomerLookupEntity.RadioButton == "A")
                {
                    ParameterLookup.CPERIOD = "";
                }
                else
                {
                    ParameterLookup.CPERIOD = ReceiptFromCustomerLookupEntity.VAR_GSM_PERIOD +
                                              ReceiptFromCustomerLookupEntity.Month;
                }

                var loResult = await _model.CBL00100ReceiptFromCustomerLookupAsync(ParameterLookup);
                ReceiptFromCustomerLookupGrid = new ObservableCollection<CBL00100DTO>(loResult);
                foreach (var list in ReceiptFromCustomerLookupGrid)
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

        public async Task<CBL00100DTO> GetReceiptFromCustomer(CBL00100ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            CBL00100DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.CBL00100GetRecordAsync(poEntity);
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