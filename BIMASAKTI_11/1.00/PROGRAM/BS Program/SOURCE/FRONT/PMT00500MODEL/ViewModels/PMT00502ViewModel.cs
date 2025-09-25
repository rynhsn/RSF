using PMT00500COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT00500MODEL
{
    public class PMT00502ViewModel : R_ViewModel<PMT00500DTO>
    {
        private PMT00500Model _PMT00500Model = new PMT00500Model();
        private PMT00510Model _PMT00510Model = new PMT00510Model();

        #region Property Class
        public ObservableCollection<PMT00500DTO> LOIGrid { get; set; } = new ObservableCollection<PMT00500DTO>();
        public ObservableCollection<PMT00510DTO> LOIUNITGrid { get; set; } = new ObservableCollection<PMT00510DTO>();
        #endregion

        #region Property ViewModel
        public PMT00500PropertyDTO PROPERTY_DATA { get; set; } = new PMT00500PropertyDTO();
        public string CPAR_TRANS_STS { get; set; } = "00,10,20";
        public bool LHAS_UNIT { get; set; }
        #endregion

        #region List Property
        public List<KeyValuePair<string, string>> TransStatusList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("00,10,20", R_FrontUtility.R_GetMessage(typeof(PMT00500FrontResources.Resources_Dummy_Class), "_DraftOpenInProgress")),
            new KeyValuePair<string, string>("30", R_FrontUtility.R_GetMessage(typeof(PMT00500FrontResources.Resources_Dummy_Class), "_Approved")),
            new KeyValuePair<string, string>("80", R_FrontUtility.R_GetMessage(typeof(PMT00500FrontResources.Resources_Dummy_Class), "_Closed")),
            new KeyValuePair<string, string>("90,98", R_FrontUtility.R_GetMessage(typeof(PMT00500FrontResources.Resources_Dummy_Class), "_CancelledRejected")),
        };
        #endregion

        public async Task GetLOIList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00500Model.GetLOIListStreamAsync(PROPERTY_DATA.CPROPERTY_ID, CPAR_TRANS_STS);
                if (loResult.Count > 0)
                {
                    loResult.ForEach(x =>
                    {
                        if (DateTime.TryParseExact(x.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldRefDate))
                        {
                            x.DREF_DATE = ldRefDate;
                        }
                        else
                        {
                            x.DREF_DATE = null;
                        }
                        if (DateTime.TryParseExact(x.CHO_ACTUAL_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldHOActualDate))
                        {
                            x.DHO_ACTUAL_DATE = ldHOActualDate;
                        }
                        else
                        {
                            x.DHO_ACTUAL_DATE = null;
                        }
                        if (DateTime.TryParseExact(x.COPEN_DATE, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var ldOpenDate))
                        {
                            x.DOPEN_DATE = ldOpenDate;
                        }
                        else
                        {
                            x.DOPEN_DATE = null;
                        }
                    });
                }

                LOIGrid = new ObservableCollection<PMT00500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIUnitList(PMT00510DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT00510Model.GetLOIUnitListStreamAsync(poEntity);
                LHAS_UNIT = loResult.Count > 0;

                LOIUNITGrid = new ObservableCollection<PMT00510DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMT00500DTO> SubmitRedraftLOI(PMT00500SubmitRedraftDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT00500DTO loRtn = null;

            try
            {
               loRtn = await _PMT00500Model.SubmitRedraftAgreementTransAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public async Task<PMT00500UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            PMT00500UploadFileDTO loResult = null;

            try
            {
                loResult = await _PMT00500Model.DownloadTemplateFileAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
