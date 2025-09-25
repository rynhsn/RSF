using LMM01500COMMON;
using LMM01500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01500MODEL
{
    public class LMM01520ViewModel : R_ViewModel<LMM01520DTO>
    {
        private LMM01520Model _LMM01520Model = new LMM01520Model();

        public LMM01520DTO InvPinalty = new LMM01520DTO();

        public string PropertyValueContext = "";
        public string InvGrpCode { get; set; } = "";
        public string InvGrpName { get; set; } = "";

        public bool MinPinaltyAmount { get; set; } = false;
        public bool MaxPinaltyAmount { get; set; } = false;

        public async Task GetInvoicePinalty(LMM01520DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _LMM01520Model.R_ServiceGetRecordAsync(poParam);

                switch (loResult.CPENALTY_TYPE)
                {
                    case "10":
                        loResult.NPENALTY_TYPE_VALUE_MonthlyAmmount = loResult.NPENALTY_TYPE_VALUE;
                        break;
                    case "11":
                        loResult.NPENALTY_TYPE_VALUE_MonthlyPercentage = loResult.NPENALTY_TYPE_VALUE;
                        loResult.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth = loResult.CPENALTY_TYPE_CALC_BASEON;
                        break;
                    case "20":
                        loResult.NPENALTY_TYPE_VALUE_DailyAmmount = loResult.NPENALTY_TYPE_VALUE;
                        break;
                    case "21":
                        loResult.NPENALTY_TYPE_VALUE_DailyPercentage = loResult.NPENALTY_TYPE_VALUE;
                        loResult.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays = loResult.CPENALTY_TYPE_CALC_BASEON;
                        break;
                    case "30":
                        loResult.NPENALTY_TYPE_VALUE_OneTimeAmmount = loResult.NPENALTY_TYPE_VALUE;
                        break;
                    case "31":
                        loResult.NPENALTY_TYPE_VALUE_OneTimePercentage = loResult.NPENALTY_TYPE_VALUE;
                        break;
                }

                InvPinalty = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void ValidationPinalty(LMM01520DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                bool lCancel;

                lCancel = string.IsNullOrEmpty(poParam.CPENALTY_ADD_ID);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1511"));
                }
                lCancel = string.IsNullOrEmpty(poParam.CCUTOFDATE_BY);
                if (lCancel)
                {
                    loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Resources_Dummy_Class),
                        "1512"));
                }


            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveInvoicePinalty(LMM01520DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (!string.IsNullOrEmpty(poNewEntity.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth))
                {
                    poNewEntity.CPENALTY_TYPE_CALC_BASEON = poNewEntity.CPENALTY_TYPE_CALC_BASEON_CalcBaseonMonth;
                }
                else if (!string.IsNullOrEmpty(poNewEntity.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays))
                {
                    poNewEntity.CPENALTY_TYPE_CALC_BASEON = poNewEntity.CPENALTY_TYPE_CALC_BASEON_CalcBaseonDays;
                }

                if (poNewEntity.NPENALTY_TYPE_VALUE_MonthlyAmmount > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_MonthlyAmmount;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_MonthlyPercentage > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_MonthlyPercentage;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_DailyAmmount > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_DailyAmmount;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_DailyPercentage > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_DailyPercentage;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_OneTimeAmmount > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_OneTimeAmmount;
                }
                else if (poNewEntity.NPENALTY_TYPE_VALUE_OneTimePercentage > 0)
                {
                    poNewEntity.NPENALTY_TYPE_VALUE = poNewEntity.NPENALTY_TYPE_VALUE_OneTimePercentage;
                }

                var loResult = await _LMM01520Model.R_ServiceSaveAsync(poNewEntity, peCRUDMode);

                InvPinalty = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public List<RadioButton> RadioPinaltyType { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "10", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_MonAmount")},
            new RadioButton { Id = "11", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_MonPercen")},
            new RadioButton { Id = "20", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_DayAmount")},
            new RadioButton { Id = "21", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_DayPercen")},
            new RadioButton { Id = "30", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_OneTimeAmount")},
            new RadioButton { Id = "31", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_OneTimePercen")},
        };

        public List<RadioButton> RadioPinaltyTypeCalcBaseonMonth { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Principal")},
            new RadioButton { Id = "02", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_PrincipalPinalty")},

        };

        public List<RadioButton> RadioPinaltyTypeCalcBaseonDays { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_Principal")},
            new RadioButton { Id = "02", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_PrincipalPinalty")},

        };

        public List<RadioButtonInt> RadioRounded { get; set; } = new List<RadioButtonInt>
        {
            new RadioButtonInt { Id = -2, Text = "-2"},
            new RadioButtonInt { Id = -1, Text = "-1"},
            new RadioButtonInt { Id = 0, Text = "0"},
            new RadioButtonInt { Id = 1, Text = "1"},
            new RadioButtonInt { Id = 2, Text = "2"},
        };

        public List<RadioButton> RadioCutOfDate { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_DueDate")},
            new RadioButton { Id = "02", Text = R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_OneDayNextMon")},

        };

        public List<RadioButton> RadioPinaltyFeeStartFrom { get; set; } = new List<RadioButton>
        {
            new RadioButton { Id = "01", Text =  R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_GracePeriod")},
            new RadioButton { Id = "02", Text =  R_FrontUtility.R_GetMessage(typeof(Resources_Dummy_Class), "_DueDate")},

        };
    }
}
