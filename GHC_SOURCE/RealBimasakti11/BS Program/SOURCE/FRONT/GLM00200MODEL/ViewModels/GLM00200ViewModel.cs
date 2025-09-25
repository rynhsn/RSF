
using GLM00200COMMON;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using Lookup_GSModel.ViewModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GLM00200MODEL
{
    public class GLM00200ViewModel : R_ViewModel<JournalParamDTO>
    {
        private PublicLookupModel _lookup = new PublicLookupModel();

        private GLM00200MODEL _recurringJrnModel = new GLM00200MODEL();

        private GLM00200InitModel _recurringJrnHelperModel = new GLM00200InitModel();

        private PublicLookupModel _lookupGSModel = new PublicLookupModel();

        public RecurringJournalListParamDTO RecurringJrnListSearchParam { get; set; } = new RecurringJournalListParamDTO();

        public ObservableCollection<JournalDTO> RecurringJrnList { get; set; } = new ObservableCollection<JournalDTO>();

        public ObservableCollection<JournalDetailGridDTO> RecurringJrnDtList { get; set; } = new ObservableCollection<JournalDetailGridDTO>();

        public JournalDTO RecurringJrnRecord { get; set; } = new JournalDTO();

        public AllInitRecordDTO InitDataRecord { get; set; } = new AllInitRecordDTO();

        public List<StatusDTO> StatusList { get; set; } = new List<StatusDTO>();

        public List<CurrencyDTO> CurrencyList { get; set; } = new List<CurrencyDTO>();

        public List<KeyValuePair<string, string>> PeriodMonthList { get; set; }

        public int MaxYear { get; set; } = 0;
        public int MinYear { get; set; } = 0;

        public async Task GetInitData()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                //get & generate init data 
                PeriodMonthList = Enumerable.Range(1, 12).Select(i => i.ToString("D2")).Select(s => new KeyValuePair<string, string>(s, s)).ToList();
                InitDataRecord = await _recurringJrnHelperModel.GetAllInitRecordAsync();

                MaxYear = InitDataRecord.PERIOD_YEAR.IMAX_YEAR;
                MinYear = InitDataRecord.PERIOD_YEAR.IMIN_YEAR;
                RecurringJrnListSearchParam.CPERIOD_YYYY = int.TryParse(InitDataRecord.GL_SYSTEM_PARAM.CSOFT_PERIOD_YY, out var year) ? year : 0;
                RecurringJrnListSearchParam.CPERIOD_MM = InitDataRecord.GL_SYSTEM_PARAM.CSTART_PERIOD_MM ?? "";
                Data.CCURRENCY_CODE = CurrencyList.FirstOrDefault(x => x.CCURRENCY_CODE == InitDataRecord.COMPANY_INFO.CLOCAL_CURRENCY_CODE)?.CCURRENCY_CODE ?? "";
                RecurringJrnListSearchParam.CSEARCH_TEXT = "";
                Data.DSTART_DATE = InitDataRecord.DTODAY != default ? InitDataRecord.DTODAY : DateTime.Now;

                StatusList = await _recurringJrnHelperModel.GetListStatusAsync();
                StatusList.Insert(0, new StatusDTO { CCODE = "", CNAME = "All" });
                RecurringJrnListSearchParam.CSTATUS = StatusList[0].CCODE ?? "";
                var loCurrencies = await _lookupGSModel.GSL00300GetCurrencyListAsync();
                CurrencyList = R_FrontUtility.ConvertCollectionToCollection<CurrencyDTO>(loCurrencies).ToList();
                CurrencyList.Insert(0, new CurrencyDTO { CCURRENCY_CODE = "", CCURRENCY_NAME = "" });

                var loLookupDept = await _lookupGSModel.GSL00700GetDepartmentListAsync(new GSL00700ParameterDTO());
                RecurringJrnListSearchParam.CDEPT_CODE = loLookupDept[0].CDEPT_CODE ?? "";
                RecurringJrnListSearchParam.CDEPT_NAME = loLookupDept[0].CDEPT_NAME ?? "";
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ShowAllJournals()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                RecurringJrnListSearchParam.CPERIOD_YYYYMM = RecurringJrnListSearchParam.CPERIOD_YYYY + RecurringJrnListSearchParam.CPERIOD_MM;
                var loResult = await _recurringJrnModel.GetRecurringJrnListAsync(RecurringJrnListSearchParam);

                foreach (var loItem in loResult)
                {
                    if (DateTime.TryParseExact(loItem.CSTART_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime pdParsedStartDate))
                    {
                        loItem.DSTART_DATE = pdParsedStartDate;
                    }

                    if (DateTime.TryParseExact(loItem.CNEXT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime pdParsedNextDate))
                    {
                        loItem.DNEXT_DATE = pdParsedNextDate;
                    }

                }
                RecurringJrnList = new ObservableCollection<JournalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task ShowAllJournalDetail(JournalDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _recurringJrnModel.GetRecurringJrnDtListAsync(poParam);

                RecurringJrnDtList = new ObservableCollection<JournalDetailGridDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        public async Task<UploadByte> DownloadTemplate()
        {
            var loEx = new R_Exception();
            UploadByte loResult = null;

            try
            {
                loResult = await _recurringJrnModel.DownloadTemplateAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public async Task UpdateJournalStatusAsync(GLM00200UpdateStatusDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _recurringJrnModel.UpdateJrnStatusAsync(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

    }
}
