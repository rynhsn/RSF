using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using TXB00200Common;
using TXB00200Common.DTOs;
using TXB00200Common.Params;

namespace TXB00200Model.ViewModel
{
    public class TXB00200ViewModel : R_ViewModel<TXB00200DTO>
    {
        private TXB00200Model _model = new TXB00200Model();

        public TXB00200DTO CurrentPeriod = new TXB00200DTO();
        public List<TXB00200PropertyDTO> PropertyList = new List<TXB00200PropertyDTO>();
        public List<TXB00200PeriodDTO> PeriodList = new List<TXB00200PeriodDTO>();
        public List<TXB00200SoftClosePeriodToDoListDTO> SoftClosePeriodErrorList = new List<TXB00200SoftClosePeriodToDoListDTO>();
        public ObservableCollection<TXB00200SoftClosePeriodToDoListDTO> SoftClosePeriodToDoList = new ObservableCollection<TXB00200SoftClosePeriodToDoListDTO>();

        public TXB00200NextPeriodDTO NextPeriod { get; set; } = new TXB00200NextPeriodDTO();
        public string SelectedPropertyId { get; set; }
        public int SelectedYear { get; set; } = DateTime.Now.Year;
        public string SelectedPeriodNo { get; set; }

        public DataSet ExcelDataSetToDoList { get; set; }

        public async Task GetCurrentPeriod()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<TXB00200SingleDTO<TXB00200DTO>>(
                        nameof(ITXB00200.TXB00200GetSoftClosePeriod));
                CurrentPeriod = loReturn.Data;
                SelectedYear = int.TryParse(CurrentPeriod.CPERIOD_YEAR, out var loYear) ? loYear : DateTime.Now.Year;
                SelectedPeriodNo = CurrentPeriod.CPERIOD_MONTH;
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
                    await _model.GetAsync<TXB00200ListDTO<TXB00200PropertyDTO>>(
                        nameof(ITXB00200.TXB00200GetPropertyList));
                if (loReturn.Data.Any())
                {

                    PropertyList = loReturn.Data;
                    SelectedPropertyId = PropertyList[0].CPROPERTY_ID;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        //public async Task GetNextPeriod()
        //{
        //    var loEx = new R_Exception();
        //    try
        //    {
        //        var loReturn =
        //            await _model.GetAsync<TXB00200SingleDTO<TXB00200NextPeriodDTO>>(
        //                nameof(ITXB00200.TXB00200GetNextPeriod));
        //        NextPeriod = loReturn.Data;
        //        // SelectedPeriodNo = NextPeriod.CMONTH;
        //        // SelectedYear = int.TryParse(NextPeriod.CYEAR, out var loYear) ? loYear : DateTime.Now.Year;
        //    }
        //    catch (Exception ex)
        //    {
        //        loEx.Add(ex);
        //    }

        //    loEx.ThrowExceptionIfErrors();
        //}

        public async Task GetPeriodList()
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new TXB00200PeriodParam { CYEAR = SelectedYear.ToString() };

                var loReturn =
                    await _model.GetAsync<TXB00200ListDTO<TXB00200PeriodDTO>, TXB00200PeriodParam>(
                        nameof(ITXB00200.TXB00200GetPeriodList), loParam);
                PeriodList = loReturn.Data;
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
                var loParam = new TXB00200PeriodParam
                {
                    CYEAR = SelectedYear.ToString(),
                    CMONTH = Data.CPERIOD_MONTH
                };

                var loResult =
                    await _model.GetAsync<TXB00200SingleDTO<TXB00200PeriodParam>, TXB00200PeriodParam>(
                        nameof(ITXB00200.TXB00200UpdateSoftPeriod), loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        // public async Task ProcessSoftPeriod()
        // {
        //     var loEx = new R_Exception();
        //     try
        //     {
        //         var loParam = new TXB00200SoftCloseParam()
        //         {
        //             CPROPERTY_ID = SelectedPropertyId,
        //             CPERIOD_YEAR = SelectedYear.ToString(),
        //             CPERIOD_MONTH = SelectedPeriodNo
        //         };
        //         
        //         var loResult =
        //             await _model.GetAsync<TXB00200ListDTO<TXB00200SoftCloseParam>, TXB00200SoftCloseParam>(
        //                 nameof(ITXB00200.TXB00200ProcessSoftClose), loParam);
        //     }
        //     catch (Exception ex)
        //     {
        //         loEx.Add(ex);
        //     }
        //
        //     loEx.ThrowExceptionIfErrors();
        // }

        public async Task ProcessSoftClosePeriod()
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(TXB00200ContextConstant.CPROPERTY_ID, SelectedPropertyId);
                R_FrontContext.R_SetStreamingContext(TXB00200ContextConstant.CPERIOD_YEAR, SelectedYear.ToString());
                R_FrontContext.R_SetStreamingContext(TXB00200ContextConstant.CPERIOD_MONTH, SelectedPeriodNo);


                var loResult =
                    await _model.GetAsync<TXB00200ListDTO<TXB00200SoftClosePeriodToDoListDTO>>(
                        nameof(ITXB00200.TXB00200SoftClosePeriodStream));
                SoftClosePeriodErrorList = loResult.Data;

                if (SoftClosePeriodErrorList.Count > 0)
                {
                    //ubah CREF_DATE ke DREF_DATE
                    foreach (var loItem in SoftClosePeriodErrorList)
                    {
                        loItem.DREF_DATE = DateTime.TryParseExact(loItem.CREF_DATE, "yyyyMMdd",
                            CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var refDate)
                            ? refDate
                            : (DateTime?)null;
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void SetExcelDataSetToDoList()
        {
            var loConvertData = SoftClosePeriodErrorList.Select(item => new TXB00200SoftClosePeriodExcelDTO()
            {
                No = item.ISEQ_NO,
                Dept = item.CDEPT_NAME,
                TransactionType = item.CTRANS_DESCR,
                Module = item.CMODULE,
                ReferenceNo = item.CREF_NO,
                ReferenceDate = item.CREF_DATE,
                Status = item.CTRANS_STATUS_DESCR,
                Description = item.CDESCRIPTION,
                Solution = item.CSOLUTION_DESCR
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

        public void ValidateHasError(List<TXB00200SoftClosePeriodToDoListDTO> poParam)
        {
            SoftClosePeriodErrorList = poParam;
            SoftClosePeriodToDoList = new ObservableCollection<TXB00200SoftClosePeriodToDoListDTO>(poParam);

            SetExcelDataSetToDoList();
        }

    }
}