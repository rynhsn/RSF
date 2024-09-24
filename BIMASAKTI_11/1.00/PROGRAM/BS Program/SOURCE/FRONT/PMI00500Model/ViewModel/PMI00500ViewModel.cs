using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using PMI00500Common;
using PMI00500Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMI00500Model.ViewModel
{
    public class PMI00500ViewModel : R_ViewModel<PMI00500HeaderDTO>
    {
        private PMI00500Model _model = new PMI00500Model();
        public ObservableCollection<PMI00500HeaderDTO> HeaderList = new ObservableCollection<PMI00500HeaderDTO>();

        public ObservableCollection<PMI00500DTAgreementDTO> AgreementList =
            new ObservableCollection<PMI00500DTAgreementDTO>();

        public ObservableCollection<PMI00500DTReminderDTO> ReminderList =
            new ObservableCollection<PMI00500DTReminderDTO>();

        public ObservableCollection<PMI00500DTInvoiceDTO>
            InvoiceList = new ObservableCollection<PMI00500DTInvoiceDTO>();

        public PMI00500HeaderDTO HeaderEntity = new PMI00500HeaderDTO();
        public PMI00500DTAgreementDTO AgreementEntity = new PMI00500DTAgreementDTO();
        public PMI00500DTReminderDTO ReminderEntity = new PMI00500DTReminderDTO();
        
        public List<PMI00500PropertyDTO> PropertyList = new List<PMI00500PropertyDTO>();

        #region Parameter

        public string CPROPERTY_ID { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CDEPT_NAME { get; set; } = "";
        public string CTENANT_ID { get; set; } = "";
        public string CAGREEMENT_NO { get; set; } = "";
        public string CREMINDER_NO { get; set; } = "";

        #endregion

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<PMI00500ListDTO<PMI00500PropertyDTO>>(
                        nameof(IPMI00500.PMI00500GetPropertyList));
                PropertyList = loReturn.Data;
                CPROPERTY_ID = PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetHeaderList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CPROPERTY_ID, CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CDEPT_CODE, CDEPT_CODE);
                var loReturn = await _model.GetListStreamAsync<PMI00500HeaderDTO>(nameof(IPMI00500
                    .PMI00500GetHeaderListStream));
                
                HeaderList = new ObservableCollection<PMI00500HeaderDTO>(loReturn);

                //buat dummy data untuk headerList, 50 data
                // for (int i = 0; i < 50; i++)
                // {
                //     HeaderList.Add(new PMI00500HeaderDTO
                //     {
                //         CTENANT_ID = "TENANT_ID" + i,
                //         CTENANT_NAME = "TENANT_NAME" + i
                //     });
                // }

                if (HeaderList.Count <= 0)
                {
                    AgreementList.Clear();
                    ReminderList.Clear();
                    InvoiceList.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDTAgreementList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CPROPERTY_ID, CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CDEPT_CODE, CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CTENANT_ID, CTENANT_ID);
                
                var loReturn = await _model.GetListStreamAsync<PMI00500DTAgreementDTO>(nameof(IPMI00500
                    .PMI00500GetDTAgreementListStream));
                
                loReturn.ForEach(loItem =>
                {
                    loItem.DSTART_DATE = DateTime.TryParseExact(loItem.CSTART_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldStartDate)
                        ? ldStartDate
                        : (DateTime?)null;

                    loItem.DEND_DATE = DateTime.TryParseExact(loItem.CEND_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldEndDate)
                        ? ldEndDate
                        : (DateTime?)null;
                });
                
                AgreementList = new ObservableCollection<PMI00500DTAgreementDTO>(loReturn);
                
                //buat dummy data untuk headerList, 50 data
                // for (int i = 0; i < 50; i++)
                // {
                //     AgreementList.Add(new PMI00500DTAgreementDTO
                //     {
                //         CAGREEMENT_NO = "AGREEMENT_NO" + i,
                //         CAGREEMENT_STATUS = "AGREEMENT_NAME" + i
                //     });
                // }

                if (AgreementList.Count <= 0)
                {
                    ReminderList.Clear();
                    InvoiceList.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDTReminderList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CPROPERTY_ID, CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CDEPT_CODE, CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CTENANT_ID, CTENANT_ID);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CAGREEMENT_NO, CAGREEMENT_NO);
                
                var loReturn = await _model.GetListStreamAsync<PMI00500DTReminderDTO>(nameof(IPMI00500
                    .PMI00500GetDTReminderListStream));
                
                ReminderList = new ObservableCollection<PMI00500DTReminderDTO>(loReturn);
                
                //buat dummy data untuk headerList, 50 data
                // for (int i = 0; i < 50; i++)
                // {
                //     ReminderList.Add(new PMI00500DTReminderDTO
                //     {
                //         CREMINDER_NO = "REMINDER_NO" + i,
                //         CSTATUS = "REMINDER_NAME" + i
                //     });
                // }
                
                if (ReminderList.Count <= 0) InvoiceList.Clear();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetDTInvoiceList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CPROPERTY_ID, CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CDEPT_CODE, CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CTENANT_ID, CTENANT_ID);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CAGREEMENT_NO, CAGREEMENT_NO);
                R_FrontContext.R_SetStreamingContext(PMI00500ContextConstant.CREMINDER_NO, CREMINDER_NO);
                
                var loReturn = await _model.GetListStreamAsync<PMI00500DTInvoiceDTO>(nameof(IPMI00500
                    .PMI00500GetDTInvoiceListStream));
                
                loReturn.ForEach(loItem =>
                {
                    loItem.DDUE_DATE = DateTime.TryParseExact(loItem.CDUE_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldStartDate)
                        ? ldStartDate
                        : (DateTime?)null;
                });
                
                InvoiceList = new ObservableCollection<PMI00500DTInvoiceDTO>(loReturn);
                
                //buat dummy data untuk headerList, 50 data
                // for (int i = 0; i < 50; i++)
                // {
                //     InvoiceList.Add(new PMI00500DTInvoiceDTO
                //     {
                //         CINVOICE_NO = "INVOICE_NO" + i,
                //         CINVOICE_DESC = "INVOICE_NAME" + i
                //     });
                // }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}