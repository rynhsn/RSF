using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using PMT06500Common;
using PMT06500Common.DTOs;
using PMT06500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;

namespace PMT06500Model.ViewModel
{
    public class PMT06500ViewModel : R_ViewModel<PMT06500AgreementDTO>
    {
        private PMT06500InitModel _initModel = new PMT06500InitModel();
        private PMT06500Model _model = new PMT06500Model();

        public ObservableCollection<PMT06500InvoiceDTO>
            InvoiceGridList = new ObservableCollection<PMT06500InvoiceDTO>();

        public ObservableCollection<PMT06500SummaryDTO>
            SummaryGridList = new ObservableCollection<PMT06500SummaryDTO>();

        public ObservableCollection<PMT06500AgreementDTO> AgreementGridList =
            new ObservableCollection<PMT06500AgreementDTO>();

        public ObservableCollection<PMT06500OvtDTO> OvertimeGridList = new ObservableCollection<PMT06500OvtDTO>();

        public ObservableCollection<PMT06500ServiceDTO>
            ServiceGridList = new ObservableCollection<PMT06500ServiceDTO>();

        public ObservableCollection<PMT06500UnitDTO> UnitGridList = new ObservableCollection<PMT06500UnitDTO>();

        public PMT06500InvoiceDTO EntityInvoice = new PMT06500InvoiceDTO();
        public PMT06500AgreementDTO EntityAgreement = new PMT06500AgreementDTO();
        public PMT06500OvtDTO EntityOvertime = new PMT06500OvtDTO();
        public PMT06500ServiceDTO EntityService = new PMT06500ServiceDTO();

        public List<PMT06500PropertyDTO> PropertyList = new List<PMT06500PropertyDTO>();
        public List<PMT06500PeriodDTO> PeriodList = new List<PMT06500PeriodDTO>();
        public PMT06500YearRangeDTO YearRange = new PMT06500YearRangeDTO();

        public PMT06500InvoicePageParam InvoicePageParam = new PMT06500InvoicePageParam();
        public PMT06500InvoicePopupParam InvoicePopupParam = new PMT06500InvoicePopupParam();

        public string SelectedPropertyId = "";
        public string SelectedPeriodNo = DateTime.Now.Month.ToString("D2");
        public int SelectedYear = DateTime.Now.Year;

        public string SelectedPeriod = "";
        public string SelectedAgreementNo = "";

        public string CTRANS_CODE = "802410";
        public string COVT_TRANS_CODE = "802400";
        public string CTRANS_STATUS = "80";
        public string COVT_STATUS = "10";

        public async Task DeleteEntity(PMT06500InvoiceDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _initModel.GetAsync<PMT06500ListDTO<PMT06500PropertyDTO>>(
                        nameof(IPMT06500Init.PMT06500GetPropertyList));
                PropertyList = loReturn.Data;
                SelectedPropertyId = PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : SelectedPropertyId;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodList()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT06500YearParam { CYEAR = SelectedYear.ToString() };

                var loReturn =
                    await _initModel.GetAsync<PMT06500ListDTO<PMT06500PeriodDTO>, PMT06500YearParam>(
                        nameof(IPMT06500Init.PMT06500GetPeriodList), loParam);
                PeriodList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetYearRange()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _initModel.GetAsync<PMT06500SingleDTO<PMT06500YearRangeDTO>>(
                        nameof(IPMT06500Init.PMT06500GetYearRange));
                YearRange = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInvoiceGridList()
        {
            var loEx = new R_Exception();
            try
            {
                CTRANS_STATUS = "00,10,30,80";
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CPROPERTY_ID, SelectedPropertyId);

                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CTRANS_CODE, CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CTRANS_STATUS, CTRANS_STATUS);

                var loReturn =
                    await _model.GetListStreamAsync<PMT06500InvoiceDTO>(
                        nameof(IPMT06500.PMT06500GetInvoiceListStream));

                InvoiceGridList = new ObservableCollection<PMT06500InvoiceDTO>(loReturn);

                foreach (var loItem in InvoiceGridList)
                {
                    // period display "2024-08" dari inv_period
                    loItem.CPERIOD_DISPLAY = loItem.CINV_PRD.Substring(0, 4) + "-" + loItem.CINV_PRD.Substring(4, 2);
                }
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

        public async Task GetAgreementGridList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CPROPERTY_ID, SelectedPropertyId);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CPERIOD, SelectedYear + SelectedPeriodNo);

                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.COVT_TRANS_CODE, COVT_TRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CTRANS_STATUS, CTRANS_STATUS);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.COVERTIME_STATUS, COVT_STATUS);


                var loReturn =
                    await _model.GetListStreamAsync<PMT06500AgreementDTO>(
                        nameof(IPMT06500.PMT06500GetAgreementListStream));

                AgreementGridList = new ObservableCollection<PMT06500AgreementDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetOvertimeGridList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CPROPERTY_ID, SelectedPropertyId);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CPERIOD, SelectedYear + SelectedPeriodNo);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CAGREEMENT_NO,
                    EntityAgreement.CAGREEMENT_NO);

                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.COVT_TRANS_CODE, COVT_TRANS_CODE);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CTRANS_STATUS, CTRANS_STATUS);
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.COVERTIME_STATUS, COVT_STATUS);

                var loReturn =
                    await _model.GetListStreamAsync<PMT06500OvtDTO>(
                        nameof(IPMT06500.PMT06500GetOvertimeListStream));

                loReturn.ForEach(loItem =>
                {
                    loItem.DREF_DATE = DateTime.TryParseExact(loItem.CREF_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldRefDate)
                        ? ldRefDate
                        : (DateTime?)null;
                });

                OvertimeGridList = new ObservableCollection<PMT06500OvtDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetServiceGridList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CPARENT_ID, EntityOvertime.CREC_ID);

                var loReturn =
                    await _model.GetListStreamAsync<PMT06500ServiceDTO>(
                        nameof(IPMT06500.PMT06500GetServiceListStream));
                ServiceGridList = new ObservableCollection<PMT06500ServiceDTO>(loReturn);

                foreach (var loItem in ServiceGridList)
                {
                    loItem.DDATE_IN = DateTime.ParseExact(loItem.CDATE_IN + " " + loItem.CTIME_IN, "yyyyMMdd HH:mm",
                        CultureInfo.InvariantCulture);
                    loItem.DDATE_OUT = DateTime.ParseExact(loItem.CDATE_OUT + " " + loItem.CTIME_OUT, "yyyyMMdd HH:mm",
                        CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetUnitGridList()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMT06500ContextConstant.CPARENT_ID, EntityService.CREC_ID);

                var loReturn =
                    await _model.GetListStreamAsync<PMT06500UnitDTO>(
                        nameof(IPMT06500.PMT06500GetUnitListStream));
                UnitGridList = new ObservableCollection<PMT06500UnitDTO>(loReturn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void OnChangedComboOnList(string value, string formName)
        {
            switch (formName)
            {
                case "property":
                    SelectedPropertyId = value;
                    break;
                case "periodNo":
                    SelectedPeriodNo = value;
                    break;
            }
        }


        public async Task ProcessSubmit()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMT06500ProcessSubmitParam();
                loParam.CPROPERTY_ID = EntityInvoice.CPROPERTY_ID;
                loParam.CREC_ID = EntityInvoice.CREC_ID;
                loParam.CNEW_STATUS = EntityInvoice.CTRANS_STATUS == "00" ? "10" : "00";

                var loReturn =
                    await _model.GetAsync<PMT06500SingleDTO<PMT06500PropertyDTO>, PMT06500ProcessSubmitParam>(
                        nameof(IPMT06500.PMT06500ProcessSubmit), loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }

    public class PMT06500InvoicePageParam
    {
        public string CPROPERTY_ID { get; set; }
        public string CPERIOD { get; set; }
        public string CLINK_DEPT_CODE { get; set; } = "";
        public string CLINK_TRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CACTION { get; set; } = "";
        public PMT06500AgreementDTO OAGREEMENT { get; set; }
    }

    public class PMT06500InvoicePopupParam
    {
        public R_eConductorMode EMODE { get; set; }
        public string CREF_NO { get; set; } = "";
        public string CDEPT_CODE { get; set; } = "";
        public string CLINK_DEPT_CODE { get; set; } = "";
        public string CLINK_TRANS_CODE { get; set; } = "";
        public string CACTION { get; set; } = "";
        public PMT06500InvoiceDTO OINVOICE { get; set; } = new PMT06500InvoiceDTO();
    }
}