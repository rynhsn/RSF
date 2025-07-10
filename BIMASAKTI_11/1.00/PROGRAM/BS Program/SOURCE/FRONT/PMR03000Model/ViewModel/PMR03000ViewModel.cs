using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PMR03000Common.DTOs;
using PMR03000Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace PMR03000Model.ViewModel
{
    public class PMR03000ViewModel : R_ViewModel<PMR03000ReportParamDTO>
    {
        private PMR03000Model _model = new PMR03000Model();
        public List<PMR03000PropertyDTO> PropertyList = new List<PMR03000PropertyDTO>();
        public List<PMR03000PeriodDTO> PeriodRange = new List<PMR03000PeriodDTO>();
        public List<PMR03000ReportTemplateDTO> ReportTemplateList = new List<PMR03000ReportTemplateDTO>();
        public PMR03000ReportParamDTO ReportParam = new PMR03000ReportParamDTO();

        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };
        public string CustomerType { get; } = "01"; // Default to "01" for Customer Type

        public async Task Init()
        {
            ReportParam.IPERIOD_YEAR = DateTime.Now.Year;
            await GetPropertyList();
            await GetPeriodList();
            await GetReportTemplateList();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _model.PMR03000GetPropertyList();
                PropertyList = loReturn.Data;
                ReportParam.CREPORT_FILETYPE = FileType[0];
                ReportParam.CPROPERTY_ID =
                    PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : ReportParam.CPROPERTY_ID;
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
                var loParam = new PMR03000PeriodParam
                {
                    CYEAR = ReportParam.IPERIOD_YEAR.ToString(),
                };
                var loReturn = await _model.PMR03000GetPeriodList(loParam);
                PeriodRange = loReturn.Data;
                ReportParam.CPERIOD_MONTH =
                    PeriodRange.Count > 0 ? PeriodRange[0].CPERIOD_NO : ReportParam.CPERIOD_MONTH;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetReportTemplateList()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMR03000ReportTemplateParam
                {
                    CPROPERTY_ID = ReportParam.CPROPERTY_ID,
                    // CPROGRAM_ID = "PMR03000",
                    CPROGRAM_ID = "PMR03000",
                    CTEMPLATE_ID = ""
                };
                var loReturn = await _model.PMR03000GetReportTemplateList(loParam);
                ReportTemplateList = loReturn.Data;
                if (ReportTemplateList.Count > 0)
                {
                    ReportParam.ReportTemplate = ReportTemplateList[0];
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        
    }
}