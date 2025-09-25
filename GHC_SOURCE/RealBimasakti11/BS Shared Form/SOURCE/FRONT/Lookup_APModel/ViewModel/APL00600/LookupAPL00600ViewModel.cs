using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Lookup_APCOMMON.DTOs.APL00400;
using Lookup_APCOMMON.DTOs.APL00600;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace Lookup_APModel.ViewModel.APL00600
{
    public class LookupAPL00600ViewModel : R_ViewModel<APL00600DTO>
    {
        private PublicAPLookupModel _model = new PublicAPLookupModel();
        private PublicAPLookupRecordModel _modelRecord = new PublicAPLookupRecordModel();
        public ObservableCollection<APL00600DTO> SchedulePaymentGrid = new ObservableCollection<APL00600DTO>();
        public ObservableCollection<APL00600DTO> InvoiceListGrid = new ObservableCollection<APL00600DTO>();
        public APL00600ParameterDTO ParameterLookup { get; set; }

        public async Task GetSchedulePaymentList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.APL00600ApSchedulePaymentLookupAsync(ParameterLookup);
                SchedulePaymentGrid = new ObservableCollection<APL00600DTO>(loResult);
                foreach (var list in SchedulePaymentGrid)
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

                    if (string.IsNullOrWhiteSpace(list.CSCH_PAYMENT_DATE))
                    {
                        list.DSCH_PAYMENT_DATE = null;
                    }
                    else
                    {
                        list.DSCH_PAYMENT_DATE = DateTime.ParseExact(list.CSCH_PAYMENT_DATE.Trim(), "yyyyMMdd",
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

        public async Task GetInvoiceGridLIst()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.APL00600ApInvoiceListLookupAsync(ParameterLookup);
                InvoiceListGrid = new ObservableCollection<APL00600DTO>(loResult);
                foreach (var list in InvoiceListGrid)
                {
                    if (string.IsNullOrWhiteSpace(list.CDUE_DATE))
                    {
                        list.DDUE_DATE = null;
                    }
                    else
                    {
                        list.DDUE_DATE = DateTime.ParseExact(list.CDUE_DATE.Trim(), "yyyyMMdd", CultureInfo.InvariantCulture);
                    }

                   
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        }

        public async Task<APL00600DTO> GetSchedulePayment(APL00600ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            APL00600DTO loRtn = null;
            try
            {
                var loResult = await _modelRecord.APL00600GetRecordAsync(poEntity);
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