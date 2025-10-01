using PMR00170COMMON;
using PMR00170COMMON.Utility_Report;
using PMR00170FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMR00170MODEL.ViewModel
{
    public class PMR00170ViewModel : R_ViewModel<PMR00170DataTransactionDTO>
    {
        private PMR00170Model _model = new PMR00170Model();
        public List<PMR00170PropertyDTO> PropertyList = new List<PMR00170PropertyDTO>();
        public PMR00170InitialProcess InitialProcess = new PMR00170InitialProcess();
        public List<PMR00170GetMonthDTO>? GetMonthList;
        // public List<PMR00170GetReportTypeList> GetReportTypeList = new List<PMR00170GetReportTypeList> ();
        //{
        public List<PMR00170GetReportTypeList> GetReportTypeList = new List<PMR00170GetReportTypeList>
        {
            new PMR00170GetReportTypeList { Id = "1", Name = R_FrontUtility.R_GetMessage(typeof(Resources_PMR00170_Class), $"_labelReportType1")
        },
            new PMR00170GetReportTypeList { Id = "2",  Name = R_FrontUtility.R_GetMessage(typeof(Resources_PMR00170_Class), $"_labelReportType2") }
        };
        public R_ILocalizer<Resources_PMR00170_Class> _localizer;

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
           lcReportType = "1",
           lcStatusCode = "",
           lcStatusName = "";

        public int lnPeriodYearFrom;
        public int lnPeriodYearTo;

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _model.GetPropertyListStreamAsyncModel();
                PropertyList = loResult.Data!;
                if (PropertyList.Count > 0)
                {
                    PropertyCode = PropertyList[0].CPROPERTY_ID!;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                var loReturn = await _model.GetInitialProcessModel();
                InitialProcess = loReturn;

                //ASSIGN to Variable
                lnPeriodYearTo = InitialProcess.IYEAR==0?DateTime.Now.Year:InitialProcess.IYEAR;
                lnPeriodYearFrom = InitialProcess.IYEAR == 0 ? DateTime.Now.Year : InitialProcess.IYEAR;
                lcPeriodMonthFrom = InitialProcess.IMONTHS.ToString() == "0" ? DateTime.Now.Month.ToString("D2") : InitialProcess.IMONTHS.ToString();
                lcPeriodMonthTo = InitialProcess.IMONTHS.ToString() == "0" ? DateTime.Now.Month.ToString("D2") : InitialProcess.IMONTHS.ToString();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void GetMonth()
        {
            GetMonthList = new List<PMR00170GetMonthDTO>();
            for (int i = 1; i <= 12; i++)
            {
                string monthId = i.ToString("D2");
                string monthName = i.ToString("D2");
                //string monthName = R_FrontUtility.R_GetMessage(typeof(Resources_PMR00170_Class), $"_labelMonth{i}");
                PMR00170GetMonthDTO month = new PMR00170GetMonthDTO { Id = monthId, Name = monthName };
                GetMonthList.Add(month);
            }
            lcPeriodMonthFrom = DateTime.Now.Month.ToString("D2");
            lcPeriodMonthTo = DateTime.Now.Month.ToString("D2");

        }

        public void GetReportType()
        {
            for (int i = 1; i <= 2; i++)
            {
                string ReportTypeId = i.ToString();
                string ReportTypeName = R_FrontUtility.R_GetMessage(typeof(Resources_PMR00170_Class), $"_labelReportType{i}");
                PMR00170GetReportTypeList ReportType = new PMR00170GetReportTypeList { Id = ReportTypeId, Name = ReportTypeName };
                GetReportTypeList.Add(ReportType);
            }
            lcReportType = "1";

            var x = GetReportTypeList;

        }

        public void ValidationFieldEmpty(PMR00170DBParamDTO param)
        {
            var loEx = new R_Exception();
            try
            {

                if (string.IsNullOrWhiteSpace(param.CFROM_DEPARTMENT_ID) || string.IsNullOrWhiteSpace(param.CTO_DEPARTMENT_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMR00170_Class), "_validationDepartement");
                    loEx.Add(loErr);
                }
                if (string.IsNullOrWhiteSpace(param.CFROM_SALESMAN_ID) || string.IsNullOrWhiteSpace(param.CTO_SALESMAN_ID))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMR00170_Class), "_validationSalesman");
                    loEx.Add(loErr);
                }
                if (lnPeriodYearFrom == 0 || lnPeriodYearTo == 0 || string.IsNullOrWhiteSpace(lcPeriodMonthFrom) || string.IsNullOrWhiteSpace(lcPeriodMonthTo))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMR00170_Class), "_validationPeriod");
                    loEx.Add(loErr);
                }

                if (ConvertStringToDateTimeFormat(param.CTO_PERIOD) < ConvertStringToDateTimeFormat(param.CFROM_PERIOD))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMR00170_Class), "_validation_TO_nothigherthan_FROM");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public DateTime? ConvertStringToDateTimeFormat(string pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null,maka kembalikan null
                return null;
            }

            // Parse string ke DateTime
            DateTime result;
            if (DateTime.TryParseExact(pcEntity, "yyyyMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Jika parsing gagal,null
            return null;
        }

        public string ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (ptEntity == DateTime.MinValue)
            {
                // Jika DateTime adalah DateTime.MinValue, kembalikan string kosong
                return ""; // atau null, tergantung pada kebutuhan Anda
            }

            // Format DateTime ke string "yyyyMMdd"
            return ptEntity?.ToString("yyyyMM")!;
        }

    }
}
