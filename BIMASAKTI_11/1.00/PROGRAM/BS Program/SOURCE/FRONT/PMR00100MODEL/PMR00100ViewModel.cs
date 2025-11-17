using PMR00100Common.DTOs;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR00100Model
{
    public class PMR00100ViewModel : R_ViewModel<PMR00100DTO>
    {

        private PMR00100Model _model = new PMR00100Model();
        public List<PeriodDT_DTO> PeriodDTList = new List<PeriodDT_DTO>();
        public List<GetMonthDTO> GetMonthList { get; set; }
        public List<ReportTypeDTO> GetReportTypeList { get; set; }
        public List<LOOStatusDTO> LOOStatusList = new List<LOOStatusDTO>();
        public List<PMR00100DTO> LOOStatusPrintList = new List<PMR00100DTO>();
        public List<PropertyDTO> PropertyList = new List<PropertyDTO>();
        public PeriodYearRangeDTO PeriodYearEntity = new PeriodYearRangeDTO();
        public ObservableCollection<PMR00100DTO> Entity = new ObservableCollection<PMR00100DTO>();

        public PrintParamDTO param = new PrintParamDTO();
        public SaveAsDTO SaveAsParam = new SaveAsDTO();
        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };


        #region Property
        public string
            lcPeriodMonth = "",
            lcPeriod = "",
            PropertyCode = "",
            PropertyName = "",
            lcDeptCodeFrom = "",
            lcDeptNameFrom = "",
            lcDeptCodeTo = "",
            lcDeptNameTo = "",
            lcSalesmanCodeFrom = "",
            lcSalesmanNameFrom = "",
            lcSalesmanCodeTo = "",
            lcSalesmanNameTo = "",
            lcPeriodMonthFrom = "",
            lcPeriodMonthTo = "",
            lcReportType = "",
            lcStatusCode = "",
            lcStatusName = "";
        public int lnPeriodYear,
                   lnPeriodYearFrom,
                   lnPeriodYearTo;
        public bool llValidation = false;
        #endregion

        public async Task GetPeriod()
        {
            var loEx = new R_Exception();

            try
            {
                var loreturn = await _model.GetPeriodYearAsync();
                PeriodYearEntity = loreturn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPeriodDTList(PMR00100ParamDTO poData)
        {
            R_Exception loException = new R_Exception();
            try
            {
                poData.CYEAR = lnPeriodYear.ToString();
                var loReturn = await _model.GetPeriodDTListAsync(poData);
                PeriodDTList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetPropertyListAsync();
                if (loResult != null)
                {
                    PropertyList = loResult.Data;
                    if (PropertyCode == "" && loResult.Data.Count > 0)
                    {
                        var firstProperty = PropertyList.FirstOrDefault();
                        PropertyCode = firstProperty.CPROPERTY_ID ?? "";
                        PropertyName = firstProperty.CPROPERTY_NAME ?? "";
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOOStatusList()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loReturn = await _model.GetStatusListAsync();
                LOOStatusList = loReturn.Data;

                LOOStatusList.Insert(0, new LOOStatusDTO() { CCODE = "", CDESCRIPTION = "All" });

                if (LOOStatusList.Count > 0)
                {
                    lcStatusCode = LOOStatusList[0].CCODE;
                }

            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetPrintList(PrintParamDTO poData)
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.GetLOOPrintListAsync(poData);
                LOOStatusPrintList = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
