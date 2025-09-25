using CBT01200Common;
using CBT01200Common.DTOs;
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
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace CBT01200MODEL
{
    public class CBT01200ViewModel : R_ViewModel<CBT01200DTO>
    {
        #region Model
        private CBT01200InitModel _CBT01200InitModel = new CBT01200InitModel();
        private CBT01210ViewModel _CBT01210ViewModel = new CBT01210ViewModel();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();
        private CBT01200Model _CBT01200Model = new CBT01200Model();
        #endregion

        #region Initial Data
        public CBT01200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; } = new CBT01200GSCompanyInfoDTO();
        public CBT01200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new CBT01200GLSystemParamDTO();
        public CBT01200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; } = new CBT01200CBSystemParamDTO();
        public CBT01200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new CBT01200GSTransInfoDTO();
        public CBT01200GLSystemEnableOptionInfoDTO VAR_IUNDO_COMMIT_JRN { get; set; } = new CBT01200GLSystemEnableOptionInfoDTO();
        public CBT01200GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; } = new CBT01200GSPeriodYearRangeDTO();
        public List<CBT01200GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; } = new List<CBT01200GSGSBCodeDTO>();
        public List<GSL00700DTO> VAR_DEPARTEMENT_LIST { get; set; } = new List<GSL00700DTO>();
        #endregion
        
        #region property
        public DateTime
            Drefdate = DateTime.Now,
            Ddocdate = DateTime.Now;

        public int ProgressBarPercentage = 0;
        public string
            ProgressBarMessage = "",
            CCURRENT_PERIOD_YY = "",
            CCURRENT_PERIOD_MM = "",
            CSOFT_PERIOD_YY = "",
            CSOFT_PERIOD_MM = "",
            LcCrecID = "",
            lcSearch = "",
            lcPeriod = "",
            lcStatus = "",
            lcDeptCode = "",
            lcDeptName = "",
            lcRapidOrdCommit = "",
            lcLocalCurrency = "",
            lcBaseCurrency = "";

        public string COMPANYID;
        public string USERID;
        public string CommitLabel = "Commit";
        public string SubmitLabel = "Submit";
        public bool
            EnableDept = false,
            EnableButton = false,
            EnableDelete = false,
            EnableSubmit = false,
            EnableApprove = false,
            EnableCommit = false,
            EnablePrint = false,
            EnableCopy = false,
            EnableEdit = false,
            EnableNLBASE_RATE = false,
            EnableNLCURRENCY_RATE = false,
            EnableNBBASE_RATE = false,
            EnableNBCURRENCY_RATE = false,
            EnableRefNo = false,
            loCopy = false;
        


        public bool buttonRapidApprove = true;
        public bool buttonRapidCommit = true;
        public string CSTATUS_TEMP { get; set; }
        #endregion

        #region Public Property ViewModel
        public int JournalPeriodYear { get; set; }
        public string JournalPeriodMonth { get; set; }
        public CBT01200ParamDTO JournalParam { get; set; } = new CBT01200ParamDTO();
        public ObservableCollection<CBT01200DTO> JournalGrid { get; set; } = new ObservableCollection<CBT01200DTO>();
        
        public CBT01200DTO Journal = new();

        public CBT01200JournalHDParam JournalRecord = new();
        public ObservableCollection<CBT01201DTO> JournalDetailGrid { get; set; } = new ObservableCollection<CBT01201DTO>();
        public DateTime? RefDate { get; set; }
        public DateTime? DocDate { get; set; }
        #endregion
        
        #region Universal DTO
        
        public CBT01200GSCompanyInfoDTO CompanyCollection = new();
        
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
                VAR_GSM_COMPANY = await _CBT01200InitModel.GetGSCompanyInfoAsync();
                VAR_GL_SYSTEM_PARAM = await _CBT01200InitModel.GetGLSystemParamAsync();
                VAR_CB_SYSTEM_PARAM = await _CBT01200InitModel.GetCBSystemParamAsync();
                VAR_GSM_TRANSACTION_CODE = await _CBT01200InitModel.GetGSTransCodeInfoAsync();
                VAR_IUNDO_COMMIT_JRN = await _CBT01200InitModel.GetGSSystemEnableOptionInfoAsync();
                VAR_GSM_PERIOD = await _CBT01200InitModel.GetGSPeriodYearRangeAsync();
                VAR_GSB_CODE_LIST = await _CBT01200InitModel.GetGSBCodeListAsync();

                //Add all data 
                VAR_GSB_CODE_LIST.Add(new CBT01200GSGSBCodeDTO { CCODE = "", CNAME = "ALL" });

                //Get And Set List Dept Code
                VAR_DEPARTEMENT_LIST = await _PublicLookupModel.GSL00700GetDepartmentListAsync(new GSL00700ParameterDTO());

                //Set default Dept Code
                JournalParam.CDEPT_CODE = VAR_DEPARTEMENT_LIST.Any(loDeptList => loDeptList.CDEPT_CODE == VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_CODE) ? VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_CODE : "";
                JournalParam.CDEPT_NAME = VAR_DEPARTEMENT_LIST.Any(loDeptList => loDeptList.CDEPT_NAME == VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_NAME) ? VAR_GL_SYSTEM_PARAM.CCLOSE_DEPT_NAME : "";

                //Set default Journal Period
                JournalPeriodYear = int.Parse(VAR_CB_SYSTEM_PARAM.CSOFT_PERIOD_YY);
                JournalPeriodMonth = VAR_CB_SYSTEM_PARAM.CSOFT_PERIOD_MM;
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
                var loResult = await _CBT01200Model.GetJournalListAsync(JournalParam);

                JournalGrid = new ObservableCollection<CBT01200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task GetJournalRecord(CBT01200JournalHDParam poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _CBT01200Model.R_ServiceGetRecordAsync(poParam);
                JournalRecord = loResult;


                RefDate = DateTime.ParseExact(JournalRecord.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                DocDate= DateTime.ParseExact(JournalRecord.CDOC_DATE, "yyyyMMdd", CultureInfo.InvariantCulture);
                JournalRecord.CUPDATE_DATE = JournalRecord.DUPDATE_DATE.Value.ToLongDateString();
                JournalRecord.CCREATE_DATE = JournalRecord.DCREATE_DATE.Value.ToLongDateString();
                JournalRecord.CLOCAL_CURRENCY_CODE = CompanyCollection.CLOCAL_CURRENCY_CODE;
                JournalRecord.CBASE_CURRENCY_CODE = CompanyCollection.CBASE_CURRENCY_CODE;
                LcCrecID = JournalRecord.CREC_ID;
                Data.CSTATUS = JournalRecord.CSTATUS;

                //var loParam = new CBT01201DTO()
                //{
                //    CREC_ID = JournalRecord.CREC_ID
                //};
                //await _CBT01210ViewModel.GetJournalDetailList();

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            }

        public async Task SaveJournal(CBT01200JournalHDParam poEntity, eCRUDMode poCRUDMode)
        {
            var loEx = new R_Exception();

            try
            {
                if (poCRUDMode == eCRUDMode.AddMode)
                {
                    poEntity.CACTION = "NEW";
                    poEntity.CREC_ID = "";
                    poEntity.CREF_NO = VAR_GSM_TRANSACTION_CODE.LINCREMENT_FLAG ? "" : poEntity.CREF_NO;
                }
                else if (poCRUDMode == eCRUDMode.EditMode)
                {
                    poEntity.CACTION = "EDIT";
                }
                poEntity.CDOC_DATE = DocDate.Value.ToString("yyyyMMdd");
                poEntity.CREF_DATE = RefDate.Value.ToString("yyyyMMdd");
                poEntity.CTRANS_CODE = ContextConstant.VAR_TRANS_CODE;
                poEntity.CPAYMENT_TYPE = ContextConstant.CPAYMENT_TYPE;

                var loResult = await _CBT01200Model.R_ServiceSaveAsync(poEntity, poCRUDMode);

                JournalRecord = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task DeleteJournal(CBT01200JournalHDParam poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = R_FrontUtility.ConvertObjectToObject<CBT01200JournalHDParam>(poEntity);
                poEntity.LUNDO_COMMIT = false;
                loData.LAUTO_COMMIT = false;
                loData.CNEW_STATUS = "99";

                await _CBT01200Model.R_ServiceDeleteAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task UpdateJournalStatus(CBT01200UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                await _CBT01200Model.UpdateJournalStatusAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        
        public async Task<CBT01210LastCurrencyRateDTO> GetLastCurrency(CBT01210LastCurrencyRateDTO poEntity)
        {
            var loEx = new R_Exception();
            CBT01210LastCurrencyRateDTO loRtn = null;
            try
            {
                poEntity.CRATE_DATE = RefDate.Value.ToString("yyyyMMdd");
                poEntity.CRATETYPE_CODE = VAR_CB_SYSTEM_PARAM.CRATETYPE_CODE;
                var loResult = await _CBT01200Model.GetLastCurrencyAsync(poEntity);

                loRtn = loResult;
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

