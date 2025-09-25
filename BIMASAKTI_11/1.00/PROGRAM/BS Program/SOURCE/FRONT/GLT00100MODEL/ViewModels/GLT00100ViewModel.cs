using GLT00100COMMON;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSModel;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GLT00100MODEL
{
    public class GLT00100ViewModel : R_ViewModel<GLT00100DTO>
    {
        #region Model
        private GLT00100UniversalModel _GLT00100UniversalModel = new GLT00100UniversalModel();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();
        private GLT00100Model _GLT00100Model = new GLT00100Model();
        private GLT00110Model _GLT00110Model = new GLT00110Model();

        #endregion

        #region Initial Data
        public GLT00100GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new GLT00100GLSystemParamDTO();
        public GLT00100GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new GLT00100GSTransInfoDTO();
        public GLT00100GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; } = new GLT00100GLSystemEnableOptionInfoDTO();
        public GLT00100GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; } = new GLT00100GSPeriodYearRangeDTO();
        public List<GLT00100GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; } = new List<GLT00100GSGSBCodeDTO>();
        public List<GSL00700DTO> VAR_DEPARTEMENT_LIST { get; set; } = new List<GSL00700DTO>();
        #endregion

        #region Public Property ViewModel
        public int JournalPeriodYear { get; set; }
        public string JournalPeriodMonth { get; set; }
        public GLT00100ParamDTO JournalParam { get; set; } = new GLT00100ParamDTO();
        public ObservableCollection<GLT00100DTO> JournalGrid { get; set; } = new ObservableCollection<GLT00100DTO>();
        public ObservableCollection<GLT00101DTO> JournalDetailGrid { get; set; } = new ObservableCollection<GLT00101DTO>();
        public GLT00100DTO Journal { get; set; } = new GLT00100DTO();

        #endregion

        #region ComboBox ViewModel
        public List<KeyValuePair<string, string>> PeriodMonthList { get; } = Enumerable.Range(1, 12).Select(i => i.ToString("D2")).Select(s => new KeyValuePair<string, string>(s, s)).ToList();
        #endregion
        public async Task GetAllUniversalData()
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                var loResult = await _GLT00100UniversalModel.GetTabJournalListUniversalVarAsync();

                //Set Universal Data
                VAR_GL_SYSTEM_PARAM = loResult.VAR_GL_SYSTEM_PARAM;
                VAR_GSM_TRANSACTION_CODE = loResult.VAR_GSM_TRANSACTION_CODE;
                VAR_IUNDO_COMMIT_JRN = loResult.VAR_IUNDO_COMMIT_JRN;
                VAR_GSM_PERIOD = loResult.VAR_GSM_PERIOD;
                VAR_GSB_CODE_LIST = loResult.VAR_GSB_CODE_LIST;

                //Add all data 
                VAR_GSB_CODE_LIST.Add(new GLT00100GSGSBCodeDTO { CCODE = "", CNAME = "ALL" });

                //Get And Set List Dept Code
                var loDeptResult = await _PublicLookupModel.GSL00700GetDepartmentListAsync(new GSL00700ParameterDTO());
                VAR_DEPARTEMENT_LIST = loDeptResult;

                //Set Dept Code
                JournalParam.CDEPT_CODE = VAR_DEPARTEMENT_LIST.Any(loDeptList => loDeptList.CDEPT_CODE == VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_CODE) ? VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_CODE : "";
                JournalParam.CDEPT_NAME = VAR_DEPARTEMENT_LIST.Any(loDeptList => loDeptList.CDEPT_NAME == VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_NAME) ? VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_NAME : "";

                //Set Journal Period
                JournalPeriodYear = int.Parse(loResult.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD_YY);
                JournalPeriodMonth = loResult.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD_MM;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournal(GLT00100DTO poParam)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                var loResult = await _GLT00110Model.GetJournalRecordAsync(new GLT00110DTO() { CREC_ID = poParam.CREC_ID });
                Journal = R_FrontUtility.ConvertObjectToObject<GLT00100DTO>(loResult);
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
                JournalParam.CPERIOD = JournalPeriodYear + JournalPeriodMonth;
                var loResult = await _GLT00100Model.GetJournalListAsync(JournalParam);

                JournalGrid = new ObservableCollection<GLT00100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetJournalDetailList(GLT00101DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResultTemp = await _GLT00100Model.GetJournalDetailListAsync(poEntity);
                var loResult = MapCDateToDDate(loResultTemp, "CDOCUMENT_DATE", "DDOCUMENT_DATE");

                JournalDetailGrid = new ObservableCollection<GLT00101DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public List<T> MapCDateToDDate<T>(List<T> items, string cDateColumnName, string dDateColumnName)
        {
            return items.Select(item =>
            {
                var cDateProp = typeof(T).GetProperty(cDateColumnName);
                var dDateProp = typeof(T).GetProperty(dDateColumnName);

                if (cDateProp != null && dDateProp != null)
                {
                    var cDateValue = (string)cDateProp.GetValue(item);
                    if (!string.IsNullOrWhiteSpace(cDateValue) && cDateValue.Length >= 8)
                    {
                        var dDateValue = DateTime.ParseExact(cDateValue, "yyyyMMdd", CultureInfo.InvariantCulture);
                        dDateProp.SetValue(item, dDateValue);
                    }
                }
                return item;
            }).ToList();
        }


        public async Task UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _GLT00100Model.UpdateJournalStatusAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task<bool> ValidationRapidApproval(GLT00100RapidApprovalValidationDTO poEntity)
        {
            var loEx = new R_Exception();
            bool loRtn = false;

            try
            {
                var loResult = await _GLT00100Model.ValidationRapidApprovalAsync(poEntity);
                if (loResult != null)
                {
                    loRtn = string.IsNullOrWhiteSpace(loResult.CRESULT);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
    }
}
