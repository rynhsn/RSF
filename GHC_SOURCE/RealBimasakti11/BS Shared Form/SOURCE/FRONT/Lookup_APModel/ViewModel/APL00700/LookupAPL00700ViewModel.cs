using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00700;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_APModel.ViewModel.APL00700
{
    public class LookupAPL00700ViewModel : R_ViewModel<APL00700DTO>
    {
        private PublicAPLookupModel _model = new PublicAPLookupModel();
        private PublicAPLookupRecordModel _modelRecord = new PublicAPLookupRecordModel();
        public ObservableCollection<APL00700DTO> CancelPaymentToSupplierGrid = new ObservableCollection<APL00700DTO>();
        public APL00700DTO CancelPaymentToSupplierLookupEntity = new APL00700DTO();
        public APL00700DTO SupplierLookupEntity = new APL00700DTO();
        public APL00700DTO SchedulePaymentLookupEntity = new APL00700DTO();
        public APL00700ParameterDTO ParameterLookup { get; set; }

        public List<APL00700DTO> Month = new List<APL00700DTO>()
        {
            new APL00700DTO() { Code = "01", Desc = "01" },
            new APL00700DTO() { Code = "02", Desc = "02" },
            new APL00700DTO() { Code = "03", Desc = "03" },
            new APL00700DTO() { Code = "04", Desc = "04" },
            new APL00700DTO() { Code = "05", Desc = "05" },
            new APL00700DTO() { Code = "06", Desc = "06" },
            new APL00700DTO() { Code = "07", Desc = "07" },
            new APL00700DTO() { Code = "08", Desc = "08" },
            new APL00700DTO() { Code = "09", Desc = "09" },
            new APL00700DTO() { Code = "10", Desc = "10" },
            new APL00700DTO() { Code = "11", Desc = "11" },
            new APL00700DTO() { Code = "12", Desc = "12" }
        };

        public string monthName = DateTime.Now.Month.ToString("00");
        public int IYEAR = DateTime.Now.Year;
        
        public async Task GetInitialCancelPaymentToSupplierLookup()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.APL00700InitialProcessAsync();
                CancelPaymentToSupplierLookupEntity = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetCancelPaymentToSupplierLookup()
        {
            var loEx = new R_Exception();
            try
            {
                ParameterLookup.CPERIOD = IYEAR+ monthName;
                ParameterLookup.CSUPPLIER_ID = SupplierLookupEntity.CSUPPLIER_ID;
                ParameterLookup.CSCH_PAYMENT_ID = SchedulePaymentLookupEntity.CSCH_PAYMENT_ID;
                var loResult = await _model.APL00700CancelPaymentToSupplierLookupAsync(ParameterLookup);
                CancelPaymentToSupplierGrid = new ObservableCollection<APL00700DTO>(loResult);
                foreach (var list in CancelPaymentToSupplierGrid)
                {
                    if (string.IsNullOrWhiteSpace(list.CREF_DATE))
                    {
                        list.DREF_DATE = null;
                    }
                    else
                    {
                        list.DREF_DATE = DateTime.ParseExact(list.CREF_DATE.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }
                    if (string.IsNullOrWhiteSpace(list.CSCH_PAYMENT_DATE))
                    {
                        list.DSCH_PAYMENT_DATE = null;
                    }
                    else
                    {
                        list.DSCH_PAYMENT_DATE = DateTime.ParseExact(list.CSCH_PAYMENT_DATE.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }

                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<APL00700DTO> GetRecordCancelPaymentToSupplierLookup(APL00700ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00700DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.APL00700GetRecordAsync(poEntity);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return CancelPaymentToSupplierLookupEntity;
        }
    }
}