using PMB04000COMMON.Context;
using PMB04000COMMON.DTO.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMB04000COMMON.DTO.Utilities;
using System.Globalization;
using PMB04000FrontResources;
using R_BlazorFrontEnd.Helpers;
using PMB04000COMMONPrintBatch.ParamDTO;

namespace PMB04000MODEL.ViewModel
{
    public class PMB04000ReceiptViewModel
    {
        private readonly PMB04000Model _model = new PMB04000Model();
        public PMB04000ParamDTO oParameterFromInvoice = new PMB04000ParamDTO();
        public PMB04000ParamDTO oParameterReceipt = new PMB04000ParamDTO();
        public ObservableCollection<PMB04000DTO> loOfficialReceipt = new ObservableCollection<PMB04000DTO>();
        public ObservableCollection<TemplateDTO> loTemplateList = new ObservableCollection<TemplateDTO>();

        public PeriodYearDTO oPeriodYearRange=  new PeriodYearDTO();
        public bool _lReceiptExist;
        public bool llEnabledComboTemplate;
        public string _mergeRefNoParamater = "";
        public string pcValueTemplate = "";
        public List<PeriodMonthDTO>? GetMonthList;
        public PMB04000ParamReportDTO oParameterPrintReceipt = new PMB04000ParamReportDTO();
        public async Task GetOfficialReceiptList(string paramPeriod)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                string paramTransCode = "940010";
                string lcCPERIOD = paramPeriod == "0" ? "" : paramPeriod;
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.CPROPERTY_ID, oParameterFromInvoice.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.CDEPT_CODE, oParameterFromInvoice.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.CTENANT_ID, oParameterFromInvoice.CTENANT_ID);
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.LALL_TENANT, oParameterFromInvoice.LALL_TENANT);
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.CINVOICE_TYPE, oParameterFromInvoice.CINVOICE_TYPE);
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.CPERIOD, lcCPERIOD);
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.CTRANS_CODE, paramTransCode);

                var loResult = await _model.GetInvReceiptListAsync();
                if (loResult.Data.Any())
                {
                    _lReceiptExist = true;
                    foreach (var item in loResult.Data!)
                    {
                        item.DREF_DATE = ConvertStringToDateTimeFormat(item.CREF_DATE!)!;
                    }
                }
                else
                {
                    _lReceiptExist = false;
                }
                loOfficialReceipt = new ObservableCollection<PMB04000DTO>(loResult.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public void ValidationTabReceipt()
        {
            var loEx = new R_Exception();
            try
            {
                if ((!oParameterFromInvoice.LALL_PERIOD) &&
               (oParameterFromInvoice.IPERIOD_YEAR < oPeriodYearRange.IMIN_YEAR
               || string.IsNullOrWhiteSpace(oParameterFromInvoice.CPERIOD_MONTH)))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMB0400_Class), "_validationPeriod");
                    loEx.Add(loErr);
                }

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }
        public async Task GetTemplateList()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.CPROPERTY_ID, oParameterFromInvoice.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.CPROGRAM_ID, "PMB04000");
                R_FrontContext.R_SetStreamingContext(PMB04000ContextDTO.CTEMPLATE_ID, pcValueTemplate);

                var loResult = await _model.GetTemplateListAsync();

                loTemplateList = new ObservableCollection<TemplateDTO>(loResult.Data) ?? new ObservableCollection<TemplateDTO>();
                llEnabledComboTemplate = loResult.Data?.Any() == true;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        private DateTime? ConvertStringToDateTimeFormat(string? pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                return null;
            }
            else
            {
                // Parse string ke DateTime
                DateTime result;
                if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
