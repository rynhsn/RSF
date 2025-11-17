using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using APR00600COMMON.DTOs;
using APR00600COMMON.Params;
using APR00600MODEL.DTO;

namespace APR00600MODEL.ViewModel
{
    public class APR00600ViewModel : R_ViewModel<APR00600GetReportDTO>
    {
        private APR00600Model _model = new APR00600Model();
        public List<APR00600PropertyDTO> PropertyList = new List<APR00600PropertyDTO>();
        public List<APR00600GetPeriodDtListDTO> PeriodListFrom = new List<APR00600GetPeriodDtListDTO>();
        public List<APR00600GetPeriodDtListDTO> PeriodListTo = new List<APR00600GetPeriodDtListDTO>();
        public APR00600GetCompanyInfoDTO CompanyInfo = new APR00600GetCompanyInfoDTO();
        public APR00600GetPeriodeYearRangeDTO PeriodeYearRange = new APR00600GetPeriodeYearRangeDTO();
        public APR00600GetSystemParamDTO SystemParam = new APR00600GetSystemParamDTO();
        //public APR00600ParamDbDTO PoParam = new APR00600ParamDbDTO();
        //public APR00600GetReportParamDTO PoInitParam = new APR00600GetReportParamDTO();
        public APR00600ReportParamDTO PoReportParam = new APR00600ReportParamDTO();
        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public int _IFromYear;
        public string _CFromMonth;
        public int _IToYear;
        public string _CToMonth;
        public bool LEnableToCode = true;
        public string CFR_CODE_NAME = "";
        public string CTO_CODE_NAME = "";
        public bool LEnableBtn = true;



        public List<KeyValuePair<string, string>> TypeList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("L", R_FrontUtility.R_GetMessage(typeof(APR00600FrontResources.Resources_Dummy_Class), "CLOCAL_CURRENCY")),
            new KeyValuePair<string, string>("B", R_FrontUtility.R_GetMessage(typeof(APR00600FrontResources.Resources_Dummy_Class), "CBASE_CURRENCY")),
        };

        public List<APR00600TempDTO> FilterByList = new List<APR00600TempDTO>
        {
            new APR00600TempDTO { CCODE = "SUPPLIER_ID", CNAME = R_FrontUtility.R_GetMessage(typeof(APR00600FrontResources.Resources_Dummy_Class), "SUPPLIER_ID")},
            new APR00600TempDTO { CCODE = "SUPPLIER_NAME", CNAME = R_FrontUtility.R_GetMessage(typeof(APR00600FrontResources.Resources_Dummy_Class), "SUPPLIER_NAME") },
            new APR00600TempDTO { CCODE = "SUPPLIER_CATEGORY", CNAME = R_FrontUtility.R_GetMessage(typeof(APR00600FrontResources.Resources_Dummy_Class), "SUPPLIER_CATEGORY")},
            new APR00600TempDTO { CCODE = "JOURNAL_GROUP", CNAME = R_FrontUtility.R_GetMessage(typeof(APR00600FrontResources.Resources_Dummy_Class), "CJURNAL_GROUP") }
        };

        public async Task Init()
        {
            PoReportParam.CCURRENCY_TYPE = "L";
            PoReportParam.CFILTER_BY = "SUPPLIER_ID";
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
                if (PropertyList.Count == 0)
                {
                    var loReturn = await _model.APR00600GetPropertyList();
                    PropertyList = loReturn.Data;
                    PoReportParam.CREPORT_FILETYPE = FileType[0];
                    PoReportParam.CPROPERTY_ID =
                        PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : PoReportParam.CPROPERTY_ID;
                    PoReportParam.CPROPERTY_NAME =
                        PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_NAME : PoReportParam.CPROPERTY_NAME;
                }
                

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
                R_FrontContext.R_SetStreamingContext(APR00600ContextHeaderDTO.CYEAR, _IFromYear.ToString());
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
                R_FrontContext.R_SetStreamingContext(APR00600ContextHeaderDTO.CYEAR, _IToYear.ToString());
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
                R_FrontContext.R_SetStreamingContext(APR00600ContextHeaderDTO.CMODE, "");
                R_FrontContext.R_SetStreamingContext(APR00600ContextHeaderDTO.CYEAR, "");
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
                R_FrontContext.R_SetStreamingContext(APR00600ContextHeaderDTO.CPROPERTY_ID, PoReportParam.CPROPERTY_ID);
                var loReturn = await _model.GetSystemParam();
                SystemParam = loReturn.Data;

                _IFromYear = SystemParam.CCURRENT_PERIOD_YY != null ? int.Parse(SystemParam.CCURRENT_PERIOD_YY) : 0;
                _IToYear = SystemParam.CCURRENT_PERIOD_YY != null ? int.Parse(SystemParam.CCURRENT_PERIOD_YY) : 0;
                _CFromMonth = SystemParam.CCURRENT_PERIOD_MM != null ? SystemParam.CCURRENT_PERIOD_MM : "";
                _CToMonth = SystemParam.CCURRENT_PERIOD_MM != null ? SystemParam.CCURRENT_PERIOD_MM : "";
                
                
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
