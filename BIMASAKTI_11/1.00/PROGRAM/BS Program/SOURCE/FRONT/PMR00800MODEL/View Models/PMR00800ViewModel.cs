using PMR00800COMMON;
using PMR00800COMMON.DTO_s;
using PMR00800COMMON.DTO_s.General;
using PMR00800COMMON.DTO_s.Print;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMR00800MODEL.View_Models
{
    public class PMR00800ViewModel
    {
        private PMR00800Model _initModel = new PMR00800Model();

        public List<PropertyDTO> PropertyList = new List<PropertyDTO>();

        public List<PeriodDtDTO> PeriodDTList = new List<PeriodDtDTO>();

        public PMR00800ParamDTO ReportParam = new PMR00800ParamDTO();

        public PeriodYearRangeDTO PeriodYearRange = new PeriodYearRangeDTO();

        public PeriodDtDTO PeriodDtInfo = new PeriodDtDTO();

        //for save as page

        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public PMR00800SaveAsDTO SaveAsParam = new PMR00800SaveAsDTO();

        public int PickedPeriodYear { get; set; } = DateTime.Now.Year;

        public string SelectedPeriodMonth { get; set; }

        public async Task InitialProcess()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                DateTime loTodayDate = DateTime.Today;

                // Get from back
                PropertyList = new List<PropertyDTO>(await _initModel.GetPropertyListAsync());
                await GetMonthDTListAsync();
                PeriodYearRange = await _initModel.GetPeriodYearRangeRecordAsync(new PeriodYearRangeParamDTO() { CMODE = "", CYEAR = "" });

                // Set default value
                var loFirstProperty = PropertyList.FirstOrDefault();
                ReportParam.CPROPERTY_ID = loFirstProperty?.CPROPERTY_ID ?? "";
                ReportParam.CPROPERTY_NAME = loFirstProperty?.CPROPERTY_NAME ?? "";
                SelectedPeriodMonth = loTodayDate.Month.ToString("D2");
                PickedPeriodYear = loTodayDate.Year;
                ReportParam.CTO_BUILDING = "";
                ReportParam.CTO_BUILDING_NAME = "";
                ReportParam.CFROM_BUILDING = "";
                ReportParam.CFROM_BUILDING_NAME = "";
                ReportParam.LIS_PRINT = true;
                ReportParam.CREPORT_FILENAME = "";
                ReportParam.CREPORT_FILETYPE = "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetMonthDTListAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMR00800ContextConstant.CYEAR, PickedPeriodYear.ToString());
                PeriodDTList = new List<PeriodDtDTO>(await _initModel.GetPeriodDtListAsync());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
