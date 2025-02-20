using PMT01300COMMON;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT01300MODEL
{
    public class PMT01300ViewModel : R_ViewModel<PMT01300DTO>
    {
        private PMT01300Model _PMT01300Model = new PMT01300Model();
        private PMT01310Model _PMT01310Model = new PMT01310Model();
        private PMT01300InitModel _PMT01300InitModel = new PMT01300InitModel();

        #region Property Class
        public ObservableCollection<PMT01300DTO> LOIGrid { get; set; } = new ObservableCollection<PMT01300DTO>();
        public ObservableCollection<PMT01310DTO> LOIUNITGrid { get; set; } = new ObservableCollection<PMT01310DTO>();
        public List<PMT01300PropertyDTO> PropertyList { get; set; } = new List<PMT01300PropertyDTO>();
        #endregion

        #region Property ViewModel
        public string PROPERTY_ID { get; set; } = "";
        public string CPAR_TRANS_STS { get; set; } = "30";
        public bool LHAS_UNIT { get; set; }
        #endregion

        #region List Property
        public List<KeyValuePair<string, string>> TransStatusList { get; } = new List<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("00,10,20", R_FrontUtility.R_GetMessage(typeof(PMT01300FrontResources.Resources_Dummy_Class), "_DraftOpenInProgress")),
            new KeyValuePair<string, string>("30", R_FrontUtility.R_GetMessage(typeof(PMT01300FrontResources.Resources_Dummy_Class), "_Approved")),
            new KeyValuePair<string, string>("80", R_FrontUtility.R_GetMessage(typeof(PMT01300FrontResources.Resources_Dummy_Class), "_Closed")),
            new KeyValuePair<string, string>("90,98", R_FrontUtility.R_GetMessage(typeof(PMT01300FrontResources.Resources_Dummy_Class), "_CancelledRejected")),
        };
        #endregion
        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _PMT01300InitModel.GetAllPropertyListAsync();

                PropertyList = loResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task GetLOIList()
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01300Model.GetLOIListStreamAsync(PROPERTY_ID, CPAR_TRANS_STS);
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

                LOIGrid = new ObservableCollection<PMT01300DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task GetLOIUnitList(PMT01310DTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                var loResult = await _PMT01310Model.GetLOIUnitListStreamAsync(poEntity);
                LHAS_UNIT = loResult.Count > 0;

                LOIUNITGrid = new ObservableCollection<PMT01310DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<PMT01300DTO> SubmitRedraftLOI(PMT01300SubmitRedraftDTO poEntity)
        {
            var loEx = new R_Exception();
            PMT01300DTO loRtn = null;

            try
            {
               loRtn = await _PMT01300Model.SubmitRedraftAgreementTransAsync(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }
        public async Task<PMT01300UploadFileDTO> DownloadTemplate()
        {
            var loEx = new R_Exception();
            PMT01300UploadFileDTO loResult = null;

            try
            {
                loResult = await _PMT01300Model.DownloadTemplateFileAsync();
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
