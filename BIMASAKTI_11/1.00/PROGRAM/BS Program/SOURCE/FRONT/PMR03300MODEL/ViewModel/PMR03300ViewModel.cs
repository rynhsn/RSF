using PMR03300COMMON.DTOs;
using PMR03300COMMON.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PMR03300FrontResources;
using PMR03300MODEL.DTO;

namespace PMR03300MODEL.ViewModel
{
    public class PMR03300ViewModel : R_ViewModel<PMR03300GetReportDTO>
    {
        private PMR03300Model _model = new PMR03300Model();
        public List<PMR03300PropertyDTO> PropertyList = new List<PMR03300PropertyDTO>();
        public List<PMR03300GetPeriodDtListDTO> PeriodListFrom = new List<PMR03300GetPeriodDtListDTO>();
        public List<PMR03300GetPeriodDtListDTO> PeriodListTo = new List<PMR03300GetPeriodDtListDTO>();
        public PMR03300GetCompanyInfoDTO CompanyInfo = new PMR03300GetCompanyInfoDTO();
        public PMR03300GetPeriodeYearRangeDTO PeriodeYearRange = new PMR03300GetPeriodeYearRangeDTO();
        public PMR03300GetSystemParamDTO SystemParam = new PMR03300GetSystemParamDTO();
        //public PMR03300ParamDbDTO PoParam = new PMR03300ParamDbDTO();
        //public PMR03300GetReportParamDTO PoInitParam = new PMR03300GetReportParamDTO();
        public PMR03300ReportParamDTO PoReportParam = new PMR03300ReportParamDTO();
        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public int _IFromYear;
        public string _CFromMonth; 
        public int _IToYear;
        public string _CToMonth;
        public bool LEnableToCode = true;
        public string CFR_CODE_NAME = "";
        public string CTO_CODE_NAME = "";



        public List<KeyValuePair<string, string>> TypeList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("L", R_FrontUtility.R_GetMessage(typeof(PMR03300FrontResources.Resources_Dummy_Class), "CLOCAL_CURRENCY")),
            new KeyValuePair<string, string>("B", R_FrontUtility.R_GetMessage(typeof(PMR03300FrontResources.Resources_Dummy_Class), "CBASE_CURRENCY")),
        };

        public List<GlobalTempDTO> FilterByList = new List<GlobalTempDTO>
        {
            new GlobalTempDTO { CCODE = "CUSTOMER_ID", CNAME = R_FrontUtility.R_GetMessage(typeof(PMR03300FrontResources.Resources_Dummy_Class), "CCUSTOMER_ID")},
            new GlobalTempDTO { CCODE = "CUSTOMER_NAME", CNAME = R_FrontUtility.R_GetMessage(typeof(PMR03300FrontResources.Resources_Dummy_Class), "CCUSTOMER_NAME") },
            new GlobalTempDTO { CCODE = "CUSTOMER_CATEGORY", CNAME = R_FrontUtility.R_GetMessage(typeof(PMR03300FrontResources.Resources_Dummy_Class), "CCUSTOMER_CATEGORY")},
            new GlobalTempDTO { CCODE = "JOURNAL_GROUP", CNAME = R_FrontUtility.R_GetMessage(typeof(PMR03300FrontResources.Resources_Dummy_Class), "CJURNAL_GROUP") }
        };

        public async Task Init()
        {
            PoReportParam.CCURRENCY_TYPE = "L";
            PoReportParam.CFILTER_BY = "CUSTOMER_ID";
            await GetPropertyList();
            await GetCompanyInfo();
            await GetSystemParam();
            await GetPeriodeYearRange();
            await GetPeriodDtListFrom();
            await GetPeriodDtListTo();
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _model.PMR03300GetPropertyList();
                PropertyList = loReturn.Data;
                PoReportParam.CREPORT_FILETYPE = FileType[0];
                PoReportParam.CPROPERTY_ID =
                    PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : PoReportParam.CPROPERTY_ID;
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodDtListFrom()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMR03300ContextHeaderDTO.CYEAR, _IFromYear.ToString());
                var loReturn = await _model.GetPeriodDtList();
                PeriodListFrom = loReturn.Data;
                _IFromYear = PeriodListFrom.Count > 0 ? int.Parse(PeriodListFrom[0].CCYEAR) : _IFromYear;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPeriodDtListTo()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMR03300ContextHeaderDTO.CYEAR, _IToYear.ToString());
                var loReturn = await _model.GetPeriodDtList();
                PeriodListTo = loReturn.Data;
                _IToYear = PeriodListTo.Count > 0 ? int.Parse(PeriodListTo[0].CCYEAR) : _IToYear;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodeYearRange()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMR03300ContextHeaderDTO.CMODE, "");
                R_FrontContext.R_SetStreamingContext(PMR03300ContextHeaderDTO.CYEAR, "");
                var loReturn = await _model.GetPeriodeYearRange();
                PeriodeYearRange = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetCompanyInfo()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn = await _model.GetCompanyInfo();
                CompanyInfo = loReturn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParam()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMR03300ContextHeaderDTO.CPROPERTY_ID, PoReportParam.CPROPERTY_ID);
                var loReturn = await _model.GetSystemParam();
                SystemParam = loReturn.Data;
                _IFromYear = int.Parse(SystemParam.CCURRENT_PERIOD_YY);
                _CFromMonth = SystemParam.CCURRENT_PERIOD_MM;
                _IToYear = int.Parse(SystemParam.CCURRENT_PERIOD_YY);
                _CToMonth = SystemParam.CCURRENT_PERIOD_MM;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
