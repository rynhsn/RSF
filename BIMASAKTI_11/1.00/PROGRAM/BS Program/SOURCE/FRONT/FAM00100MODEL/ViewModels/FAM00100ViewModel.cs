using FAM00100Common.DTOs.FAM00100;
using FAM00100FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace FAM00100Model.ViewModels
{
    public class FAM00100ViewModel : R_ViewModel<FAM00100DTO>
    {
        private FAM00100Model _FAM00100Model = new FAM00100Model();

        #region Property Class
        public FAM00100GSPeriodYearRangeDTO GSPeriodYearRange { get; set; } = new FAM00100GSPeriodYearRangeDTO();
        public FAM00100DTO FASystemParam { get; set; } = new FAM00100DTO();
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

        public List<KeyValuePair<string, string>> CAUTO_DEPT_TYPE_LIST { get; } = new List<KeyValuePair<string, string>>()
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
        public async Task<FAM00100ValidateInitDTO> GetInitialValidate()
        {
            var loEx = new R_Exception();
            FAM00100ValidateInitDTO loResult = null;
            try
            {
                loResult = await _FAM00100Model.GetInitValidateAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loResult;
        }

        public async Task GetInitialProcess()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _FAM00100Model.GetGSPeriodYearRangeAsync();
                GSPeriodYearRange = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParamCB()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _FAM00100Model.GetSystemParamCBAsync();

                if (!string.IsNullOrEmpty(loResult.CICLINK_DATE))
                {
                    if (TryParseYyyyMMdd(loResult.CICLINK_DATE, out var ic))
                        ICLinkDate = ic;
                }
                if (!string.IsNullOrEmpty(loResult.CPJLINK_DATE))
                {
                    if (TryParseYyyyMMdd(loResult.CPJLINK_DATE, out var pj))
                        PJLinkDate = pj;
                }
                if (!string.IsNullOrEmpty(loResult.CGLLINK_DATE))
                {
                    if (TryParseYyyyMMdd(loResult.CGLLINK_DATE, out var gl))
                        GLLinkDate = gl;
                }

                FASystemParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private static bool TryParseYyyyMMdd(string value, out DateTime result)
        {
            result = default;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            value = value.Trim();

            if (value.Length != 8)
                return false;

            return DateTime.TryParseExact(
                value,
                "yyyyMMdd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result
            );
        }


        public async Task SaveSystemParamCB(FAM00100DTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                // Update the entity with link dates before saving
                if (ICLinkDate.HasValue)
                    poEntity.CICLINK_DATE = ICLinkDate.Value.ToString("yyyyMMdd");
                if (PJLinkDate.HasValue)
                    poEntity.CPJLINK_DATE = PJLinkDate.Value.ToString("yyyyMMdd");
                if (GLLinkDate.HasValue)
                    poEntity.CGLLINK_DATE = GLLinkDate.Value.ToString("yyyyMMdd");

                // Build period strings as year then month (YYYYMM) so save always uses current UI values
                var currentYy = poEntity.CCURRENT_PERIOD_YY_INT != 0 ? poEntity.CCURRENT_PERIOD_YY_INT.ToString() : (poEntity.CCURRENT_PERIOD_YY ?? "");
                var softYy = poEntity.CSOFT_PERIOD_YY_INT != 0 ? poEntity.CSOFT_PERIOD_YY_INT.ToString() : (poEntity.CSOFT_PERIOD_YY ?? "");
                poEntity.CCURRENT_PERIOD = currentYy + (poEntity.CCURRENT_PERIOD_MM ?? "").PadLeft(2, '0');
                poEntity.CSOFT_PERIOD = softYy + (poEntity.CSOFT_PERIOD_MM ?? "").PadLeft(2, '0');

                var loParam = new FAM00100SaveParameterDTO { Entity = poEntity, CRUDMode = poCRUDMode };
                var loResult = await _FAM00100Model.SaveSystemParamCBAsync(loParam);

                if (!string.IsNullOrEmpty(loResult.CICLINK_DATE))
                {
                    if (TryParseYyyyMMdd(loResult.CICLINK_DATE, out var ic))
                        ICLinkDate = ic;
                }
                if (!string.IsNullOrEmpty(loResult.CPJLINK_DATE))
                {
                    if (TryParseYyyyMMdd(loResult.CPJLINK_DATE, out var pj))
                        PJLinkDate = pj;
                }
                if (!string.IsNullOrEmpty(loResult.CGLLINK_DATE))
                {
                    if (TryParseYyyyMMdd(loResult.CGLLINK_DATE, out var gl))
                        GLLinkDate = gl;
                }
                FASystemParam = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
