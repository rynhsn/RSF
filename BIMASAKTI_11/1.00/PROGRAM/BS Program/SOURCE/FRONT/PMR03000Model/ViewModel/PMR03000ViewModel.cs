using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PMR03000Common.DTOs;
using PMR03000Common.DTOs.Print;
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
        public List<PMR03000MessageInfoDTO> MessageInfoList = new List<PMR03000MessageInfoDTO>();
        public PMR03000MessageInfoDTO MessageInfo = new PMR03000MessageInfoDTO();
        public PMR03000ReportParamDTO ReportParam = new PMR03000ReportParamDTO();

        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };
        public string CustomerType { get; } = "01"; // Default to "01" for Customer Type
        public string MessageType { get; } = "03"; // Default to "03" for Message Type

        public async Task Init()
        {
            await GetPropertyList();
            ReportParam.IPERIOD_YEAR = DateTime.Now.Year;
            await GetPeriodList();
            await GetReportTemplateList();
            await GetMessageInfoList();
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
                // ReportParam.CPERIOD_MONTH = PeriodRange.Count > 0 ? PeriodRange[0].CPERIOD_NO : ReportParam.CPERIOD_MONTH;
                ReportParam.CPERIOD_MONTH = DateTime.Now.Month.ToString("D2");
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
                    ReportParam.ReportTemplate = ReportTemplateList.FirstOrDefault(x => x.LDEFAULT) ?? ReportTemplateList[0];
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetMessageInfoList()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMR03000MessageInfoParam
                {
                    CMESSAGE_TYPE = MessageType // Assuming "03" is the message type for report generation Billing Statement
                };
                var loReturn = await _model.PMR03000GetMessageInfoList(loParam);
                MessageInfoList = loReturn.Data;
                if (MessageInfoList.Count > 0)
                {
                    // Assuming the first message info is the default one to use
                    MessageInfo = MessageInfoList[0];
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