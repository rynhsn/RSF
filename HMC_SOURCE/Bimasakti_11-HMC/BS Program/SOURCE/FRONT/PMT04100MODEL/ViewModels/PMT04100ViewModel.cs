using PMT04100COMMON;
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

namespace PMT04100MODEL
{
    public class PMT04100ViewModel : R_ViewModel<PMT04100DTO>
    {
        #region Model
        private PMT04100InitialProcessModel _PMT04100InitialProcessModel = new PMT04100InitialProcessModel();
        private PublicLookupModel _PublicLookupModel = new PublicLookupModel();
        private PMT04100Model _PMT04100Model = new PMT04100Model();
        #endregion

        #region Initial Data
        public PMT04100GLSystemParamDTO VAR_GL_SYSTEM_PARAM { get; set; } = new PMT04100GLSystemParamDTO();
        public PMT04100PMSystemParamDTO VAR_PM_SYSTEM_PARAM { get; set; } = new PMT04100PMSystemParamDTO();
        public List<PMT04100GSGSBCodeDTO> VAR_GSB_CODE_LIST { get; set; } = new List<PMT04100GSGSBCodeDTO>();
        public List<PMT04100PropertyDTO> VAR_GS_PROPERTY_LIST { get; set; } = new List<PMT04100PropertyDTO>();
        public List<GSL00700DTO> VAR_DEPT_CODE_LIST { get; set; } = new List<GSL00700DTO>();
        #endregion

        #region Public Property ViewModel
        public string SearchText { get; set; }
        public int JournalPeriodYear { get; set; }
        public string JournalPeriodMonth { get; set; }
        public PMT04100ParamDTO JornalParam { get; set; } = new PMT04100ParamDTO();
        public ObservableCollection<PMT04100DTO> JournalGrid { get; set; } = new ObservableCollection<PMT04100DTO>();
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
                var loResult = await _PMT04100InitialProcessModel.GetTabJournalListInitialProcessAsync();

                //Set Universal Data
                VAR_GL_SYSTEM_PARAM = loResult.VAR_GL_SYSTEM_PARAM;
                VAR_GS_PROPERTY_LIST = loResult.VAR_GS_PROPERTY_LIST;
                VAR_GSB_CODE_LIST = loResult.VAR_GSB_CODE_LIST;
                VAR_GSB_CODE_LIST.Add(new PMT04100GSGSBCodeDTO { CCODE="", CNAME="ALL" });

                //Get And Set List Dept Code
                VAR_DEPT_CODE_LIST = await _PublicLookupModel.GSL00700GetDepartmentListAsync(new GSL00700ParameterDTO());
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetPMSystemParam(string pcPropertyId)
        {
            var loEx = new R_Exception();

            try
            {
                // Get Universal Data
                var loParam = new PMTInitialParamDTO() { CPROPERTY_ID = pcPropertyId };
                var loResult = await _PMT04100InitialProcessModel.GetPMSystemParamAsync(loParam);

                //Set Universal Data
                VAR_PM_SYSTEM_PARAM = loResult;
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
                var loResult = await _PMT04100Model.GetJournalListAsync(JornalParam);
                loResult.ForEach(x =>
                {
                    if (DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                    {
                        x.DREF_DATE = ldRefDate;
                    }
                });

                JournalGrid = new ObservableCollection<PMT04100DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

    }
}
