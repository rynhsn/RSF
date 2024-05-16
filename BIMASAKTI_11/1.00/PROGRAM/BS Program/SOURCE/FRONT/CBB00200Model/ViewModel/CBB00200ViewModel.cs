using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CBB00200Common;
using CBB00200Common.DTOs;
using CBB00200Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace CBB00200Model.ViewModel
{
    public class CBB00200ViewModel : R_ViewModel<CBB00200ClosePeriodResultDTO>
    {
        // private CBB00200InitModel _initModel = new CBB00200InitModel();
        private CBB00200Model _model = new CBB00200Model();

        // public CBB00200ClosePeriodResultDTO ClosePeriodResult { get; set; }
        public CBB00200SystemParamDTO SystemParam = new CBB00200SystemParamDTO();

        public ObservableCollection<CBB00200ClosePeriodToDoListDTO> ClosePeriodToDoList =
            new ObservableCollection<CBB00200ClosePeriodToDoListDTO>();

        public DataSet ExcelDataSetToDoList { get; set; }

        public async Task GetSystemParam()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult =
                    await _model.GetAsync<CBB00200SingleDTO<CBB00200SystemParamDTO>>(
                        nameof(ICBB00200.CBB00200GetSystemParam));
                SystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<int> ClosePeriod(string pcPeriod)
        {
            var loEx = new R_Exception();
            var loReturn = 0;
            try
            {
                var loParam = new CBB00200ClosePeriodParam { CPERIOD = pcPeriod };
                var loResult =
                    await _model.GetAsync<CBB00200SingleDTO<CBB00200ClosePeriodResultDTO>, CBB00200ClosePeriodParam>(
                        nameof(ICBB00200.CBB00200ClosePeriod), loParam);
                loReturn = loResult.Data.IERROR_COUNT;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loReturn;
        }

        public async Task GetClosePeriodToDoList(string pcPeriod)
        {
            var loEx = new R_Exception();
            try
            {
                R_FrontContext.R_SetStreamingContext(CBB00200ContextConstant.CPERIOD, pcPeriod);
                var loResult =
                    await _model.GetListStreamAsync<CBB00200ClosePeriodToDoListDTO>(
                        nameof(ICBB00200.CBB00200ClosePeriodToDoListStream));
                ClosePeriodToDoList = new ObservableCollection<CBB00200ClosePeriodToDoListDTO>(loResult);

                SetExcelDataSetToDoList();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void SetExcelDataSetToDoList()
        {
            var loConvertData = ClosePeriodToDoList.Select(item => new CBB00200ExcelToDoListDTO()
            {
                No = item.INO,
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