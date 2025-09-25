using PMT00500BACK;
using PMT00500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;

namespace PMT00500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT00500Controller : ControllerBase, IPMT00500
    {
        private LoggerPMT00500 _Logger;
        private readonly ActivitySource _activitySource;
        public PMT00500Controller(ILogger<LoggerPMT00500> logger)
        {
            //Initial and Get Logger
            LoggerPMT00500.R_InitializeLogger(logger);
            _Logger = LoggerPMT00500.R_GetInstanceLogger();
            _activitySource = PMT00500ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT00500Controller));
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion

        [HttpPost]
        public IAsyncEnumerable<PMT00500DTO> GetLOIListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT00500DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIListStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIListStream");
                string lcPropertyID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                string lcTransStatusList = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPAR_TRANS_STS);

                _Logger.LogInfo("Call Back Method GetAllLOI");
                var loCls = new PMT00500Cls();
                var loTempRtn = loCls.GetAllLOI(lcPropertyID, lcTransStatusList);

                _Logger.LogInfo("Call Stream Method Data GetLOIListStream");
                loRtn = GetStreamData<PMT00500DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLOIListStream");

            return loRtn;
        }

        [HttpPost]
        public PMT00500SingleResult<PMT00500DTO> GetLOI(PMT00500DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetLOI");
            var loEx = new R_Exception();
            PMT00500SingleResult<PMT00500DTO> loRtn = new PMT00500SingleResult<PMT00500DTO>();
            _Logger.LogInfo("Start GetLOI");

            try
            {
                var loCls = new PMT00500Cls();

                _Logger.LogInfo("Call Back Method GetLOIDisplay");
                loRtn.Data = loCls.GetLOIDisplay(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLOI");

            return loRtn;
        }

        [HttpPost]
        public PMT00500SingleResult<PMT00500DTO> SaveLOI(PMT00500SaveDTO<PMT00500DTO> poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveLOI");
            var loEx = new R_Exception();
            PMT00500SingleResult<PMT00500DTO> loRtn = new PMT00500SingleResult<PMT00500DTO>();
            _Logger.LogInfo("Start SaveLOI");

            try
            {
                var loCls = new PMT00500Cls();

                _Logger.LogInfo("Call Back Method SaveLOIDisplay");
                loRtn.Data = loCls.SaveLOI(poEntity.Data, poEntity.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveLOI");

            return loRtn;
        }

        [HttpPost]
        public PMT00500SingleResult<PMT00500DTO> SubmitRedraftAgreementTrans(PMT00500SubmitRedraftDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SubmitRedraftAgreementTrans");
            var loEx = new R_Exception();
            PMT00500SingleResult<PMT00500DTO> loRtn = new PMT00500SingleResult<PMT00500DTO>();
            _Logger.LogInfo("Start SubmitRedraftAgreementTrans");

            try
            {
                var loCls = new PMT00500Cls();

                _Logger.LogInfo("Call Back Method SubmitRedraftAgreementTransDisplay");
                loCls.UpdateAgreementTransStatus(poEntity);

                _Logger.LogInfo("Call Back Method GetLOIDisplay");
                var loParam = R_Utility.R_ConvertObjectToObject<PMT00500SubmitRedraftDTO, PMT00500DTO>(poEntity);
                loRtn.Data = loCls.GetLOIDisplay(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SubmitRedraftAgreementTrans");

            return loRtn;
        }

        [HttpPost]
        public PMT00500UploadFileDTO DownloadTemplateFile()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateFile");
            var loEx = new R_Exception();
            var loRtn = new PMT00500UploadFileDTO();
            _Logger.LogInfo("Start DownloadTemplateFile");

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_PM_API");

                _Logger.LogInfo("Load File Template From DownloadTemplateFile");
                var lcResourceFile = "BIMASAKTI_PM_API.Template.LeaseManager.xlsx";
                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.FileBytes = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End DownloadTemplateFile");

            return loRtn;
        }

        [HttpPost]
        public PMT00500SingleResult<PMT00500LeaseDTO> LeaseProcess(PMT00500LeaseDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("LeaseProcess");
            var loEx = new R_Exception();
            PMT00500SingleResult<PMT00500LeaseDTO> loRtn = new PMT00500SingleResult<PMT00500LeaseDTO>();
            _Logger.LogInfo("Start LeaseProcess");

            try
            {
                var loCls = new PMT00500Cls();

                _Logger.LogInfo("Call Back Method LeaseProcessDisplay");
                loCls.LeaseProcessSP(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End LeaseProcess");

            return loRtn;
        }
    }
}