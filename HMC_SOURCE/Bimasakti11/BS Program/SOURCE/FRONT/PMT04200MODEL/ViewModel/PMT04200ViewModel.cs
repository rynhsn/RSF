using PMT04200Common;
using PMT04200Common.DTOs;
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

namespace PMT04200MODEL
{
    public class PMT04200ViewModel : R_ViewModel<PMT04200DTO>
    {
        #region Model
        private PMT04200InitModel _PMT04200InitModel = new PMT04200InitModel();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();
        private PMT04200Model _PMT04200Model = new PMT04200Model();
        #endregion
        #region Initial Data
        public PMT04200GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; } = new PMT04200GSCompanyInfoDTO();
        public PMT04200GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new PMT04200GLSystemParamDTO();
        public PMT04200CBSystemParamDTO VAR_CB_SYSTEM_PARAM { get; set; } = new PMT04200CBSystemParamDTO();
        public PMT04200PMSystemParamDTO VAR_PM_SYSTEM_PARAM { get; set; } = new PMT04200PMSystemParamDTO();
        public PMT04200GSTransInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; } = new PMT04200GSTransInfoDTO();
        public PMT04200GSPeriodYearRangeDTO VAR_GSM_PERIOD { get; set; } = new PMT04200GSPeriodYearRangeDTO();
        public List<PMT04200GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; } = new List<PMT04200GSGSBCodeDTO>();
        public List<GSL00700DTO> VAR_DEPARTEMENT_LIST { get; set; } = new List<GSL00700DTO>();
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
        #region Public Property ViewModel
        public string SearchText { get; set; }
        public PMT04200ParamDTO JornalParam { get; set; } = new PMT04200ParamDTO();
        public ObservableCollection<PMT04200DTO> JournalGrid { get; set; } = new ObservableCollection<PMT04200DTO>();
        public PMT04200DTO TransactionRecord { get; set; } = new();
        public int ParamPeriodYear { get; set; }
        public string ParamPeriodMonth { get; set; }
        #endregion
        #region Property
        public string PropertyDefault = "";
        public List<PropertyListDTO> VAR_PROPERTY_LIST { get; set; } = new List<PropertyListDTO>();
        public async Task GetPropertyList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT04200InitModel.GetProperyListAsync();
                VAR_PROPERTY_LIST = loResult.Data;
                PropertyDefault = VAR_PROPERTY_LIST.FirstOrDefault().CPROPERTY_ID.ToString();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        

        #endregion
        
        public async Task GetAllUniversalData()
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                VAR_GSM_COMPANY = await _PMT04200InitModel.GetGSCompanyInfoAsync();
                VAR_GL_SYSTEM_PARAM = await _PMT04200InitModel.GetGLSystemParamAsync();
                VAR_CB_SYSTEM_PARAM = await _PMT04200InitModel.GetCBSystemParamAsync();
                VAR_PM_SYSTEM_PARAM = await _PMT04200InitModel.GetPMSystemParamAsync();
                VAR_GSM_TRANSACTION_CODE = await _PMT04200InitModel.GetGSTransCodeInfoAsync();
                VAR_GSM_PERIOD = await _PMT04200InitModel.GetGSPeriodYearRangeAsync();
                VAR_GSB_CODE_LIST = await _PMT04200InitModel.GetGSBCodeListAsync();
                
                //Add all data 
                VAR_GSB_CODE_LIST.Add(new PMT04200GSGSBCodeDTO { CCODE = "", CNAME = "ALL" });

                //Get And Set List Dept Code
                VAR_DEPARTEMENT_LIST = await _PublicLookupModel.GSL00700GetDepartmentListAsync(new GSL00700ParameterDTO());
                
                ParamPeriodYear = int.Parse(VAR_PM_SYSTEM_PARAM.CSOFT_PERIOD_YY);
                ParamPeriodMonth = VAR_PM_SYSTEM_PARAM.CSOFT_PERIOD_MM;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region GetTransaction

        
        public async Task GetJournalList()
        {
            var loEx = new R_Exception();

            try
            {
                JornalParam.CPERIOD = ParamPeriodYear + ParamPeriodMonth;
                JornalParam.CPROPERTY_ID = PropertyDefault;
                var loResult = await _PMT04200Model.GetJournalListAsync(JornalParam);
                loResult.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                    {
                        x.DREF_DATE = ldRefDate;
                    }
                });
                JournalGrid = new ObservableCollection<PMT04200DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #endregion

    
    }
}

