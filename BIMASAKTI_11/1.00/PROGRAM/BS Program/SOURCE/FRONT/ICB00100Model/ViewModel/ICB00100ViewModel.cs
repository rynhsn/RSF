using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ICB00100Common;
using ICB00100Common.DTOs;
using ICB00100Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace ICB00100Model.ViewModel
{
    public class ICB00100ViewModel : R_ViewModel<ICB00100SystemParamDTO>
    {
        private ICB00100Model _model = new ICB00100Model();

        public ICB00100DTO CurrentPeriod = new ICB00100DTO();
        public List<ICB00100PropertyDTO> PropertyList { get; set; } = new List<ICB00100PropertyDTO>();
        public ICB00100SystemParamDTO SystemParam = new ICB00100SystemParamDTO();
        public ICB00100PeriodYearRangeDTO YearRange = new ICB00100PeriodYearRangeDTO();
        public ICB00100SoftClosePeriodDTO ProcessResult = new ICB00100SoftClosePeriodDTO();
        public ObservableCollection<ICB00100ValidateSoftCloseDTO> ValidateSoftCloseList = new ObservableCollection<ICB00100ValidateSoftCloseDTO>();
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
        public ObservableCollection<ICB00100SoftClosePeriodToDoListDTO> SoftClosePeriodToDoList =
            new ObservableCollection<ICB00100SoftClosePeriodToDoListDTO>();
        public string SelectedPropertyId { get; set; } = string.Empty;

        public async Task Init()
        {
            var loEx = new R_Exception();
            try
            {
                await GetPropertyList();
                await _getPeriodYearRange();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<ICB00100ListDTO<ICB00100PropertyDTO>>(
                        nameof(IICB00100.ICB00100GetPropertyList));
                PropertyList = loReturn.Data;
                SelectedPropertyId = PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region update spec

        public async Task GetSystemParam()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new ICB00100SystemParamParam { CPROPERTY_ID = SelectedPropertyId };
                var loReturn =
                    await _model.GetAsync<ICB00100SingleDTO<ICB00100SystemParamDTO>, ICB00100SystemParamParam>(
                        nameof(IICB00100.ICB00100GetSystemParam), loParam);

                if (loReturn.Data == null)
                {
                    SystemParam = new ICB00100SystemParamDTO();
                    return;
                }
                SystemParam = loReturn.Data;
                SystemParam.ISOFT_PERIOD_YY = int.Parse(SystemParam.CSOFT_PERIOD_YY);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task _getPeriodYearRange()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<ICB00100SingleDTO<ICB00100PeriodYearRangeDTO>>(
                        nameof(IICB00100.ICB00100GetPeriodYearRange));
                YearRange = loReturn.Data ?? new ICB00100PeriodYearRangeDTO();
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
                R_FrontContext.R_SetStreamingContext(ICB00100ContextConstant.CPROPERTY_ID, SelectedPropertyId);
                R_FrontContext.R_SetStreamingContext(ICB00100ContextConstant.CPERIOD_YEAR, SystemParam.ISOFT_PERIOD_YY.ToString());
                R_FrontContext.R_SetStreamingContext(ICB00100ContextConstant.CPERIOD_MONTH, SystemParam.CSOFT_PERIOD_MM);
                
                var loResult =
                    await _model.GetAsync<ICB00100ListDTO<ICB00100ValidateSoftCloseDTO>>(
                        nameof(IICB00100.ICB00100ValidateSoftPeriod));

                ValidateSoftCloseList = new ObservableCollection<ICB00100ValidateSoftCloseDTO>(loResult.Data);
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
                var loParam = new ICB00100PeriodParam
                {
                    CPROPERTY_ID = SelectedPropertyId,
                    CPERIOD_YEAR = Data.ISOFT_PERIOD_YY.ToString(),
                    CPERIOD_MONTH = Data.CSOFT_PERIOD_MM
                };
                
                var loResult =
                    await _model.GetAsync<ICB00100SingleDTO<ICB00100SoftClosePeriodDTO>, ICB00100PeriodParam>(
                        nameof(IICB00100.ICB00100ProcessSoftPeriod), loParam);
                ProcessResult = loResult.Data;
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
                var loParam = new ICB00100PeriodParam
                {
                    CPROPERTY_ID = SelectedPropertyId,
                    CPERIOD_YEAR = Data.ISOFT_PERIOD_YY.ToString(),
                    CPERIOD_MONTH = Data.CSOFT_PERIOD_MM
                };
                
                var loResult =
                    await _model.GetAsync<ICB00100SingleDTO<ICB00100PeriodParam>, ICB00100PeriodParam>(
                        nameof(IICB00100.ICB00100UpdateSoftPeriod), loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public void SetExcelDataSetToDoList()
        {
            var loConvertData = ValidateSoftCloseList.Select(item => new ICB00100ExcelToDoListDTO()
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

        #endregion
    }
}