using PMM01500COMMON;
using PMM01500FrontResources;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMM01500MODEL
{
    public class PMM01521ViewModel : R_ViewModel<PMM01520DTO>
    {
        private PMM01500Model _PMM01500Model = new PMM01500Model();
        private PMM01520Model _PMM01520Model = new PMM01520Model();
        public ObservableCollection<PMM01520DTO> PinaltyDateGrid { get; set; } = new ObservableCollection<PMM01520DTO>();
        public List<PMM01500UniversalDTO> RoundingModeList { get; set; } = new List<PMM01500UniversalDTO>();

        public PMM01520DTO InvPinalty = new PMM01520DTO();
        public PMM01520DTO InvPinaltyDate = new PMM01520DTO();
        public string PropertyId { get; set; } = "";
        public string InvGrpCode { get; set; } = "";
        public string InvGrpName { get; set; } = "";
        public bool MinPinaltyAmount { get; set; } = false;
        public bool MaxPinaltyAmount { get; set; } = false;

        #region List Property
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
        #endregion
        public async Task GetUniversalList()
        {
            var loEx = new R_Exception();

            try
            {
                RoundingModeList = await _PMM01500Model.GetAllUniversalListAsync("_ROUNDING_MODE");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetInvoicePinaltyDateList()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _PMM01520Model.GetPinaltyDateListStreamAsync(InvPinalty);

                loResult.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CPENALTY_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldPenaltyDate))
                    {
                        x.DPENALTY_DATE = ldPenaltyDate;
                    }
                    else
                    {
                        x.DPENALTY_DATE = null;
                    }
                });


                PinaltyDateGrid = new ObservableCollection<PMM01520DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetInvoicePinaltyDate(PMM01520DTO poParam)
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _PMM01520Model.GetPinaltyDateAsync(poParam);

                if (DateTime.TryParseExact(loResult.CPENALTY_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldPenaltyDate))
                {
                    loResult.DPENALTY_DATE = ldPenaltyDate;
                }
                else
                {
                    loResult.DPENALTY_DATE = null;
                }

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

                MaxPinaltyAmount = loResult.NMAX_PENALTY_AMOUNT > 0;
                MinPinaltyAmount = loResult.NMIN_PENALTY_AMOUNT > 0;

                InvPinaltyDate = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveInvoicePinalty(PMM01520DTO poNewEntity, eCRUDMode peCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CPENALTY_DATE = poNewEntity.DPENALTY_DATE.Value.ToString("yyyyMMdd");

                var loParameter = new PMM01520SaveParameterDTO<PMM01520DTO> { poData = poNewEntity, poCRUDMode = peCRUDMode };
                var loResult = await _PMM01520Model.SaveDeletePinaltyAsync(loParameter);

                if (DateTime.TryParseExact(loResult.CPENALTY_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldPenaltyDate))
                {
                    loResult.DPENALTY_DATE = ldPenaltyDate;
                }
                else
                {
                    loResult.DPENALTY_DATE = null;
                }

                InvPinaltyDate = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task DeleteInvoicePinalty(PMM01520DTO poNewEntity)
        {
            var loEx = new R_Exception();

            try
            {
                poNewEntity.CPENALTY_DATE = poNewEntity.DPENALTY_DATE.Value.ToString("yyyyMMdd");
                var loParameter = new PMM01520SaveParameterDTO<PMM01520DTO> { poData = poNewEntity, poCRUDMode = eCRUDMode.DeleteMode };
                await _PMM01520Model.SaveDeletePinaltyAsync(loParameter);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
