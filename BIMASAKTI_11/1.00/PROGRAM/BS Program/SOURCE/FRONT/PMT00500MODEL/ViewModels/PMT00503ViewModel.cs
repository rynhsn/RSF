using PMT00500COMMON;
using R_APICommonDTO;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_ProcessAndUploadFront;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PMT00500MODEL
{
    public class PMT00503ViewModel : R_IProcessProgressStatus
    {
        private PMT00500Model _PMT00500Model = new PMT00500Model();
        private PMT00500InitModel _PMT00500InitModel = new PMT00500InitModel();

        #region Proses Batch
        // Action StateHasChanged
        public Action StateChangeAction { get; set; }

        // Action Get Error Unhandle
        public Action<R_APIException> ShowErrorAction { get; set; }

        // Func Proses is Success
        public Func<Task> ActionIsCompleteSuccess { get; set; }
        #endregion

        #region Public Property
        public PMT00500DTO LOI { get; set; } = new PMT00500DTO();
        public PMT00500TransCodeInfoGSDTO VAR_GSM_TRANS_CODE_LOI { get; set; } = new PMT00500TransCodeInfoGSDTO();
        public List<PMT00500UniversalDTO> VAR_LEASE_MODE { get; set; } = new List<PMT00500UniversalDTO>();
        #endregion

        #region Property ViewModel
        public string Message { get; set; } = "";
        public int Percentage { get; set; } = 0;
        public bool VisibleError { get; set; } = false;
        public string CompanyID { get; set; }
        public string UserId { get; set; }
        #endregion

        public async Task GetInitialVar()
        {
            var loEx = new R_Exception();
            try
            {
                var loResult = await _PMT00500InitModel.GetTransCodeInfoAsync("LOI");

                VAR_GSM_TRANS_CODE_LOI = loResult;
                
                var loUniversalListResult = await _PMT00500InitModel.GetAllUniversalListAsync("_BS_LEASE_MODE");
                VAR_LEASE_MODE = loUniversalListResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }       
        
        public async Task ProcessLeaseBatch(List<PMT00510DTO> poUnitList, List<PMT00520DTO> poUtilityList, List<PMT00530DTO> poChargesList)
        {
            var loEx = new R_Exception();
            R_BatchParameter loBatchPar;
            R_ProcessAndUploadClient loCls;
            PMT00500LeaseDTO Bigobject;
            List<R_KeyValue> loUserParameneters;

            try
            {
                Bigobject = R_FrontUtility.ConvertObjectToObject<PMT00500LeaseDTO>(LOI);
                Bigobject.UnitList = R_FrontUtility.ConvertCollectionToCollection<PMT00510MappingLeaseDTO>(poUnitList).ToList();
                Bigobject.UtilityList = R_FrontUtility.ConvertCollectionToCollection<PMT00520MappingLeaseDTO>(poUtilityList).ToList();
                Bigobject.ChargesList = R_FrontUtility.ConvertCollectionToCollection<PMT00530MappingLeaseDTO>(poChargesList).ToList();


                //Instantiate ProcessClient
                loCls = new R_ProcessAndUploadClient(
                    pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM",
                    poProcessProgressStatus: this);

                //preapare Batch Parameter
                loBatchPar = new R_BatchParameter();

                loBatchPar.COMPANY_ID = CompanyID;
                loBatchPar.USER_ID = UserId;
                loBatchPar.ClassName = "PMT00500BACK.PMT00502Cls";
                loBatchPar.BigObject = Bigobject;

                var lcGuid = await loCls.R_BatchProcess<PMT00500LeaseDTO>(loBatchPar, 100);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
        EndBlock:

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessLease(List<PMT00510DTO> poUnitList, List<PMT00520DTO> poUtilityList, List<PMT00530DTO> poChargesList)
        {
            var loEx = new R_Exception();
            PMT00500LeaseDTO Bigobject;
            List<R_KeyValue> loUserParameneters;

            try
            {
                Bigobject = R_FrontUtility.ConvertObjectToObject<PMT00500LeaseDTO>(LOI);
                Bigobject.UnitList = R_FrontUtility.ConvertCollectionToCollection<PMT00510MappingLeaseDTO>(poUnitList).ToList();
                Bigobject.UtilityList = R_FrontUtility.ConvertCollectionToCollection<PMT00520MappingLeaseDTO>(poUtilityList).ToList();
                Bigobject.ChargesList = R_FrontUtility.ConvertCollectionToCollection<PMT00530MappingLeaseDTO>(poChargesList).ToList();

                await _PMT00500Model.LeaseProcessAsync(Bigobject);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        #region Status
        public async Task ProcessComplete(string pcKeyGuid, eProcessResultMode poProcessResultMode)
        {
            var loEx = new R_APIException();

            try
            {
                if (poProcessResultMode == eProcessResultMode.Success)
                {
                    Message = string.Format("Process Complete and success with GUID {0}", pcKeyGuid);
                    VisibleError = false;
                    await ActionIsCompleteSuccess();
                }

                if (poProcessResultMode == eProcessResultMode.Fail)
                {
                    Message = string.Format("Process Complete but fail with GUID {0}", pcKeyGuid);
                    await ServiceGetError(pcKeyGuid);
                    VisibleError = true;
                }
            }
            catch (Exception ex)
            {
                loEx.add(ex);
            }

            // Call Method Action StateHasChange
            StateChangeAction();

            loEx.ThrowExceptionIfErrors();
        }

        public async Task ProcessError(string pcKeyGuid, R_APIException ex)
        {
            Message = string.Format("Process Error with GUID {0}", pcKeyGuid);

            // Call Method Action Error Unhandle
            ShowErrorAction(ex);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        public async Task ReportProgress(int pnProgress, string pcStatus)
        {
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            Percentage = pnProgress;
            Message = string.Format("Process Progress {0} with status {1}", pnProgress, pcStatus);

            // Call Method Action StateHasChange
            StateChangeAction();

            await Task.CompletedTask;
        }

        private async Task ServiceGetError(string pcKeyGuid)
        {
            R_APIException loException = new R_APIException();

            List<R_ErrorStatusReturn> loResultData;
            R_GetErrorWithMultiLanguageParameter loParameterData;
            R_ProcessAndUploadClient loCls;

            try
            {
                // Add Parameter
                loParameterData = new R_GetErrorWithMultiLanguageParameter()
                {
                    COMPANY_ID = CompanyID,
                    USER_ID = UserId,
                    KEY_GUID = pcKeyGuid,
                    RESOURCE_NAME = "RSP_PM_MANTAIN_LEASE_OWNERSHIPResources"
                };

                loCls = new R_ProcessAndUploadClient(pcModuleName: "PM",
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: "R_DefaultServiceUrlPM");

                // Get error result
                loResultData = await loCls.R_GetStreamErrorProcess(loParameterData);

                // check error if unhandle
                if (loResultData.Count > 0)
                {
                    var loUnhandleEx = loResultData.Select(x => new R_BlazorFrontEnd.Exceptions.R_Error(x.SeqNo.ToString(), x.ErrorMessage)).ToList();
                    var loEx = new R_Exception();
                    loUnhandleEx.ForEach(x => loEx.Add(x));

                    loException = R_FrontUtility.R_ConvertToAPIException(loEx);
                }
            }
            catch (Exception ex)
            {
                loException.add(ex);
            }

            loException.ThrowExceptionIfErrors();
        }
        #endregion
    }
}
