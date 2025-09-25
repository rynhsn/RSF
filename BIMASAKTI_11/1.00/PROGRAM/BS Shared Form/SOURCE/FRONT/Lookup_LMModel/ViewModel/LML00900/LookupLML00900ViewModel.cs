using Lookup_PMCOMMON.DTOs;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Linq;
using R_BlazorFrontEnd.Helpers;
using Lookup_PMFrontResources;

namespace Lookup_PMModel.ViewModel.LML00900
{
    public class LookupLML00900ViewModel
    {
        private PublicLookupLMModel _model = new PublicLookupLMModel();
        private PublicLookupLMGetRecordModel _modelGetRecord = new PublicLookupLMGetRecordModel();
        public ObservableCollection<LML00900FrontDTO> TransactionList = new ObservableCollection<LML00900FrontDTO>();
        public LML00900InitialProcessDTO _GetInitialProcess = new LML00900InitialProcessDTO();
        
        public LML00900ParameterDTO _Parameter = new LML00900ParameterDTO();

        public List<MonthDTO>? GetPeriodMonth { get; set; }
        public int PeriodYear = DateTime.Now.Year;
        public string PeriodMonth = DateTime.Now.Month.ToString("D2");
        public List<PeriodDTO> Period { get; set; } = new List<PeriodDTO>
            { new PeriodDTO { CCODE = "A", CNAME = "All" },
              new PeriodDTO { CCODE = "P", CNAME = "For Period" } };
        public string PeriodValue = "A";
        string lcPeriodParam = "";
        public bool _btnOk = false;
        public bool _enableYearMonthField = false;
        public async Task GetInitialProcess()
        {
            R_Exception loException = new R_Exception();
            try
            {
                var loResult = await _model.GetInitialProcessAsyncModel();
                _GetInitialProcess = loResult;
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
        }
        public async Task GetTransactionList()
        {
            var loEx = new R_Exception();

            try
            {
                string? lcPeriodParam = "";
                if (PeriodValue == "P")
                {
                    lcPeriodParam = PeriodYear.ToString() + PeriodMonth;
                };
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPROPERTY_ID, _Parameter.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CDEPT_CODE, _Parameter.CDEPT_CODE ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTRANS_CODE, _Parameter.CTRANS_CODE ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CTENANT_ID, _Parameter.CTENANT_ID ?? "");
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CPERIOD, lcPeriodParam);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LHAS_REMAINING, _Parameter.LHAS_REMAINING);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.LNO_REMAINING, _Parameter.LNO_REMAINING);
                R_FrontContext.R_SetStreamingContext(ContextConstantPublicLookup.CCURRENCY_CODE, _Parameter.CCURRENCY_CODE);

                var loResult = await _model.LML00900GetTransactionListAsync();

                if (loResult.Data.Count() > 0)
                {
                    _btnOk = true;
                    List<LML00900FrontDTO> tempTransactionist = new List<LML00900FrontDTO>();

                    foreach (var item in loResult.Data!)
                    {
                        tempTransactionist.Add(ConvertToEntityFrontList(item));
                    }
                    TransactionList = new ObservableCollection<LML00900FrontDTO>(tempTransactionist);
                }
                else
                {
                    _btnOk = false;
                    TransactionList.Clear();
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
        public async Task<LML00900DTO> GetTransaction(LML00900ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LML00900DTO loRtn = null;
            try
            {
                var loResult = await _modelGetRecord.LML00900GetTransactionAsync(poParam);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            return loRtn!;
        }

        #region Utility
        public void ValidationFieldEmpty()
        {
            var loEx = new R_Exception();
            try
            {
                string lcPeriodYear = PeriodYear.ToString();
                if (PeriodValue == "P" && string.IsNullOrWhiteSpace(lcPeriodYear))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "00901");
                    loEx.Add(loErr);
                }
                if (PeriodValue == "P" && string.IsNullOrWhiteSpace(PeriodMonth))
                {
                    var loErr = R_FrontUtility.R_GetError(typeof(Resources_Dummy_Class_LookupPM), "00902");
                    loEx.Add(loErr);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            if (loEx.HasError)
            {
                loEx.ThrowExceptionIfErrors();
            }
        }
        public void GetMonth()
        {
            GetPeriodMonth = new List<MonthDTO>();

            for (int i = 1; i <= 12; i++)
            {
                string monthId = i.ToString("D2");
                MonthDTO month = new MonthDTO { Id = monthId };
                GetPeriodMonth.Add(month);
            }

        }
        public LML00900FrontDTO ConvertToEntityFrontList(LML00900DTO poEntity)
        {
            R_Exception loException = new R_Exception();
            LML00900FrontDTO? loReturn = null;
            try
            {
                loReturn = R_FrontUtility.ConvertObjectToObject<LML00900FrontDTO>(poEntity);
                loReturn.DREF_DATE = ConvertStringToDateTimeFormat(poEntity.CREF_DATE!);
                loReturn.DDOC_DATE = ConvertStringToDateTimeFormat(poEntity.CDOC_DATE!);
                loReturn.DDUE_DATE = ConvertStringToDateTimeFormat(poEntity.CDUE_DATE!);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            return loReturn!;
        }
        private DateTime? ConvertStringToDateTimeFormat(string pcEntity)
        {
            if (string.IsNullOrWhiteSpace(pcEntity))
            {
                // Jika string kosong atau null, kembalikan DateTime.MinValue atau nilai default yang sesuai
                return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
            }
            // Parse string ke DateTime
            DateTime result;
            if (DateTime.TryParseExact(pcEntity, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }

            // Jika parsing gagal, kembalikan DateTime.MinValue atau nilai default yang sesuai
            return null; // atau DateTime.MinValue atau DateTime.Now atau nilai default yang sesuai dengan kebutuhan Anda
        }

        private string ConvertDateTimeToStringFormat(DateTime? ptEntity)
        {
            if (ptEntity == DateTime.MinValue)
            {
                // Jika DateTime adalah DateTime.MinValue, kembalikan string kosong
                return ""; // atau null, tergantung pada kebutuhan Anda
            }

            // Format DateTime ke string "yyyyMMdd"
            return ptEntity?.ToString("yyyyMMdd")!;
        }
        #endregion
    }
}
