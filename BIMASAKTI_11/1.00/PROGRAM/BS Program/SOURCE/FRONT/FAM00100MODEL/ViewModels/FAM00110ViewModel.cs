using FAM00100Common.DTOs.FAM00100;
using FAM00100FrontResources;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FAM00100Model
{
    public class FAM00110ViewModel
    {
        private FAM00100Model _FAM00100Model = new FAM00100Model();

        #region Property Class
        public FAM00100DTO SystemParameterFA { get; set; } = new FAM00100DTO();
        public FAM00100GSPeriodYearRangeDTO GSPeriodYearRange { get; set; } = new FAM00100GSPeriodYearRangeDTO();
        #endregion

        #region Combo Box Helper List
        public List<KeyValuePair<string, string>> CBANK_IN_MODE_LIST { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("D",  R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_RadioDBankInMode")),
            new KeyValuePair<string, string>("B", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_RadioBBankInMode"))
        };
        public List<KeyValuePair<string, string>> CJOURNAL_TYPE_LIST { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("S",  R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_SummaryByJrnal")),
            new KeyValuePair<string, string>("D", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_DetailByAsset"))
        };
        public List<KeyValuePair<string, string>> CAUTO_DEPR_TYPE_LIST { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("P",  R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_AutoDeptP")),
            new KeyValuePair<string, string>("A", R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_AutoDeptA"))
        };
        public List<KeyValuePair<string, string>> PeriodMonthList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("01", "01"),
            new KeyValuePair<string, string>("02", "02"),
            new KeyValuePair<string, string>("03", "03"),
            new KeyValuePair<string, string>("04", "04"),
            new KeyValuePair<string, string>("05", "05"),
            new KeyValuePair<string, string>("06", "06"),
            new KeyValuePair<string, string>("07", "07"),
            new KeyValuePair<string, string>("08", "08"),
            new KeyValuePair<string, string>("09", "09"),
            new KeyValuePair<string, string>("10", "10"),
            new KeyValuePair<string, string>("11", "11"),
            new KeyValuePair<string, string>("12", "12")
        };

        #endregion

        #region Property ViewModel
        public DateTime? ICLinkDate { get; set; }
        public DateTime? PJLinkDate { get; set; }
        public DateTime? GLLinkDate { get; set; }
        #endregion
        public async Task GetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _FAM00100Model.GetGSPeriodYearRangeAsync();
                GSPeriodYearRange = loResult;

                SystemParameterFA.CBANK_IN_MODE = "D";
                SystemParameterFA.CCURRENT_PERIOD_MM = "01";
                SystemParameterFA.CCURRENT_PERIOD_YY_INT = loResult.IMIN_YEAR;
                SystemParameterFA.CSOFT_PERIOD_MM = "01";
                SystemParameterFA.CSOFT_PERIOD_YY_INT = loResult.IMIN_YEAR;
                SystemParameterFA.IBY_DEPT_LENGTH = 3;
                SystemParameterFA.IJRNGRP_LENGTH = 3;
                ICLinkDate = DateTime.Today;
                PJLinkDate = DateTime.Today;
                GLLinkDate = DateTime.Today;
                SystemParameterFA.CAUTO_DEPR_TYPE = "P";
                SystemParameterFA.CASSET_JOURNAL_TYPE = "S";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<FAM00100DTO> CreateSystemParamFA()
        {
            var loEx = new R_Exception();
            FAM00100DTO loResult = null;

            try
            {
                SystemParameterFA.CICLINK_DATE = ICLinkDate.Value.ToString("yyyyMMdd");
                SystemParameterFA.CPJLINK_DATE = PJLinkDate.Value.ToString("yyyyMMdd");
                SystemParameterFA.CGLLINK_DATE = GLLinkDate.Value.ToString("yyyyMMdd");
                SystemParameterFA.CCURRENT_PERIOD = SystemParameterFA.CCURRENT_PERIOD_YY_INT + SystemParameterFA.CCURRENT_PERIOD_MM;
                SystemParameterFA.CSOFT_PERIOD = SystemParameterFA.CSOFT_PERIOD_YY_INT + SystemParameterFA.CSOFT_PERIOD_MM;
                var loParam = new FAM00100SaveParameterDTO { Entity = SystemParameterFA, CRUDMode = eCRUDMode.AddMode };
                loResult = await _FAM00100Model.SaveSystemParamCBAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }
    }
}
