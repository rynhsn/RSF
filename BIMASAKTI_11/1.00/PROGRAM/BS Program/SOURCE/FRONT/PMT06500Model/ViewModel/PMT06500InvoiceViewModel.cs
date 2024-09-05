using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using PMT06500Common;
using PMT06500Common.DTOs;
using PMT06500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_CommonFrontBackAPI;

namespace PMT06500Model.ViewModel
{
    public class PMT06500InvoiceViewModel : R_ViewModel<PMT06500InvoiceDTO>
    {
        private PMT06500InitModel _initModel = new PMT06500InitModel();
        private PMT06500Model _model = new PMT06500Model();

        public ObservableCollection<PMT06500SummaryDTO>
            SummaryGridList = new ObservableCollection<PMT06500SummaryDTO>();

        public PMT06500InvoiceDTO EntityInvoice = new PMT06500InvoiceDTO();

        public PMT06500TransCodeDTO TransCode = new PMT06500TransCodeDTO();

        public PMT06500InvoicePopupParam InvoicePopupParam = new PMT06500InvoicePopupParam();

        public string SelectedPropertyId = "";
        public string SelectedPeriodNo = DateTime.Now.Month.ToString("D2");
        public int SelectedYear = DateTime.Now.Year;

        public string SelectedPeriod;
        public string SelectedAgreementNo;

        public string CTRANS_CODE = "802410";
        public string COVT_TRANS_CODE = "802400";
        public string CTRANS_STATUS = "80";
        public string COVT_STATUS = "10";

        public async Task GetEntity()
        {
            var loEx = new R_Exception();

            try
            {
                var loEntity = InvoicePopupParam.OINVOICE;
                EntityInvoice = await _model.R_ServiceGetRecordAsync(loEntity);
                EntityInvoice.DREF_DATE = DateTime.TryParseExact(EntityInvoice.CREF_DATE, "yyyyMMdd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal, out var ldRefDate)
                    ? ldRefDate
                    : (DateTime?)null;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task SaveEntity(PMT06500InvoiceDTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CREF_DATE = poNewEntity.DREF_DATE?.ToString("yyyyMMdd");
                poNewEntity.CPERIOD = poNewEntity.CINV_PRD;
                if (peCRUDMode == eCRUDMode.AddMode)
                {
                    poNewEntity.CTRANS_CODE = CTRANS_CODE;
                }

                poNewEntity.CLINK_DEPT_CODE = InvoicePopupParam.CLINK_DEPT_CODE;
                poNewEntity.CLINK_TRANS_CODE = InvoicePopupParam.CLINK_TRANS_CODE;
                var loParameter = new SavingInvoiceParamDTO<PMT06500InvoiceDTO>
                {
                    Entity = poNewEntity,
                    CRUDMode = peCRUDMode
                };
                // EntityInvoice = await _model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);
                await _model.GetAsync<PMT06500SingleDTO<PMT06500InvoiceDTO>, SavingInvoiceParamDTO<PMT06500InvoiceDTO>>(
                    nameof(IPMT06500.PMT06500SavingInvoice), loParameter);
                
                EntityInvoice = poNewEntity;
                // EntityInvoice.DREF_DATE = DateTime.TryParseExact(EntityInvoice.CREF_DATE, "yyyyMMdd",
                //     CultureInfo.InvariantCulture,
                //     DateTimeStyles.AssumeUniversal, out var ldRefDate)
                //     ? ldRefDate
                //     : (DateTime?)null;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransCode()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT06500TransCodeParam { CTRANS_CODE = CTRANS_CODE };
                var loReturn =
                    await _initModel.GetAsync<PMT06500SingleDTO<PMT06500TransCodeDTO>, PMT06500TransCodeParam>(
                        nameof(IPMT06500Init.PMT06500GetTransCode), loParam);
                TransCode = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSummaryGridList(string pcRefNo, string pcDeptCode, string pcLinkDeptCode,
            string pcLinkTransCode, string pcSaveMode)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CPROPERTY_ID, SelectedPropertyId);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CPERIOD, SelectedPeriod);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CAGREEMENT_NO, SelectedAgreementNo);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CREF_NO, pcRefNo);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CDEPT_CODE, pcDeptCode);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CLINK_DEPT_CODE, pcLinkDeptCode);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CLINK_TRANS_CODE, pcLinkTransCode);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CACTION, pcSaveMode);

                var loReturn =
                    await _model.GetListStreamAsync<PMT06500SummaryDTO>(
                        nameof(IPMT06500.PMT06500GetSummaryListStream));

                SummaryGridList = new ObservableCollection<PMT06500SummaryDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}