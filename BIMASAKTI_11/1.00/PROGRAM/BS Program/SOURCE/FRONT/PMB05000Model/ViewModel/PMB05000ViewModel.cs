using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using PMB05000Common;
using PMB05000Common.DTOs;
using PMB05000Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace PMB05000Model.ViewModel
{
    public class PMB05000ViewModel : R_ViewModel<PMB05000SystemParamDTO>
    {
        private PMB05000Model _model = new PMB05000Model();

        public PMB05000SystemParamDTO SystemParam = new PMB05000SystemParamDTO();
        public PMB05000PeriodYearRangeDTO YearRange = new PMB05000PeriodYearRangeDTO();
        public PMB05000SoftClosePeriodDTO ProcessResult = new PMB05000SoftClosePeriodDTO();
        public List<PMB05000PropertyDTO> Properties = new List<PMB05000PropertyDTO>();

        public ObservableCollection<PMB05000ValidateSoftCloseDTO> ValidateSoftCloseList =
            new ObservableCollection<PMB05000ValidateSoftCloseDTO>();

        public PMB05000PropertyDTO Property = new PMB05000PropertyDTO();
        public DataSet ExcelDataSetToDoList { get; set; }

        public List<KeyValuePair<string, string>> ComboPeriod = new List<KeyValuePair<string, string>>
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

        public bool EnableBtn { get; set; } = true;

        public async Task GetProperties()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult =
                    await _model.GetAsync<PMB05000ListDTO<PMB05000PropertyDTO>>(
                        nameof(IPMB05000.PMB05000GetProperties));
                Properties = loResult.Data;
                Property.CPROPERTY_ID = (Properties.Count > 0) ? Properties[0].CPROPERTY_ID : string.Empty;
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
                var loParam = new PMB05000SystemParamParam()
                {
                    CPROPERTY_ID = Property.CPROPERTY_ID
                };
                var loResult =
                    await _model.GetAsync<PMB05000SingleDTO<PMB05000SystemParamDTO>, PMB05000SystemParamParam>(
                        nameof(IPMB05000.PMB05000GetSystemParam), loParam);
                SystemParam = loResult.Data;
                SystemParam.ISOFT_PERIOD_YY = int.Parse(SystemParam.CSOFT_PERIOD_YY);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodYearRange()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult =
                    await _model.GetAsync<PMB05000SingleDTO<PMB05000PeriodYearRangeDTO>>(
                        nameof(IPMB05000.PMB05000GetPeriod));
                YearRange = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task UpdateSoftPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMB05000PeriodParam
                {
                    CCURRENT_SOFT_PERIOD = SystemParam.ISOFT_PERIOD_YY + SystemParam.CSOFT_PERIOD_MM,
                    CPROPERTY_ID = Property.CPROPERTY_ID
                };

                var loResult =
                    await _model.GetAsync<PMB05000SingleDTO<PMB05000PeriodParam>, PMB05000PeriodParam>(
                        nameof(IPMB05000.PMB05000UpdateSoftPeriod), loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ValidateSoftPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(PMB05000ContextConstant.CPERIOD,
                    SystemParam.ISOFT_PERIOD_YY + SystemParam.CSOFT_PERIOD_MM);
                R_FrontContext.R_SetStreamingContext(PMB05000ContextConstant.CPROPERTY_ID,
                    Property.CPROPERTY_ID);

                var loResult =
                    await _model.GetListStreamAsync<PMB05000ValidateSoftCloseDTO>(
                        nameof(IPMB05000.PMB05000ValidateSoftPeriod));

                ValidateSoftCloseList = new ObservableCollection<PMB05000ValidateSoftCloseDTO>(loResult);
                // convert cref date ke dref date
                foreach (var loItem in ValidateSoftCloseList)
                {
                    loItem.DREF_DATE = DateTime.TryParseExact(loItem.CREF_DATE, "yyyyMMdd",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.AssumeUniversal, out var ldRefDate)
                        ? ldRefDate
                        : (DateTime?)null;
                    // loItem.DREF_DATE = DateTime.ParseExact(loItem.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                }

                SetExcelDataSetToDoList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessSoftPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMB05000PeriodParam
                {
                    CCURRENT_SOFT_PERIOD = SystemParam.ISOFT_PERIOD_YY + SystemParam.CSOFT_PERIOD_MM,
                    CPROPERTY_ID = Property.CPROPERTY_ID
                };

                var loResult =
                    await _model.GetAsync<PMB05000SingleDTO<PMB05000SoftClosePeriodDTO>, PMB05000PeriodParam>(
                        nameof(IPMB05000.PMB05000ProcessSoftPeriod), loParam);
                ProcessResult = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        private void SetExcelDataSetToDoList()
        {
            var loConvertData = ValidateSoftCloseList.Select(item => new PMB05000ExcelToDoListDTO()
            {
                No = item.INO,
                Dept = item.CDEPARTMENT,
                TransactionType = item.CTRANSACTION_NAME,
                RefNo = item.CREF_NO,
                RefDate = item.CREF_DATE,
                Status = item.CSTATUS_NAME,
                Description = item.CDESCRIPTION,
                Solution = item.CSOLUTION
            }).ToList();

            var loDataTable = R_FrontUtility.R_ConvertTo(loConvertData);
            loDataTable.TableName = "Todo List";
            // loDefinition.TableName = "Definition";

            var loDataSet = new DataSet();
            loDataSet.Tables.Add(loDataTable);
            // loDataSet.Tables.Add(loDefinition);

            // Asign Dataset
            ExcelDataSetToDoList = loDataSet;
        }
    }
}