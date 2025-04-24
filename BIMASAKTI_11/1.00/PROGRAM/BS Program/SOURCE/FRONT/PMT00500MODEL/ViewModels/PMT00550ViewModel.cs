using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using PMT00500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT00500MODEL
{
    public class PMT00550ViewModel : R_ViewModel<PMT00550DTO>
    {
        private PMT00550Model _PMT00550Model = new PMT00550Model();

        #region Property Class
        public PMT00500DTO LOI { get; set; } = new PMT00500DTO();
        public PMT00550DTO LOI_InvoicePlan { get; set; } = new PMT00550DTO();
        public ObservableCollection<PMT00550DTO> LOIInvoicePlanGrid { get; set; } = new ObservableCollection<PMT00550DTO>();
        public ObservableCollection<PMT00551DTO> LOIInvoicePlanChargesGrid { get; set; } = new ObservableCollection<PMT00551DTO>();
        public int TotalSeqNo { get; set; }
        public decimal TotalAmount { get; set; }
        public int InvoiceSeqNo { get; set; }
        public decimal InvoiceAmount { get; set; }
        public int PaidSeqNo { get; set; }
        public decimal PaidAmount { get; set; }
        #endregion

        public async Task GetLOIInvoicePlanList(PMT00550DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poEntity.CREC_ID = LOI.CREC_ID;
                var loResult = await _PMT00550Model.GetAgreementInvPlanListStreamAsync(poEntity);
                TotalSeqNo = loResult.Count > 0 ? loResult.Count : 0;
                TotalAmount = loResult.Count > 0 ? loResult.Sum(x => x.NTOTAL_AMOUNT) : 0;
                InvoiceSeqNo = loResult.Where(x => x.LINVOICED).Count() > 0 ? loResult.Where(x => x.LINVOICED).Count() : 0;
                InvoiceAmount = loResult.Where(x => x.LINVOICED).Count() > 0 ? loResult.Where(x => x.LINVOICED).Sum(x => x.NTOTAL_AMOUNT) : 0;
                PaidSeqNo = loResult.Where(x => x.LPAYMENT).Count() > 0 ? loResult.Where(x => x.LPAYMENT).Count() : 0;
                PaidAmount = loResult.Where(x => x.LPAYMENT).Count() > 0 ? loResult.Where(x => x.LPAYMENT).Sum(x => x.NTOTAL_AMOUNT) : 0;

                loResult.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldStartDate))
                    {
                        x.DSTART_DATE = ldStartDate;
                    }
                    else
                    {
                        x.DSTART_DATE = null;
                    }
                    if (DateTime.TryParseExact(x.CEND_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldEndDate))
                    {
                        x.DEND_DATE = ldEndDate;
                    }
                    else
                    {
                        x.DEND_DATE = null;
                    }
                    if (DateTime.TryParseExact(x.CINV_PERIOD, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldInvPeiod))
                    {
                        x.CINV_PERIOD_Display = ldInvPeiod.ToString("MM-yyyy");
                    }
                    else
                    {
                        x.CINV_PERIOD_Display = "";
                    }
                });

                LOIInvoicePlanGrid = new ObservableCollection<PMT00550DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIInvPlanChargeList(PMT00551DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00550Model.GetLOIInvPlanChargeStreamAsync(poEntity);
                loResult.ForEach(x =>
                {
                    x.CCHARGES_ID_NAME = x.CCHARGES_NAME + " (" + x.CCHARGES_ID + ")";
                    
                });

                LOIInvoicePlanChargesGrid = new ObservableCollection<PMT00551DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
