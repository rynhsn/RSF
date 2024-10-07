using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APR00500Common;
using APR00500Common.DTOs;
using APR00500Common.DTOs.Print;
using APR00500Common.Params;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;

namespace APR00500Model.ViewModel
{
    public class APR00500ViewModel : R_ViewModel<APR00500DataResultDTO>
    {
        private APR00500Model _model = new APR00500Model();
        public List<APR00500PropertyDTO> PropertyList = new List<APR00500PropertyDTO>();
        public APR00500ReportParam ReportParam = new APR00500ReportParam();

        public APR00500PeriodYearRangeDTO YearRange = new APR00500PeriodYearRangeDTO();
        public APR00500SystemParamDTO SystemParam = new APR00500SystemParamDTO();
        public APR00500TransCodeInfoDTO TransCodeInfo = new APR00500TransCodeInfoDTO();

        public List<APR00500PeriodDTO> PeriodListFrom = new List<APR00500PeriodDTO>();
        public List<APR00500PeriodDTO> PeriodListTo = new List<APR00500PeriodDTO>();
        // public List<APR00500FunctDTO> CodeInfoList = new List<APR00500FunctDTO>();

        // public string CodeInfoId;

        public bool CheckDept = false;
        public bool CheckRefDate = false;
        public bool CheckDueDate = false;
        public bool CheckSupplier = false;
        public bool CheckRefNo = false;
        public bool CheckCurrency = false;
        public bool CheckTotalAmt = false;
        public bool CheckRemainingAmt = false;
        public bool CheckDaysLate = false;

        public List<string> FileType = new List<string> { "XLSX", "XLS", "CSV" };

        public async Task Init()
        {
            await GetPropertyList();
            await GetYearRange();
            await GetSystemParam();
            await GetTransCodeInfo();
            await GetPeriodList();
            // await GetCodeInfoList();

            ReportParam.DCUT_OFF_DATE = DateTime.Now;
            ReportParam.IFROM_PERIOD_YY = int.Parse(DateTime.Now.Year.ToString());
            ReportParam.CFROM_PERIOD_MM = PeriodListFrom[0].CPERIOD_NO;
            ReportParam.ITO_PERIOD_YY = int.Parse(DateTime.Now.Year.ToString());
            ReportParam.CTO_PERIOD_MM = PeriodListTo[0].CPERIOD_NO;
            ReportParam.DFROM_REFERENCE_DATE = DateTime.Now;
            ReportParam.DTO_REFERENCE_DATE = DateTime.Now;
            ReportParam.DFROM_DUE_DATE = DateTime.Now;
            ReportParam.DTO_DUE_DATE = DateTime.Now;
        }

        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<APR00500ListDTO<APR00500PropertyDTO>>(
                        nameof(IAPR00500.APR00500GetPropertyList));
                PropertyList = loReturn.Data;
                ReportParam.CPROPERTY_ID =
                    PropertyList.Count > 0 ? PropertyList[0].CPROPERTY_ID : ReportParam.CPROPERTY_ID;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetYearRange()
        {
            var loEx = new R_Exception();
            try
            {
                var loReturn =
                    await _model.GetAsync<APR00500SingleDTO<APR00500PeriodYearRangeDTO>>(
                        nameof(IAPR00500.APR00500GetYearRange));
                YearRange = loReturn.Data;
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
                var loResult =
                    await _model.GetAsync<APR00500SingleDTO<APR00500SystemParamDTO>>(
                        nameof(IAPR00500.APR00500GetSystemParam));
                SystemParam = loResult.Data;
                SystemParam.ISOFT_PERIOD_YY = int.Parse(SystemParam.CSOFT_PERIOD_YY);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetTransCodeInfo()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult =
                    await _model.GetAsync<APR00500SingleDTO<APR00500TransCodeInfoDTO>>(
                        nameof(IAPR00500.APR00500GetTransCodeInfo));
                TransCodeInfo = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPeriodList()
        {
            var loEx = new R_Exception();
            try
            {
                var lcYear = DateTime.Now.Year.ToString();
                var loParam = new APR00500PeriodParam() { CYEAR = lcYear };
                var loResult =
                    await _model.GetAsync<APR00500ListDTO<APR00500PeriodDTO>, APR00500PeriodParam>(
                        nameof(IAPR00500.APR00500GetPeriodList), loParam);
                PeriodListFrom = loResult.Data;
                PeriodListTo = loResult.Data;
                ReportParam.CFROM_PERIOD_MM = PeriodListFrom[0].CPERIOD_NO;
                ReportParam.CTO_PERIOD_MM = PeriodListTo[0].CPERIOD_NO;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region GetCodeInfoList

        // public async Task GetCodeInfoList()
        // {
        //     var loEx = new R_Exception();
        //     try
        //     {
        //         var loReturn =
        //             await _model.GetAsync<APR00500ListDTO<APR00500FunctDTO>>(nameof(IAPR00500
        //                 .APR00500GetCodeInfoList));
        //         CodeInfoList = loReturn.Data;
        //         CodeInfoId = CodeInfoList.FirstOrDefault()?.CCODE;
        //     }
        //     catch (Exception ex)
        //     {
        //         loEx.Add(ex);
        //     }
        //
        //     loEx.ThrowExceptionIfErrors();
        // }

        #endregion
    }
}