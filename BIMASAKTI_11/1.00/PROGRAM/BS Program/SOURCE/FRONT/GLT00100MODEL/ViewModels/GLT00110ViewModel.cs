using GLT00100COMMON;
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

namespace GLT00100MODEL
{
    public class GLT00110ViewModel : R_ViewModel<GLT00110DTO>
    {
        #region Model
        private GLT00100UniversalModel _GLT00100UniversalModel = new GLT00100UniversalModel();
        private GLT00100Model _GLT00100Model = new GLT00100Model();
        private GLT00110Model _GLT00110Model = new GLT00110Model();
        public string _currentSelectedAccountType="";
        #endregion

        #region Initial Data
        public GLT00100GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new GLT00100GSTransInfoDTO();
        public GLT00100GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new GLT00100GLSystemParamDTO();
        public GLT00100GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; } = new GLT00100GLSystemEnableOptionInfoDTO();
        public GLT00100TodayDateDTO VAR_TODAY { get; set; } = new GLT00100TodayDateDTO();
        public GLT00100GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; } = new GLT00100GSCompanyInfoDTO();
        public List<GLT00100GSCurrencyDTO> VAR_CURRENCY_LIST { get; set; } = new List<GLT00100GSCurrencyDTO>();
        public List<GLT00100GSCenterDTO> VAR_CENTER_LIST { get; set; } = new List<GLT00100GSCenterDTO>();
        public GLT00100GSPeriodDTInfoDTO VAR_CCURRENT_PERIOD_START_DATE { get; set; } = new GLT00100GSPeriodDTInfoDTO();
        #endregion

        #region Public Property ViewModel

        public GLT01100FrontPredefinedParamDTO ExternalParam { get; set; } = new GLT01100FrontPredefinedParamDTO();
        public DateTime RefDate { get; set; }
        public DateTime? DocDate { get; set; }
        public GLT00110DTO Journal { get; set; } = new GLT00110DTO();
        public ObservableCollection<GLT00101DTO> JournalDetailGrid { get; set; } = new ObservableCollection<GLT00101DTO>();
        public ObservableCollection<GLT00101DTO> JournalDetailGridTemp { get; set; } = new ObservableCollection<GLT00101DTO>();
        #endregion

        public async Task GetAllUniversalData()
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                var loResult = await _GLT00100UniversalModel.GetTabJournalEntryUniversalVarAsync();

                //Set Universal Data
                VAR_GL_SYSTEM_PARAM = loResult.VAR_GL_SYSTEM_PARAM;
                VAR_GSM_TRANSACTION_CODE = loResult.VAR_GSM_TRANSACTION_CODE;
                VAR_TODAY = loResult.VAR_TODAY;
                VAR_GSM_COMPANY = loResult.VAR_GSM_COMPANY;
                VAR_CURRENCY_LIST = await _GLT00100UniversalModel.GetCurrencyListAsync();
                VAR_CENTER_LIST = await _GLT00100UniversalModel.GetCenterListAsync();
                VAR_CENTER_LIST.Add(new GLT00100GSCenterDTO
                {
                    CCENTER_CODE="",
                    CCENTER_NAME="",
                }); //to handle that if center reset, 
                VAR_IUNDO_COMMIT_JRN = loResult.VAR_IUNDO_COMMIT_JRN;

                var loParam = new GLT00100ParamGSPeriodDTInfoDTO() { CCYEAR = loResult.VAR_GL_SYSTEM_PARAM.CCURRENT_PERIOD_YY, CPERIOD_NO = loResult.VAR_GL_SYSTEM_PARAM.CCURRENT_PERIOD_MM };
                VAR_CCURRENT_PERIOD_START_DATE = await _GLT00100UniversalModel.GetGSPeriodDTInfoAsync(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Journal
        public async Task GetJournal(GLT00110DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _GLT00110Model.GetJournalRecordAsync(poEntity);
                RefDate = DateTime.ParseExact(loResult.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                DocDate = DateTime.ParseExact(loResult.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<GLT00110LastCurrencyRateDTO> GetLastCurrency(GLT00110LastCurrencyRateDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00110LastCurrencyRateDTO loRtn = null;
            try
            {
                poEntity.CRATE_DATE = RefDate.ToString("yyyyMMdd");
                poEntity.CRATETYPE_CODE = VAR_GL_SYSTEM_PARAM.CRATETYPE_CODE;
                var loResult = await _GLT00110Model.GetLastCurrencyAsync(poEntity);
                loRtn = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
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
        public async Task DeleteJournal(GLT00110DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<GLT00100UpdateStatusDTO>(poEntity);
                loData.LUNDO_COMMIT = false;
                loData.LAUTO_COMMIT = false;
                loData.CNEW_STATUS = "99";

                await UpdateJournalStatus(loData);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task SaveJournal(GLT00110HeaderDetailDTO poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poEntity.HeaderData.CACTION = "NEW";
                    poEntity.HeaderData.CREC_ID = "";
                    poEntity.HeaderData.CREF_NO = VAR_GSM_TRANSACTION_CODE.LINCREMENT_FLAG ? "" : poEntity.HeaderData.CREF_NO;
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poEntity.HeaderData.CACTION = "EDIT";
                }
                poEntity.HeaderData.CREF_DATE = RefDate.ToString("yyyyMMdd");
                poEntity.HeaderData.CDOC_DATE = DocDate.Value.ToString("yyyyMMdd");
                poEntity.HeaderData.CTRANS_CODE = ContextConstant.VAR_TRANS_CODE;

                //CR6
                poEntity.HeaderData.CSOURCE_TRANS_CODE = string.IsNullOrWhiteSpace(ExternalParam.PARAM_CALLER_TRANS_CODE) ? "" : ExternalParam.PARAM_CALLER_TRANS_CODE;
                poEntity.HeaderData.CSOURCE_REF_NO = string.IsNullOrWhiteSpace(ExternalParam.PARAM_CALLER_REF_NO) ? "" : ExternalParam.PARAM_CALLER_REF_NO;
                poEntity.HeaderData.CSOURCE_MODULE = string.IsNullOrWhiteSpace(ExternalParam.PARAM_CALLER_ID) ? "GL" : ExternalParam.PARAM_CALLER_ID.Substring(0, 2);

                //save journal
                var loResult = await _GLT00110Model.SaveJournalAsync(poEntity);

                Journal = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Detail Journal
        public async Task GetJournalDetailList(GLT00101DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResultTemp = await _GLT00100Model.GetJournalDetailListAsync(poEntity);
                var loResult = ConvertCDateToDDateHelper(loResultTemp, "CDOCUMENT_DATE", "DDOCUMENT_DATE");
                JournalDetailGrid = new ObservableCollection<GLT00101DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public void JournalDetailListAddDataFromExternal(GLT00101DTO poNewEntity)
        {
            var loEx = new R_Exception();

            try
            {
                JournalDetailGrid.Add(poNewEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        public List<T> ConvertCDateToDDateHelper<T>(List<T> items, string cDateColumnName, string dDateColumnName)
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
    }
}
