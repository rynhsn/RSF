using PMT01300BACK;
using PMT01300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;

namespace PMT01300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMT01300Controller : ControllerBase, IPMT01300
    {
        private LoggerPMT01300 _Logger;
        private readonly ActivitySource _activitySource;

        public PMT01300Controller(ILogger<LoggerPMT01300> logger)
        {
            //Initial and Get Logger
            LoggerPMT01300.R_InitializeLogger(logger);
            _Logger = LoggerPMT01300.R_GetInstanceLogger();
            _activitySource = PMT01300ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(PMT01300Controller));
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
        public IAsyncEnumerable<PMT01300DTO> GetLOIListStream()
        {
            using Activity activity = _activitySource.StartActivity("GetLOIListStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<PMT01300DTO> loRtn = null;
            _Logger.LogInfo("Start GetLOIListStream");

            try
            {
                _Logger.LogInfo("Set Param GetLOIListStream");
                string lcPropertyID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                string lcTransStatusList = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPAR_TRANS_STS);

                _Logger.LogInfo("Call Back Method GetAllLOI");
                var loCls = new PMT01300Cls();
                var loTempRtn = loCls.GetAllLOI(lcPropertyID, lcTransStatusList);

                _Logger.LogInfo("Call Stream Method Data GetLOIListStream");
                loRtn = GetStreamData<PMT01300DTO>(loTempRtn);
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
        public PMT01300SingleResult<PMT01300DTO> GetLOI(PMT01300DTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("GetLOI");
            var loEx = new R_Exception();
            PMT01300SingleResult<PMT01300DTO> loRtn = new PMT01300SingleResult<PMT01300DTO>();
            _Logger.LogInfo("Start GetLOI");

            try
            {
                var loCls = new PMT01300Cls();

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
        public PMT01300SingleResult<PMT01300DTO> SaveLOI(PMT01300SaveDTO<PMT01300DTO> poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveLOI");
            var loEx = new R_Exception();
            PMT01300SingleResult<PMT01300DTO> loRtn = new PMT01300SingleResult<PMT01300DTO>();
            _Logger.LogInfo("Start SaveLOI");

            try
            {
                var loCls = new PMT01300Cls();

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
        public PMT01300SingleResult<PMT01300DTO> SubmitRedraftAgreementTrans(PMT01300SubmitRedraftDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SubmitRedraftAgreementTrans");
            var loEx = new R_Exception();
            PMT01300SingleResult<PMT01300DTO> loRtn = new PMT01300SingleResult<PMT01300DTO>();
            _Logger.LogInfo("Start SubmitRedraftAgreementTrans");

            try
            {
                var loCls = new PMT01300Cls();

                _Logger.LogInfo("Call Back Method SubmitRedraftAgreementTransDisplay");
                loCls.UpdateAgreementTransStatus(poEntity);

                _Logger.LogInfo("Call Back Method GetLOIDisplay");
                var loParam = R_Utility.R_ConvertObjectToObject<PMT01300SubmitRedraftDTO, PMT01300DTO>(poEntity);
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
        public PMT01300UploadFileDTO DownloadTemplateFile()
        {
            using Activity activity = _activitySource.StartActivity("DownloadTemplateFile");
            var loEx = new R_Exception();
            var loRtn = new PMT01300UploadFileDTO();
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
        public PMT01300ListResult<PMT01300ReportTemplateDTO> GetReportTemplateList(
            PMT01300ReportTemplateParamDTO poParam)
        {
            string? lcMethod = nameof(GetReportTemplateList);
            _Logger.LogInfo(string.Format("Start Method {0}", lcMethod));
            R_Exception loException = new R_Exception();
            List<PMT01300ReportTemplateDTO> loRtnTmp;
            var loReturn = new PMT01300ListResult<PMT01300ReportTemplateDTO>();

            try
            {
                _Logger.LogInfo(string.Format("initialization loDbPar in Method {0}", lcMethod));
                var loDbParameter = new PMT01300ReportTemplateParamDTO();

                _Logger.LogInfo(string.Format("Assign Data to loDbPar in Method {0}", lcMethod));
                loDbParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loDbParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loDbParameter.CPROPERTY_ID = poParam.CPROPERTY_ID;
                loDbParameter.CPROGRAM_ID = "PMT01300";
                loDbParameter.CTEMPLATE_ID = "";
                
                _Logger.LogDebug("{@ObjectParameter}", loDbParameter);

                //Use Context!

                _Logger.LogInfo(string.Format("initialization loCls in Method {0}", lcMethod));
                var loCls = new PMT01300Cls();
                _Logger.LogDebug("{@ObjectCls}", loCls);

                _Logger.LogInfo(string.Format("Get Data From Back/Cls in Method {0}", lcMethod));
                loRtnTmp = loCls.GetReportTemplate(poParameter: loDbParameter);
                _Logger.LogDebug("{@ObjectReturnTemporary}", loRtnTmp);

                loReturn.Data = loRtnTmp;
                // _Logger.LogDebug("{@ObjectReturn}", loReturn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
            }

            if (loException.Haserror)
                _Logger.LogError("{@ErrorObject}", loException.Message);

            loException.ThrowExceptionIfErrors();

            _Logger.LogInfo(string.Format("End Method {0}", lcMethod));

            return loReturn!;
        }
    }
}