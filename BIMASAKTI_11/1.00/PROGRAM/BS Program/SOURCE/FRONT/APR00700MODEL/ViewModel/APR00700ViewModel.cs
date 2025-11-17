using APR00700COMMON;
using APR00700COMMON.DTO_s;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using APR00700COMMON.DTOs;

namespace APR00700MODEL.ViewModel
{
    public class APR00700ViewModel
    {
        private APR00700Model _APR00700model = new APR00700Model();
        public APR00700SystemParamDTO SystemParam = new APR00700SystemParamDTO();

        public ObservableCollection<PropertyDTO> _properties = new ObservableCollection<PropertyDTO>();
        public ObservableCollection<PeriodDtDTO> _fromPeriods = new ObservableCollection<PeriodDtDTO>();
        public ObservableCollection<PeriodDtDTO> _toPeriods = new ObservableCollection<PeriodDtDTO>();
        public APR00700SPParamDTO _ReportParam = new APR00700SPParamDTO();

        public DateTime _InitToday = new DateTime();
        public PeriodYearDTO _PeriodYear = new PeriodYearDTO();

        public int _YearFromPeriod = 0;
        public int _YearToPeriod = 0;
        public string _MonthFromPeriod = "";
        public string _MonthToPeriod = "";

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> TypeList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("L", R_FrontUtility.R_GetMessage(typeof(APR00700FrontResources.Resources_Dummy_Class), "_local_cur")),
            new KeyValuePair<string, string>("B", R_FrontUtility.R_GetMessage(typeof(APR00700FrontResources.Resources_Dummy_Class), "_base_cur")),
        };
        #endregion

        public async Task GetPropertyAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _APR00700model.GetPropertyListAsync();

                _properties = new ObservableCollection<PropertyDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<List<PeriodDtDTO>> GetPeriodDtAsync(string pcYear)
        {
            R_Exception loEx = new R_Exception();
            List<PeriodDtDTO> loRtn = null;
            try
            {
                R_FrontContext.R_SetStreamingContext(APR00700ContextConstant.CYEAR, pcYear);
                var loResult = await _APR00700model.GetPeriodDtListAsync();
                loRtn = loResult.ToList();
                loRtn.ForEach(x => x.CPERIOD_NAME_DISPLAY = x.CPERIOD_NO);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;

        }

        public async Task GetPeriodYearAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loRtn = await _APR00700model.GetPeriodYearRecordAsync(new PeriodYearDTO() { CMODE = "", CYEAR = "" });
                _PeriodYear = loRtn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetSystemParam(string pcPropertyId)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new APR00700SystemParamDTO()
                {
                    CPROPERTY_ID = pcPropertyId
                };

                var loResult = await _APR00700model.APR00700GetSystemParam(loParam);
                SystemParam = loResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
