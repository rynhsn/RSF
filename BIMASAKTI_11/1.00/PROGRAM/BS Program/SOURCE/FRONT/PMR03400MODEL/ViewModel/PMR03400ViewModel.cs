using PMR03400COMMON;
using PMR03400COMMON.DTO_s;
using PMR03400FrontResources;
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

using PMR03400COMMON.DTOs;

namespace PMR03400MODEL.ViewModel
{
    public class PMR03400ViewModel
    {
        private PMR03400Model _PMR03400model = new PMR03400Model();
        public PMR03400SystemParamDTO SystemParam = new PMR03400SystemParamDTO();

        public ObservableCollection<PropertyDTO> _properties = new ObservableCollection<PropertyDTO>();
        public ObservableCollection<PeriodDtDTO> _fromPeriods = new ObservableCollection<PeriodDtDTO>();
        public ObservableCollection<PeriodDtDTO> _toPeriods = new ObservableCollection<PeriodDtDTO>();
        public PMR03400SPParamDTO _ReportParam = new PMR03400SPParamDTO();

        public DateTime _InitToday = new DateTime();
        public PeriodYearDTO _PeriodYear = new PeriodYearDTO();

        public int _YearFromPeriod = 0;
        public int _YearToPeriod = 0;
        public string _MonthFromPeriod = "";
        public string _MonthToPeriod = "";

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> TypeList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("L", R_FrontUtility.R_GetMessage(typeof(PMR03400FrontResources.Resources_Dummy_Class), "_local_cur")),
            new KeyValuePair<string, string>("B", R_FrontUtility.R_GetMessage(typeof(PMR03400FrontResources.Resources_Dummy_Class), "_base_cur")),
        };
        #endregion

        public async Task GetPropertyAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _PMR03400model.GetPropertyListAsync();

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
                R_FrontContext.R_SetStreamingContext(PMR03400ContextConstant.CYEAR, pcYear);
                var loResult = await _PMR03400model.GetPeriodDtListAsync();
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

        public async Task<PeriodYearDTO> GetPeriodYearAsync()
        {
            R_Exception loEx = new R_Exception();
            PeriodYearDTO loRtn = null;
            try
            {
                loRtn = await _PMR03400model.GetPeriodYearRecordAsync(new PeriodYearDTO() { CMODE = "", CYEAR = "" });
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task GetSystemParam(string pcPropertyId)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = new PMR03400SystemParamDTO()
                {
                    CPROPERTY_ID = pcPropertyId
                };

                var loResult = await _PMR03400model.PMR03400GetSystemParam(loParam);
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
