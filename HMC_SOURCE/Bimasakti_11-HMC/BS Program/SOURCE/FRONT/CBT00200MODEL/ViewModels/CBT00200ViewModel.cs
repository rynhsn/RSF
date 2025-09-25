using CBT00200COMMON;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CBT00200MODEL
{
    public class CBT00200ViewModel : R_ViewModel<CBT00200DTO>
    {
        #region Model
        private CBT00200InitialProcessModel _CBT00200InitialProcessModel = new CBT00200InitialProcessModel();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();
        private CBT00200Model _CBT00200Model = new CBT00200Model();
        private CBT00210Model _CBT00210Model = new CBT00210Model();
        #endregion

        #region Initial Data
        public CBT00200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new CBT00200GLSystemParamDTO();
        public CBT00200GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; } = new CBT00200GSPeriodYearRangeDTO();
        public CBT00200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; } = new CBT00200CBSystemParamDTO();
        public CBT00200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new CBT00200GSTransInfoDTO();
        public List<CBT00200GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; } = new List<CBT00200GSGSBCodeDTO>();
        public List<GSL00700DTO> VAR_DEPT_CODE_LIST { get; set; } = new List<GSL00700DTO>();
        #endregion

        #region Public Property ViewModel
        public string SearchText { get; set; } = "";
        public int JournalPeriodYear { get; set; }
        public string JournalPeriodMonth { get; set; }
        public CBT00200ParamDTO JornalParam { get; set; } = new CBT00200ParamDTO();
        public ObservableCollection<CBT00200DTO> JournalGrid { get; set; } = new ObservableCollection<CBT00200DTO>();
        public ObservableCollection<CBT00210DTO> JournalDetailGrid { get; set; } = new ObservableCollection<CBT00210DTO>();
        #endregion

        #region ComboBox ViewModel
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
        public async Task GetAllUniversalData()
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                var loResult = await _CBT00200InitialProcessModel.GetTabJournalListInitialProcessAsync();

                //Set Universal Data
                VAR_CB_SYSTEM_PARAM = loResult.VAR_CB_SYSTEM_PARAM;
                VAR_GL_SYSTEM_PARAM = loResult.VAR_GL_SYSTEM_PARAM;
                VAR_GSM_TRANSACTION_CODE = loResult.VAR_GSM_TRANSACTION_CODE;
                VAR_GSM_PERIOD = loResult.VAR_GSM_PERIOD;

                var loGSBListResult = await _CBT00200InitialProcessModel.GetGSBCodeListAsync();
                VAR_GSB_CODE_LIST = loGSBListResult;
                VAR_GSB_CODE_LIST.Add(new CBT00200GSGSBCodeDTO { CCODE="", CNAME="ALL" });

                //Get And Set List Dept Code
                VAR_DEPT_CODE_LIST = await _PublicLookupModel.GSL00700GetDepartmentListAsync(new GSL00700ParameterDTO());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetJournalList()
        {
            var loEx = new R_Exception();

            try
            {
                JornalParam.CPERIOD = JournalPeriodYear + JournalPeriodMonth;
                var loResult = await _CBT00200Model.GetJournalListAsync(JornalParam);
                loResult.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
                    {
                        x.DREF_DATE = ldDocDate;
                    }
                });

                JournalGrid = new ObservableCollection<CBT00200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalDetailList(CBT00210DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _CBT00210Model.GetJournalDetailListAsync(poEntity);
                loResult.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CDOCUMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldDocDate))
                    {
                        x.DDOCUMENT_DATE = ldDocDate;
                    }
                });

                JournalDetailGrid = new ObservableCollection<CBT00210DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task UpdateJournalStatus(CBT00200UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                 await _CBT00200Model.UpdateJournalStatusAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
    }
}
